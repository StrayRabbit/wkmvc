namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRO_PROJECT_FILES
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string DocStyle { get; set; }

        public int Fk_ForeignId { get; set; }

        public int FK_UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string DocName { get; set; }

        [Required]
        [StringLength(50)]
        public string DocNewName { get; set; }

        [Required]
        [StringLength(500)]
        public string DocPath { get; set; }

        [Required]
        [StringLength(10)]
        public string DocFileExt { get; set; }

        [Required]
        [StringLength(50)]
        public string DocSize { get; set; }

        public DateTime UploadDate { get; set; }

        [Required]
        [StringLength(50)]
        public string CreateUser { get; set; }

        [Required]
        [StringLength(50)]
        public string CreateIP { get; set; }

        public int? AcceptanceStatus { get; set; }
    }
}
