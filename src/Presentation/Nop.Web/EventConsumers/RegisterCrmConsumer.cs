using Nop.Core.Domain.Customers;
using Nop.Services.Events;
using Nop.Web.Services;
using System.Threading.Tasks;

namespace Nop.Web.EventConsumers
{
    public class RegisterCrmConsumer : IConsumer<CustomerRegisteredEvent>
    {
        private readonly ICrmIntegrationService _crm;

        public RegisterCrmConsumer(ICrmIntegrationService crm)
        {
            _crm = crm;
        }

        public async Task HandleEventAsync(CustomerRegisteredEvent eventMessage)
        {
            var customer = eventMessage.Customer;

            if (!await _crm.ContactExistsAsync(customer.Email))
            {
                await _crm.CreateContactAsync(customer.FirstName, customer.LastName, customer.Email);
            }
        }
    }
}
