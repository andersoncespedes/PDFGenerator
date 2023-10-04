using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.Data;

    public class PDFGeneratorContext : DbContext
    {
        public DbSet<Producto> Productos {get; set;}
        public DbSet<Marca> Marcas {get; set;}
        public PDFGeneratorContext(DbContextOptions<PDFGeneratorContext> options) : base(options){

        }
        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }
    }
