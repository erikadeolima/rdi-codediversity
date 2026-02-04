using AulaTeste20.Contracts;
using AulaTeste20.Data;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Contacts API", Version = "v1" });
});

builder.Services.AddSingleton<ContactRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts API v1");
});

// Initialize database (with error handling so app starts even without DB)
try
{
    await app.Services.GetRequiredService<ContactRepository>().InitializeAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Warning: Could not initialize database: {ex.Message}");
}

// Health Check
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

// CREATE
app.MapPost("/contacts", async (ContactCreateRequest req, ContactRepository repo) =>
{
    if (string.IsNullOrWhiteSpace(req.Name))
        return Results.BadRequest(new { error = "Name is required." });

    if (string.IsNullOrWhiteSpace(req.Email))
        return Results.BadRequest(new { error = "Email is required." });

    try
    {
        var id = await repo.CreateAsync(req.Name.Trim(), req.Email.Trim());
        return Results.Created($"/contacts/{id}", new { id });
    }
    catch (NpgsqlException ex) when (ex.SqlState == "23505")
    {
        // 23505 = unique constraint violation
        return Results.Conflict(new { error = "Email already exists." });
    }
});

// READ ALL
app.MapGet("/contacts", async (ContactRepository repo) =>
{
    var items = await repo.GetAllAsync();
    return Results.Ok(items);
});

// READ BY ID
app.MapGet("/contacts/{id:int}", async (int id, ContactRepository repo) =>
{
    var item = await repo.GetByIdAsync(id);
    return item is null ? Results.NotFound() : Results.Ok(item);
});

// UPDATE
app.MapPut("/contacts/{id:int}", async (int id, ContactUpdateRequest req, ContactRepository repo) =>
{
    if (string.IsNullOrWhiteSpace(req.Name))
        return Results.BadRequest(new { error = "Name is required." });

    if (string.IsNullOrWhiteSpace(req.Email))
        return Results.BadRequest(new { error = "Email is required." });

    try
    {
        var updated = await repo.UpdateAsync(id, req.Name.Trim(), req.Email.Trim());
        return updated ? Results.NoContent() : Results.NotFound();
    }
    catch (NpgsqlException ex) when (ex.SqlState == "23505")
    {
        return Results.Conflict(new { error = "Email already exists." });
    }
});

// DELETE
app.MapDelete("/contacts/{id:int}", async (int id, ContactRepository repo) =>
{
    var deleted = await repo.DeleteAsync(id);
    return deleted ? Results.NoContent() : Results.NotFound();
});

app.Run();