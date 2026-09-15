using Microsoft.EntityFrameworkCore;
using Javapunk.Models;

namespace Javapunk.Data;

    public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
    {
    }
    public DbSet<Users> Users {get; set;}
    public DbSet<Modules> Modules {get; set;}
    public DbSet<Questions> Questions {get; set;}
    public DbSet<Answers> Answers {get; set;}
    public DbSet<Completed_modules> Completed_modules {get; set;}
}
