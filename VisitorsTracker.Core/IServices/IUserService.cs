using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
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

        ProfileDTO GetProfileById(Guid id, Guid fromId);

        //IEnumerable<UserDTO> Get(UsersFilterViewModel model, out int count, Guid id); // to do

        IEnumerable<UserDTO> GetUsersByRole(string role);

        UserDTO GetUserByRefreshToken(string token);
    }
}
