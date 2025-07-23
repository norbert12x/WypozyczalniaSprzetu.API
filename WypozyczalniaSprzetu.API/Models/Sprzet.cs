using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;
using WypozyczalniaSprzetu.API.Models;

public class Sprzet
{
    public int Id { get; set; }
    public string Nazwa { get; set; }
    public int KategoriaId { get; set; }
    public decimal CenaZaDzien { get; set; }
    public bool Dostepny { get; set; }

    [JsonIgnore]
    [BindNever]
    public KategoriaSprzetu? Kategoria { get; set; }

    [JsonIgnore]
    [BindNever]
    public ICollection<Rezerwacja>? Rezerwacje { get; set; }
}
