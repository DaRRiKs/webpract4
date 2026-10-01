using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webpract4.Common;
using webpract4.Data;
using webpract4.Mapping;
using webpract4.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = _ =>
        new BadRequestObjectResult(new ReturnResult<object>
        {
            IsSuccess = false,
            ErrorMessage = "Invalid team data."
        });
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddAutoMapper(_ => { }, typeof(AutoMapperProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
