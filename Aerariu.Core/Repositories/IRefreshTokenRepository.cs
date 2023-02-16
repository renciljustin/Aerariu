using Aerariu.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerariu.Core.Repositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken> CreateTokenAsync(Guid userId);
        void Refresh(RefreshToken token);
        void Revoke(RefreshToken token);
    }
}
