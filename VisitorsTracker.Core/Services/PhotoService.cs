using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using VisitorsTracker.Core.IServices;
using VisitorsTracker.Db.EFCore;
using VisitorsTracker.Db.Entities;

namespace VisitorsTracker.Core.Services
{
    public class PhotoService : BaseService<string>, IPhotoService
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
            _client = new Lazy<HttpClient>(() => clientFactory.CreateClient());
            _appEnvironment = appEnvironment;
        }

        public async Task<string> AddPhoto(IFormFile uploadedFile, User user) // todo
        {
            if (!IsValidImage(uploadedFile))
            {
                throw new ArgumentException();
            }

            string path = "/Img/" + uploadedFile.FileName;
            // сохраняем файл в папку Files в каталоге wwwroot
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            user.Photo = path;

            //_context.Files.Add(path);
            /*FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
            _context.Files.Add(file);*/
            _context.SaveChanges();

            //Insert(photo);
            await _context.SaveChangesAsync();

            return path;
        }

        public async Task<string> AddPhotoByURL(string url, User user) // to do
        {
            if (!await IsImageUrl(url))
            {
                throw new ArgumentException();
            }

            Uri uri = new Uri(url);
            byte[] imgData = _client.Value.GetByteArrayAsync(uri).Result;
            /*var photo = new Photo
            {
                Thumb = imgData,
                Img = imgData,
            };

            Insert(photo);
            await _context.SaveChangesAsync();*/

            return url;
        }

        private async Task<bool> IsImageUrl(string url)
        {
            try
            {
                HttpResponseMessage result = await _client.Value.GetAsync(url);
                return result.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task Delete(User user)
        {
            /*var photo = _context.Photos.Find(id);
            if (photo != null)
            {
                Delete(photo);
                await _context.SaveChangesAsync();
            }*/
            user.Photo = string.Empty;
        }

        private static bool IsValidImage(IFormFile file) => file != null;// && file.IsImage();
    }
}
