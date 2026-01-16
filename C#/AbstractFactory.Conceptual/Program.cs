// Patrón de Diseño Abstract Factory
//
// Propósito: Permite producir familias de objetos relacionados sin especificar sus
// clases concretas.

using System;

namespace RefactoringGuru.DesignPatterns.AbstractFactory.Conceptual
{
    // La interfaz Abstract Factory declara un conjunto de métodos que devuelven
    // diferentes productos abstractos. Estos productos se llaman una familia y están
    // relacionados por un tema o concepto de alto nivel. Los productos de una familia
    // generalmente pueden colaborar entre sí. Una familia de productos puede
    // tener varias variantes, pero los productos de una variante son incompatibles
    // con los productos de otra.
    public interface IAbstractGUIFactory
    {
        IAbstractProductAButton CreateProductAButton();

        IAbstractProductBCheckbox CreateProductBCheckbox();
    }

    // Las Fábricas Concretas producen una familia de productos que pertenecen a una sola
    // variante. La fábrica garantiza que los productos resultantes sean compatibles.
    // Tenga en cuenta que las firmas de los métodos de la Fábrica Concreta devuelven un
    // producto abstracto, mientras que dentro del método se instancia un producto concreto.
    class ConcreteFactory1Windows : IAbstractGUIFactory
    {
        public IAbstractProductAButton CreateProductAButton()
        {
            return new ConcreteProductA1WinButton();
        }

        public IAbstractProductBCheckbox CreateProductBCheckbox()
        {
            return new ConcreteProductB1WinCheckbox();
        }
    }

    // Cada Fábrica Concreta tiene una variante de producto correspondiente.
    class ConcreteFactory2MAC : IAbstractGUIFactory
    {
        public IAbstractProductAButton CreateProductAButton()
        {
            return new ConcreteProductA2MacButton();
        }

        public IAbstractProductBCheckbox CreateProductBCheckbox()
        {
            return new ConcreteProductB2MacCheckbox();
        }
    }

    // Cada producto distinto de una familia de productos debe tener una interfaz base.
    // Todas las variantes del producto deben implementar esta interfaz.
    public interface IAbstractProductAButton
    {
        string UsefulFunctionA();
    }

    // Los Productos Concretos son creados por las Fábricas Concretas correspondientes.
    class ConcreteProductA1WinButton : IAbstractProductAButton
    {
        public string UsefulFunctionA()
        {
            return "El resultado del producto A1: un botón de Windows.";
        }
    }

    class ConcreteProductA2MacButton : IAbstractProductAButton
    {
        public string UsefulFunctionA()
        {
            return "El resultado del producto A2: un botón de Mac.";
        }
    }

    // Aquí está la interfaz base de otro producto. Todos los productos pueden
    // interactuar entre sí, pero la interacción adecuada solo es posible entre
    // productos de la misma variante concreta.
    public interface IAbstractProductBCheckbox
    {
        // El Producto B puede hacer lo suyo...
        string UsefulFunctionB();

        // ...pero también puede colaborar con el ProductoA.
        //
        // La Abstract Factory se asegura de que todos los productos que crea sean de
        // la misma variante y, por lo tanto, compatibles.
        string AnotherUsefulFunctionB(IAbstractProductAButton collaborator);
    }

    // Los Productos Concretos son creados por las Fábricas Concretas correspondientes.
    class ConcreteProductB1WinCheckbox : IAbstractProductBCheckbox
    {
        public string UsefulFunctionB()
        {
            return "El resultado del producto B1: un Checkbox de Windows.";
        }

        // La variante, Producto B1, solo puede funcionar correctamente con la
        // variante, Producto A1. Sin embargo, acepta cualquier instancia de
        // AbstractProductA como argumento.
        public string AnotherUsefulFunctionB(IAbstractProductAButton collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"El resultado de B1 (un Checkbox de Windows) colaborando con ({result})";
        }
    }

    class ConcreteProductB2MacCheckbox : IAbstractProductBCheckbox
    {
        public string UsefulFunctionB()
        {
            return "El resultado del producto B2: un Checkbox de Mac.";
        }

       // La variante, Producto B2, solo puede funcionar correctamente con la
       // variante, Producto A2. Sin embargo, acepta cualquier instancia de
       // AbstractProductA como argumento.
        public string AnotherUsefulFunctionB(IAbstractProductAButton collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"El resultado de B2 (un Checkbox de Mac) colaborando con ({result})";
        }
    }

    // El código del cliente trabaja con fábricas y productos solo a través de tipos
    // abstractos: AbstractFactory y AbstractProduct. Esto le permite pasar cualquier
    // subclase de fábrica o producto al código del cliente sin romperlo.
    class Client
    {
        public void Main()
        {
            // El código del cliente puede funcionar con cualquier clase de fábrica concreta.
            Console.WriteLine("Cliente: Probando código del cliente con el primer tipo de fábrica: SO Windows");
            ClientMethod(new ConcreteFactory1Windows());
            Console.WriteLine();

            Console.WriteLine("Cliente: Probando el mismo código del cliente con el segundo tipo de fábrica: SO MAC");
            ClientMethod(new ConcreteFactory2MAC());
        }

        public void ClientMethod(IAbstractGUIFactory factory)
        {
            var productA = factory.CreateProductAButton();
            var productB = factory.CreateProductBCheckbox();

            Console.WriteLine(productB.UsefulFunctionB());
            Console.WriteLine(productB.AnotherUsefulFunctionB(productA));
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