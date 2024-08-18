using System.Net;
using WebApplication5.Models;

namespace WebApplication5.Repositories
{
    public class BookRepository : IBookRepository
    {
        //NOT: BU KISIM DB BAĞLANTILI OLACAK DBCONTEX
        private readonly List<Book> _books = new List<Book>();
        private readonly List<Staff> _staff = new List<Staff>();

        //Bu kısım kitap talebinde bulunan bir üyenin görevli tarafından kabul edilmesi
        public async Task<bool> ApproveBookasync(int bookId, int staffId)
        {
            var book = await GetBookByIdAsync(bookId);
            var staff = await GetStaffByIdAsync(staffId);
            if (book == null || staff == null)
            {
                return false;//kitap mevcut değil
            }

            book.IsApproved = true;
            return true;
        }

        public async Task<bool> RejectedBookasync(int bookId, int staffId)
        {
            var book = await GetBookByIdAsync(bookId);
            var staff = await GetStaffByIdAsync(staffId);
            if (book == null || staff == null)
            {
                return false;//kitap mevcut değil
            }

            book.IsApproved = false;
            return true;
        }


        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return _books.FirstOrDefault(b => b.Id == bookId);
        }

        public async Task<Staff> GetStaffByIdAsync(int staffId)
        {
            return _staff.FirstOrDefault(s => s.Id == staffId);
        }

        
    }
}
