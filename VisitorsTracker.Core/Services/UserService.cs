using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
            /*userDto.Id = result.Id;
            if (!userDto.EmailConfirmed)
            {
                await _mediator.Publish(new RegisterVerificationMessage(userDto));
            }*/
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
               await Delete(user);
            }

            try
            {
                user.Photo = await AddPhoto(avatar, user);
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

        public UserDTO GetUserByRefreshToken(string token)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .Include(u => u.RefreshTokens)
                .SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token.Equals(token)));

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<string> AddPhoto(IFormFile uploadedFile, User user) // todo
        {
            if (!IsValidImage(uploadedFile))
            {
                throw new ArgumentException();
            }

            string path = "/Img/" + uploadedFile.FileName;
            // сохраняем файл в папку Files в каталоге wwwroot
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            user.Photo = path;

            //_context.Files.Add(path);
            /*FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
            _context.Files.Add(file);*/
            _context.SaveChanges();

            //Insert(photo);
            await _context.SaveChangesAsync();

            return path;
        }

        public async Task<string> AddPhotoByURL(string url, User user) // to do
        {
            if (!await IsImageUrl(url))
            {
                throw new ArgumentException();
            }

            Uri uri = new Uri(url);
            byte[] imgData = _client.Value.GetByteArrayAsync(uri).Result;
            /*var photo = new Photo
            {
                Thumb = imgData,
                Img = imgData,
            };

            Insert(photo);
            await _context.SaveChangesAsync();*/

            return url;
        }

        private async Task<bool> IsImageUrl(string url)
        {
            try
            {
                HttpResponseMessage result = await _client.Value.GetAsync(url);
                return result.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task Delete(User user)
        {
            /*var photo = _context.Photos.Find(id);
            if (photo != null)
            {
                Delete(photo);
                await _context.SaveChangesAsync();
            }*/
            user.Photo = string.Empty;
        }

        private static bool IsValidImage(IFormFile file) => file != null;// && file.IsImage();
    }
}
