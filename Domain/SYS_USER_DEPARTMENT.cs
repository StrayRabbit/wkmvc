namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_USER_DEPARTMENT
    {
        public int ID { get; set; }

        public int USER_ID { get; set; }

        [StringLength(50)]
        public string DEPARTMENT_ID { get; set; }

        public virtual SYS_DEPARTMENT SYS_DEPARTMENT { get; set; }
        public virtual SYS_USER SYS_USER { get; set; }
    }
}
