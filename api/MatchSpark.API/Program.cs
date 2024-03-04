using MatchSpark.API.Extensions;
using MatchSpark.Data.Services;
using MatchSpark.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<UserServices>();

var app = builder.Build();

app.UseErrorHandlingMiddleware();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public partial class Program { }
