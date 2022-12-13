namespace OfficesApi.Contracts.Responses.OfficeReceptionist;

public class GetOfficeReceptionistResponse
{
    public Guid Id { get; set; }
    public Guid OfficeId { get; set; }
    public Guid ReceptionistId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public Guid PhotoId { get; set; }
    public string PhotoUrl { get; set; }
}