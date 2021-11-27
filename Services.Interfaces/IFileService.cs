using Microsoft.AspNetCore.Http;

namespace Services.Interfaces
{
    public interface IFileService
    {
        string UploadAvatar(IFormFile file, string userId);
        bool RemoveAvatar(string usedId);
    }
}
