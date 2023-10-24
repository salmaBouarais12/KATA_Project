using Kata.DemoApi;
using KATA.Dal;
using KATA.Dal.Repositories;
using KATA.Domain.Interfaces.API;
using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Interfaces.Sevices;
using KATA.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((ctx, lc) => lc
      .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
      .Enrich.FromLogContext()
      .ReadFrom.Configuration(ctx.Configuration));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddTransient<IValueService, ValueService>();
builder.Services.AddScoped<IValueService, ValueService>();
//builder.Services.AddSingleton<IValueService, ValueService>();
builder.Services.AddScoped<IValueRepository, ValueRepository>();

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddScoped<IWeatherForeCastService, WeatherForeCastService>();
builder.Services.AddScoped<IDemoAPI, DemoAPI>();
builder.Services.AddDbContext<DbKataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// In Configure method:
builder.Services.AddHttpClient("DemoAPIClient",httpClient =>
{
    httpClient.BaseAddress = new Uri("http://localhost:5278");
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration.GetValue<string>("UrlAuthority");

        options.TokenValidationParameters.ValidateAudience = false;
    });


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");
app.MapControllers();
app.Run();

