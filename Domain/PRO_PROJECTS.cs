namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRO_PROJECTS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRO_PROJECTS()
        {
            PRO_PROJECT_MESSAGE = new HashSet<PRO_PROJECT_MESSAGE>();
            PRO_PROJECT_STAGES = new HashSet<PRO_PROJECT_STAGES>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProjectTitle { get; set; }

        [StringLength(72)]
        public string FK_RELATIONID { get; set; }

        [StringLength(36)]
        public string Fk_DepartId { get; set; }

        public int Fk_UserId { get; set; }

        public int ProjectStatus { get; set; }

        public int Fk_BussinessCustomer { get; set; }

        public int ProjectLevels { get; set; }

        public decimal ProjectMoney { get; set; }

        public int ProjectTimeLimit { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ContractCode { get; set; }

        [Required]
        [StringLength(500)]
        public string ContractFile { get; set; }

        [Required]
        [StringLength(50)]
        public string SignPersion { get; set; }

        public DateTime SignDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRO_PROJECT_MESSAGE> PRO_PROJECT_MESSAGE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRO_PROJECT_STAGES> PRO_PROJECT_STAGES { get; set; }
    }
}
