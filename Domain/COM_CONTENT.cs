namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class COM_CONTENT
    {
        public int ID { get; set; }

        [Required]
        [StringLength(72)]
        public string FK_RELATIONID { get; set; }

        [Column(TypeName = "ntext")]
        public string CONTENT { get; set; }

        public byte[] CONTENTBLOB { get; set; }

        [Required]
        [StringLength(200)]
        public string FK_TABLE { get; set; }

        public DateTime CREATEDATE { get; set; }
    }
}
