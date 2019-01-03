using System;
using System.Collections.Generic;

namespace Interpreter
{
    public class InterpreterMainApp
    {
        public static void Main()
        {
            var topBread = new WheatBread();
            var topCondiments = new CondimentList(new List<ICondiment> { new MayoCondiment(), new MustardCondiment() });
            var ingredients = new IngredientList(new List<IIngredient> { new LettuceIngredient(), new ChickenIngredient() });
            var bottomCondiments = new CondimentList(new List<ICondiment> { new KetchupCondiment() });
            var bottomBread = new WheatBread();

            var sandwhich = new Sandwhich(topBread, topCondiments, ingredients, bottomCondiments, bottomBread);
            var context = new Context();
            sandwhich.Interpret(context);
            Console.WriteLine(context.Output);
        }
    }

    public class Context
    {
        public string Output { get; set; }
    }

    public interface IExpression
    {
        void Interpret(Context context);
    }

    public class Sandwhich : IExpression
    {
        private readonly IExpression _TopBread;
        private readonly IExpression _TopCondiments;
        private readonly IExpression _Ingredients;
        private readonly IExpression _BottomCondiments;
        private readonly IExpression _BottomBread;

        public Sandwhich(IExpression topBread, IExpression topCondiments, IExpression ingredients, IExpression bottomCondiments, IExpression bottomBread)
        {
            _TopBread = topBread;
            _TopCondiments = topCondiments;
            _Ingredients = ingredients;
            _BottomCondiments = bottomCondiments;
            _BottomBread = bottomBread;
        }

        public void Interpret(Context context)
        {
            context.Output += "|";
            _TopBread.Interpret(context);
            context.Output += "|";
            context.Output += "<--";
            _TopCondiments.Interpret(context);
            context.Output += "-";
            _Ingredients.Interpret(context);
            context.Output += "-";
            _BottomCondiments.Interpret(context);
            context.Output += "-->";
            context.Output += "|";
            _BottomBread.Interpret(context);
            context.Output += "|";
        }
    }

    public interface IIngredient : IExpression { }

    public class IngredientList : IExpression
    {
        private readonly List<IIngredient> _Ingredients;
        public IngredientList(List<IIngredient> ingredients) => _Ingredients = ingredients;
        public void Interpret(Context context)
        {
            foreach (IIngredient ingredient in _Ingredients)
                ingredient.Interpret(context);
        }
    }

    public class TomatoIngredient : IIngredient
    {
        public void Interpret(Context context) => context.Output += string.Format(" {0} ", "Tomato");
    }

    public class LettuceIngredient : IIngredient
    {
        public void Interpret(Context context) => context.Output += string.Format(" {0} ", "Lettuce");
    }

    public class ChickenIngredient : IIngredient
    {
        public void Interpret(Context context) => context.Output += string.Format(" {0} ", "Chicken");
    }

    public class CondimentList : IExpression
    {
        private readonly List<ICondiment> _Condiments;
        public CondimentList(List<ICondiment> condiments) => _Condiments = condiments;
        public void Interpret(Context context)
        {
            foreach (ICondiment condiment in _Condiments)
                condiment.Interpret(context);
        }
    }

    public interface ICondiment : IExpression { }

    public class MayoCondiment : ICondiment
    {
        public void Interpret(Context context) => context.Output += string.Format(" {0} ", "Mayo");
    }

    public class MustardCondiment : ICondiment
    {
        public void Interpret(Context context) => context.Output += string.Format(" {0} ", "Mustard");
    }

    public class KetchupCondiment : ICondiment
    {
        public void Interpret(Context context) => context.Output += string.Format(" {0} ", "Ketchup");
    }

    public interface IBread : IExpression { }

    public class WhiteBread : IBread
    {
        public void Interpret(Context context) => context.Output += string.Format(" {0} ", "White-Bread");
    }

    public class WheatBread : IBread
    {
        public void Interpret(Context context) => context.Output += string.Format(" {0} ", "Wheat-Bread");
    }
}