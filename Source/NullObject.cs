using System;

namespace NullObject
{
    public class NullObjectMainApp
    {
        public static void Main()
        {
            var autoRepository = new AutoRepository();
            var automobile = autoRepository.Find("bmw");
            if (automobile != AutoRepository.NullAutomobile)
            {
                automobile.Start();
                automobile.Stop();
            }
        }
    }

    public interface IAutomobile
    {
        Guid Id { get; }
        string Name { get; }
        void Start();
        void Stop();
    }

    public class NullAutomobile : IAutomobile
    {
        public Guid Id => Guid.Empty;
        public string Name => string.Empty;
        public void Start() { }
        public void Stop() { }
    }    

    public class MiniCooper : IAutomobile
    {
        public Guid Id => Guid.NewGuid();
        public string Name => "Mini Cooper S";
        public void Start() => Console.WriteLine("Mini Cooper started. 1.6L of raw power is ready to go.");
        public void Stop() => Console.WriteLine("Mini Cooper stopped.");
    }

    public class BMW335XI : IAutomobile
    {        
        public Guid Id => new Guid("68BECCDC-0FBD-4FB9-B0BB-D5D8A2AFD9F8");
        public string Name => "BMW 335 Xi";
        public void Start() => Console.WriteLine("Beemer started. All 4 wheels under power.");
        public void Stop() => Console.WriteLine("Beemer stopped.");
    }

    public class AutoRepository
    {
        public static IAutomobile NullAutomobile { get; } = new NullAutomobile();

        public IAutomobile Find(string carName)
        {
            if (carName.Contains("mini"))
                return new MiniCooper();
            return NullAutomobile;
        }
    }
}