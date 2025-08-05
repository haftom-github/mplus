namespace Dd.Domain.Reservation.Utils;

public static class ScheduleMath {
    public static int Gcd(int a, int b) {
        while (b != 0) {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}