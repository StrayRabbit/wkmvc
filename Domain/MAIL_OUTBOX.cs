namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MAIL_OUTBOX
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MAIL_OUTBOX()
        {
            MAIL_ATTACHMENT = new HashSet<MAIL_ATTACHMENT>();
            MAIL_INBOX = new HashSet<MAIL_INBOX>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(72)]
        public string FK_RELATIONID { get; set; }

        [Required]
        [StringLength(50)]
        public string FK_UserId { get; set; }

        [Required]
        [StringLength(500)]
        public string ToUser { get; set; }

        [Required]
        [StringLength(100)]
        public string MailTitle { get; set; }

        public int SendStatus { get; set; }

        public int MailType { get; set; }

        public DateTime SendDate { get; set; }

        public DateTime SaveDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MAIL_ATTACHMENT> MAIL_ATTACHMENT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MAIL_INBOX> MAIL_INBOX { get; set; }
    }
}
