using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace Fortu.Mediator.DependencyInjection.Tests
{
    public class UnitTest1
    {
        public class A : IMessage
        {

        }

        public class AH : IMessageHandler<A>
        {
            public async Task Handle(A message)
            {
                await Task.CompletedTask;
            }
        }

        [Fact]
        public void Test1()
        {
            var aa = typeof(AH);
            var services = new ServiceCollection();
            services.RegisterHandler<AH>();

            var provider = services.BuildServiceProvider();
            var got = provider.GetService<IMessageHandler<A>>();

            Assert.IsType<AH>(got as AH);
        }

        public class B : IMessage<int>
        {

        }

        public class BH : IMessageHandler<B, int>
        {
            public async Task<int> Handle(B message)
            {
                await Task.CompletedTask;
                return 3;
            }
        }

        [Fact]
        public async Task Test2()
        {
            var aa = typeof(AH);
            var services = new ServiceCollection();
            services.RegisterHandler<BH>();

            var provider = services.BuildServiceProvider();
            var got = provider.GetService<IMessageHandler<B, int>>() as BH;

            Assert.Equal(3, await got.Handle(new B()));

            Assert.IsType<BH>(got);
        }
    }
}
