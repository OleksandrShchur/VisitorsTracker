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
using VisitorsTracker.Db.EFCore;
using VisitorsTracker.Db.Entities;

namespace VisitorsTracker.Core.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IPhotoService _photoService;
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;

        public UserService(
            AppDbContext context,
            IMapper mapper,
            IPhotoService photoSrv,
            IMediator mediator,
            IEmailService emailService)
            : base(context, mapper)
        {
            _photoService = photoSrv;
            _mediator = mediator;
            _emailService = emailService;
        }

        public async Task Create(UserDTO userDto)
        {
            if (_context.Users.Any(u => u.Email == userDto.Email))
            {
                throw new VisitorsTrackerException("Email already exists in database");
            }

            var user = _mapper.Map<User>(userDto);

            user.Role = _context.Roles.FirstOrDefault(r => r.Name == "User");

            var result = Insert(user);
            if (result.Email != user.Email || result.Id == Guid.Empty)
            {
                throw new VisitorsTrackerException("Registration failed");
            }

            await _context.SaveChangesAsync();
            userDto.Id = result.Id;
        }

        public async Task Update(UserDTO userDto)
        {

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
            await _context.SaveChangesAsync();
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
                await _photoService.Delete(user);
            }

            try
            {
                user.Photo = await _photoService.AddPhoto(avatar, user);
                Update(user); // delete, if Update have already done in Photo service
                await _context.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                throw new VisitorsTrackerException("Bad image file");
            }
        }

        public UserDTO GetById(Guid id)
        {
            var user = _mapper.Map<UserDTO>(
                _context.Users
                .Include(u => u.Photo)
                .Include(u => u.Role)
                .FirstOrDefault(x => x.Id == id));

            return user;
        }

        public UserDTO GetByEmail(string email)
        {
            var user = _mapper.Map<UserDTO>(
                 _context.Users
                .Include(u => u.Photo)
                .Include(u => u.Role)
                .AsNoTracking()
                .FirstOrDefault(o => o.Email == email));

            return user;
        }

        public ProfileDTO GetProfileById(Guid id, Guid fromId)
        {
            var user = _mapper.Map<UserDTO, ProfileDTO>(GetById(id));

            /*var rel = _context.Relationships
                .FirstOrDefault(x => x.UserFromId == fromId && x.UserToId == id);
            user.Attitude = (rel != null)
                ? (byte)rel.Attitude
                : (byte)Attitude.None;

            user.Rating = GetRating(user.Id);*/

            return user;
        }

        /*public IEnumerable<UserDTO> Get(UsersFilterViewModel model, out int count, Guid id)
        {

        }*/ // to do

        public IEnumerable<UserDTO> GetUsersByRole(string role)
        {
            var users = _context.Users
               .Include(u => u.Role)
               .Where(user => user.Role.Name == role)
               .AsNoTracking()
               .AsEnumerable();

            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }
    }
}
