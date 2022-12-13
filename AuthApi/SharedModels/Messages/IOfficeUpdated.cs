namespace SharedModels.Messages;

public interface IOfficeUpdated
{
    Guid Id { get; set; }
    string Address { get; set; }
}