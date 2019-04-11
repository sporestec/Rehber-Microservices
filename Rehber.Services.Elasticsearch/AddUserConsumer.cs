using MassTransit;
using Rehber.Model.MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rehber.Services.Elasticsearch
{
    public class AddUserConsumer : IConsumer<IEmployeeAdded>
    {
        public AddUserConsumer() { }

        public Task Consume(ConsumeContext<IEmployeeAdded> context)
        {
            return context.RespondAsync<IEmployeeAdded>(new
            {
                context.Message.Employee
            });
            //return Task.CompletedTask;
        }
    }
}
