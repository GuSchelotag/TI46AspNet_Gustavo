using Microsoft.EntityFrameworkCore;
using PrimeiroProjeto.Models;

namespace PrimeiroProjeto.Data
{
    public class Context(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Cliente> Clientes { get; set; }
    }
}
