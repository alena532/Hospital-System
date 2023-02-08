namespace AppointmentsApi.Common.Constants;

public static class ScheduleConstants
{
    public static readonly TimeSpan Begin;
    public static readonly TimeSpan End;

    static ScheduleConstants()
    {
        Begin = new TimeSpan(9,0,0);
        End = new TimeSpan(17,0,0);
    }
    
}