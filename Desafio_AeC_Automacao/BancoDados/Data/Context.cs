using Desafio_AeC_Automacao.BancoDados.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio_AeC_Automacao.BancoDados.Context
{
    public class MySqlContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                var ConnectionString = "Server=localhost;port=3306;database=dbdesafio_aec;uid=root;password=root";
                options.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
            }
        }

        // -----------------------------------------------------
        // ----------------> Classes
        public DbSet<ClsCursos> ClsCursos { get; set; }

    }
}
