using WebApplication5.Models;

namespace WebApplication5.Repositories
{
    public interface IBookRepository
    {
        Task<bool> ApproveBookasync(int bookId, int staffId);
        Task<bool> RejectedBookasync(int bookId, int staffId);
        Task<Book> GetBookByIdAsync(int bookId);
        Task<Staff> GetStaffByIdAsync(int staffId);

    }
}
