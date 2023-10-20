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
    [Route("WhatsAppConfig")]
    //[AuthorizeUser]
    public class WhatsAppConfigController : BaseApiController
    {
        APISettings _settings;

        public WhatsAppConfigController(IOptions<APISettings> settings)
        {
            _settings = settings.Value;
        }

        [HttpPost("GetWhatsAppConfig")]
        public async Task<IActionResult> GetWhatsAppConfigurationList([FromBody] WhatsAppConfigDTO whatsAppConfigDTO)
        {

            WhatsAppConfigurationList response = new WhatsAppConfigurationList();
            response = await mediator.Send(new WhatsAppConfigCommand
            {
                whatsAppConfigDTO = whatsAppConfigDTO
            });

            if (response == null)
                return Ok(APIResponse<string>.Unauthorized("Please check login credentials"));

            return Ok(response);
        }


        [HttpPost("UpdateStatusWhatsAppConfig")]
        public async Task<IActionResult> WhatsAppConfigurationListStatusUpdate([FromBody] WhatsAppConfigDTO whatsAppConfigDTO)
        {
            WhatsAppConfigDTO response = new WhatsAppConfigDTO();
            response = await mediator.Send(new UpdateStatusWhatsAppConfigCommand
            {
                whatsAppConfigDTO = whatsAppConfigDTO
            });
            return Ok(response);
        }
    }
}
