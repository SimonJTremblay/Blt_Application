using BltApplication.Models;
using BltData.Models;
using Microsoft.EntityFrameworkCore;

namespace BltData
{
    public class BltContext : DbContext
    {
        public BltContext(DbContextOptions options) : base(options) { }

        public DbSet<Project> Project { get; set; }
        public DbSet<Deliverable> Deliverable { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<Bluf> Bluf { get; set; }
    }
}
