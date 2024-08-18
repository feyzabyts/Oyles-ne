namespace WebApplication5.Models
{
    public class Staff:User
    {
        public bool IsApprovedS { get; set; } //üye onay için
        public bool IsRejectedS { get; set; }  //üye red için
    }
}
