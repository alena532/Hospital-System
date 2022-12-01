namespace OfficesApi.Contracts.Requests.OfficeReceptionist;

public class CreateOfficeReceptionistRequest
{
    public int ReceptionistId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public int PhotoId { get; set; }
    public string PhotoUrl { get; set; }
}