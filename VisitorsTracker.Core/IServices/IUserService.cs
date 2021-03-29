using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorsTracker.Core.DTOs;
using VisitorsTracker.Db.Entities;

namespace VisitorsTracker.Core.IServices
{
    public interface IUserService
    {
        Task Create(UserDTO userDto);

        Task Update(UserDTO userDTO);

        Task ChangeRole(Guid uId, Guid rId);

        Task ChangeAvatar(Guid uId, IFormFile avatar);

        UserDTO GetById(Guid id);

        UserDTO GetByEmail(string email);

        UserProfileDTO GetProfileById(Guid id, Guid fromId);

        IEnumerable<UserDTO> GetUsersByRole(string role);

        UserDTO GetUserByRefreshToken(string token);

        Task<string> AddPhoto(IFormFile uploadedFile, User user);

        Task<string> AddPhotoByURL(string url, User user);

        Task DeleteImage(User id);
    }
}
