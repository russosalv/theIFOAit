using System.Diagnostics;
using Azure.Storage.Blobs;
using IFOA.Exceptions;
using Microsoft.Extensions.Configuration;

namespace IFOA.Shared.Services;

public class PictureBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    public const string ContainerName = "pictures";
    public PictureBlobStorageService(IConfiguration configuration)
    {
        
        var pictureBlobStorageConnectionString = configuration.TryGetConfigurationValue(IfoaConfigurationKeys.PictureBlobStorageConnectionString);

        if (Debugger.IsAttached)
        {
            if (string.IsNullOrEmpty(pictureBlobStorageConnectionString))
            {
                // set pictureBlobStorageConnectionString as local storage emulator
                pictureBlobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=stifoa;AccountKey=XSyBPPS04YbrZ0Fdo93jbc1PiCPqt98gCEgX/NjxnJHDZQr9DkrKQpFCBBKJw1uBmGGlSPXSG9FS+AStqPSAKA==;EndpointSuffix=core.windows.net";
            }
        }
        
        if (string.IsNullOrEmpty(pictureBlobStorageConnectionString))
        {
            throw new MissingConfigurationException(IfoaConfigurationKeys.PictureBlobStorageConnectionString);
        }

        _blobServiceClient = new BlobServiceClient(pictureBlobStorageConnectionString);
    }
    
    public async Task<BlobClient> UploadPictureAsync(string base64Image, string fileName)
    {
        var base64Data = base64Image.Split(',')[1];
        var containerClient = await CreateContainerIfNotExistAsync(ContainerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        var bytes = Convert.FromBase64String(base64Data);
        await blobClient.UploadAsync(new MemoryStream(bytes), true);
        return blobClient;
    }
    
    private async Task<BlobContainerClient> CreateContainerIfNotExistAsync(string containerName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();
        return containerClient;
    }
    
    public async Task<string> GetBase64ImageAsync(string blobUri)
    {
        var blobClient = new BlobClient(new Uri(blobUri));
        var blobDownloadInfo = await blobClient.DownloadAsync();
        var bytes = new byte[blobDownloadInfo.Value.ContentLength];
        await blobDownloadInfo.Value.Content.ReadAsync(bytes, 0, (int)blobDownloadInfo.Value.ContentLength);
        return Convert.ToBase64String(bytes);
    }
}