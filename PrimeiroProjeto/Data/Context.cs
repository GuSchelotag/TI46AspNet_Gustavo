using Microsoft.EntityFrameworkCore;

namespace PrimeiroProjeto.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }
    }
}
