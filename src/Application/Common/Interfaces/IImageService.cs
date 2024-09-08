using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
    public interface IImageService
    {
        public Task<string> UploadImageAsync(IFormFile file, string key);
        public Task<string> DeleteImage(string imageId);
    }
}
