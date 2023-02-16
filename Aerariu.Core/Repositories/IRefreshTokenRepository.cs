using Aerariu.Core.Models;

namespace Aerariu.Core.Repositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken> CreateTokenAsync(Guid userId);
        void Refresh(RefreshToken token);
        void Revoke(RefreshToken token);
    }
}
