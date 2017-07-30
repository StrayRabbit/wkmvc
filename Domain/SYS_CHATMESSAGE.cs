namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_CHATMESSAGE
    {
        public int ID { get; set; }

        public int FromUser { get; set; }

        public int MessageType { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string MessageContent { get; set; }

        [StringLength(50)]
        public string ToGroup { get; set; }

        public DateTime MessageDate { get; set; }

        [Required]
        [StringLength(50)]
        public string MessageIP { get; set; }
    }
}
