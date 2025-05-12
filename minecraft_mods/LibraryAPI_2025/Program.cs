using BLL.Interfaces;
using BLL.Services;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using DTO.Collection;
using DTO.Developer;
using DTO.Difficulty;
using DTO.Focus;
using DTO.Mod;
using DTO.ModLoader;
using DTO.Tag;
using DTO.ModVersion;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Подключение к БД
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));


// Репозитории
builder.Services.AddTransient<IRepository<ModDto, CreateModDto, UpdateModDto>, ModRepository>();
builder.Services.AddTransient<IRepository<TagDto, CreateTagDto, UpdateTagDto>, TagRepository>();
builder.Services.AddTransient<IRepository<ModVersionDto, CreateModVersionDto, UpdateModVersionDto>, VersionRepository>();
builder.Services.AddTransient<IRepository<ModLoaderDto, CreateModLoaderDto, UpdateModLoaderDto>, ModLoaderRepository>();
builder.Services.AddTransient<IRepository<DeveloperDto, CreateDeveloperDto, UpdateDeveloperDto>, DeveloperRepository>();
builder.Services.AddTransient<IRepository<CollectionDto, CreateCollectionDto, UpdateCollectionDto>, CollectionRepository>();
builder.Services.AddTransient<IRepository<DifficultyDto, CreateDifficultyDto, UpdateDifficultyDto>, DifficultyRepository>();
builder.Services.AddTransient<IRepository<FocusDto, CreateFocusDto, UpdateFocusDto>, FocusRepository>();


// Сервисы
builder.Services.AddScoped<IService<ModDto, CreateModDto, UpdateModDto>, ModService>();
builder.Services.AddScoped<IService<TagDto, CreateTagDto, UpdateTagDto>, TagService>();
builder.Services.AddScoped<IService<ModVersionDto, CreateModVersionDto, UpdateModVersionDto>, VersionService>();
builder.Services.AddScoped<IService<ModLoaderDto, CreateModLoaderDto, UpdateModLoaderDto>, ModLoaderService>();
builder.Services.AddScoped<IService<DeveloperDto, CreateDeveloperDto, UpdateDeveloperDto>, DeveloperService>();
builder.Services.AddScoped<IService<CollectionDto, CreateCollectionDto, UpdateCollectionDto>, CollectionService>();
builder.Services.AddScoped<IService<DifficultyDto, CreateDifficultyDto, UpdateDifficultyDto>, DifficultyService>();
builder.Services.AddScoped<IService<FocusDto, CreateFocusDto, UpdateFocusDto>, FocusService>();


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