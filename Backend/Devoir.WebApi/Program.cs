using Devoir.CustomException;
using Devoir.Repositories.Country;
using Devoir.Repositories.SqlConnectionFactory;
using Devoir.Services.Country;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register the CountryRepository and CountryService
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>(_ =>
{
    if (connectionString != null) return new SqlConnectionFactory(connectionString);
    throw new ArgumentNullException(nameof(connectionString));
});
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICountryService, CountryService>();

builder.Services.AddExceptionHandlers();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(option => {
    option.AddPolicy("AllowSpecificOrigin", policy => {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
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

app.UseCors("AllowSpecificOrigin");

app.Run();