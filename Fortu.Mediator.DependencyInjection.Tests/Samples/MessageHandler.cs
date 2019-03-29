using System.Threading.Tasks;

namespace Fortu.Mediator.DependencyInjection.Tests.Samples
{
    public class MessageHandler : IMessageHandler<Message>
    {
        public Task Handle(Message message)
            => Task.CompletedTask;
    }
}
