using Anantarupa.Database;
using Anantarupa.Dto;
using Anantarupa.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GameContext>();
builder.Services.AddScoped<UserService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/items", async (GameContext db) =>
    await db.Items.Select(item => ItemDto.FromEntity(item)).ToListAsync()
);

app.MapGet("api/items/{id}", async(int id, GameContext db) => {
    var itemEntity = await db.Items.FindAsync(id);

    if(itemEntity == null) return Results.NotFound($"Item with id: {id} does not exist");

    return Results.Ok(ItemDto.FromEntity(itemEntity));
});

app.MapGet("api/users", async (GameContext db) =>
    await db.UserData.Select(user => UserDto.FromEntity(user)).ToListAsync()
);

app.MapGet("api/users/{id}/currencies", async (int id, [FromServices] UserService userService) =>{
    try
    {
        var result = await userService.GetUserCurrencies(id);
        return Results.Ok(result);
    }
    catch(KeyNotFoundException ex)
    {
        return Results.NotFound(ex.Message);    
    }
});

app.MapGet("api/users/{id}/inventory", async (int id, [FromServices] UserService userService) => {
    try
    {
        var result = await userService.GetUserItems(id);
        return Results.Ok(result);
    }
    catch(KeyNotFoundException ex)
    {
        return Results.NotFound(ex.Message);    
    }
});

app.MapPost("api/users/{user_id}/purchase/{item_id}", async (int user_id, int item_id, [FromServices] UserService userService) => {
    try
    {
        await userService.PurchaseItem(user_id, item_id);

        return Results.Ok("Success");
    }
    catch(KeyNotFoundException ex)
    {
        return Results.NotFound($"Error: {ex.Message}");
    }
    catch(InvalidOperationException ex)
    {
        return Results.BadRequest($"Error: Cannot purchase the requested item. {ex.Message}");
    }
});

app.Run();
