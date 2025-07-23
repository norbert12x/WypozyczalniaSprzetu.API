using Microsoft.EntityFrameworkCore;
using WypozyczalniaSprzetu.API.Models;

namespace WypozyczalniaSprzetu.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Klient> Klienci { get; set; }
        public DbSet<KategoriaSprzetu> KategorieSprzetu { get; set; }
        public DbSet<Sprzet> Sprzety { get; set; }
        public DbSet<Rezerwacja> Rezerwacje { get; set; }
        public DbSet<Zwrot> Zwroty { get; set; }

        public decimal ObliczRabaty(Klient klient)
        {
            var liczbaWypozyczen = Rezerwacje
                .Where(r => r.KlientId == klient.Id && r.DataRezerwacji >= DateTime.Now.AddMonths(-6))
                .Count();

            if (liczbaWypozyczen >= 5)
            {
                return 0.1m;
            }

            return 0m;
        }
    }
}