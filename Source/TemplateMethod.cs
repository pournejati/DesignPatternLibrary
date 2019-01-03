using System;
using System.Data;
using System.Data.SqlClient;

namespace TemplateMethod
{
    public class TemplateMethodMainApp
    {
        public static void Main(string[] args)
        {
            DataAccessObject daoCategories = new Categories();
            daoCategories.Run();

            DataAccessObject daoProducts = new Products();
            daoProducts.Run();
        }
    }

    public abstract class DataAccessObject
    {
        protected string _ConnectionString;
        protected DataSet _DataSet;

        public virtual void Connect() => _ConnectionString = "provider=Microsoft.JET.OLEDB.4.0; data source=..\\..\\..\\db1.mdb";
        public abstract void Select();
        public abstract void Process();
        public virtual void Disconnect() => _ConnectionString = string.Empty;

        // The 'Template Method'.
        public void Run()
        {
            Connect();
            Select();
            Process();
            Disconnect();
        }
    }

    public class Categories : DataAccessObject
    {
        public override void Select()
        {
            var sql = "SELECT CategoryName FROM Categories";
            var dataAdapter = new SqlDataAdapter(sql, _ConnectionString);

            _DataSet = new DataSet();
            dataAdapter.Fill(_DataSet, "Categories");
        }

        public override void Process()
        {
            var dataTable = _DataSet.Tables["Categories"];
            foreach (DataRow row in dataTable.Rows)
                Console.WriteLine(row["CategoryName"]);
        }
    }

    public class Products : DataAccessObject
    {
        public override void Select()
        {
            var sql = "select ProductName from Products";
            var dataAdapter = new SqlDataAdapter(sql, _ConnectionString);

            _DataSet = new DataSet();
            dataAdapter.Fill(_DataSet, "Products");
        }

        public override void Process()
        {
            var dataTable = _DataSet.Tables["Products"];
            foreach (DataRow row in dataTable.Rows)
                Console.WriteLine(row["ProductName"]);
        }
    }
}