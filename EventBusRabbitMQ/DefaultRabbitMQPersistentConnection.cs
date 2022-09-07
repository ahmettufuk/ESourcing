using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace EventBusRabbitMQ
{
    public class DefaultRabbitMqPersistentConnection:IRabbitMqPersistentConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private  IConnection _connection;
        private readonly  

       

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool IsConnected { get; }
        public bool TryConnect()
        {
            throw new NotImplementedException();
        }

        public IModel CreateModel()
        {
            throw new NotImplementedException();
        }
    }
}
