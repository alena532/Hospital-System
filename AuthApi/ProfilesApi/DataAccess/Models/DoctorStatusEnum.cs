namespace ProfilesApi.DataAccess.Models;

public enum DoctorStatusEnum : int
{
    AtWork,
    OnVacation,
    SickDay,
    SickLeave,
    SelfIsolation,
    LeaveWithoutPay,
    Inactive
}