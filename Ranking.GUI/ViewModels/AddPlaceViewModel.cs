using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ranking.GUI.ViewModels
{
    public class AddPlaceViewModel
    {
        public AddPlaceViewModel()
        {
            Picture = new List<HttpPostedFileBase>();
        }

        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Kraj")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Zdjęcie")]
        public IEnumerable<HttpPostedFileBase> Picture { get; set; }
    }
}