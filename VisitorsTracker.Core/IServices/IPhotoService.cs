using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using VisitorsTracker.Db.Entities;

namespace VisitorsTracker.Core.IServices
{
    public interface IPhotoService
    {
        Task<string> AddPhoto(IFormFile uploadedFile, User user);

        Task<string> SavePhotoInFolder(string url);

        Task<string> DeleteImage(User user);
    }
}
