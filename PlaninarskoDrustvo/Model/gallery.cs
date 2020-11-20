namespace PlaninarskoDrustvo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mc.gallery")]
    public partial class gallery
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public gallery()
        {
            images = new HashSet<image>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime time { get; set; }

        public int event_id { get; set; }
        [NotMapped]
        public string formatedDate { get; set; }

        [NotMapped]
        public int numOfImages { get; set; }

        [NotMapped]
        public string coverImage { get; set; }
        
        public virtual _event _event { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<image> images { get; set; }
    }
}
