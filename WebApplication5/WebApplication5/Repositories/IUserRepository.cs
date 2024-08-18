using WebApplication5.Models;

namespace WebApplication5.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ApproveRegisterAsync(int userId, int staffId);// oluşan kaydı görevli onaylayacak
        Task<MessageTransaction> SendMessageAsync(int senderId, int receiverId, string message,string content);//Mesaj gönderme işlemi
        Task<int> PunishAsync(int userId, DateTime fdate, DateTime lDate);//first date last date ceza için
        Task<int> PunishStaffAsync(int staffId, DateTime date, DateTime dateAns);//görevlinin ceza puanı
                                                

    }
}
