using System.Collections.Generic;

namespace Strategy
{
    public class StrategyMainApp
    {
        private static void Main()
        {
            var studentRecords = new CustomList();
            studentRecords.Add("Samual");
            studentRecords.Add("Jimmy");
            studentRecords.Add("Sandra");
            studentRecords.Add("Vivek");
            studentRecords.Add("Anna");

    studentRecords.Sort(new SortAsc());
    studentRecords.Sort(new SortDesc());
        }
    }

    public interface ISortStrategy
    {
        void Sort(List<string> list);
    }

    public class SortAsc : ISortStrategy
    {
        public void Sort(List<string> list) => list.Sort();
    }

    public class SortDesc : ISortStrategy
    {
        public void Sort(List<string> list)
        {
            list.Sort();
            list.Reverse();
        }
    }

    public class CustomList
    {
        private List<string> _list = new List<string>();

        public void Add(string name) => _list.Add(name);
        public void Sort(ISortStrategy sortstrategy) => sortstrategy.Sort(_list);
    }
}