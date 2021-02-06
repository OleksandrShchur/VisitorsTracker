using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using VisitorsTracker.Db.Entities;

namespace VisitorsTracker.Core.IServices
{
    public interface IPhotoService
    {
        Task<string> AddPhoto(IFormFile uploadedFile, User user);

        Task<string> AddPhotoByURL(string url, User user);

        Task Delete(User id);
    }
}
