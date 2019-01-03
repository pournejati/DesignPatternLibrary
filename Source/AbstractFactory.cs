using System;
using System.Collections.Generic;

namespace AbstractFactory
{
    public class AbstractFactoryMainApp
    {
        public static void Main()
        {
            IVehicleFactory kiaFactory = new KiaFactory();
            IVehicle kiaEconomy = kiaFactory.GetEconomyCar();
            IVehicle kiaLuxury = kiaFactory.GetLuxuryCar();

            IVehicleFactory hyundaiFactory = new HyundaiFactory();
            IVehicle hyundaiEconomy = hyundaiFactory.GetEconomyCar();
            IVehicle hyundaiLuxury = hyundaiFactory.GetLuxuryCar();
        }
    }

    public interface IVehicle
    {
        string GetName();
    }

    public class KiaPride : IVehicle
    {
        public string GetName()
        {
            return "Kia - Pride";
        }
    }

    public class KiaRegal : IVehicle
    {
        public string GetName()
        {
            return "Kia - Regal";
        }
    }

    public class HyundaiAccent : IVehicle
    {
        public string GetName()
        {
            return "Hyundai - Accent";
        }
    }

    public class HyundaiSantaFe : IVehicle
    {
        public string GetName()
        {
            return "Hyundai - SantaFe";
        }
    }

    public interface IVehicleFactory
    {
        IVehicle GetLuxuryCar();
        IVehicle GetEconomyCar();
    }

    public class KiaFactory : IVehicleFactory
    {
        public IVehicle GetLuxuryCar()
        {
            return new KiaPride();
        }

        public IVehicle GetEconomyCar()
        {
            return new KiaRegal();
        }
    }

    public class HyundaiFactory : IVehicleFactory
    {
        public IVehicle GetLuxuryCar()
        {
            return new HyundaiAccent();
        }

        public IVehicle GetEconomyCar()
        {
            return new HyundaiSantaFe();
        }
    }
}