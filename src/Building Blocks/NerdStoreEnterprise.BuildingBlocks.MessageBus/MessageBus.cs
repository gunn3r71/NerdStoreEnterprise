using System;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Internals;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages.IntegrationEvents;
using Polly;
using RabbitMQ.Client.Exceptions;

namespace NerdStoreEnterprise.BuildingBlocks.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private IBus _bus;
        private readonly string _connectionString;
        
        public MessageBus(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool IsConnected => _bus?.Advanced.IsConnected ?? false;

        public void Publish<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            _bus.PubSub.Publish(message);
        }

        public async Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            await _bus.PubSub.PublishAsync(message);
        }

        public void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class
        {
            TryConnect();
            _bus.PubSub.SubscribeAsync(subscriptionId, onMessage);
        }

        public void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class
        {
            TryConnect();
            _bus.PubSub.SubscribeAsync(subscriptionId, onMessage);
        }

        public TResponse Request<TRequest, TResponse>(TRequest request) where TRequest : IntegrationEvent where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Rpc.Request<TRequest, TResponse>(request);
        }

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request) where TRequest : IntegrationEvent where TResponse : ResponseMessage
        {
            TryConnect();
            return await _bus.Rpc.RequestAsync<TRequest, TResponse>(request);
        }

        public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> respond)
        {
            TryConnect();
            return _bus.Rpc.Respond(respond);
        }

        public AwaitableDisposable<IDisposable> RespondAsync<TRequest, TResponse>(
            Func<CreatedUserIntegrationEvent, Task<ResponseMessage>> respond)
        {
            TryConnect();
            return _bus.Rpc.RespondAsync(respond);
        }
        
        private void TryConnect()
        {
            if (IsConnected) return;

            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(() =>  _bus = RabbitHutch.CreateBus(_connectionString));
        }

        public void Dispose() => _bus?.Dispose();
    }
}