namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class COM_UPLOAD
    {
        [StringLength(36)]
        public string ID { get; set; }

        [StringLength(72)]
        public string FK_USERID { get; set; }

        [StringLength(100)]
        public string UPOPEATOR { get; set; }

        public DateTime? UPTIME { get; set; }

        [StringLength(400)]
        public string UPOLDNAME { get; set; }

        [StringLength(400)]
        public string UPNEWNAME { get; set; }

        public decimal? UPFILESIZE { get; set; }

        [StringLength(20)]
        public string UPFILEUNIT { get; set; }

        [StringLength(2000)]
        public string UPFILEPATH { get; set; }

        [StringLength(40)]
        public string UPFILESUFFIX { get; set; }

        [StringLength(2000)]
        public string UPFILETHUMBNAIL { get; set; }

        [StringLength(2000)]
        public string UPFILETHUMBNAILFORPAD { get; set; }

        [StringLength(2000)]
        public string UPFILETHUMBNAILFORPHONE { get; set; }

        [StringLength(40)]
        public string UPFILEIP { get; set; }

        [StringLength(1000)]
        public string UPFILEURL { get; set; }
    }
}
