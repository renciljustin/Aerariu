using Aerariu.Core.Models;
using Aerariu.Core.Repositories;
using Aerariu.Utils.Tools;
using Microsoft.EntityFrameworkCore;

namespace Aerariu.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AerariuDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            var passwordHash = user.PasswordHash;
            var passwordSalt = user.PasswordSalt;

            var passwordMatched = await PasswordHasher.VerifyPasswordAsync(password, passwordHash, passwordSalt);

            return passwordMatched;
        }

        public async Task CreateUserAsync(User user, string password)
        {
            (string passwordHash, string passwordSalt) = await PasswordHasher.HashPasswordAsync(password);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await base.AddAsync(user);
        }

        public async Task<IEnumerable<string>> GetRolesAsync(User user)
        {
            var userId = user.Id;

            return await _entitySet.Include(user => user.UserRoles)
                .ThenInclude(userRole => userRole.Role)
                .Where(user => user.Id == userId)
                .SelectMany(user => user.UserRoles.Select(role => role.Role.RoleName))
                .ToListAsync();
        }
    }
}
