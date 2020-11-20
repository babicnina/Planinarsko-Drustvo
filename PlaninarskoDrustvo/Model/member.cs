namespace PlaninarskoDrustvo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mc.member")]
    public partial class member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public member()
        {
            membership_fee = new HashSet<membership_fee>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string first_name { get; set; }

        [Required]
        [StringLength(45)]
        public string last_name { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime join_date { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime? leaving_date { get; set; }
        public virtual account account { get; set; }

        [NotMapped]
        public string formatedJoinDate { get; set; }
        [NotMapped]
        public string formatedLeavingDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<membership_fee> membership_fee { get; set; }
    }
}
