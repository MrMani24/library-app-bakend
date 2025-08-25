using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using library_app_bakend.DbContextes;
using library_app_bakend.DTOs.Books;
using library_app_bakend.DTOs.Common;
using library_app_bakend.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace library_app_bakend.Endpoints
{
    public static class BookEndpoints
    {
        public static void MapBookEndpoits(this WebApplication app)
        {
            app.MapGet("books/list", async ([FromServices] LibraryDB db) =>
            {
                var result = await db.Books.Select(b => new BookListDto
                {
                    Id = b.Guid,
                    Title = b.Title,
                    Writer = b.Writer,
                    Publisher = b.Publisher,
                    Price = b.Price
                }).ToListAsync();
            return result;
            });
            app.MapPost("books/create", async ([FromServices] LibraryDB db, [FromBody] BookAddDto bookDto) =>
            {
                var BOok = new Book
                {
                    Title = !string.IsNullOrEmpty(bookDto.Title) ? bookDto.Title : "بی عنوان",
                    Writer = bookDto.Writer,
                    Price = bookDto.Price,
                    Publisher = bookDto.Publisher,
                };
                await db.Books.AddAsync(BOok);
                await db.SaveChangesAsync();
                return new CommandResultDto
                {
                    Massage = "books Creatd!",
                    Successfull = true
                };
            });
            app.MapPut("books/update/{id}", async ([FromRoute] string id, [FromBody] BookUpdateDto bookUpdateDto, [FromServices] LibraryDB db) =>
            {
                var selBook = await db.Books.FirstOrDefaultAsync(m => m.Guid == id);
                if (selBook == null)
                {
                    return new CommandResultDto
                    {
                        Successfull = false,
                        Massage = "Book not fonud!"
                    };
                }
                selBook.Title = !string.IsNullOrEmpty(bookUpdateDto.Title) ? bookUpdateDto.Title : "ّبی عنوان";
                selBook.Price = bookUpdateDto.Price;
                selBook.Writer = bookUpdateDto.Writer;
                selBook.Publisher = bookUpdateDto.Publisher;
                await db.SaveChangesAsync();
                return new CommandResultDto
                {
                    Successfull = true,
                    Massage = "Books Updetad"
                };
            });
            app.MapDelete("books/remove/{guid}", async ([FromRoute] string guid, [FromServices] LibraryDB db) =>
            {
                var book = await db.Books.FirstOrDefaultAsync(m => m.Guid == guid);
                if (book == null)
                {
                    return new CommandResultDto
                    {
                        Successfull = false,
                        Massage = "Book not Fonud!"
                    };
                }
                db.Books.Remove(book);
                await db.SaveChangesAsync();
                return new CommandResultDto
                {
                    Successfull = true,
                    Massage = "Book Remove!"
                };
            });
        }
    }
}