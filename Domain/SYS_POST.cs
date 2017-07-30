namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_POST
    {
        [StringLength(36)]
        public string ID { get; set; }

        [Required]
        [StringLength(36)]
        public string FK_DEPARTID { get; set; }

        [StringLength(100)]
        public string POSTNAME { get; set; }

        [Required]
        [StringLength(36)]
        public string POSTTYPE { get; set; }

        [StringLength(500)]
        public string REMARK { get; set; }

        public int? SHOWORDER { get; set; }

        [StringLength(50)]
        public string CREATEUSER { get; set; }

        public DateTime CREATEDATE { get; set; }

        public DateTime? UPDATEDATE { get; set; }

        [StringLength(36)]
        public string UPDATEUSER { get; set; }

        public virtual SYS_DEPARTMENT SYS_DEPARTMENT { get; set; }
    }
}
