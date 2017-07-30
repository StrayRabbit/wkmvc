namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRO_PROJECT_STAGES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRO_PROJECT_STAGES()
        {
            PRO_PROJECT_TEAMS = new HashSet<PRO_PROJECT_TEAMS>();
        }

        public int ID { get; set; }

        public int FK_ProjectId { get; set; }

        [Required]
        [StringLength(50)]
        public string StageTitle { get; set; }

        public int NeedNumber { get; set; }

        public int StageTimeLimit { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int StageStatus { get; set; }

        public bool IsOverTime { get; set; }

        public int OverDays { get; set; }

        public int OrderNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string CreateUser { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(50)]
        public string UpdateUser { get; set; }

        public DateTime UpdateDate { get; set; }

        public virtual PRO_PROJECTS PRO_PROJECTS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRO_PROJECT_TEAMS> PRO_PROJECT_TEAMS { get; set; }
    }
}
