using API_Sat_2023II.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Sat_2023II.DAL
{
    public class DataBaseContext : DbContext
    {
        //Con este contructor me podré conectar a la BD, me brinda unas opciones de configuración que ya están definidas internamente
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique(); //Esto es un índice para evitar nombres duplicados de países
        }

        public DbSet<Country> Countries { get; set; } //Esta línea me toma la clase Country y me la mappea en SQL SERVER para crear una tabla llamada COUNTRIES

    }
}
