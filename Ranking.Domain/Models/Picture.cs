namespace Ranking.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Picture")]
    public partial class Picture
    {
        public int PictureID { get; set; }

        public int PlaceId { get; set; }

        [Required]
        [StringLength(100)]
        public string Source { get; set; }
    }
}
