namespace PlaninarskoDrustvo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mc.account")]
    public partial class account
    {
        [Required]
        [StringLength(45)]
        public string username { get; set; }

        [Required]
        [StringLength(255)]
        public string password { get; set; }

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string type { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int member_id { get; set; }

        public sbyte theme { get; set; }

        public sbyte language { get; set; }

        [StringLength(255)]
        public string profile_pic { get; set; }

        public virtual member member { get; set; }
    }
}
