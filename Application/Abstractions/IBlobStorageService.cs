namespace Application.Abstractions
{

    public interface IBlobStorageService
    {
        Task<string> UploadAsync(Stream stream, string fileName, string contentType);
    }
}
