using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsTracker.Core.IServices
{
    public interface IPhotoService
    {
        Task<string> AddPhoto(IFormFile uploadedFile);

        Task<string> AddPhotoByURL(string url);

        Task Delete(Guid id);
    }
}
