using Microsoft.EntityFrameworkCore;
using MovieAPI2.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
