using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorsTracker.Core.DTOs;
using VisitorsTracker.Core.Exceptions;
using VisitorsTracker.Core.IServices;
using VisitorsTracker.Core.Notifications;
using VisitorsTracker.Db.EFCore;
using VisitorsTracker.Db.Entities;

namespace VisitorsTracker.Core.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IMediator _mediator;
        private readonly IPhotoService _photoService;

        public UserService(
            AppDbContext context,
            IMapper mapper,
            IMediator mediator,
            IPhotoService photoService)
            : base(context, mapper)
        { 
            _mediator = mediator;
            _photoService = photoService;
        }

        public async Task Create(UserDTO userDto)
        {
            if (_context.Users.Any(u => u.Email == userDto.Email))
            {
                throw new VisitorsTrackerException("Email already exists in database");
            }

            var user = _mapper.Map<User>(userDto);

            user.Role = _context.Roles.FirstOrDefault(r => r.Name == "User");

            var result =  await Insert(user);
            if (result.Email != user.Email || result.Id == Guid.Empty)
            {
                throw new VisitorsTrackerException("Registration failed");
            }

            userDto.Id = result.Id;
            await _mediator.Publish(new UserProfileCreatedMessage(userDto));
        }

        public async Task Update(UserDTO userDTO)
        {
            if (string.IsNullOrEmpty(userDTO.Email))
            {
                throw new VisitorsTrackerException("EMAIL cannot be empty");
            }

            if (!_context.Users.Any(u => u.Id == userDTO.Id))
            {
                throw new VisitorsTrackerException("Not found");
            }

            var result = _mapper.Map<UserDTO, User>(userDTO);

            await Update(result);
            _context.Entry(result).State = EntityState.Detached;
        }

        public async Task ChangeRole(Guid uId, Guid rId)
        {
            var newRole = _context.Roles.Find(rId);
            if (newRole == null)
            {
                throw new VisitorsTrackerException("Invalid role Id");
            }

            var user = _context.Users.Find(uId);
            if (user == null)
            {
                throw new VisitorsTrackerException("Invalid user Id");
            }

            user.Role = newRole;
            await Update(user);
        }

        public async Task ChangeAvatar(Guid userId, IFormFile avatar)
        {
            var user = _context.Users
                .Include(u => u.Photo)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new VisitorsTrackerException("User not found");
            }

            if (user.Photo != null)
            {
                await _photoService.DeleteImage(user);
            }

            try
            {
                user.Photo = await _photoService.AddImage(avatar, user);
            }
            catch (ArgumentException)
            {
                throw new VisitorsTrackerException("Bad image file");
            }

            await Update(user);
        }

        public UserDTO GetById(Guid id)
        {
            var user = _mapper.Map<UserDTO>(
                _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(x => x.Id == id));

            return user;
        }

        public UserDTO GetByEmail(string email)
        {
            var user = _mapper.Map<UserDTO>(
                 _context.Users
                .Include(u => u.Role)
                .AsNoTracking()
                .FirstOrDefault(o => o.Email == email));

            return user;
        }

        public IEnumerable<UserDTO> GetUsersByRole(string role)
        {
            var users = _context.Users
               .Include(u => u.Role)
               .Where(user => user.Role.Name == role)
               .AsNoTracking()
               .AsEnumerable();

            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public UserDTO GetUserByRefreshToken(string token)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .Include(u => u.RefreshTokens)
                .SingleOrDefault(u => u.RefreshTokens
                .Any(t => t.Token.Equals(token, StringComparison.OrdinalIgnoreCase)));

            return _mapper.Map<UserDTO>(user);
        }
    }
}
