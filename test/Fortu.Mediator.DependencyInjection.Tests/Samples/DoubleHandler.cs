using System.Threading.Tasks;

namespace Fortu.Mediator.DependencyInjection.Tests.Samples
{
    public class DoubleHandler : IMessageHandler<Message>, IMessageHandler<GenericMessage, bool>
    {
        public Task Handle(Message message)
            => Task.CompletedTask;

        public Task<bool> Handle(GenericMessage message)
            => Task.FromResult(true);
    }
}
