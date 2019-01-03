using System.Collections.Concurrent;

namespace ObjectPool
{
    public class ObjectPoolMainApp
    {
        public static void Main()
        {
            ObjectPool<ExpensiveClass> objPool = new ObjectPool<ExpensiveClass>();
            ExpensiveClass obj = objPool.Get();
            objPool.Release(obj);
        }
    }

    public class ExpensiveClass
    {
        public int MyProperty { get; set; }
    }

    public class ObjectPool<T> where T : new()
    {
        private readonly ConcurrentBag<T> _Items = new ConcurrentBag<T>();
        private int _Counter = 0;
        private readonly int Max = 10;

        public T Get()
        {
            if (_Items.TryTake(out T item))
            {
                _Counter--;
                return item;
            }
            else
            {
                T obj = new T();
                _Items.Add(obj);
                _Counter++;
                return obj;
            }
        }

        public void Release(T item)
        {
            if (_Counter < Max)
            {
                _Items.Add(item);
                _Counter++;
            }
        }
    }
}