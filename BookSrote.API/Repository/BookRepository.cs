﻿using BookSrote.API.Data;
using BookSrote.API.Models;
using BookStore.API.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace BookSrote.API.Repository
{
    public class BookRepository: IBookRepository
    {
        private readonly BookStoreContext _context;
        public BookRepository(BookStoreContext context)
        {
            _context= context;
        }

        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            var records = await _context.Books.Select(x => new BookModel() 
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
            }).ToListAsync();

            return records;
        }

        public async Task<BookModel> GetBookByIdAsync(int bookId)
        {
            var record = await _context.Books.Where(x =>x.Id == bookId).Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
            }).FirstOrDefaultAsync();

            return record;
        }

        public async Task<int> AddBookAsync(BookModel bookModel)
        {
            var book = new Books()
            {
                Title = bookModel.Title,
                Description = bookModel.Description,
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book.Id;
        }

        public async Task UpdateBookByIdAsync(int bookId, BookModel bookModel)
        {
            var record = await _context.Books.FindAsync(bookId);
            if (record != null)
            {
                record.Id = bookId;
                record.Title = bookModel.Title;
                record.Description = bookModel.Description;
                await _context.SaveChangesAsync();
            }

            //return record;
        }

        public async Task UpdateBookPatch(int bookId, JsonPatchDocument bookModel)
        {
       

            //return record;
        }

    }
}
