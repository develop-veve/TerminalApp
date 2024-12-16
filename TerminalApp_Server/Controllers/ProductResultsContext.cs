using Microsoft.EntityFrameworkCore;
using TerminalApp_Shared.Models;

namespace TerminalApp_Server.Data
{
    public class ProductResultsContext : DbContext
    {
        public ProductResultsContext(DbContextOptions<ProductResultsContext> options) : base(options) { }

        public DbSet<ProductResult> ProductResults { get; set; }
    }
}
