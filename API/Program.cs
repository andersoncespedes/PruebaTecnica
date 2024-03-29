using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using API.Extensions;
using System.Reflection;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
builder.Services.BindServices();
builder.Services.ConfigureJson();
builder.Services.AddDbContext<APIContext>(options => {
    string ConecctionString = builder.Configuration.GetConnectionString("SqlServerConn");
    options.UseSqlServer(ConecctionString);
});
builder.Services.ConfigureRatelimiting();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseIpRateLimiting();

app.UseAuthorization();

app.MapControllers();

app.Run();
