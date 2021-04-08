using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using VisitorsTracker.Db.Entities;

namespace VisitorsTracker.Core.IServices
{
    public interface IPhotoService
    {
        Task<string> AddPhoto(IFormFile uploadedFile, Guid uId);

        Task<string> SavePhotoInFolder(string url);

        Task DeleteImage(Guid userId);
    }
}
