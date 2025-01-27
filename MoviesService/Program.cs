using MassTransit;
using Microsoft.EntityFrameworkCore;
using TicketEase.Service.Movies.DbContexts;
using TicketEase.Service.Movies.Repositories;
using TicketEase.Service.Movies.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure RabbitMQ and MassTransit
builder.Services.AddMassTransit(config =>
{

    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"]);
    });
});


// Add services to the container.
builder.Services.AddMassTransitHostedService();
builder.Services.AddControllers();

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add DbContext to the container
builder.Services.AddDbContext<MovieDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
    dbContext.Database.Migrate();  // Applies migrations automatically
}

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
