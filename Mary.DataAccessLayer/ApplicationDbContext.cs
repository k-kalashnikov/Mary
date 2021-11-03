using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Mary.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext([NotNull] DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Models.User> Users { get; set; }
    }
}
