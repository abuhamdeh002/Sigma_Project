using Sigma_Project.Services;

var builder = WebApplication.CreateBuilder(args);

// Adds services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<ICandidateRepository, CsvCandidateRepository>();  // Registers the CSV repository

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
