namespace OfficesApi.Contracts.Responses;

public class ErrorResponse
{
    public List<ErrorModel> Errors { get; set; } = new();
    
}