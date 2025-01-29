using MassTransit;
using Microsoft.EntityFrameworkCore;
using TicketEase.Common.Events;
using TicketEase.Service.Functions.Consumers;
using TicketEase.Service.Functions.DbContexts;
using TicketEase.Service.Functions.Repositories;
using TicketEase.Service.Functions.Services;

var builder = WebApplication.CreateBuilder(args);

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Configure RabbitMQ and MassTransit
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<TicketsPurchasedEventConsumer>();
    config.AddConsumer<TicketsCancelledEventConsumer>();
    config.AddConsumer<CheckSeatsAvailabilityConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"]);
        // Configure receive endpoints with specific queues for each consumer
        cfg.ReceiveEndpoint(builder.Configuration["RabbitMq:Queues:ticket-purchased-functions-queue"], e =>
        {
            e.ConfigureConsumer<TicketsPurchasedEventConsumer>(context);
        });

        cfg.ReceiveEndpoint(builder.Configuration["RabbitMq:Queues:ticket-cancelled-queue"], e =>
        {
            e.ConfigureConsumer<TicketsCancelledEventConsumer>(context);
        });

        cfg.ReceiveEndpoint(builder.Configuration["RabbitMq:Queues:check-seats-availability-queue"], e =>
        {
            e.ConfigureConsumer<CheckSeatsAvailabilityConsumer>(context);
        });
    });
});

var RabbitMqHost = builder.Configuration["RabbitMq:Host"];

// Add services to the container.
builder.Services.AddMassTransitHostedService();
builder.Services.AddControllers();

builder.Services.AddScoped<IFunctionRepository, FunctionRepository>();
builder.Services.AddScoped<IFunctionsService, FunctionService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add DbContext to the container
builder.Services.AddDbContext<FunctionsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var DefaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowLocalhost");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FunctionsDbContext>();
    dbContext.Database.Migrate();  // Applies migrations automatically
}

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
