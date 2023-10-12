using Application.DTOs.PushNotification;
using Application.Features.PushNotification.Commands;
using Application;
using Domain.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace WebAPI.Controllers.PushNotification
{
    [Route("EmailConfig")]
    //[AuthorizeUser]
    public class EmailConfigController : BaseApiController
    {
        APISettings _settings;
        public EmailConfigController(IOptions<APISettings> settings)
        {
            _settings = settings.Value;
        }

        [HttpPost("GetEmailConfig")]
        public async Task<IActionResult> GetEmailConfigurationList([FromBody] EmailConfigDTO emailConfigDTO)
        {

            EmailConfigurationList response = new EmailConfigurationList();
            response = await mediator.Send(new EmailConfigCommand
            {
                emailConfigDTO = emailConfigDTO
            });

            if (response == null)
                return Ok(APIResponse<string>.Unauthorized("Please check login credentials"));

            return Ok(response);
        }


        [HttpPost("UpdateStatusEmailConfig")]
        public async Task<IActionResult> EmailConfigurationListStatusUpdate([FromBody] EmailConfigDTO emailConfigDTO)
        {
            EmailConfigDTO response = new EmailConfigDTO();
            response = await mediator.Send(new UpdateStatusEmailConfigCommand
            {
                emailConfigDTO = emailConfigDTO
            });
            return Ok(response);
        }

    }
}
