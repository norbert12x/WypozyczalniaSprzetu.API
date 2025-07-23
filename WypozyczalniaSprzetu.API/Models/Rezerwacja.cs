namespace WypozyczalniaSprzetu.API.Models
{
    public class Rezerwacja
    {
        public int Id { get; set; }
        public int KlientId { get; set; }
        public int SprzetId { get; set; }
        public DateTime DataOd {  get; set; }
        public DateTime DataDo { get; set; }
        public decimal CenaCalkowita { get; set; }
        public string Status { get; set; }
        public DateTime DataRezerwacji { get; set; }

        public Klient Klient { get; set; }
        public Sprzet Sprzet { get; set; }
        public Zwrot Zwrot { get; set; }
    }
}
