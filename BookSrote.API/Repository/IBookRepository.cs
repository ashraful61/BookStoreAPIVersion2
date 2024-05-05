using BookSrote.API.Models;

namespace BookSrote.API.Repository
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookByIdAsync(int bookId);
        Task<int> AddBookAsync(BookModel bookModel);
        Task UpdateBookByIdAsync(int bookId, BookModel bookModel);
    }
}
