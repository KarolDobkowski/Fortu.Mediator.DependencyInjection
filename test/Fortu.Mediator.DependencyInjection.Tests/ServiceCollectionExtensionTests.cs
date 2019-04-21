using System;
using System.Threading.Tasks;
using Fortu.Mediator.DependencyInjection.Tests.Samples;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Fortu.Mediator.DependencyInjection.Tests
{
    public class ServiceCollectionExtensionTests
    {
        [Fact]
        public void RegisterHandler_ShouldRegisterMessage()
        {
            var services = new ServiceCollection();
            services.RegisterHandler<MessageHandler>();

            var provider = services.BuildServiceProvider();
            var instance = provider.GetService<IMessageHandler<Message>>();

            Assert.IsType<MessageHandler>(instance);
        }

        [Fact]
        public async Task RegisterHandler_ShouldRegisterGenericMessage()
        {
            var services = new ServiceCollection();
            services.RegisterHandler<GenericMessageHandler>();

            var provider = services.BuildServiceProvider();
            var instance = provider.GetService<IMessageHandler<GenericMessage, bool>>();

            Assert.IsType<GenericMessageHandler>(instance);
            Assert.True(await instance.Handle(new GenericMessage()));
        }

        [Fact]
        public void RegisterHandler_WhenTypeIsNotMessageHandler_ThrowsInvalidOperationException()
        {
            var services = new ServiceCollection();
            Assert.Throws<InvalidOperationException>(() => services.RegisterHandler<EmptyClass>());
        }

        [Fact]
        public async Task RegisterHandler_ShouldRegisterAllHandlersInClass()
        {
            var services = new ServiceCollection();
            services.RegisterHandler<DoubleHandler>();

            var provider = services.BuildServiceProvider();
            var instance1 = provider.GetService<IMessageHandler<GenericMessage, bool>>();
            var instance2 = provider.GetService<IMessageHandler<Message>>();

            Assert.IsType<DoubleHandler>(instance1);
            Assert.IsType<DoubleHandler>(instance2);
            Assert.True(await instance1.Handle(new GenericMessage()));
        }

        [Fact]
        public void AddMediator_ShouldRegisterMediator()
        {
            var services = new ServiceCollection();
            services.AddMediator();

            var provider = services.BuildServiceProvider();
            var instance = provider.GetService<IMediator>();

            Assert.IsType<Mediator>(instance);
        }
    }
}
