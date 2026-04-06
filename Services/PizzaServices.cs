using BoschPizza.Models;
 
namespace BoschPizza.Services;
public class PizzaService{
   
    static List<Pizza> Pizzas { get; set;}
    static int nextId = 3;
 
    //metodo construtor
    static PizzaService(){
        Pizzas = new List<Pizza>{
           
            new Pizza { Id= 1, Name = "Calabresa", IsGlutenFree = false},
            new Pizza { Id= 2, Name = "Vegetariana", IsGlutenFree = true},
        };
    }
 
    // Busca todos os itens da lista
    public static List<Pizza> GetAll() => Pizzas;
 
    // Busca pizza por ID
    public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);
 
    // Adicionar nova pizza
    public static void Add(Pizza pizza){
       
        pizza.Id = nextId;
        Pizzas.Add(pizza);
    }
 
    public static void Delete(int id){
       
        var pizza = Get(id);
        if (pizza is null) return;
        Pizzas.Remove(pizza);
    }
 
    public static void Update(Pizza pizza){
       
        var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
        if (index == -1) return;
        Pizzas[index] = pizza;
    }
}