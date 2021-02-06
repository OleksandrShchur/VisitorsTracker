using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VisitorsTracker.Core.IServices;
using VisitorsTracker.Db.EFCore;

namespace VisitorsTracker.Core.Services
{
    public class PhotoService : BaseService<Photo>, IPhotoService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly Lazy<HttpClient> _client;

        public PhotoService(
            AppDbContext context,
            IHttpClientFactory clientFactory)
            : base(context)
        {
            _clientFactory = clientFactory;
            _client = new Lazy<HttpClient>(() => clientFactory.CreateClient());
        }

        public async Task<string> AddPhoto(IFormFile uploadedFile)
        {
            if (!IsValidImage(uploadedFile))
            {
                throw new ArgumentException();
            }

            string photoUrl;

            Insert(photo);
            await _context.SaveChangesAsync();

            return photo;
        }

        public async Task<Photo> AddPhotoByURL(string url)
        {
            if (!await IsImageUrl(url))
            {
                throw new ArgumentException();
            }

            Uri uri = new Uri(url);
            byte[] imgData = _client.Value.GetByteArrayAsync(uri).Result;
            var photo = new Photo
            {
                Thumb = imgData,
                Img = imgData,
            };

            Insert(photo);
            await _context.SaveChangesAsync();

            return photo;
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

        public async Task Delete(Guid id)
        {
            var photo = _context.Photos.Find(id);
            if (photo != null)
            {
                Delete(photo);
                await _context.SaveChangesAsync();
            }
        }

        private static bool IsValidImage(IFormFile file) => file != null && file.IsImage();
    }
}
