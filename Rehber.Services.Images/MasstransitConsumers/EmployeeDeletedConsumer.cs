using MassTransit;
using Rehber.Data.DBContexts;
using Rehber.Model.MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rehber.Services.ImagesApi.MasstransitConsumers
{
    public class EmployeeDeletedConsumer : IConsumer<IEmployeeDeleted>
    {
        public EmployeeDeletedConsumer() { }

        public Task Consume(ConsumeContext<IEmployeeDeleted> context)
        {
            if (context.Message.EmployeeId > 0)
            {
                var _context = new RehberImageServisContext();
                var userImages = _context.UserImages.Where(r => r.EmployeeId == context.Message.EmployeeId).SingleOrDefault();
                if (userImages != null)
                {
                    _context.UserImages.Remove(userImages);
                    _context.SaveChangesAsync();
                }
            }
            return Task.CompletedTask;
        }
    }
}