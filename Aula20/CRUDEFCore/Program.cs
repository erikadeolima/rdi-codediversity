using CRUDEFCore.Contracts;
using CRUDEFCore.Data;
using CRUDEFCore.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("Default");
    options.UseSqlite(cs);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CREATE
app.MapPost("/contacts", async (ContactCreateRequest req, AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(req.Name))
        return Results.BadRequest(new { error = "Name is required." });

    if (string.IsNullOrWhiteSpace(req.Email))
        return Results.BadRequest(new { error = "Email is required." });

    var email = req.Email.Trim();
    var name = req.Name.Trim();

    var exists = await db.Contacts.AnyAsync(x => x.Email == email);
    if (exists)
        return Results.Conflict(new { error = "Email already exists." });

    var contact = new Contact
    {
        Name = name,
        Email = email,
        CreatedAt = DateTime.UtcNow
    };

    db.Contacts.Add(contact);
    await db.SaveChangesAsync();

    return Results.Created($"/contacts/{contact.Id}", new { contact.Id });
});

// READ ALL
app.MapGet("/contacts", async (AppDbContext db) =>
{
    var items = await db.Contacts
        .OrderByDescending(x => x.Id)
        .ToListAsync();

    return Results.Ok(items);
});

// READ BY ID
app.MapGet("/contacts/{id:int}", async (int id, AppDbContext db) =>
{
    var item = await db.Contacts.FindAsync(id);
    return item is null ? Results.NotFound() : Results.Ok(item);
});

// UPDATE
app.MapPut("/contacts/{id:int}", async (int id, ContactUpdateRequest req, AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(req.Name))
        return Results.BadRequest(new { error = "Name is required." });

    if (string.IsNullOrWhiteSpace(req.Email))
        return Results.BadRequest(new { error = "Email is required." });

    var contact = await db.Contacts.FindAsync(id);
    if (contact is null)
        return Results.NotFound();

    var newEmail = req.Email.Trim();
    var newName = req.Name.Trim();

    var emailInUseByAnother = await db.Contacts
        .AnyAsync(x => x.Email == newEmail && x.Id != id);

    if (emailInUseByAnother)
        return Results.Conflict(new { error = "Email already exists." });

    contact.Name = newName;
    contact.Email = newEmail;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

// DELETE
app.MapDelete("/contacts/{id:int}", async (int id, AppDbContext db) =>
{
    var contact = await db.Contacts.FindAsync(id);
    if (contact is null)
        return Results.NotFound();

    db.Contacts.Remove(contact);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();
