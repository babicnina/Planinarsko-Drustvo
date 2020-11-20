namespace PlaninarskoDrustvo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mc.membership_fee")]
    public partial class membership_fee
    {
        public short year { get; set; }

        public decimal amount { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime? time { get; set; }

        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int member_id { get; set; }

        public virtual member member { get; set; }
    }
}
