using library_app_bakend.DbContextes;
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
    return db.Books.ToList();
});
app.MapPost("books/create", ([FromServices] LibraryDB db ,[FromBody] Book book) =>
{
    db.Books.Add(book);
    db.SaveChanges();
    return new {Massage = "books Creatd!" };
});
app.MapPut("books/update/{id}", ([FromRoute] int id, [FromBody] Book book ,[FromServices]LibraryDB db) =>
{
    var b = db.Books.Find(id);
    if (b == null)
    {
        return new { Massage = "Book not fonud!" };
    }
    b.Title = book.Title;
    b.Price = book.Price;
    b.Writer = book.Writer;
    b.Writer = book.Writer;
    db.SaveChanges();
    return new { Massage = "books Updetad" };
});
app.MapDelete("books/remove/{id}", ([FromRoute] int id , [FromServices] LibraryDB db) =>
{
    var book = db.Books.Find(id);
    if (book == null)
    {
        return new { IsOk=false , Result = "Notfound!" };
    }
    db.Books.Remove(book);
    db.SaveChanges();
    return new { IsOk=true , Result = "books Remove" };
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