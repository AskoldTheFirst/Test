using API.DTOs;

namespace API.Biz.Interfaces
{
    public interface IUserProfile
    {
        Task<UserProfileDto> GetUserProfileAsync(string username);

        Task SaveUserProfileAsync(UserProfileDto profile, string username);
    }
}