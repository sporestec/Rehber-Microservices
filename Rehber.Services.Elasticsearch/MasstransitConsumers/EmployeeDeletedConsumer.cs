using MassTransit;
using Rehber.Model.MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rehber.Services.Elasticsearch.MasstransitConsumers
{
    public class EmployeeDeletedConsumer : IConsumer<IEmployeeDeleted>
    {
        public EmployeeDeletedConsumer() { }

        public Task Consume(ConsumeContext<IEmployeeDeleted> context)
        {
            ElasticsearchClient elastic = new ElasticsearchClient();
            elastic.DeleteEmployee(context.Message.EmployeeId);
            return Task.CompletedTask;
        }
    }
}
