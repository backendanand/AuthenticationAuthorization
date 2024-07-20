using AuthenticationAuthorization.Data;
using AuthenticationAuthorization.Models;
using AuthenticationAuthorization.Repositories.Interfaces;

namespace AuthenticationAuthorization.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        public UserRepository(DapperContext context)
        {
            _context = context;
        }
        public (string passwordHash, string passwordSalt) CreatePasswordHash(string password)
        {
            throw new NotImplementedException();
        }

        public Task CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
