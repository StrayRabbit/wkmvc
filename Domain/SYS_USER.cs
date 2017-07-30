namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SYS_USER()
        {
            SYS_USER_ONLINE = new HashSet<SYS_USER_ONLINE>();
            SYS_USER_PERMISSION = new HashSet<SYS_USER_PERMISSION>();
            SYS_USER_ROLE = new HashSet<SYS_USER_ROLE>();
            SYS_USERINFO = new HashSet<SYS_USERINFO>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string NAME { get; set; }

        [StringLength(20)]
        public string ACCOUNT { get; set; }

        [StringLength(1000)]
        public string PASSWORD { get; set; }

        public bool ISCANLOGIN { get; set; }

        public int? SHOWORDER1 { get; set; }

        public int? SHOWORDER2 { get; set; }

        [StringLength(50)]
        public string PINYIN1 { get; set; }

        [StringLength(50)]
        public string PINYIN2 { get; set; }

        [Column(TypeName = "ntext")]
        public string FACE_IMG { get; set; }

        [StringLength(36)]
        public string LEVELS { get; set; }

        [StringLength(36)]
        public string DPTID { get; set; }

        [StringLength(36)]
        public string CREATEPER { get; set; }

        public DateTime? CREATEDATE { get; set; }

        [StringLength(36)]
        public string UPDATEUSER { get; set; }

        public DateTime? UPDATEDATE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SYS_USER_ONLINE> SYS_USER_ONLINE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SYS_USER_PERMISSION> SYS_USER_PERMISSION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SYS_USER_ROLE> SYS_USER_ROLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SYS_USERINFO> SYS_USERINFO { get; set; }
    }
}
