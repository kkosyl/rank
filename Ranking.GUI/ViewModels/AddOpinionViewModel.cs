using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ranking.GUI.ViewModels
{
    public class AddOpinionViewModel
    {
        [Required]
        [StringLength(10)]
        public string Nick { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime AddDate { get; set; }

        public int Grade { get; set; }
    }
}