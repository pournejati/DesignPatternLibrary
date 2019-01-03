namespace FactoryMethod
{
    public class FactoryMethodMainApp
    {
        public static void Main()
        {
            IAutoFactory autoFactory = new BmwFactory();
            IAuto auto = autoFactory.GetInstance();
        }
    }

    internal interface IAuto
    {
        string GetName();
    }

    internal class Bmw : IAuto
    {
        public string GetName() => "Bmw";
    }

    internal interface IAutoFactory
    {
        IAuto GetInstance();
    }

    internal class BmwFactory : IAutoFactory
    {
        public IAuto GetInstance() => new Bmw();
    }
}