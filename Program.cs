using Microsoft.EntityFrameworkCore;
using quizApi.Data;
using quizApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IQuizStore, DatabaseQuizStore>();

var dataDir = Path.Combine(builder.Environment.ContentRootPath, "Data");
Directory.CreateDirectory(dataDir);
var dbPath = Path.Combine(dataDir, "quiz.db");
var quizConn = $"Data Source={dbPath}";
builder.Services.AddDbContext<QuizDbContext>(options => options.UseSqlite(quizConn));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173", "http://127.0.0.1:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<QuizDbContext>();
    await DbInitializer.InitializeAsync(db);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Yerel ağda http://IP:5003 ile erişimde tarayıcıyı HTTPS’e zorlamaz (telefon/tablet için).
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

var adminIndexPath = Path.Combine(app.Environment.WebRootPath ?? Path.Combine(app.Environment.ContentRootPath, "wwwroot"), "admin", "index.html");
app.MapGet("/admin", () => Results.File(adminIndexPath, "text/html"));
app.MapGet("/admin/", () => Results.File(adminIndexPath, "text/html"));

app.MapFallbackToFile("index.html");

await app.RunAsync();
