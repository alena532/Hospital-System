namespace DocumentsApi.Contracts.Responses;

public class GetPhotoResponse
{
    public string FileName { get; set; }
    public byte[] Bytes { get; set; }
}