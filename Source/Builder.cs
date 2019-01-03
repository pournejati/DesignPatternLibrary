using System;
using System.Collections.Generic;
using System.Linq;

namespace Builder
{
    public class BuilderMainApp
    {
        public static void Main()
        {
            VehicleMaker vehicleMaker1 = new VehicleMaker(new MotorCycleBuilder());
            vehicleMaker1.BuildVehicle();
            IVehicle vehicle1 = vehicleMaker1.GetVehicle();
            Console.WriteLine(vehicle1.GetFeatures());

            VehicleMaker vehicleMaker2 = new VehicleMaker(new CarBuilder());
            vehicleMaker2.BuildVehicle();
            IVehicle vehicle2 = vehicleMaker2.GetVehicle();
            Console.WriteLine(vehicle2.GetFeatures());
        }
    }

    public interface IVehicle
    {
        string VehicleType { get; set; }
        string this[string key] { get; set; }
        string GetFeatures();
    }

    public class Vehicle : IVehicle
    {
        public string VehicleType { get; set; }

        private readonly Dictionary<string, string> _parts = new Dictionary<string, string>();
        public string this[string key]
        {
            get => _parts[key];
            set => _parts[key] = value;
        }

    public string GetFeatures() => string.Join('\n', _parts.Select(x => $"{x.Key}: {x.Value}"));
    }

    public interface IVehicleBuilder
    {
        void Construct();
        IVehicle GetVehicle();
    }

    public class MotorCycleBuilder : IVehicleBuilder
    {
        private IVehicle _vehicle;

        public void Construct()
        {
            _vehicle = new Vehicle
            {
                VehicleType = "MotorCycle",
                ["frame"] = "MotorCycle Frame",
                ["engine"] = "500 cc",
                ["wheels"] = "2",
                ["doors"] = "0"
            };
        }

        public IVehicle GetVehicle()
        {
            return _vehicle;
        }
    }

    public class CarBuilder : IVehicleBuilder
    {
        private IVehicle _vehicle;

        public void Construct()
        {
            _vehicle = new Vehicle
            {
                VehicleType = "Car",
                ["frame"] = "Car Frame",
                ["engine"] = "2500 cc",
                ["wheels"] = "4",
                ["doors"] = "4"
            };
        }

        public IVehicle GetVehicle()
        {
            return _vehicle;
        }
    }

    public class VehicleMaker
    {
        private IVehicleBuilder _builder;
        public VehicleMaker(IVehicleBuilder builder)
        {
            _builder = builder;
        }

        public void BuildVehicle()
        {
            _builder.Construct();
        }

        public IVehicle GetVehicle()
        {
            return _builder.GetVehicle();
        }
    }
}