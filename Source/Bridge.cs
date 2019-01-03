using System.Collections.Generic;

namespace Bridge
{
    public class BridgeMainApp
    {
        public static void Main()
        {
            var emailSender = new EmailSender();
            var smsSender = new EmailSender();

            var contacts = new List<ContactBase>
            {
                new CustomerContact(emailSender)
                {
                    Id = 1000,
                    Name = "Client Name"
                },
                new FriendContact(smsSender)
                {
                    Name = "John Smith"
                }
            };

            foreach (var contact in contacts)
                contact.SendMessage();
        }
    }

    public interface IMessageSender
    {
        void Send(string message);        
    }

    public class EmailSender : IMessageSender
    {
        public void Send(string message)
        {
            // Email forwarding logic...
        }
    }

    public class SMSSender : IMessageSender
    {
        public void Send(string message)
        {
            // SMS forwarding logic...
        }
    }

    public abstract class ContactBase
    {
        protected IMessageSender MessageSender { get; set; }
        abstract public void SendMessage();
    }

    public class CustomerContact : ContactBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CustomerContact(IMessageSender messageSender)
        {
            MessageSender = messageSender;
        }

        public override void SendMessage() => MessageSender.Send($"Hello customer {Name} with id {Id}.");
    }

    public class FriendContact : ContactBase
    {
        public string Name { get; set; }

        public FriendContact(IMessageSender messageSender)
        {
            MessageSender = messageSender;
        }

        public override void SendMessage() => MessageSender.Send($"Hello {Name}! How is it going?");
    }
}