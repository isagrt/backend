// Cria o build da aplicação e auxilia na configuração dos serviços e recursos do projeto
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Adiciona suporte em controllers, isso ajuda para que a API reconheça a classe controllers como ponto de entrada
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// adiciona a geração do Swagger/OpenAPI
builder.Services.AddSwaggerGen();

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
