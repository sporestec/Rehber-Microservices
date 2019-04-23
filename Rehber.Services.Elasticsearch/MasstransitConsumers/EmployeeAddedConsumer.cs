using MassTransit;
using Rehber.Model.MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rehber.Services.Elasticsearch.MasstransitConsumers
{
    public class EmployeeAddedConsumer : IConsumer<IEmployeeAdded>
    {
        public EmployeeAddedConsumer() { }

        public Task Consume(ConsumeContext<IEmployeeAdded> context)
        {
            ElasticsearchClient elastic = new ElasticsearchClient();
            elastic.IndexEmployee(context.Message.Employee);
            return Task.CompletedTask;
        }
    }
}
