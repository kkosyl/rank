﻿using Ranking.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ranking.GUI.ViewModels.Opinions
{
    public class AddOpinionViewModel
    {
        public int PlaceId { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name="Nick")]
        public string Nick { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Uwagi")]
        public string Content { get; set; }

        public DateTime AddDate { get; set; }

        [Required]
        [Display(Name = "Ocena")]
        public double Rate { get; set; }
    }
}