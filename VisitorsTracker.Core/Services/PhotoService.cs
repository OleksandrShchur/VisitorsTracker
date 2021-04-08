using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VisitorsTracker.Core.Exceptions;
using VisitorsTracker.Core.Extensions;
using VisitorsTracker.Core.IServices;
using VisitorsTracker.Db.EFCore;
using VisitorsTracker.Db.Entities;

namespace VisitorsTracker.Core.Services
{
    public class PhotoService : BaseService<User>, IPhotoService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly Lazy<HttpClient> _client;
        private readonly IWebHostEnvironment _appEnvironment;

        public PhotoService(
            AppDbContext context,
            IHttpClientFactory clientFactory,
            IWebHostEnvironment appEnvironment)
            : base(context)
        {
            _clientFactory = clientFactory;
            _client = new Lazy<HttpClient>(() => _clientFactory.CreateClient());
            _appEnvironment = appEnvironment;
        }

        public async Task<string> AddPhoto(IFormFile uploadedFile, Guid uId)
        {
            if (!IsValidImage(uploadedFile))
            {
                throw new ArgumentException();
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == uId);

            if (user == null)
            {
                throw new VisitorsTrackerException("User is equal null");
            }

            user.Photo = $"/Photos/{Guid.NewGuid()}_{uploadedFile.FileName}";

            // save file in Img folder in wwwroot directory
            using (var fileStream = new FileStream($"{_appEnvironment.WebRootPath}{user.Photo}",
                FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }

            await Update(user);

            return user.Photo;
        }

        public async Task<string> SavePhotoInFolder(string url)
        {
            if (!IsImageUrl(url))
            {
                throw new ArgumentException();
            }

            string photoPath = $"/Photos/{Guid.NewGuid()}.jpeg";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            using (
                Stream contentStream = await (await _client.Value.SendAsync(request)).Content.ReadAsStreamAsync(),
                stream = new FileStream($"{_appEnvironment.WebRootPath}{photoPath}", FileMode.Create))
            {

                await contentStream.CopyToAsync(stream);
            }

            return photoPath;
        }

        public async Task DeleteImage(Guid userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new VisitorsTrackerException("User is equal null");
            }

            var path = $"{_appEnvironment.WebRootPath}{user.Photo}";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            user.Photo = null;

            await Update(user);
        }

        private bool IsImageUrl(string url)
        {
            string pattern = @"^http(s) ?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$";

            return Regex.IsMatch(url, pattern, RegexOptions.None);
        }

        private static bool IsValidImage(IFormFile file) => file != null && file.IsImage();
    }
}
