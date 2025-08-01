namespace Dd.Domain.Reservation.Enums;

public enum AppointmentStatus
{
    /// <summary>
    /// indicates an appointment that is scheduled but not yet checked in
    /// </summary>
    Scheduled,
    
    /// <summary>
    /// indicates the patient has availed himself for the appointment
    /// </summary>
    CheckedIn,
    
    /// <summary>
    /// indicates an appointment that is postponed before starting examination
    /// </summary>
    Postponed,
    
    /// <summary>
    /// indicates an examination is in progress
    /// </summary>
    InProgress,
    
    /// <summary>
    /// indicates an appointment has ended with a new appointment created after examination
    /// </summary>
    Reappointed,
    
    /// <summary>
    /// indicates an appointment that has been handled completely and no further action is needed
    /// </summary>
    Completed,
    
    /// <summary>
    /// indicates an appointment that is cancelled before examination
    /// </summary>
    Cancelled,
    
    /// <summary>
    /// indicates a patient did not show up for the appointment
    /// </summary>
    NoShow
}