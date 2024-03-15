using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SelfieAwookie.API.UI.ExtensionsMethods;
using SelfieAwookie.API.UI.Middlewares;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;
using SelfieAWookies.Core.Selfies.Infrastructures.Loggers;
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

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
   // options.SignIn.RequireConfirmedEmail = true;
}).AddEntityFrameworkStores<SelfiesContext>();

builder.Services.AddCustomOptions(builder.Configuration);
builder.Services.AddInjections()
                .AddCustomSecurity(builder.Configuration);

builder.Logging.AddProvider(new SelfieAwookieLoggerProvider());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<RegisterRequestMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors(SecurityMethods.DEFAULT_POLICY_2);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
