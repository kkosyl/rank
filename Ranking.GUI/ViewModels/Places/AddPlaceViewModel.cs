using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ranking.GUI.ViewModels.Places
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

        [Display(Name = "Ulica")]
        public string Address { get; set; }

        [Display(Name = "Telefon")]
        [RegularExpression("^([0-9]{9})|(([0-9]{3}-){2}[0-9]{3})$")]
        public string Telephone { get; set; }

        [Required]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Zdjęcie")]
        public IEnumerable<HttpPostedFileBase> Picture { get; set; }
    }
}