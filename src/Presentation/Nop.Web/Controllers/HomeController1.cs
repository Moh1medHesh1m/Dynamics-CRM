using Microsoft.AspNetCore.Mvc;
using Nop.Web.Models.Common;
using Nop.Web.Services;
using Nop.Services.Localization;
using Nop.Web.Models.Customer;

namespace Nop.Web.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ICrmIntegrationService _crmIntegrationService;
        private readonly ILocalizationService _localizationService;

        public ContactUsController(ICrmIntegrationService crmIntegrationService, ILocalizationService localizationService)
        {
            _crmIntegrationService = crmIntegrationService;
            _localizationService = localizationService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterModel
            {
                CustomProperties = new Dictionary<string, object>()
            };

            // Check for temp data (success message from POST)
            if (TempData["SubmissionResult"] != null)
            {
                model.CustomProperties["SubmissionResult"] = TempData["SubmissionResult"];
            }

            return View("~/Views/Common/CrmRegister.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(string firstName, string lastName, string email)
        {
            if (await _crmIntegrationService.ContactExistsAsync(email))
            {
                var errorModel = new RegisterModel
                {
                    CustomProperties = new Dictionary<string, object>
                    {
                        ["SubmissionError"] = "A contact with this email already exists in our CRM."
                    }
                };
                return View("~/Views/Common/CrmRegister.cshtml", errorModel);
            }

            await _crmIntegrationService.CreateContactAsync(firstName, lastName, email);

            // Store success message in TempData and redirect
            TempData["SubmissionResult"] = "Submitted successfully!";
            return RedirectToAction("Register");
        }
    }
}
