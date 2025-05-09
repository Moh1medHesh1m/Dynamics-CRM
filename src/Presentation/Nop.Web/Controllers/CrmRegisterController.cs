using Microsoft.AspNetCore.Mvc;
using Nop.Web.Models.Customer;
using Nop.Web.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Nop.Web.Controllers
{
    public class CrmRegisterController : BasePublicController
    {
        private readonly ICrmIntegrationService _crmService;

        public CrmRegisterController(ICrmIntegrationService crmService)
        {
            _crmService = crmService;
        }

        [HttpGet]
        public IActionResult CrmRegister()
        {
            var model = new RegisterModel
            {
                CustomProperties = new Dictionary<string, object>()
            };

            if (TempData.ContainsKey("SuccessMessage"))
            {
                model.CustomProperties["SubmissionResult"] = TempData["SuccessMessage"];
            }

            if (TempData.ContainsKey("SubmissionError"))
            {
                model.CustomProperties["SubmissionError"] = TempData["SubmissionError"];
                TempData.Remove("SubmissionError");
            }

            return View("~/Views/Common/CrmRegister.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(string firstName, string lastName, string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                TempData["SubmissionError"] = "Email is required.";
                return RedirectToAction("CrmRegister");
            }

            if (await _crmService.ContactExistsAsync(email))
            {
                TempData["SubmissionError"] = "A contact with this email already exists.";
                return RedirectToAction("CrmRegister");
            }

            await _crmService.CreateContactAsync(firstName, lastName, email);
            TempData["SuccessMessage"] = "Thank you! Your form has been submitted successfully.";

            return RedirectToAction("CrmRegister");
        }
    }
}