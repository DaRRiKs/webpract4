using Microsoft.EntityFrameworkCore;
using webpract4.Models;

namespace webpract4.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Team> Teams { get; set; }
}
