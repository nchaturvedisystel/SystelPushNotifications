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
    [Route("Service")]
    //[AuthorizeUser]
    public class ServiceMasterController : BaseApiController
    {
        APISettings _settings;
        public ServiceMasterController(IOptions<APISettings> settings)
        {
            _settings = settings.Value;
        }

        [HttpPost("GetService")]
        public async Task<IActionResult> GetServiceMasterList([FromBody] ServiceMasterDTO serviceMasterDTO)
        {

            ServiceMasterList response = new ServiceMasterList();
            response = await mediator.Send(new ServiceMasterCommand
            {
                serviceMasterDTO = serviceMasterDTO
            });

            if (response == null)
                return Ok(APIResponse<string>.Unauthorized("Please check login credentials"));

            return Ok(response);
        }


        [HttpPost("UpdateStatusService")]
        public async Task<IActionResult> UpdateStatusServiceMaster([FromBody] ServiceMasterDTO serviceMasterDTO)
        {
            ServiceMasterDTO response = new ServiceMasterDTO();
            response = await mediator.Send(new UpdateStatusServiceMasterCommand
            {
                serviceMasterDTO = serviceMasterDTO
            });
            return Ok(response);
        }
    }
}
