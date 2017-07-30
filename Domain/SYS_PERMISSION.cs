namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_PERMISSION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SYS_PERMISSION()
        {
            SYS_ROLE_PERMISSION = new HashSet<SYS_ROLE_PERMISSION>();
            SYS_USER_PERMISSION = new HashSet<SYS_USER_PERMISSION>();
        }

        public int ID { get; set; }

        public int MODULEID { get; set; }

        [StringLength(36)]
        public string NAME { get; set; }

        [StringLength(100)]
        public string PERVALUE { get; set; }

        [StringLength(50)]
        public string ICON { get; set; }

        public int? SHOWORDER { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CREATEDATE { get; set; }

        [StringLength(36)]
        public string CREATEUSER { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UPDATEDATE { get; set; }

        [StringLength(36)]
        public string UPDATEUSER { get; set; }

        public virtual SYS_MODULE SYS_MODULE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SYS_ROLE_PERMISSION> SYS_ROLE_PERMISSION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SYS_USER_PERMISSION> SYS_USER_PERMISSION { get; set; }
    }
}
