using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MinilaCore.Services.Imprementations;
using MinilaCore.Services.Interfaces;
using MinilaDataAcess.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<MinilaDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MinilaDatabeConnection"));
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<MinilaDBContext>();

builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
