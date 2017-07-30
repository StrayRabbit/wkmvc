namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_CODE
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string CODETYPE { get; set; }

        [StringLength(200)]
        public string NAMETEXT { get; set; }

        [StringLength(100)]
        public string CODEVALUE { get; set; }

        public int? SHOWORDER { get; set; }

        public bool ISCODE { get; set; }

        [StringLength(2000)]
        public string REMARK { get; set; }

        public DateTime? CREATEDATE { get; set; }

        [StringLength(36)]
        public string CREATEUSER { get; set; }

        public DateTime? UPDATEDATE { get; set; }

        [StringLength(36)]
        public string UPDATEUSER { get; set; }

        public int? PARENTID { get; set; }
    }
}
