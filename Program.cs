using library_app_bakend.DbContextes;
using library_app_bakend.Entites;
using Microsoft.Extensions.DependencyModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("books/list", () =>
{
    using var db = new LibraryDB();
    return db.Books.ToList();
});
app.MapPost("books/create", (Book book) =>
{
    using var db = new LibraryDB();
    db.Books.Add(book);
    db.SaveChanges();
    return "books Creatd!";
});
app.MapPut("books/update/{id}", (int id, Book book) =>
{
    using var db = new LibraryDB();
    var b = db.Books.Find(id);
    if (b == null)
    {
        return "Book not fonud!";
    }
    b.Title = book.Title;
    b.Price = book.Price;
    b.Writer = book.Writer;
    b.Writer = book.Writer;
    db.SaveChanges();
    return "books Updetad";
});
app.MapDelete("books/remove/{id}", (int id) =>
{
    using var db = new LibraryDB();
    var book = db.Books.Find(id);
    if (book == null)
    {
        return "not found";
    }
    db.Books.Remove(book);
    db.SaveChanges();
    return "books Remove";
});

app.MapGet("member/list", () =>
{
    using var db = new LibraryDB();
    return db.Members.ToList();
});
app.MapPost("member/create", (Member member) =>
{
    using var db = new LibraryDB();
    db.Members.Add(member);
    db.SaveChanges();
    return "Member Created";
});
app.MapPut("member/update/{id}", (int id, Member member) =>
{
    using var db = new LibraryDB();
    var s = db.Members.Find(id);
    if (s == null)
    {
        return "Member not fonud!";
    }
    s.Firstname = member.Firstname;
    s.Lastname = member.Lastname;
    s.Gender = member.Gender;
    db.SaveChanges();
    return "Mamber Update";
});
app.MapDelete("Member/remove/{id}", (int id) =>
{
    using var db = new LibraryDB();
    var member = db.Members.Find(id);
    if (member == null)
    {
        return "not found";
    }
    db.Members.Remove(member);
    db.SaveChanges();
    return "member Remove";
});

app.Run();

