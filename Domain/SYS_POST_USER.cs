namespace Domain
{
    public partial class SYS_POST_USER
    {
        public int ID { get; set; }
        public int FK_USERID { get; set; }
        public string FK_POSTID { get; set; }

        public virtual SYS_USER SYS_USER { get; set; }
    }
}
