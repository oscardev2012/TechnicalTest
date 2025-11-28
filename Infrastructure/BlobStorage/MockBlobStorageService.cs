using Application.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.BlobStorage
{

    public class BlobStorageServiceMock : IBlobStorageService
    {
        private readonly string _basePath;

        public BlobStorageServiceMock(IConfiguration config)
        {
            _basePath = config["Storage:BasePath"] ?? "uploads";
            Directory.CreateDirectory(_basePath);
        }

        public async Task<string> UploadAsync(Stream stream, string fileName, string contentType)
        {
            var path = Path.Combine(_basePath, fileName);

            using var fs = File.Create(path);
            await stream.CopyToAsync(fs);

            return $"/uploads/{fileName}";
        }
    }
}
