using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Flyweight
{
    public class FlyweightMainApp
    {
        public static void Main()
        {
            var factory = new ApiFactory();

            var endPoint1 = factory.GetEndPoint("EndPoint1");
            var client1 = endPoint1.GetClient("Api/Test1");
            var endPoint2 = factory.GetEndPoint("EndPoint2");
            var client2 = endPoint2.GetClient("Api/Test1");
        }
    }

    public class ApiFactory
    {
        private Dictionary<string, IApiEndPoint> _EndPoints = new Dictionary<string, IApiEndPoint>();

        public IApiEndPoint GetEndPoint(string name)
        {
            IApiEndPoint endPoint = null;
            if (_EndPoints.ContainsKey(name))
                endPoint = _EndPoints[name];
            else
            {
                switch (name)
                {
                    case "EndPoint1": endPoint = new EndPoint1(); break;
                    case "EndPoint2": endPoint = new EndPoint2(); break;
                }
                _EndPoints.Add(name, endPoint);
            }
            return endPoint;
        }
    }

    public interface IApiEndPoint
    {
        string Name { get; set; }
        string BaseUrl { get; set; }
        HttpClient GetClient(string route);
    }

    public class EndPoint1 : IApiEndPoint
    {
        public string Name { get; set; } = "Name1";
        public string BaseUrl { get; set; }

        public HttpClient GetClient(string route) => throw new NotImplementedException();
    }

    public class EndPoint2 : IApiEndPoint
    {
        public string Name { get; set; }
        public string BaseUrl { get; set; }

        public HttpClient GetClient(string route) => throw new NotImplementedException();
    }
}