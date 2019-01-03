using System;

namespace Decorator
{
    public class DecoratorMainApp
    {
        static void Main()
        {
            var regularPizza = new RegularPizza();
            Console.WriteLine(regularPizza.Info());

            var regularPizzaAndCheese = new Cheese(regularPizza);
            Console.WriteLine(regularPizzaAndCheese.Info());

            var largePizza = new LargePizza();
            Console.WriteLine(largePizza.Info());

            var largePizzaAndCheese = new Cheese(largePizza);
            Console.WriteLine(largePizzaAndCheese.Info());

            var largePizzaAndMushroomAndCheese = new Mushroom(largePizzaAndCheese);
            Console.WriteLine(largePizzaAndMushroomAndCheese.Info());
        }
    }

    public interface IPizzaItem
    {
        int Calorie { get; set; }
        string Info();
    }

    public class RegularPizza : IPizzaItem
    {
        public int Calorie { get; set; } = 500;
        public string Info() => "Regular Pizza";
    }

    public class LargePizza : IPizzaItem
    {
        public int Calorie { get; set; } = 800;
        public string Info() => "Large Pizza";
    }

    public abstract class PizzaItemDecorator : IPizzaItem
    {
        public int Calorie { get; set; }
        protected IPizzaItem PizzaItem;

        public PizzaItemDecorator(IPizzaItem pizzaItem)
        {
            PizzaItem = pizzaItem;
        }

        public virtual string Info() => PizzaItem.Info();
    }

    public class Cheese : PizzaItemDecorator
    {
        protected IPizzaItem _pizzaItem;
        public Cheese(IPizzaItem pizzaItem) : base(pizzaItem)
        {
            Calorie = pizzaItem.Calorie + 300;
        }
        public override string Info() => $"{base.Info()} and extra cheese";
    }

    public class Mushroom : PizzaItemDecorator
    {
        protected IPizzaItem _pizzaItem;
        public Mushroom(IPizzaItem pizzaItem) : base(pizzaItem)
        {
            Calorie = pizzaItem.Calorie + 100;
        }
        public override string Info() => $"{base.Info()} and extra mushroom";
    }
}