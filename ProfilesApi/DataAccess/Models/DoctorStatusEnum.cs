namespace ProfilesApi.DataAccess.Models;

public enum DoctorStatusEnum 
{
    AtWork = 0,
    OnVacation,
    SickDay,
    SickLeave,
    SelfIsolation,
    LeaveWithoutPay,
    Inactive
}