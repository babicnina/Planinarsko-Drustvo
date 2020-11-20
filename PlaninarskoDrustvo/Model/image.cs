namespace PlaninarskoDrustvo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mc.image")]
    public partial class image
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string path { get; set; }

        public int gallery_id { get; set; }

        public virtual gallery gallery { get; set; }
    }
}
