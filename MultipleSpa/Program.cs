var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddReverseProxy()
        .AddSpas([new("AngularApp", "https://localhost:4200"), new("VueApp", "https://localhost:5173")]);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("VueApp/{**path:nonfile}", "VueApp/index.html");
app.MapFallbackToFile("AngularApp/{**path:nonfile}", "AngularApp/index.html");

if (builder.Environment.IsDevelopment())
{
    app.MapReverseProxy();
}

app.Run();
