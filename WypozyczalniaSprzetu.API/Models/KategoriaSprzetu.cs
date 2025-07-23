using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

public class KategoriaSprzetu
{
    public int Id { get; set; }
    public string Nazwa { get; set; }

    [JsonIgnore]
    [BindNever]
    public ICollection<Sprzet>? Sprzety { get; set; }
}
