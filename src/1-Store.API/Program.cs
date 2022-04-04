using System.Text;
using AutoMapper;
using EscNet.IoC.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Store.API.Token;
using Store.API.ViewModels;
using Store.Core.Communication.Handlers;
using Store.Core.Communication.Mediator;
using Store.Core.Communication.Mediator.Interfaces;
using Store.Core.Communication.Messages.Notifications;
using Store.Domain.Entities;
using Store.Infra.Context;
using Store.Infra.Interfaces;
using Store.Infra.Repositories;
using Store.Services.DTO;
using Store.Services.Interfaces;
using Store.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



#region Jwt


var secretKey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

#endregion

#region AutoMapper

var autoMapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Costumer, CostumerDto>().ReverseMap();
    cfg.CreateMap<CreateCostumerViewModel, CostumerDto>().ReverseMap();
});

builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

#endregion

#region Services
builder.Services.AddSingleton(d => builder.Configuration);
builder.Services.AddScoped<ICostumerServices, CostumerServices>();
builder.Services.AddScoped<ICostumerRepository, CostumerRepository>();
builder.Services.AddScoped<IOrderRepository, orderRepository>();
#endregion

#region Database
builder.Services.AddDbContext<StoreContext>(options => options.UseNpgsql("ConnectionStrings:StoreAPIPostgreSQL"));
#endregion

#region Mediator

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();

#endregion

#region Token

builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();

#endregion

#region Swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "R6 API",
        Version = "v1",
        Description = "API de uma loja",
        Contact = new OpenApiContact
        {
            Name = "Gabriel Bobrov",
            Email = "gabrielbobrov@outlook.com.br",
            Url = new Uri("https://github.com/GabrielBobrov")
        },
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor utilize Bearer <TOKEN>",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
    });
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
