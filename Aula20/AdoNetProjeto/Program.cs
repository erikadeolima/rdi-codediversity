using AdoNetProjeto.Contracts;
using AdoNetProjeto.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ContactRepository>();

var app = builder.Build();

await app.Services.GetRequiredService<ContactRepository>().InitializeAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
    catch (Microsoft.Data.Sqlite.SqliteException ex) when (ex.SqliteErrorCode == 19)
    {
        // 19 = constraint violation (ex: unique email)
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
    catch (Microsoft.Data.Sqlite.SqliteException ex) when (ex.SqliteErrorCode == 19)
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
