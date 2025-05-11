using Common.ApiFormat;
using CustomerAuthorization;
using CustomerAuthorization.Implements;
using CustomerAuthorization.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using WebService.Authorization.Application;
using WebService.Authorization.HttpApi.Host.Infrastructure;
using WebService.Authorization.HttpApi.Host.Infrastructure.DependencyInjection;
using WebService.Authorization.HttpApi.Host.Transformer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseParameterTransformer()));
});
builder.Services.AddEndpointsApiExplorer();

var confiruare = builder.Configuration;

#region -- Swagger
builder.Services.AddSwaggerGen(swaggersetting =>
{
    swaggersetting.SwaggerDoc("v1", new OpenApiInfo { Title = "Authorization API", Version = "v1" });

    swaggersetting.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Description = "Format:Bearer {token}",
    });

    swaggersetting.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme } },
            Array.Empty<string>()
        }
    });
});
#endregion

#region -- Auth

builder.Services.AddJwtSdk(confiruare);
var jwtSection = builder.Configuration.GetSection("JwtToken");
var issuer = jwtSection["Issuer"];
var audience = jwtSection["Audience"];
var key = jwtSection["Key"];

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IGetCurrentUser>(provider =>
{
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    var user = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
    return new GetCurrentUser(user);
});

#endregion

#region --Mapster

builder.Services.AddMapster();
ApplicationMappingConfig.RegisterMappings();
ControllerMappingConfig.RegisterMappings();

#endregion

#region --Dependency Injection
builder.Services.DependencyInjectionOption(confiruare);
builder.Services.DependencyInjectionClass();
#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ApiResponseMiddleware>();
app.MapControllers();

app.Run();
