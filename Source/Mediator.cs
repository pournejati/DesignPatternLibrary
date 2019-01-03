using System.Collections.Generic;

namespace Mediator
{
    public class MediatorMainApp
    {
        public static void Main()
        {
            var messageMediator = new Mediator();

            var participant1 = new PublicParticipant();
            var participant2 = new PublicParticipant();
            var participant3 = new PrivateParticipant();
            var participant4 = new PrivateParticipant();

            messageMediator.AddParticipant(participant1);
            messageMediator.AddParticipant(participant2);
            messageMediator.AddParticipant(participant3);
            messageMediator.AddParticipant(participant4);

            participant1.BroadcastMessage("Hi All!");
            participant2.BroadcastMessage("All you need is love!");
            participant3.BroadcastMessage("Can't buy me love...");
            participant4.BroadcastMessage("My sweet love.");
        }
    }

    public interface IParticipant
    {
        IMediator Mediator { get; set; }
        void BroadcastMessage(string message);
    }

    public class PublicParticipant : IParticipant
    {
        public IMediator Mediator { get; set; }
        public void BroadcastMessage(string message) => Mediator.BroadcastMessage($"Public participant: {message}");
    }

    public class PrivateParticipant : IParticipant
    {
        public IMediator Mediator { get; set; }
        public void BroadcastMessage(string message) => Mediator.BroadcastMessage(message);
    }

    public interface IMediator
    {
        void AddParticipant(IParticipant participant);
        void BroadcastMessage(string message);
    }

    public class Mediator : IMediator
    {
        private List<IParticipant> _participants = new List<IParticipant>();

        public void AddParticipant(IParticipant participant)
        {
            if (!_participants.Contains(participant))
                _participants.Add(participant);
            participant.Mediator = this;
        }

        public void BroadcastMessage(string message) => _participants.ForEach(participant => participant.BroadcastMessage(message));
    }
}