using MassTransit;
using Rehber.Model.MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rehber.Services.Elasticsearch.MasstransitConsumers
{
    public class EmployeeUpdatedConsumer : IConsumer<IEmployeeUpdated>
    {
        public EmployeeUpdatedConsumer() { }

        public Task Consume(ConsumeContext<IEmployeeUpdated> context)
        {
            ElasticsearchClient elastic = new ElasticsearchClient();
            elastic.UpdateEmployee(context.Message.Employee);
            return Task.CompletedTask;
        }
    }
}
