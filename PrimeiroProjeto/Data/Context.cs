using Microsoft.EntityFrameworkCore;
using PrimeiroProjeto.Models;

namespace PrimeiroProjeto.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}

