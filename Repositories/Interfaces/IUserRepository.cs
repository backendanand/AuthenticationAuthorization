using AuthenticationAuthorization.Models;

namespace AuthenticationAuthorization.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUser(User user);
        Task<User> GetUserByUserName(string userName);
        (string passwordHash, string passwordSalt) CreatePasswordHash(string password);
    }
}
