using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;

namespace MadStick.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MadStickContext _context;

        public UserRepository(MadStickContext context)
        {
            _context = context;
        }
        public ApplicationUser GetUserById(string userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }
    }
}