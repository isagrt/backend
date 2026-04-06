// Importa o contexto do banco de dados
using BoschPizza.Data;
// Importa o serviço de geração de Token
using BoschPizza.Services;
// Importa recurso do Entity Framework Core
using Microsoft.EntityFrameworkCore;

//Importar recursos de autenticação do JWT
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;

//Importar os Models
using Microsoft.OpenApi.Models;

// Cria o build da aplicação e auxilia na configuração dos serviços e recursos do projeto
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Adiciona suporte em controllers, isso ajuda para que a API reconheça a classe controllers como ponto de entrada
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// adiciona a geração do Swagger/OpenAPI
builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo
   {
      Title = "Bosch Pizza API",
      Version = "v1"
   });
 
   // 🔐 Definição do esquema de segurança (JWT)
   options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
   {
      Name = "Authorization",
      Type = SecuritySchemeType.Http,
      Scheme = "bearer",
      BearerFormat = "JWT",
      In = ParameterLocation.Header,
      Description = "Digite: Bearer {seu token}"
   });
 
   // Aplicar segurança global
  options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
   });
});

//Obter a string de conexao do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//Registradr o AppDbContext usando Mysql
builder.Services.AddDbContext<AppDbContext>(Options => Options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//Regista o tokenservice para injelção de dependencias
builder.Services.AddScoped<TokenService>();

//Le a chave JWT do arquivo de configuração 
var jwtkey = builder.Configuration["Jwt:Key"];

//Configuração da autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(Options =>
{
    Options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtkey!))
    };
}
);

// Adiciona a autorização 
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // ativa o swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

// redireciona o HTTPS
app.UseHttpsRedirection();

//ativa a autenticação
app.UseAuthorization();

// imapeia os controllers
app.MapControllers();

// Inicia a aplicação
app.Run();
