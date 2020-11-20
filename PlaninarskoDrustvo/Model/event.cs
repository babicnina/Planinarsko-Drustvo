namespace PlaninarskoDrustvo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mc.event")]
    public partial class _event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public _event()
        {
            galleries = new HashSet<gallery>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime start { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime? end { get; set; }

        //[Required]
        //[StringLength(45)]
        //public string location { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string description { get; set; }

        [Column(TypeName = "enum")]
        [StringLength(65532)]
        public string type { get; set; }
        [StringLength(255)]
        
        public string cover { get; set; }
        [NotMapped]
        public string formatedStartDate { get; set; }

        [NotMapped]
        public string formatedEndDate { get; set; }
        [NotMapped]
        public string shortDescription { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<gallery> galleries { get; set; }
    }
}
