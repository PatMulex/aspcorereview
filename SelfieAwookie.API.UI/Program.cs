using Microsoft.EntityFrameworkCore;
using SelfieAwookie.API.UI.ExtensionsMethods;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;
using SelfieAWookies.Core.Selfies.Infrastructures.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SelfiesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SelfiesDatabase"), sqlOptions => { });
});

builder.Services.AddInjections();
builder.Services.AddCustomSecurity(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(SecurityMethods.DEFAULT_POLICY_2);

app.UseAuthorization();

app.MapControllers();

app.Run();
