using library_app_bakend.DbContextes;
using library_app_bakend.DTOs.Books;
using library_app_bakend.DTOs.Common;
using library_app_bakend.DTOs.Members;
using library_app_bakend.Entites;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors();
builder.Services.AddDbContext<LibraryDB>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors(policy =>
{
    policy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
});
app.UseHttpsRedirection();

app.MapGet("books/list", ([FromServices] LibraryDB db) =>
{
    return db.Books.Select(b => new BookListDto
    {
        Id = b.Guid,
        Title = b.Title,
        Writer = b.Writer,
        Publisher = b.Publisher,
        Price = b.Price
    }
    ).ToList();
});
app.MapPost("books/create", ([FromServices] LibraryDB db, [FromBody] BookAddDto bookDto) =>
{
    var BOok = new Book
    {
        Title = !string.IsNullOrEmpty(bookDto.Title) ? bookDto.Title : "بی عنوان",
        Writer = bookDto.Writer,
        Price = bookDto.Price,
        Publisher = bookDto.Publisher,
    };
    db.Books.Add(BOok);
    db.SaveChanges();
    return new CommandResultDto
    {
        Massage = "books Creatd!",
        Successfull = true
    };
});
app.MapPut("books/update/{id}", ([FromRoute] string id, [FromBody] BookUpdateDto bookUpdateDto, [FromServices] LibraryDB db) =>
{
    var selBook = db.Books.FirstOrDefault(m => m.Guid == id);
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
    db.SaveChanges();
    return new CommandResultDto
    {
        Successfull = true,
        Massage = "Books Updetad"
    };
});
app.MapDelete("books/remove/{guid}", ([FromRoute] string guid, [FromServices] LibraryDB db) =>
{
    var book = db.Books.FirstOrDefault(m => m.Guid == guid);
    if (book == null)
    {
        return new CommandResultDto
        {
            Successfull = false,
            Massage = "Book not Fonud!"
        };
    }
    db.Books.Remove(book);
    db.SaveChanges();
    return new CommandResultDto
    {
        Successfull = true,
        Massage = "Book Remove!"
    };
});

app.MapGet("members/list", ([FromServices] LibraryDB db) =>
{

    return db.Members.Select(m => new MemberListDto
    {
        Id = m.Guid,
        Firstname = m.Firstname,
        Lastname = m.Lastname,
        Wealth = m.Wealth
    }).ToList();

});
app.MapPost("member/create", ([FromServices] LibraryDB db, [FromBody] MemberAddDto memberAddDto) =>
{
    var MEmber = new Member
    {
        Gender = memberAddDto.Gender,
        Firstname = memberAddDto.Firstname,
        Lastname = memberAddDto.Lastname,
        Wealth = memberAddDto.Wealth,
        Username = memberAddDto.Username,
        Password = memberAddDto.Password
    };
    db.Members.Add(MEmber);
    db.SaveChanges();
    return new CommandResultDto
    {
        Successfull = true,
        Massage = "Member Added!"
    };
});
app.MapPut("member/update/{guid}", (string guid, MemberUpdateDto memberUpdateDto) =>
{
    using var db = new LibraryDB();
    var s = db.Members.FirstOrDefault(m => m.Guid == guid);
    if (s == null)
    {
        return new CommandResultDto
        {
            Successfull = false,
            Massage = "Member Not Fonud!"
        };
    }
    s.Firstname = memberUpdateDto.Firstname;
    s.Lastname = memberUpdateDto.Lastname;
    s.Wealth = memberUpdateDto.Wealth;
    db.SaveChanges();
    return new CommandResultDto
    {
        Successfull = true,
        Massage = "Mamber Update!"
    };
});
app.MapDelete("member/remove/{guid}", (string guid) =>
{
    using var db = new LibraryDB();
    var member = db.Members.FirstOrDefault(m => m.Guid == guid);
    if (member == null)
    {
        return new CommandResultDto
        {
            Successfull = false,
            Massage = "Not Found!"
        };
    }
    db.Members.Remove(member);
    db.SaveChanges();
    return new CommandResultDto
    {
        Successfull = true,
        Massage = "Member Removed."
    };
});

app.Run();