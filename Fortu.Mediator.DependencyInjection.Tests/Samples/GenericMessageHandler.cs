using System.Threading.Tasks;

namespace Fortu.Mediator.DependencyInjection.Tests.Samples
{
    public class GenericMessageHandler : IMessageHandler<GenericMessage, bool>
    {
        public Task<bool> Handle(GenericMessage message)
            => Task.FromResult(true);
    }
}
