using CustomerAuthorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using WebService.Authorization.HttpApi.Host.DependencyInjection;
using WebService.Authorization.HttpApi.Host.Transformer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseParameterTransformer()));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var confiruare = builder.Configuration;
builder.Services.DependencyInjectionOption(confiruare);
builder.Services.DependencyInjectionClass();
builder.Services.AddJwtSdk(confiruare);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
