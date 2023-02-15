using Aerariu.Core.models;
using Microsoft.EntityFrameworkCore;

namespace Aerariu.Persistence
{
    public class AerariuDbContext : DbContext
    {
        public AerariuDbContext(DbContextOptions<AerariuDbContext> options): base(options) { }
    }
}
