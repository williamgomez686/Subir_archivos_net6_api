using Microsoft.EntityFrameworkCore;
using Subir_archivos.Models;

namespace Subir_archivos.Context
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {
                
        }  
        public DbSet<Archivo> archivos { get; set; }
    }
}
