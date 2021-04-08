using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorsTracker.Core.DTOs;

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

        IEnumerable<UserDTO> GetUsersByRole(string role);

        UserDTO GetUserByRefreshToken(string token);
    }
}
