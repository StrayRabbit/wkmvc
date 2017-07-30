namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MAIL_ATTACHMENT
    {
        public int ID { get; set; }

        public int FK_MailID { get; set; }

        [Required]
        [StringLength(50)]
        public string AttName { get; set; }

        [Required]
        [StringLength(50)]
        public string AttNewName { get; set; }

        [Required]
        [StringLength(500)]
        public string AttPath { get; set; }

        [Required]
        [StringLength(10)]
        public string AttExt { get; set; }

        [Required]
        [StringLength(50)]
        public string AttSize { get; set; }

        [Required]
        [StringLength(50)]
        public string CreateIP { get; set; }

        public DateTime UploadDate { get; set; }

        public virtual MAIL_OUTBOX MAIL_OUTBOX { get; set; }
    }
}
