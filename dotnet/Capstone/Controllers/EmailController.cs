using Capstone.Models;
using Capstone.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class EmailController : ControllerBase
    {
        private readonly ICampDao camp;

        public EmailController(ICampDao camp)
        {
            this.camp = camp;
        }


        [HttpPost]
        public ActionResult SendEmail(Email email)
        {
            // In a production environment, the following API key would be stored in a secure location
            // such as an Enviornment Variable or in Web.config.  
            // private readonly string apiKey = "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";

            // Example of setting & getting the api key from an Environment Variable
            //Environment.SetEnvironmentVariable("SENDGRID_API_KEY", "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");
            //var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");




            var client = new SendGridClient("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");
            var from = new EmailAddress("TEOscar15@gmail.com", "Team Oscar");
            var subject = email.Subject;
            var plainTextContent = email.Body;
            var htmlContent = "<strong>Your friends at Camp Oscar</strong>";


            foreach (int familyId in email.Recipients)
            {
                Family family = camp.FetchFamily(familyId);
                var to = new EmailAddress(family.EmailAddress, family.FullName);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                try
                {
                    var response = client.SendEmailAsync(msg).ConfigureAwait(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return Ok();
        }
    }
}
