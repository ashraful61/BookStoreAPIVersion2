﻿using AutoMapper;
using BookSrote.API.Data;
using BookSrote.API.Models;
using BookStore.API.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace BookSrote.API.Repository
{
    public class BookRepository: IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;
        public BookRepository(BookStoreContext context, IMapper mapper)
        {
            _context= context;
            _mapper = mapper;
        }

        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            //var records = await _context.Books.Select(x => new BookModel() 
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Description = x.Description,
            //}).ToListAsync();

            //return records;

            //using auto mapper
            var books = await _context.Books.ToListAsync();
            return _mapper.Map<List<BookModel>>(books);
        }

        public async Task<BookModel> GetBookByIdAsync(int bookId)
        {
            //var record = await _context.Books.Where(x =>x.Id == bookId).Select(x => new BookModel()
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Description = x.Description,
            //}).FirstOrDefaultAsync();

            //return record;
            var book = await _context.Books.FindAsync(bookId);
            return _mapper.Map<BookModel>(book);
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
            //var record = await _context.Books.FindAsync(bookId);
            //if (record != null)
            //{
            //    record.Id = bookId;
            //    record.Title = bookModel.Title;
            //    record.Description = bookModel.Description;
            //    await _context.SaveChangesAsync();
            //}

            var book = new Books()
            {
                Id = bookId,
                Title = bookModel.Title,
                Description = bookModel.Description,
            };

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            //return record;
        }

        public async Task UpdateBookPatchAsync(int bookId, JsonPatchDocument bookModel)
        {
            var book = await _context.Books.FindAsync(bookId);
            if(book != null)
            {
                bookModel.ApplyTo(book);
                await _context.SaveChangesAsync();
                    
            }

        }

        public async Task DeleteBookByIdAsync(int bookId)
        {
            //var book = await _context.Books.FindAsync(bookId);
            //var book = _context.Books.Where(x => x.Id == bookId).FirstOrDefault();
            var book = new Books()
            {
                Id = bookId
            };
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }


    }
}
