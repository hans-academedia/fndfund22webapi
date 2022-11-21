using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Services
{
    public class FileService
    {
        private BlobServiceClient serviceClient;
        private BlobContainerClient containerClient;
        private BlobClient blobClient;

        public FileService(IConfiguration configuration)
        {
            serviceClient = new BlobServiceClient(configuration.GetConnectionString("StorageAccount"));
            try 
            {
                containerClient = serviceClient.CreateBlobContainer("images");
                containerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            }
            catch 
            {
                containerClient = serviceClient.GetBlobContainerClient("images");
            }
        }

        public async Task<string> Upload(IFormFile profileImage)
        {
            try
            {
                using var file = profileImage.OpenReadStream();
                blobClient = containerClient.GetBlobClient($"img_{Guid.NewGuid()}{Path.GetExtension(profileImage.FileName)}");
                await blobClient.UploadAsync(file);

                return blobClient.Uri.AbsoluteUri;
            }
            catch { }
            return string.Empty;
        }
    }
}
