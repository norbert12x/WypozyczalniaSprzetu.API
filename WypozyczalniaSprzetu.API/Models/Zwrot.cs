namespace WypozyczalniaSprzetu.API.Models
{
    public class Zwrot
    {
        public int Id { get; set; }
        public int RezerwacjaId { get; set; }
        public DateTime DataZwrotu { get; set; }
        public bool CzySpóźnione { get; set; }
        public decimal Kara {  get; set; }

        public Rezerwacja Rezerwacja { get; set; }
    }
}
