namespace ProfilesApi.Contracts;

public class GetPhotoResponse
{
    public string FileName { get; set; }
    public byte[] Bytes { get; set; }
}