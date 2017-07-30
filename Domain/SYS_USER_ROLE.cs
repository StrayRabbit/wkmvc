namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_USER_ROLE
    {
        public int ID { get; set; }

        public int FK_USERID { get; set; }

        public int FK_ROLEID { get; set; }

        public virtual SYS_ROLE SYS_ROLE { get; set; }

        public virtual SYS_USER SYS_USER { get; set; }
    }
}
