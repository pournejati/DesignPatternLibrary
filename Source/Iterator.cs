using System;
using System.Collections;

namespace Iterator
{
    public class IteratorMainApp
    {
        public static void Main()
        {
            var collection = new Collection();
            collection[0] = new Item("Item 1");
            collection[1] = new Item("Item 2");
            collection[2] = new Item("Item 3");
            collection[3] = new Item("Item 4");
            collection[4] = new Item("Item 5");

            var iterator = collection.CreateIterator();
            iterator.Step = 2;

            for (var item = iterator.First(); !iterator.IsDone; item = iterator.Next())
                Console.WriteLine(item.Name);
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public Item(string name) => Name = name;
    }

    public interface IAbstractCollection
    {
        Iterator CreateIterator();
    }

    public class Collection : IAbstractCollection
    {
        private readonly ArrayList _Items = new ArrayList();

        public Iterator CreateIterator() => new Iterator(this);
        public int Count => _Items.Count;

        public object this[int index]
        {
            get => _Items[index];
            set => _Items.Add(value);
        }
    }

    public interface IAbstractIterator
    {
        Item First();
        Item Next();
        bool IsDone { get; }
        Item CurrentItem { get; }
    }

    public class Iterator : IAbstractIterator
    {
        private Collection _Collection;
        private int _Current = 0;

        public Iterator(Collection collection) => _Collection = collection;

        public Item First()
        {
            _Current = 0;
            return _Collection[_Current] as Item;
        }

        public Item Next()
        {
            _Current += Step;
            return IsDone ? null : _Collection[_Current] as Item;
        }

        public int Step { get; set; } = 1;
        public Item CurrentItem => _Collection[_Current] as Item;
        public bool IsDone => _Current >= _Collection.Count;
    }
}