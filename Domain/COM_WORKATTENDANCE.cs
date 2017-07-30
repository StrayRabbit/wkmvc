namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class COM_WORKATTENDANCE
    {
        public int ID { get; set; }

        public int FK_UserId { get; set; }

        public bool Is_SiginAM { get; set; }

        public bool Is_SigOutAM { get; set; }

        public DateTime SiginAMDate { get; set; }

        public DateTime SigOutAMDate { get; set; }

        public bool Is_SiginPM { get; set; }

        public bool Is_SigOutPM { get; set; }

        public DateTime SiginPM { get; set; }

        public DateTime SigOutPM { get; set; }

        public bool IsLateAM { get; set; }

        public int LateAMMinutes { get; set; }

        public bool IsEarlyOutAM { get; set; }

        public int EarlyOutAMMinutes { get; set; }

        public bool IsLatePM { get; set; }

        public int LatePMMinutes { get; set; }

        public bool IsEarlyOutPM { get; set; }

        public int EarlyOutPMMinutes { get; set; }

        public double WorkHours { get; set; }

        [Required]
        [StringLength(50)]
        public string SigIP { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
