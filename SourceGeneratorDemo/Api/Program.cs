

using SourceGeneratorDemo.Core;
using SourceGeneratorDemo.Infrastructure.Weather;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddCore()
    .AddInfrastructureWeather()
    .AddAutoMapper(typeof(DiCompositorCore), typeof(DiCompositorInfrastructure), typeof(Program))
    .AddSwaggerGen(c => c.UseDateOnlyTimeOnlyStringConverters())
    .AddDateOnlyTimeOnlyStringConverters()
    .AddControllers();

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

app.Run();
