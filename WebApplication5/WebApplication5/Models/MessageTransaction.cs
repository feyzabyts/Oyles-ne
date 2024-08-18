namespace WebApplication5.Models
{
    public class MessageTransaction
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; } //gönderenin ne yazdığı
        public string Message { get; set; } //mesajın bir içeriği olacak
        public DateTime SentDate { get; set; }
        public DateTime DateAnswer { get; set; }
        public bool Success { get; set; }

        public User Sender { get; set; }
        public Staff Receiver { get; set; }
        public Admin ReceiverA { get; set; }


    }
}
