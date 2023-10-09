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
    [Route("Schedular")]
    //[AuthorizeUser]
    public class SchedularMasterController : BaseApiController
    {
        APISettings _settings;
        public SchedularMasterController(IOptions<APISettings> settings)
        {
            _settings = settings.Value;
        }

        [HttpPost("GetSchedular")]
        public async Task<IActionResult> GetSchedularMasterList([FromBody] SchedularMasterDTO schedularMasterDTO)
        {

            SchedularMasterList response = new SchedularMasterList();
            response = await mediator.Send(new SchedularMasterCommand
            {
                schedularMasterDTO = schedularMasterDTO
            });

            if (response == null)
                return Ok(APIResponse<string>.Unauthorized("Please check login credentials"));

            return Ok(response);
        }


        [HttpPost("UpdateStatusSchedular")]
        public async Task<IActionResult> UpdateStatusSchedularMaster([FromBody] SchedularMasterDTO schedularMasterDTO)
        {
            SchedularMasterDTO response = new SchedularMasterDTO();
            response = await mediator.Send(new UpdateStatusScheMasterCommand
            {
                schedularMasterDTO = schedularMasterDTO
            });
            return Ok(response);
        }
    }
}
