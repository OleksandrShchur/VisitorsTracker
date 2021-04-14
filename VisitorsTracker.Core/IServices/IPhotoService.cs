using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using VisitorsTracker.Db.Entities;

namespace VisitorsTracker.Core.IServices
{
    public interface IPhotoService
    {
        Task<string> AddImage(IFormFile uploadedFile, User user);

        Task<string> SaveImageInFolder(string url);

        Task DeleteImage(User user);
    }
}
