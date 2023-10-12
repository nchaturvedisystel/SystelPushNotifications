using Application.DTOs.PushNotification;
using Application.Features.PushNotification.Commands;
using Application;
using Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace WebAPI.Controllers.PushNotification
{
    [Route("ServiceSchedular")]
    //[AuthorizeUser]
    public class ServiceSchedularController : BaseApiController
    {
        APISettings _settings;
        public ServiceSchedularController(IOptions<APISettings> settings)
        {
            _settings = settings.Value;
        }

        [HttpPost("GetServiceSchedular")]
        public async Task<IActionResult> GetServiceSchedularList([FromBody] ServiceSchedularDTO serviceSchedularDTO)
        {

            ServiceSchedularList response = new ServiceSchedularList();
            response = await mediator.Send(new ServiceSchedularCommand
            {
                serviceSchedularDTO = serviceSchedularDTO
            });

            if (response == null)
                return Ok(APIResponse<string>.Unauthorized("Please check login credentials"));

            return Ok(response);
        }


        [HttpPost("UpdateStatusServiceSchedular")]
        public async Task<IActionResult> UpdateStatusServiceSchedular([FromBody] ServiceSchedularDTO serviceSchedularDTO)
        {
            ServiceSchedularDTO response = new ServiceSchedularDTO();
            response = await mediator.Send(new UpdateStatusServiceSchedularCommand
            {
                serviceSchedularDTO = serviceSchedularDTO
            });
            return Ok(response);
        }
    }
}
