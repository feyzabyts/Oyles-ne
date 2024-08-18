
using WebApplication5.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication5.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<bool> ApproveRegisterAsync(int userId, int staffId)
        {
            var userT = await _context.User.FindAsync(userId);
            var staffT = await _context.Staff.FindAsync(staffId);

            if (userT == null || staffT == null || staffT.IsRejectedS)
                return false;

            staffT.IsApprovedS = true;
            _context.Staff.Update(staffT);
            await _context.SaveChangesAsync();

            return true;
        }

      

        public async Task<MessageTransaction> SendMessageAsync(int senderId, int receiverId, string message,string content)
        {
            var sender = await _context.User.FindAsync(senderId);
            var receiver = await _context.Staff.FindAsync(receiverId);
            var receiverA = await _context.Admin.FindAsync(receiverId);

            if (sender == null || receiver == null|| receiverA==null)
            {
                return new MessageTransaction
                {
                    Success = false,
                    Message = "Sender or receiver not found."
                };
            }

            var contentU = new MessageTransaction
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                SentDate = DateTime.UtcNow
            };

            _context.Messages.Add(contentU);
            await _context.SaveChangesAsync();

            return new MessageTransaction
            {
                Success = true,
                Message = "Message sent successfully."
            };

        }



        public async Task<int> PunishAsync(int userId, DateTime fdate, DateTime lDate)
        {
            var userT = await _context.BookTransaction.AnyAsync(u => u.UserId == userId);
            if (!userT)
            {
                return 0; // Kullanıcı mevcut değilse ceza yok
            }
            var fdateT = await _context.BookTransaction
                .Where(fd => fd.UserId == userId && fd.TransactionDate.Date == fdate.Date)
                .Select(fd => fd.TransactionDate)
                .FirstOrDefaultAsync();

            var ldateT = await _context.BookTransaction
                .Where(ld => ld.UserId == userId && ld.ReturnTransactionDate.Date == lDate.Date)
                .Select(ld => ld.ReturnTransactionDate)
                .FirstOrDefaultAsync();

            if (fdateT == default || ldateT == default)
            {
                return 0; // Tarihlerle ilgili geçerli veriler bulunamadıysa ceza yok
            }

            TimeSpan ts = ldateT - fdateT;
            int kalanGun = ts.Days;
            int cezaP = 0;

            var clean = await _context.BookTransaction
                .Where(c => c.CleanBook)
                .FirstOrDefaultAsync();

            if (kalanGun > 20 && (clean == null || !clean.CleanBook))
            {
                // Ceza uygula
                cezaP += -(kalanGun / 3);
            }
            else
            {
                cezaP += kalanGun / 5;
            }

            return cezaP;
        }




        public async Task<int> PunishStaffAsync(int staffId, DateTime date, DateTime dateAns)
        {
            var staffT = await _context.Staff.AnyAsync(s => s.Id == staffId);
            if (!staffT)
            {
                return 0; // Personel mevcut değilse ceza yok
            }

            var sdateT = await _context.MessageTransaction
                .Where(d => d.ReceiverId == staffId && d.SentDate.Date == date.Date)
                .Select(d => d.Date)
                .FirstOrDefaultAsync();

            var aDateT = await _context.MessageTransaction
                .Where(ad => ad.ReceiverId == staffId && ad.DateAnswer.Date == dateAns.Date)
                .Select(ad => ad.DateAnswer)
                .FirstOrDefaultAsync();

            if (sdateT == default || aDateT == default)
            {
                return 0; // Tarihlerle ilgili geçerli veriler bulunamadıysa ceza yok
            }
            
            TimeSpan ts = aDateT - sdateT;
            int gecenGun = ts.Days;
            int sCeza = 0;

            if (gecenGun > 5)
            {
                sCeza += -5;
            }
            else if (gecenGun < 5 && gecenGun>0) // Eğer gecenGun sıfırdan büyükse
            {
                sCeza += 1 / gecenGun; // Gün sayısına bölünerek ceza hesaplanır
            }

            return sCeza;
        }





    }
}

        
   
