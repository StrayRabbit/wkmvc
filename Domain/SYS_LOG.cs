namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_LOG
    {
        public int ID { get; set; }

        public DateTime? DATES { get; set; }

        [StringLength(20)]
        public string LEVELS { get; set; }

        [StringLength(200)]
        public string LOGGER { get; set; }

        [StringLength(100)]
        public string CLIENTUSER { get; set; }

        [StringLength(20)]
        public string CLIENTIP { get; set; }

        [StringLength(500)]
        public string REQUESTURL { get; set; }

        [StringLength(20)]
        public string ACTION { get; set; }

        [StringLength(4000)]
        public string MESSAGE { get; set; }

        [StringLength(4000)]
        public string EXCEPTION { get; set; }
    }
}
