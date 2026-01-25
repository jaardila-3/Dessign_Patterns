// Patrón de Diseño Builder - Ejemplo Pizzería
//
// Este ejemplo muestra cómo construir diferentes tipos de pizzas
// paso a paso, permitiendo crear pizzas personalizadas o usar
// recetas predefinidas a través del Director (chef).

using System;
using System.Collections.Generic;
using System.Text;

namespace RefactoringGuru.DesignPatterns.Builder.Conceptual
{
    // La interfaz Builder define los pasos para construir una pizza
    public interface IConstructorPizza
    {
        void EstablecerTamaño(string tamaño);
        void AgregarMasa(string tipoMasa);
        void AgregarSalsa(string tipoSalsa);
        void AgregarQueso(string tipoQueso);
        void AgregarIngredientes(List<string> ingredientes);
        void AgregarCoccion(string tipoCoccion);
    }
    
    // Concrete Builder - Implementa la construcción de pizzas
    public class ConstructorPizzaPersonalizada : IConstructorPizza
    {
        private Pizza _pizza;
        
        public ConstructorPizzaPersonalizada()
        {
            this.Reiniciar();
        }
        
        public void Reiniciar()
        {
            this._pizza = new Pizza();
        }
        
        public void EstablecerTamaño(string tamaño)
        {
            this._pizza.Tamaño = tamaño;
        }
        
        public void AgregarMasa(string tipoMasa)
        {
            this._pizza.TipoMasa = tipoMasa;
        }
        
        public void AgregarSalsa(string tipoSalsa)
        {
            this._pizza.Salsa = tipoSalsa;
        }
        
        public void AgregarQueso(string tipoQueso)
        {
            this._pizza.Queso = tipoQueso;
        }
        
        public void AgregarIngredientes(List<string> ingredientes)
        {
            this._pizza.Ingredientes.AddRange(ingredientes);
        }
        
        public void AgregarCoccion(string tipoCoccion)
        {
            this._pizza.TipoCoccion = tipoCoccion;
        }
        
        // Devuelve la pizza construida y prepara el builder para una nueva pizza
        public Pizza ObtenerPizza()
        {
            Pizza resultado = this._pizza;
            this.Reiniciar();
            return resultado;
        }
    }
    
    // El producto final - Pizza
    public class Pizza
    {
        public string Tamaño { get; set; }
        public string TipoMasa { get; set; }
        public string Salsa { get; set; }
        public string Queso { get; set; }
        public List<string> Ingredientes { get; set; }
        public string TipoCoccion { get; set; }
        
        public Pizza()
        {
            Ingredientes = new List<string>();
        }
        
        public string ObtenerDescripcion()
        {
            var descripcion = new StringBuilder();
            descripcion.AppendLine("=== PIZZA PREPARADA ===");
            descripcion.AppendLine($"Tamaño: {Tamaño}");
            descripcion.AppendLine($"Masa: {TipoMasa}");
            descripcion.AppendLine($"Salsa: {Salsa}");
            descripcion.AppendLine($"Queso: {Queso}");
            
            if (Ingredientes.Count > 0)
            {
                descripcion.AppendLine($"Ingredientes: {string.Join(", ", Ingredientes)}");
            }
            else
            {
                descripcion.AppendLine("Ingredientes: Ninguno");
            }
            
            descripcion.AppendLine($"Cocción: {TipoCoccion}");
            descripcion.AppendLine("=====================");
            
            return descripcion.ToString();
        }
    }
    
    // El Director (Chef) - Conoce las recetas para construir pizzas específicas
    public class Chef
    {
        private IConstructorPizza _constructor;
        
        public IConstructorPizza Constructor
        {
            set { _constructor = value; }
        }
        
        // Receta para una Pizza Margarita clásica
        public void ConstruirPizzaMargarita()
        {
            _constructor.EstablecerTamaño("Mediana");
            _constructor.AgregarMasa("Delgada");
            _constructor.AgregarSalsa("Salsa de tomate italiana");
            _constructor.AgregarQueso("Mozzarella fresca");
            _constructor.AgregarIngredientes(new List<string> { "Albahaca fresca", "Aceite de oliva" });
            _constructor.AgregarCoccion("Horno de leña a 450°C");
        }
        
        // Receta para una Pizza Pepperoni
        public void ConstruirPizzaPepperoni()
        {
            _constructor.EstablecerTamaño("Grande");
            _constructor.AgregarMasa("Tradicional");
            _constructor.AgregarSalsa("Salsa de tomate");
            _constructor.AgregarQueso("Mozzarella");
            _constructor.AgregarIngredientes(new List<string> { "Pepperoni", "Orégano" });
            _constructor.AgregarCoccion("Horno convencional a 220°C");
        }
        
        // Receta para una Pizza Vegetariana
        public void ConstruirPizzaVegetariana()
        {
            _constructor.EstablecerTamaño("Grande");
            _constructor.AgregarMasa("Integral");
            _constructor.AgregarSalsa("Salsa de tomate con hierbas");
            _constructor.AgregarQueso("Mozzarella light");
            _constructor.AgregarIngredientes(new List<string> 
            { 
                "Pimientos", 
                "Champiñones", 
                "Cebolla", 
                "Aceitunas", 
                "Tomate cherry" 
            });
            _constructor.AgregarCoccion("Horno eléctrico a 200°C");
        }
        
        // Receta para una Pizza Hawaiana
        public void ConstruirPizzaHawaiana()
        {
            _constructor.EstablecerTamaño("Mediana");
            _constructor.AgregarMasa("Tradicional");
            _constructor.AgregarSalsa("Salsa de tomate");
            _constructor.AgregarQueso("Mozzarella");
            _constructor.AgregarIngredientes(new List<string> { "Jamón", "Piña" });
            _constructor.AgregarCoccion("Horno convencional a 220°C");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // El cliente crea el chef y el constructor
            var chef = new Chef();
            var constructor = new ConstructorPizzaPersonalizada();
            chef.Constructor = constructor;
            
            Console.WriteLine("*** PIZZERÍA DON BUILDER ***\n");
            
            // Usando el chef para crear pizzas con recetas predefinidas
            Console.WriteLine("1. Pizza Margarita:");
            chef.ConstruirPizzaMargarita();
            Console.WriteLine(constructor.ObtenerPizza().ObtenerDescripcion());
            
            Console.WriteLine("2. Pizza Pepperoni:");
            chef.ConstruirPizzaPepperoni();
            Console.WriteLine(constructor.ObtenerPizza().ObtenerDescripcion());
            
            Console.WriteLine("3. Pizza Vegetariana:");
            chef.ConstruirPizzaVegetariana();
            Console.WriteLine(constructor.ObtenerPizza().ObtenerDescripcion());
            
            Console.WriteLine("4. Pizza Hawaiana:");
            chef.ConstruirPizzaHawaiana();
            Console.WriteLine(constructor.ObtenerPizza().ObtenerDescripcion());
            
            // El cliente también puede construir pizzas personalizadas sin el chef
            Console.WriteLine("5. Pizza Personalizada del Cliente:");
            constructor.EstablecerTamaño("Familiar");
            constructor.AgregarMasa("Masa de cerveza");
            constructor.AgregarSalsa("Salsa BBQ");
            constructor.AgregarQueso("Mezcla de quesos");
            constructor.AgregarIngredientes(new List<string> 
            { 
                "Pollo", 
                "Tocino", 
                "Cebolla morada", 
                "Jalapeños" 
            });
            constructor.AgregarCoccion("Horno de piedra a 300°C");
            Console.WriteLine(constructor.ObtenerPizza().ObtenerDescripcion());
            
            Console.WriteLine("\n¡Gracias por su visita!");
            Console.ReadKey();
        }
    }
}