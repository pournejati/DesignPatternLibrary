namespace Memento
{
    public class MementoMainApp
    {
        public static void Main()
        {
            var salesProspect = new SalesProspect
            {
                Name = "Noel van Halen",
                Phone = "(412) 256-0990",
                Budget = 25000.0
            };

            var salesProspectMemento = salesProspect.Copy();
            var prospectMemory = new MementoHolder(salesProspectMemento);

            salesProspect.Name = "Leo Welch";
            salesProspect.Phone = "(310) 209-7111";
            salesProspect.Budget = 1000000.0;

            salesProspect.Restore(prospectMemory.Memento);
        }
    }

    public class SalesProspect
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Budget { get; set; }

        public Memento Copy() => new Memento
        {
            Name = Name,
            Phone = Phone,
            Budget = Budget
        };

        public void Restore(Memento memento)
        {
            Name = memento.Name;
            Phone = memento.Phone;
            Budget = memento.Budget;
        }
    }

    public class Memento
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Budget { get; set; }      
    }

    public class MementoHolder
    {
        public Memento Memento { set; get; }
        public MementoHolder(Memento memento) => Memento = memento;
    }
}