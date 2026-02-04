using AulaTeste20.Contracts;
using AulaTeste20.Data;
using AulaTeste20.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Contacts API", Version = "v1" });
});

// builder.Services.AddSingleton<ContactRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts API v1");
});

//try
//{
//    await app.Services.GetRequiredService<ContactRepository>().InitializeAsync();
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"Warning: Could not initialize database: {ex.Message}");
//}
var connectionString = builder.Configuration.GetConnectionString("Default");
await using var connection = new NpgsqlConnection(connectionString);
await connection.OpenAsync();


await using var cmd = new NpgsqlCommand(@"
    CREATE TABLE IF NOT EXISTS ""Contacts"" (
        ""Id"" SERIAL PRIMARY KEY,
        ""Name"" TEXT NOT NULL,
        ""Email"" TEXT NOT NULL UNIQUE,
        ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW()
    )
", connection);
await cmd.ExecuteNonQueryAsync();

// Health Check
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

// CREATE
// app.MapPost("/contacts", async (ContactCreateRequest req, ContactRepository repo) =>
app.MapPost("/contacts", async (ContactCreateRequest req, AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(req.Name))
        return Results.BadRequest(new { error = "Name is required." });

    if (string.IsNullOrWhiteSpace(req.Email))
        return Results.BadRequest(new { error = "Email is required." });

    /* try
        {
            var id = await repo.CreateAsync(req.Name.Trim(), req.Email.Trim());
            return Results.Created($"/contacts/{id}", new { id });
        }
        catch (NpgsqlException ex) when (ex.SqlState == "23505")
        {
            // 23505 = unique constraint violation */
    // Check if email already exists
    if (await db.Contacts.AnyAsync(c => c.Email == req.Email.Trim()))
        return Results.Conflict(new { error = "Email already exists." });

    var contact = new Contact
    {
        Name = req.Name.Trim(),
        Email = req.Email.Trim(),
        CreatedAt = DateTime.UtcNow
    };

    db.Contacts.Add(contact);
    await db.SaveChangesAsync();

    return Results.Created($"/contacts/{contact.Id}", new { id = contact.Id });
});

// READ ALL
// app.MapGet("/contacts", async (ContactRepository repo) =>
app.MapGet("/contacts", async (AppDbContext db) =>
{
    //     var items = await repo.GetAllAsync();
    var items = await db.Contacts
        .OrderByDescending(c => c.Id)
        .ToListAsync();
    return Results.Ok(items);
});

// READ BY ID
// app.MapGet("/contacts/{id:int}", async (int id, ContactRepository repo) =>
app.MapGet("/contacts/{id:int}", async (int id, AppDbContext db) =>
{
    //     var item = await repo.GetByIdAsync(id);
    var item = await db.Contacts.FindAsync(id);
    return item is null ? Results.NotFound() : Results.Ok(item);
});

// UPDATE
// app.MapPut("/contacts/{id:int}", async (int id, ContactUpdateRequest req, ContactRepository repo) =>
app.MapPut("/contacts/{id:int}", async (int id, ContactUpdateRequest req, AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(req.Name))
        return Results.BadRequest(new { error = "Name is required." });

    if (string.IsNullOrWhiteSpace(req.Email))
        return Results.BadRequest(new { error = "Email is required." });

    /* 
    try
    {
        var updated = await repo.UpdateAsync(id, req.Name.Trim(), req.Email.Trim());
        return updated ? Results.NoContent() : Results.NotFound();
    }
    catch (NpgsqlException ex) when (ex.SqlState == "23505")
    {
    
     */
    var contact = await db.Contacts.FindAsync(id);
    if (contact is null)
        return Results.NotFound();

    // Check if email is already used by another contact
    if (await db.Contacts.AnyAsync(c => c.Email == req.Email.Trim() && c.Id != id))
        return Results.Conflict(new { error = "Email already exists." });

    contact.Name = req.Name.Trim();
    contact.Email = req.Email.Trim();

    await db.SaveChangesAsync();

    return Results.NoContent();
});

// DELETE
// app.MapDelete("/contacts/{id:int}", async (int id, ContactRepository repo) =>
app.MapDelete("/contacts/{id:int}", async (int id, AppDbContext db) =>
{
    /* 
        var deleted = await repo.DeleteAsync(id);
    return deleted ? Results.NoContent() : Results.NotFound();
    try
    {
        var updated = await repo.UpdateAsync(id, req.Name.Trim(), req.Email.Trim());
        return updated ? Results.NoContent() : Results.NotFound();
    }
    catch (NpgsqlException ex) when (ex.SqlState == "23505")
    { */
    var contact = await db.Contacts.FindAsync(id);
    if (contact is null)
        return Results.NotFound();

    db.Contacts.Remove(contact);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();

