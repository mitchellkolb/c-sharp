using Microsoft.EntityFrameworkCore;
using TallyBoard.Models;

namespace TallyBoard.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Person> People => Set<Person>();
    }
}
