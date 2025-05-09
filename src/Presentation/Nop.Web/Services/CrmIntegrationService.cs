using Microsoft.Extensions.Options;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Nop.Web.Configuration;
using System;
using System.Threading.Tasks;

namespace Nop.Web.Services
{
    public interface ICrmIntegrationService
    {
        Task<bool> ContactExistsAsync(string email);
        Task CreateContactAsync(string firstName, string lastName, string email);
    }

    public class CrmIntegrationService : ICrmIntegrationService
    {
        private readonly ServiceClient _client;

        public CrmIntegrationService(IOptions<CrmSettings> options)
        {
            _client = new ServiceClient(options.Value.DataverseConnectionString);


        }

        public async Task<bool> ContactExistsAsync(string email)
        {

            if (string.IsNullOrWhiteSpace(email))
                return false; 

            var query = new QueryExpression("contact")
            {
                ColumnSet = new ColumnSet("contactid"),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("emailaddress1", ConditionOperator.Equal, email)
                    }
                }
            };

            var result = await _client.RetrieveMultipleAsync(query);
            return result.Entities.Count > 0;
        }

        public async Task CreateContactAsync(string firstName, string lastName, string email)
        {

            var contact = new Entity("contact")
            {
                ["firstname"] = firstName,
                ["lastname"] = lastName,
                ["emailaddress1"] = email
            };

            await _client.CreateAsync(contact);
        }
    }
}
