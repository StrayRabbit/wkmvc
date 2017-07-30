namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_BUSSINESSCUSTOMER
    {
        public int ID { get; set; }

        [StringLength(36)]
        public string Fk_DepartId { get; set; }

        [StringLength(72)]
        public string FK_RELATIONID { get; set; }

        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(10)]
        public string CompanyProvince { get; set; }

        [Required]
        [StringLength(10)]
        public string CompanyCity { get; set; }

        [Required]
        [StringLength(10)]
        public string CompanyArea { get; set; }

        [StringLength(500)]
        public string CompanyAddress { get; set; }

        [StringLength(50)]
        public string CompanyTel { get; set; }

        [StringLength(100)]
        public string CompanyWebSite { get; set; }

        [StringLength(50)]
        public string ChargePersionName { get; set; }

        public int ChargePersionSex { get; set; }

        [StringLength(20)]
        public string ChargePersionQQ { get; set; }

        [StringLength(50)]
        public string ChargePersionEmail { get; set; }

        [StringLength(50)]
        public string ChargePersionPhone { get; set; }

        public bool IsValidate { get; set; }

        [Required]
        [StringLength(50)]
        public string CreateUser { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(50)]
        public string UpdateUser { get; set; }

        public DateTime UpdateDate { get; set; }

        public int CustomerStyle { get; set; }
    }
}
