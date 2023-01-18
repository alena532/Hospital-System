namespace DocumentsApi.Common;

public class PhotoStoreDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string PhotosCollectionName { get; set; } = null!;
    public string PhotoPatientsCollectionName { get; set; } = null!;
    public string PhotoDoctorsCollectionName { get; set; } = null!;
}