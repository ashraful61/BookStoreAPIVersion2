﻿using BookSrote.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookSrote.API.Repository
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookByIdAsync(int bookId);
        Task<int> AddBookAsync(BookModel bookModel);
        Task UpdateBookByIdAsync(int bookId, BookModel bookModel);
        Task<IActionResult> UpdateBookPatch([FromRoute] int id, [FromBody] JsonPatchDocument bookModel);
    }
}
