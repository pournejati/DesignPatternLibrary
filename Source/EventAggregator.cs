using System;
using System.Collections.Generic;
using System.Linq;

namespace EventAggregator
{
    public static class EventAggregatorMainApp
    {
        public static void Main()
        {
            var eventAggregator = new EventAggregator();
            SubscriberA subscriber1 = new SubscriberA(eventAggregator);
            subscriber1.Subscribe();
            
            SubscriberB subscriber2 = new SubscriberB(eventAggregator);
            subscriber2.Subscribe();
                                   
            Publisher publisher1 = new Publisher(eventAggregator);
            publisher1.PublishMessage(new SampleEvent("Sample Message 1"));

            Publisher publisher2 = new Publisher(eventAggregator);
            publisher2.PublishMessage(new SampleEvent("Sample Message 2"));

            subscriber2.Unsubscribe();

            publisher1.PublishMessage(new SampleEvent("Sample Message 3"));
            publisher2.PublishMessage(new SampleEvent("Sample Message 4"));
        }
    }    

    public interface IEventAggregator
    {
        void Subscribe<TEvent>(Action<TEvent> subscriber);
        void Publish<TEvent>(TEvent publishedEvent);
        void Unsubscribe<TEvent>(Action<TEvent> subscriber);
    }

    public class EventAggregator : IEventAggregator
    {
        private Dictionary<Type, List<object>> _Subscribers = new Dictionary<Type, List<object>>();
        readonly static object _Sync = new object();

        public void Subscribe<TEvent>(Action<TEvent> subscriber)
        {
            var eventType = typeof(TEvent);
            lock (_Sync)
            { 
                if (_Subscribers.TryGetValue(eventType, out List<object> handlers))
                    handlers.Add(subscriber);
                else _Subscribers.Add(eventType, new List<object> { subscriber });
            }
        }

        public void Publish<TEvent>(TEvent publishedEvent)
        {
            var eventType = typeof(TEvent);
            lock (_Sync)
            {
                if (_Subscribers.TryGetValue(eventType, out List<object> handlers))
                    foreach (var handler in handlers.Cast<Action<TEvent>>())
                        handler?.Invoke(publishedEvent);
            }
        }        

        public void Unsubscribe<TEvent>(Action<TEvent> subscriber)
        {
            var eventType = typeof(TEvent);
            lock (_Sync)
            {
                if (_Subscribers.TryGetValue(eventType, out List<object> handlers) && handlers.Contains(subscriber))
                    handlers.Remove(subscriber);
            }                
        }
    }

    public class SampleEvent
    {
        public string Message { get; set; }
        public SampleEvent(string message) => Message = message;
    }

    public interface IPublisher<TEvent>
    {
        void PublishMessage(TEvent eventToPublish);
    }

    public class Publisher : IPublisher<SampleEvent>
    {
        private IEventAggregator _EventAggregator;
        public Publisher(IEventAggregator eventAggregator) => _EventAggregator = eventAggregator;
        public void PublishMessage(SampleEvent eventToPublish) => _EventAggregator.Publish(eventToPublish);
    }

    public interface ISubscriber<TEvent>
    {
        void Subscribe();
        void Unsubscribe();
    }

    public class SubscriberA : ISubscriber<SampleEvent>
    {
        private IEventAggregator _EventAggregator;
        public SubscriberA(IEventAggregator eve) => _EventAggregator = eve;
        public void Subscribe() => _EventAggregator.Subscribe<SampleEvent>(Subscriber);
        public void Unsubscribe() => _EventAggregator.Unsubscribe<SampleEvent>(Subscriber);

        private static void Subscriber(SampleEvent publishedEvent) => Console.WriteLine("S1: {0}", publishedEvent.Message);
    }

    public class SubscriberB : ISubscriber<SampleEvent>
    {
        private IEventAggregator _EventAggregator;
        public SubscriberB(IEventAggregator eve) => _EventAggregator = eve;
        public void Subscribe() => _EventAggregator.Subscribe<SampleEvent>(Subscriber);
        public void Unsubscribe() => _EventAggregator.Unsubscribe<SampleEvent>(Subscriber);

        private static void Subscriber(SampleEvent publishedEvent) => Console.WriteLine("S2: {0}", publishedEvent.Message);
    }
}