using System;
using System.Collections.Generic;

namespace Command
{
    public static class CommandApp
    {
        public static void Main()
        {
            var light = new Light();
            ICommand switchUp = new FlipUpCommand(light);
            ICommand switchDown = new FlipDownCommand(light);

            var lightSwitch = new LightSwitch();
            lightSwitch.StoreAndExecute(switchUp);
            lightSwitch.StoreAndExecute(switchDown);            
        }
    }

    public class Light
    {
        public void TurnOn() => Console.WriteLine("The light is on");
        public void TurnOff() => Console.WriteLine("The light is off");
    }

    public interface ICommand
    {
        void Execute();
    }

    public class LightSwitch
    {
        private List<ICommand> _Commands = new List<ICommand>();
        public void StoreAndExecute(ICommand command)
        {
            _Commands.Add(command);
            command.Execute();
        }
    }    

    public class FlipUpCommand : ICommand
    {
        private Light _Light;
        public FlipUpCommand(Light light) => _Light = light;
        public void Execute() => _Light.TurnOn();
    }

    public class FlipDownCommand : ICommand
    {
        private Light _Light;
        public FlipDownCommand(Light light) => _Light = light;
        public void Execute() => _Light.TurnOff();
    }
}