// Factory Method Design Pattern
//
// Intención: Proporciona una interfaz para crear objetos en una superclase, pero
// permite que las subclases alteren el tipo de objetos que se crearán.

using System;

namespace RefactoringGuru.DesignPatterns.FactoryMethod.Conceptual
{
    // La clase Creador declara el método de fábrica que debe devolver un objeto de la clase Transporte (Producto).
    // Las subclases de Creador suelen proporcionar la implementación de este método.
    abstract class LogisticaCreator
    {
        // Tenga en cuenta que el Creador también puede proporcionar alguna implementación predeterminada del método de fábrica.
        public abstract ITransporte CrearTransporte();

        // Tenga en cuenta también que, a pesar de su nombre, la responsabilidad principal del Creador no es crear productos.
        // Normalmente, contiene cierta lógica de negocio central que se basa en objetos Producto, devueltos por el método de fábrica.
        // Las subclases pueden cambiar indirectamente esa lógica de negocio sobrescribiendo el método de fábrica y 
        // devolviendo un tipo de producto diferente.
        public string SomeOperation()
        {
            // Llame al método de fábrica para crear un objeto Transporte (Producto).
            var product = CrearTransporte();
            //Ahora, utiliza el producto.
            var result = $"Creator: El mismo código del creador acaba de funcionar con el {product.Operation()}";

            return result;
        }
    }

    // Los creadores sobreescriben el método para cambiar el tipo del producto resultante.
    class LogisticaTerrestre : LogisticaCreator
    {
        // Tenga en cuenta que la firma del método sigue utilizando el tipo de producto abstracto, 
        // aunque el producto concreto se devuelve desde el método. De esta forma, el Creador puede 
        // mantenerse independiente de las clases de producto concreto.
        public override ITransporte CrearTransporte()
        {
            return new Camion();
        }
    }

    class LogisticaMaritima : LogisticaCreator
    {
        public override ITransporte CrearTransporte()
        {
            return new Barco();
        }
    }

    // La interfaz del producto declara las operaciones que todos los productos concretos deben implementar.
    public interface ITransporte
    {
        string Operation();
    }

    // Los productos concretos ofrecen diversas implementaciones de la interfaz del producto.
    class Camion : ITransporte
    {
        public string Operation()
        {
            return "{Transporte en Camión}";
        }
    }

    class Barco : ITransporte
    {
        public string Operation()
        {
            return "{Transporte en Barco}";
        }
    }

    class Client
    {
        public void Main()
        {
            Console.WriteLine("Aplicación: lanzada con ConcreteCreator1.");
            ClientCode(new LogisticaTerrestre());
            
            Console.WriteLine("");

            Console.WriteLine("Aplicación: lanzada con ConcreteCreator2.");
            ClientCode(new LogisticaMaritima());
        }

        // El código del cliente funciona con una instancia de un creador concreto, 
        // aunque a través de su interfaz base. Mientras el cliente siga trabajando 
        // con el creador a través de la interfaz base, puede pasarle cualquier subclase del creador.
        public void ClientCode(LogisticaCreator creator)
        {
            // ...
            Console.WriteLine("Cliente: No conozco la clase del creador," +
                "pero aún funciona.\n" + creator.SomeOperation());
            // ...
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new Client().Main();
        }
    }
}