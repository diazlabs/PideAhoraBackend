using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Common.Services
{
    public class ImageService : IImageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;

        public ImageService(IConfiguration configuration)
        {
            _configuration = configuration;

            var accessKey = _configuration.GetSection("imageService:AccessKey").Value;
            var secretKey = _configuration.GetSection("imageService:SecretKey").Value;
            var accountId = _configuration.GetSection("imageService:AccountId").Value;

            var credentials = new BasicAWSCredentials(accessKey, secretKey);

            _s3Client = new AmazonS3Client(credentials, new AmazonS3Config
            {
                ServiceURL = $"https://{accountId}.r2.cloudflarestorage.com",
            });
        }

        public async Task<string> DeleteImage(string imageId)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = "testing",
                Key = imageId,
            };

            var response = await _s3Client.DeleteObjectAsync(request);

            return response.DeleteMarker;
        }

        public async Task<string> UploadImageAsync(IFormFile file, string key)
        {
            using var inputStream = new MemoryStream();
            file.CopyTo(inputStream);

            var request = new PutObjectRequest
            {
                BucketName = "testing",
                DisablePayloadSigning = true,
                InputStream = inputStream,
                Key = key,
                ContentType = file.ContentType
            };

            var response = await _s3Client.PutObjectAsync(request);
            return response.ETag;
        }
    }
}
