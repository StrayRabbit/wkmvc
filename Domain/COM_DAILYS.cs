namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class COM_DAILYS
    {
        public int ID { get; set; }

        public int FK_USERID { get; set; }

        [StringLength(72)]
        public string FK_RELATIONID { get; set; }

        public DateTime AddDate { get; set; }

        public DateTime LastEditDate { get; set; }

        [Required]
        [StringLength(50)]
        public string DailySubIP { get; set; }
    }
}
