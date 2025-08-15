using Dd.Domain.Reservation.Entities;
using Dd.Domain.Reservation.Enums;
using Dd.Domain.Reservation.Overlap;
using Dd.Domain.Reservation.Utils;
using Dd.Domain.Reservation.ValueObjects;

namespace Dd.Domain.Reservation.Services;

public static class SlotService {
    public static List<TimeSlot> Generate(
        Guid physicianId, DateOnly date, List<WorkSchedule> workSchedules, List<BlockedSchedule> blockedSchedules) {
        
        List<Period> workingPeriods = [];
        List<Period> blockedPeriods = [];

        foreach (var workSchedule in workSchedules) {
            var period = PeriodAt(workSchedule, date);
            if (period != null) workingPeriods.Add(period);
        }

        foreach (var blockedSchedule in blockedSchedules) {
            var period = PeriodAt(blockedSchedule, date);
            if (period != null) blockedPeriods.Add(period);
        }

        var availablePeriods = Generate(workingPeriods, blockedPeriods);
        var slots = availablePeriods
            .Select(p => ToTimeSlot(physicianId, date, p)).ToList();

        return slots;
    }

    private static List<Period> Generate(List<Period> working, List<Period> blocking) {
        for (var i = 0; i < working.Count; i++) {
            foreach (var period in blocking) {
                var (org, biProd) = Block(working[i], period);
                working[i] = org;
                if(biProd != null) working.Insert(i+1, biProd);
            }
        }

        return FilterAndSort(working);
    }

    private static List<Period> FilterAndSort(List<Period> periods) {
        var filtered = periods.Where(p => p.Span > TimeSpan.Zero).ToList();
        filtered.Sort((a, b) => a.Start.CompareTo(b.Start));
        return filtered;
    }

    private static (Period orgi, Period? biprod) Block(Period working, Period blocking) {
        var org = new Period(working.Start, Min(blocking.Start, working.End));
        var biProd = new Period(Max(blocking.End, working.Start), working.End);

        if (biProd == working) (org, biProd) = (biProd, org);
        if (biProd.Span <= TimeSpan.Zero) biProd = null;
        return (org, biProd);
    }

    private static TimeSlot ToTimeSlot(Guid physicianId, DateOnly date, Period period) {
        return new TimeSlot(physicianId, date, period.Start, period.Span);
    }

    private static Period? PeriodAt(Schedule schedule, DateOnly date) {
        var (fHalf, _) = schedule.Split();
        var sequences = ToSequenceList(fHalf);
        foreach (var sequence in sequences) {
            if (sequence.IsMember(date.DayNumber))
                return new Period(fHalf.StartTime, fHalf.EndTime);
        }

        return null;
    }

    private static List<ISequence> ToSequenceList(Schedule schedule) {
        switch (schedule.RecurrenceType) {
            case RecurrenceType.Daily:
                return [
                    SequenceFactory.Create(schedule.StartDate.DayNumber, schedule.EndDate?.DayNumber,
                        schedule.RecurrenceInterval)
                ];
            
            case RecurrenceType.Weekly:
                List<ISequence> sequences = [];
                var start = schedule.StartDate.ToFirstDayOfWeek();
                foreach (var day in schedule.RecurrenceDays) {
                    while (start.DayOfWeek != day) start = start.AddDays(1);
                    var sequence = SequenceFactory.Create(start.DayNumber, schedule.EndDate?.DayNumber, schedule.RecurrenceInterval);
                    if (sequence.Start < schedule.StartDate.DayNumber) sequence = sequence.StartFromNext();
                    if (sequence != null)
                        sequences.Add(sequence);
                }
                return sequences;
            
            default:
                throw new NotImplementedException();
        }
    }
    
    private static TimeOnly Min(TimeOnly a, TimeOnly b) => a < b ? a : b;
    private static TimeOnly Max(TimeOnly a, TimeOnly b) => a > b ? a : b;
}