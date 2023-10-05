using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Reflection;
using AutoMapper;
using API.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
builder.Services.AddSwaggerGen();
builder.Services.AddAplicacionServices();
builder.Services.AddDbContext<PDFGeneratorContext>(option => {
    string con = builder.Configuration.GetConnectionString("DefaultConecction");
    option.UseMySql(con, ServerVersion.AutoDetect(con));
});
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

app.Run();
