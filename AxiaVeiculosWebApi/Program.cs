using System.Text.Json.Serialization;
using AxiaVeiculosApplication;
using AxiaVeiculosInfra;
using AxiaVeiculosWebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfra();

var app = builder.Build();

await app.Services.EnsureDatabaseCreatedAsync();

app.UseApiExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
