using Anantarupa.Database;
using Anantarupa.Dto;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var db = new GameContext();

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/items", async () =>
    await db.Items.Select(item => ItemDto.FromEntity(item)).ToListAsync()
);

app.MapGet("api/items/{id}", async(long id) => {
    var itemEntity = await db.Items.FindAsync(id);

    if(itemEntity == null) return Results.NotFound(@"Item with id: {id} is not found");

    return Results.Ok(ItemDto.FromEntity(itemEntity));
});

app.Run();
