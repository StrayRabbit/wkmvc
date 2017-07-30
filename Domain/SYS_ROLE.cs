namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_ROLE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SYS_ROLE()
        {
            SYS_ROLE_PERMISSION = new HashSet<SYS_ROLE_PERMISSION>();
            SYS_USER_ROLE = new HashSet<SYS_USER_ROLE>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(36)]
        public string FK_BELONGSYSTEM { get; set; }

        [StringLength(50)]
        public string ROLENAME { get; set; }

        public bool ISCUSTOM { get; set; }

        [StringLength(1000)]
        public string ROLEDESC { get; set; }

        [Required]
        [StringLength(36)]
        public string CREATEPERID { get; set; }

        public DateTime CREATEDATE { get; set; }

        public DateTime UPDATEDATE { get; set; }

        [StringLength(36)]
        public string UPDATEUSER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SYS_ROLE_PERMISSION> SYS_ROLE_PERMISSION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SYS_USER_ROLE> SYS_USER_ROLE { get; set; }
    }
}
