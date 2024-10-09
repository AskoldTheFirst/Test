using API.Biz.Interfaces;
using API.Database.Entities;
using API.DTOs;
using API.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace API.Biz.Service
{
    public class UserProfileService : IUserProfile
    {
        IUnitOfWork _uow;

        public UserProfileService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<UserProfileDto> GetUserProfileAsync(string username)
        {
            return await (from u in _uow.UserRepo.All
                          where u.UserName == username
                          select new UserProfileDto()
                          {
                              About = u.About,
                              Contacts = u.Contacts
                          }).SingleAsync();
        }

        public async Task SaveUserProfileAsync(UserProfileDto profile, string username)
        {
            User dbUser = await (from u in _uow.UserRepo.All
                                 where u.UserName == username
                                 select u).SingleAsync();

            dbUser.About = profile.About;
            dbUser.Contacts = profile.Contacts;

            await _uow.SaveAsync();
        }
    }
}