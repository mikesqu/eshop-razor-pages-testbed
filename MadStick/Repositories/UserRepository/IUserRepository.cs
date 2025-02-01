using MadStickWebAppTester.Data.UserEntity;

namespace MadStick.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser GetUserById(string userId);
    }
}