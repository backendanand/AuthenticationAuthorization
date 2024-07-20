using AuthenticationAuthorization.Data;
using AuthenticationAuthorization.Repositories.Interfaces;

namespace AuthenticationAuthorization.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DapperContext _context;
        public AuthRepository(DapperContext context)
        {
            _context = context;
        }
        public Task<string> Login(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
