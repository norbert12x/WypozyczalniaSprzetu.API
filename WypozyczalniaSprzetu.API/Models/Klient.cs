using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WypozyczalniaSprzetu.API.Models
{
    public class Klient
    {
        public int Id { get; set; }

        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public string Email { get; set; }

        public string Telefon { get; set; }

        public DateTime DataRejestracji { get; set; }

        // TA WŁAŚCIWOŚĆ TERAZ JEST W PEŁNI OPCJONALNA:
        [JsonIgnore]    // nie bierze udziału w JSON‑owej deserializacji
        [BindNever]     // MVC nie próbuje jej wiązać ani walidować
        public ICollection<Rezerwacja>? Rezerwacje { get; set; } = new List<Rezerwacja>();
    }
}
