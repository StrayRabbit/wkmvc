namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_SYSTEM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SYS_SYSTEM()
        {
            SYS_MODULE = new HashSet<SYS_MODULE>();
        }

        [StringLength(36)]
        public string ID { get; set; }

        [StringLength(200)]
        public string NAME { get; set; }

        [StringLength(500)]
        public string SITEURL { get; set; }

        public bool IS_LOGIN { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CREATEDATE { get; set; }

        [StringLength(2000)]
        public string REMARK { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SYS_MODULE> SYS_MODULE { get; set; }
    }
}
