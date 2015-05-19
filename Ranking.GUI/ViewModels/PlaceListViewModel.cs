using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Ranking.Domain.Models;

namespace Ranking.GUI.ViewModels
{
    public class PlaceListViewModel
    {
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
    }
    
}