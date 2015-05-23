using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ranking.GUI.ViewModels
{
    public class AddOpinionViewModel
    {
        public int PlaceId { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name="Nick")]
        public string Nick { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Uwagi")]
        public string Content { get; set; }

        public DateTime AddDate { get; set; }

        [Required]
        [Display(Name = "Ocena")]
        //[RegularExpression("[1-5]")]
        public int Grade { get; set; }
    }
}