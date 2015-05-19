namespace Ranking.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public User()
        {
            Opinions = new HashSet<Opinion>();
        }

        public int UserID { get; set; }

        [Required]
        [StringLength(15)]
        public string Nick { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        public virtual ICollection<Opinion> Opinions { get; set; }
    }
}
