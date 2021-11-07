using Microsoft.AspNetCore.Http;

namespace Website.Infrastructure.Services.Interfaces
{
    public interface IFileService
    {
        string Upload(IFormFile file, string userId);
    }
}
