using MassTransit;
using Rehber.Model.MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rehber.Services.Elasticsearch.MasstransitConsumers
{
    public class UserAddedConsumer : IConsumer<IEmployeeAdded>
    {
        public UserAddedConsumer() { }

        public Task Consume(ConsumeContext<IEmployeeAdded> context)
        {
            Elastic elastic = new Elastic();
            elastic.IndexEmployee(context.Message.Employee);
            return Task.CompletedTask;
        }
    }
}
