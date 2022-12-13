namespace OfficesApi.Contracts.Requests.OfficeReceptionist;

public class EditOfficeReceptionistRequest
{
    public Guid ReceptionistId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public Guid PhotoId { get; set; }
    public string PhotoUrl { get; set; }
}