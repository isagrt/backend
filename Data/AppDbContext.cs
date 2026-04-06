using BoschPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace BoschPizza.Data;

public class AppDbContext : DbContext
{
    //Construtor que recebe as opções do ontexto pela injeção de dependências
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    //representa a tabela pizza no banco de dados
    public DbSet<Pizza> Pizzas {get; set;}
    

}