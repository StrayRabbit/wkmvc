namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_USERINFO
    {
        public int ID { get; set; }

        public int USERID { get; set; }

        public int? POSTCODE { get; set; }

        [StringLength(200)]
        public string PHONE { get; set; }

        [StringLength(200)]
        public string OFFICEPHONE { get; set; }

        [StringLength(200)]
        public string EMAILADDRESS { get; set; }

        [StringLength(200)]
        public string SECONDPHONE { get; set; }

        public int? WORKCODE { get; set; }

        public int? SEXCODE { get; set; }

        public DateTime? BIRTHDAY { get; set; }

        public int? NATIONCODE { get; set; }

        [StringLength(18)]
        public string IDNUMBER { get; set; }

        public int? MARRYCODE { get; set; }

        public int? IDENTITYCODE { get; set; }

        [StringLength(200)]
        public string HomeTown { get; set; }

        [StringLength(200)]
        public string ACCOUNTLOCATION { get; set; }

        public int? XUELI { get; set; }

        public int? ZHICHENG { get; set; }

        [StringLength(200)]
        public string GRADUATIONSCHOOL { get; set; }

        [StringLength(200)]
        public string SPECIALTY { get; set; }

        [StringLength(200)]
        public string PHOTOOLDNAME { get; set; }

        [StringLength(200)]
        public string PHOTONEWNAME { get; set; }

        [StringLength(200)]
        public string PHOTOTYPE { get; set; }

        [StringLength(200)]
        public string RESUMEOLDNAME { get; set; }

        [StringLength(200)]
        public string RESUMENEWNAME { get; set; }

        [StringLength(200)]
        public string RESUMETYPE { get; set; }

        [StringLength(200)]
        public string HuJiSuoZaiDi { get; set; }

        [StringLength(200)]
        public string HUJIPAICHUSUO { get; set; }

        public DateTime? WORKDATE { get; set; }

        public DateTime? JINRUDATE { get; set; }

        [StringLength(200)]
        public string CARNUMBER { get; set; }

        [StringLength(15)]
        public string QQ { get; set; }

        [StringLength(200)]
        public string WEBCHATOPENID { get; set; }

        public DateTime? CREATEDATE { get; set; }

        [StringLength(36)]
        public string CREATEUSER { get; set; }

        public DateTime? UPDATEDATE { get; set; }

        [StringLength(36)]
        public string UPDATEUSER { get; set; }

        public virtual SYS_USER SYS_USER { get; set; }
    }
}
