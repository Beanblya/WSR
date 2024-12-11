using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            
        }
        public DbSet<DBLibrary.User> responseUsers { get; set; } = null!;
    }
}
