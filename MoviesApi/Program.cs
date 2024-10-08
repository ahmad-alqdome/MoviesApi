using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*************************************************************************************** Auto Mapper ***************************************************************************/

builder.Services.AddAutoMapper(typeof(Program)); //  will scan all project 

/************************************************************************************** Inject Service *************************************************************************/

builder.Services.AddTransient<IGenresService, GenresService>();
builder.Services.AddTransient<IMoviesService, MoviesService>();



/************************************************************************************** connection *****************************************************************************/
var connectionStr = builder.Configuration.GetConnectionString("DefualtConnection");

builder.Services.AddDbContext<ApplicationDbContext>(op=>

op.UseSqlServer(connectionStr)

);

builder.Services.AddCors();

/*****************************************************************************************************************************************************************************/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x=>x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
