using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Ranking.Domain.Models;

namespace Ranking.GUI.ViewModels.Places
{
    public class PlaceListViewModel
    {
        [Display(Name = "Id")]
        public int PlaceId { get; set; }

        [Display(Name="Nazwa")]
        public string Name { get; set; }

        [Display(Name="Kraj")]
        public string Country { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name="Opis")]
        public string Description { get; set; }

        [Display(Name="Zdjęcia")]
        public string Picture { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Telefon")]
        public string Telephone { get; set; }

        public bool Verified { get; set; }

        [Display(Name="Ocena")]
        public double Rate { get; set; }
    }
    
}