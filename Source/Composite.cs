using System;
using System.Collections.Generic;

namespace Composite
{
    public class CompositeApp
    {
        public static void Main()
        {
            var root = new CompositeElement { Name = "Picture" };
            root.Add(new PrimitiveElement { Name = "Red Line" });
            root.Add(new PrimitiveElement { Name = "Blue Circle" });
            root.Add(new PrimitiveElement { Name = "Green Box" });

            var childNode = new CompositeElement { Name = "Two Circles" };
            childNode.Add(new PrimitiveElement { Name = "Black Circle" });
            childNode.Add(new PrimitiveElement { Name = "White Circle" });
            root.Add(childNode);

            root.Display();
        }
    }

    public interface IElement
    {
        string Name { get; set; }

        void Add(IElement node);
        void Remove(IElement node);
        void Display();
    }

    public class CompositeElement : IElement
    {
        public string Name { get; set; }
        private List<IElement> _Nodes = new List<IElement>();

        public void Add(IElement node) => _Nodes.Add(node);
        public void Remove(IElement node) => _Nodes.Remove(node);

        public void Display()
        {
            Console.WriteLine(Name);
            foreach (IElement node in _Nodes)
                node.Display();
        }
    }

    public class PrimitiveElement : IElement
    {
        public string Name { get; set; }

        public void Add(IElement node) => Console.WriteLine("Cannot add to a PrimitiveElement");
        public void Remove(IElement node) => Console.WriteLine("Cannot remove from a PrimitiveElement");
        public void Display() => Console.WriteLine(Name);
    }
}