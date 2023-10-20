using Application.DTOs.PushNotification;
using Application.Features.PushNotification.Commands;
using Application;
using Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Application.DTOs.User;
using Microsoft.AspNetCore.Hosting;

namespace WebAPI.Controllers.PushNotification
{
    [Route("Service")]
    //[AuthorizeUser]
    public class ServiceMasterController : BaseApiController
    {
        APISettings _settings;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ServiceMasterController(IOptions<APISettings> settings, IWebHostEnvironment webHostEnvironment)
        {
            _settings = settings.Value;
            _webHostEnvironment = webHostEnvironment;
            SessionObj.WebRootPath = _webHostEnvironment.ContentRootPath;
        }

        [HttpPost("GetService")]
        public async Task<IActionResult> GetServiceMasterList([FromBody] ServiceMasterDTO serviceMasterDTO)
        {
            serviceMasterDTO.WebRootPath = SessionObj.WebRootPath;

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
