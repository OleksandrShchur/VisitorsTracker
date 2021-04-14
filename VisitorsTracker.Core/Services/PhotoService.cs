using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VisitorsTracker.Core.Exceptions;
using VisitorsTracker.Core.Extensions;
using VisitorsTracker.Core.IServices;
using VisitorsTracker.Db.Entities;

namespace VisitorsTracker.Core.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly Lazy<HttpClient> _client;
        private readonly IWebHostEnvironment _appEnvironment;

        private const string validImagePattern = @"^http(s) ?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$";

        public PhotoService(
            IHttpClientFactory clientFactory,
            IWebHostEnvironment appEnvironment)
        {
            _clientFactory = clientFactory;
            _client = new Lazy<HttpClient>(() => _clientFactory.CreateClient());
            _appEnvironment = appEnvironment;
        }

        public async Task<string> AddImage(IFormFile uploadedFile, User user)
        {
            if (!IsValidImage(uploadedFile))
            {
                throw new ArgumentException();
            }

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

            return user.Photo;
        }

        public async Task<string> SaveImageInFolder(string url)
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

        public Task DeleteImage(User user)
        {
            if (user == null)
            {
                throw new VisitorsTrackerException("User is equal null");
            }

            var path = $"{_appEnvironment.WebRootPath}{user.Photo}";

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private bool IsImageUrl(string url) =>
            Regex.IsMatch(url, validImagePattern, RegexOptions.None);

        private static bool IsValidImage(IFormFile file) => file != null && file.IsImage();
    }
}
