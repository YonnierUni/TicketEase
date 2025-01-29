using MassTransit;
using TicketEase.Service.Email.Consumers;
using TicketEase.Service.Email.Interfaces;
using TicketEase.Service.Email.Models;
using TicketEase.Service.Email.Services;

var builder = WebApplication.CreateBuilder(args);

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Configure the SMTP service
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));
builder.Services.AddTransient<IEmailSenderService, EmailSenderService>();

// Configure RabbitMQ and MassTransit
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<TicketPurchasedEventConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"]);
        cfg.ReceiveEndpoint(builder.Configuration["RabbitMq:Queues:ticket-purchased-email-queue"], e =>
        {
            e.ConfigureConsumer<TicketPurchasedEventConsumer>(context);
        });

    });
});


// Add services to the container.
builder.Services.AddMassTransitHostedService();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseCors("AllowLocalhost");


// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
