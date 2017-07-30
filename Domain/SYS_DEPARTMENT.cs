namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_DEPARTMENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SYS_DEPARTMENT()
        {
            SYS_POST = new HashSet<SYS_POST>();
        }

        [StringLength(36)]
        public string ID { get; set; }

        [StringLength(100)]
        public string CODE { get; set; }

        [StringLength(200)]
        public string NAME { get; set; }

        public int? BUSINESSLEVEL { get; set; }

        public int? SHOWORDER { get; set; }

        [StringLength(36)]
        public string CREATEPERID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CREATEDATE { get; set; }

        [StringLength(36)]
        public string PARENTID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UPDATEDATE { get; set; }

        [StringLength(36)]
        public string UPDATEUSER { get; set; }

        [StringLength(100)]
        public string PARENTCODE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SYS_POST> SYS_POST { get; set; }
    }
}
