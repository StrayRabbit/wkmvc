namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_MODULE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SYS_MODULE()
        {
            SYS_PERMISSION = new HashSet<SYS_PERMISSION>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(36)]
        public string FK_BELONGSYSTEM { get; set; }

        public int PARENTID { get; set; }

        [StringLength(50)]
        public string NAME { get; set; }

        [StringLength(50)]
        public string ALIAS { get; set; }

        public int MODULETYPE { get; set; }

        [StringLength(200)]
        public string ICON { get; set; }

        [StringLength(500)]
        public string MODULEPATH { get; set; }

        public bool ISSHOW { get; set; }

        public int SHOWORDER { get; set; }

        public int LEVELS { get; set; }

        public bool IsVillage { get; set; }

        [StringLength(50)]
        public string CREATEUSER { get; set; }

        public DateTime? CREATEDATE { get; set; }

        [StringLength(36)]
        public string UPDATEUSER { get; set; }

        public DateTime? UPDATEDATE { get; set; }

        public virtual SYS_SYSTEM SYS_SYSTEM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SYS_PERMISSION> SYS_PERMISSION { get; set; }
    }
}
