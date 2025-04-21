using Core.Mapper;
using Core.Services.Interface;
using Core.Services;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories.Interface;
using Infrastructure.Repositories;
using Infrastructure.Data;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Core services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPaginationService, PaginationService>();

//Infrastructure services
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


builder.Services.AddDbContext<StoreContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.MapControllers();

app.Run();
