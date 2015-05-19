using Ranking.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ranking.GUI.ViewModels
{
    public class PlaceDetailsViewModel
    {
        public int PlaceId { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Kraj")]
        public string Country { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Zdjęcie")]
        public IList<string> Picture { get; set; }

        [Display(Name = "Opinie")]
        public IList<KeyValuePair<string, Opinion>> Opinions { get; set; }
    }
}