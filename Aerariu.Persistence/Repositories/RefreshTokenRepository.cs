using Aerariu.Core.Models;
using Aerariu.Core.Repositories;
using Aerariu.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerariu.Persistence.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(AerariuDbContext dbContext) : base(dbContext) { }

        public async Task<RefreshToken> CreateTokenAsync(Guid userId)
        {
            var token = await StringHelper.GenerateBase64StringAsync();

            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = token,
                ValidTo = DateTime.UtcNow.AddHours(24),
                TotalRefresh = 0,
                IsRevoked = false
            };

            await this.AddAsync(refreshToken);

            return refreshToken;
        }

        public void Refresh(RefreshToken token)
        {
            token.ValidTo = DateTime.UtcNow.AddHours(24);
            token.TotalRefresh++;

            this.Update(token);
        }

        public void Revoke(RefreshToken token)
        {
            token.IsRevoked = true;

            this.Update(token);
        }
    }
}
