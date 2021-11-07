using System;
using System.IO;
using Database.DAL.Entities;
using Database.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Website.Infrastructure.Services.Interfaces;

namespace Website.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment env;
        private readonly IRepository<User> UsersRepository;

        public FileService(IWebHostEnvironment env, IRepository<User> usersRepository)
        {
            this.env = env;
            UsersRepository = usersRepository;
        }

        public string Upload(IFormFile file, string userId)
        {
            var uploadDirectory = "uploads/" + userId+"/";
            var uploadPath = Path.Combine(env.WebRootPath, uploadDirectory);

            if(Directory.Exists(uploadPath))
                Directory.Delete(uploadPath,true);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using var stream = File.Create(filePath);
            file.CopyTo(stream);

            var user = UsersRepository.Get(int.Parse(userId));
            user.AvatarPath = "/"+uploadDirectory+fileName;
            UsersRepository.Update(user);
            return fileName;
        }
    }
}
