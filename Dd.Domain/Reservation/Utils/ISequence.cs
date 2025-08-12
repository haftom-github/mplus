namespace Dd.Domain.Reservation.Utils;

public interface ISequence {
    public int Start { get; }
    public int? End { get; }
    public int Interval { get; }
    public bool IsFinite { get; }
    public int? Length { get; }
    public int S(int n);
}