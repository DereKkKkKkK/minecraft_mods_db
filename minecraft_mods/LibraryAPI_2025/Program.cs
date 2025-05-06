using BLL.Interfaces;
using BLL.Services;
using DAL.EF;
using DAL.Interfaces;
using DAL.Repositories;
using DTO.Book;
using DTO.Mod;
using DTO.Tag;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Подключение к БД
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));


// Репозитории
builder.Services.AddTransient<IRepository<BookDto, CreateBookDto, UpdateBookDto>, BookRepository>();
builder.Services.AddTransient<IRepository<ModDto, CreateModDto, UpdateModDto>, ModRepository>();
builder.Services.AddTransient<IRepository<TagDto, CreateTagDto, UpdateTagDto>, TagRepository>();


// Сервисы
builder.Services.AddScoped<IService<BookDto, CreateBookDto, UpdateBookDto>, BookService>();
builder.Services.AddScoped<IService<ModDto, CreateModDto, UpdateModDto>, ModService>();
builder.Services.AddScoped<IService<TagDto, CreateTagDto, UpdateTagDto>, TagService>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();