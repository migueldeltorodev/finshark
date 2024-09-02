using FinShark.Server.Data;
using FinShark.Server.Interfaces;
using FinShark.Server.Models;
using FinShark.Server.Repository;
using FinShark.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

//Agregando los controladores originalmente.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(options => 
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

//Agregando la conexión a la base de datos.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Agregando los servicios/repositorios.
//Inyecciones de dependencia
builder.Services.AddScoped<InterfaceStockRepository, StockRepository>();
builder.Services.AddScoped<InterfaceCommentRepository, CommentRepository>();
builder.Services.AddScoped<InterfaceTokenService, TokenService>();

//Agregando Identity options para la contraseña:
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;
}).AddEntityFrameworkStores<ApplicationDbContext>();

//Agregando la Scheme:
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = 
    options.DefaultChallengeScheme = 
    options.DefaultForbidScheme = 
    options.DefaultScheme = 
    options.DefaultSignInScheme = 
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWS:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
        )
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Agregando autenticación
app.UseAuthentication();
app.UseAuthorization();

//Agregando opcion de mapeo automatico de controladores.
app.MapControllers();

app.Run();

