namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRO_PROJECT_MESSAGE
    {
        public int ID { get; set; }

        public int FK_ProjectId { get; set; }

        [Required]
        [StringLength(500)]
        public string MessageContent { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Column(TypeName = "ntext")]
        public string UserFace { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual PRO_PROJECTS PRO_PROJECTS { get; set; }
    }
}
