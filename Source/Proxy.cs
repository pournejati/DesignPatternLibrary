using System;

namespace Proxy
{
    public class ProxyMainApp
    {
        public static void Main()
        {
            IService service = new Service();
            IService serviceProxy = new ServiceProxy(service);
            var result = serviceProxy.Execute();
        }
    }

    public interface IService
    {
        string Execute();
    }

    class Service : IService
    {
        public string Execute() => "Remote Result";
    }

    class ServiceProxy : IService
    {
        private string _resultCached;
        private IService _service;

        public ServiceProxy(IService service)
        {
            _service = service;
        }

        public string Execute()
        {
            if (string.IsNullOrEmpty(_resultCached))
                _resultCached = _service.Execute();
            return _resultCached;
        }
    }
}