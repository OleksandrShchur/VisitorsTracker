using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
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
        private readonly IEmailService _emailService;
        private readonly IHttpClientFactory _clientFactory;
        private readonly Lazy<HttpClient> _client;
        private readonly IWebHostEnvironment _appEnvironment;

        public UserService(
            AppDbContext context,
            IMapper mapper,
            IMediator mediator,
            IEmailService emailService,
            IHttpClientFactory clientFactory,
            IWebHostEnvironment appEnvironment)
            : base(context, mapper)
        { 
            _mediator = mediator;
            _emailService = emailService;
            _clientFactory = clientFactory;
            _client = new Lazy<HttpClient>(() => clientFactory.CreateClient());
            _appEnvironment = appEnvironment;
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

            Update(result);
            _context.Entry(result).State = EntityState.Detached;

            await _context.SaveChangesAsync();
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
               DeleteImage(user);
            }

            try
            {
                user.Photo = await AddPhoto(avatar, user.Id);
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

        public async Task<string> AddPhoto(IFormFile uploadedFile, Guid uId)
        {
            if (!IsValidImage(uploadedFile))
            {
                throw new ArgumentException();
            }

            var user = _context.Users
                .Include(u => u.Photo)
                .FirstOrDefault(u => u.Id == uId);

            if (user == null)
            {
                throw new VisitorsTrackerException("User is equal null");
            }
            
            user.Photo = $"\\Img\\{uploadedFile.FileName}{new Guid()}";

            // save file in Img folder in wwwroot directory
            using (var fileStream = new FileStream($"{_appEnvironment.WebRootPath}{user.Photo}",
                FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }

            Update(user);
            await _context.SaveChangesAsync();

            return user.Photo;
        }

        public async Task<string> SavePhotoInFolder(string url) // to do
        {
            if (!IsImageUrl(url))
            {
                throw new ArgumentException();
            }

            string photoPath = $"\\Img\\{new Guid()}.jpg";

            var response = _client.Value.GetAsync(url);
            using (var fileStream = new FileStream($"{_appEnvironment.WebRootPath}{photoPath}",
                FileMode.Create))
            {
                await response.Result.Content.CopyToAsync(fileStream);
            }

            return photoPath;
        }

        private bool IsImageUrl(string url)
        {
            string pattern = @"^http(s) ?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$";

            return Regex.IsMatch(url, pattern, RegexOptions.None);
        }

        private void DeleteImage(User user) => user.Photo = string.Empty;

        private static bool IsValidImage(IFormFile file) => file != null;
    }
}
