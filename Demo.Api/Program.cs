using Demo.Application;
using Demo.Application.Implementations.MiddleWare;
using Demo.Application.Implementations.Proxies;
using Demo.Application.Interfaces;
using Demo.Application.Interfaces.Proxies;
using Demo.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

const string ApplicationName = "Demo";
const string ApplicationDescription = "Demo WebApi";
const string SwaggerVersion = "2.0";

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// JWT Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["JwtSettings:Issuer"],
        ValidAudience = configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
    };
});

// Configure CORS (Define a single policy to allow the Angular app URL)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", // You can rename this if needed
        policy => policy
            .WithOrigins("http://localhost:4200") // Allow requests from the Angular app's URL
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()); // Use this only if cookies or authentication are shared
});

// Add services to the container.
builder.Services.AddMvc().AddFluentValidation(opt =>
{
    opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
//add shared services if there
//add rebbit service if there
builder.Services.AddApplicationFactory();
builder.Services.AddRepositoriesFactory();

var baseUrl = builder.Configuration["GitHubApi:BaseUrl"];
var useDefaultCredentials = builder.Configuration.GetValue<bool>("GitHubApi:UseDefaultCredentials");

// Register service with custom configuration
builder.Services.Configure<IGitHubProxy, GitHubProxy>(baseUrl, useDefaultCredentials);

//add swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SMARTI Service Challenge Web Api", Version = "v1" });
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter your JWT token in this field",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var app = builder.Build();

//run the prioritySetting
var prioritySettingsService = app.Services.GetService<IPrioritySettingsService>();
prioritySettingsService.LoadPrioritySettingsAsync();

// Configure the HTTP request pipeline.
IWebHostEnvironment env = app.Environment;

if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>(); // Add JWT middleware


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
