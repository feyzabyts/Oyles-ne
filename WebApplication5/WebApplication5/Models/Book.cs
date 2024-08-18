namespace WebApplication5.Models
{
    public class Book
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsApproved { get; set; } //kitap onay işlemi için
       // public bool IsRejected { get; set; }  //kitap red işlemi için
        public bool IsCheckedOut { get; set; }// buradaki işlemde ise bir kitap çıktı mı çıkmadı mı ödünç alındı mı alınmadı mı anlamında



    }
}
