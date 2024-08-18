namespace WebApplication5.Models
{
    public class BookTransaction
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; } //bu kısım registrator veya servis işleminde async şekilde kullanılacaktır
        public DateTime TransactionDate { get; set; }//bunun verildiği tarih
        public DateTime ReturnTransactionDate { get; set; }//geri veriliş tarihi
        public bool IsReturned { get; set; }//verildi mi
        public bool CleanBook { get; set; }//kitap temiz mi
    }

}

