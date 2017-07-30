namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MAIL_INBOX
    {
        public int ID { get; set; }

        public int FK_MailID { get; set; }

        [Required]
        [StringLength(50)]
        public string Recipient { get; set; }

        public int MailType { get; set; }

        public int ReadStatus { get; set; }

        public DateTime ReceivingTime { get; set; }

        public virtual MAIL_OUTBOX MAIL_OUTBOX { get; set; }
    }
}
