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
    [Route("ServiceVariables")]
    //[AuthorizeUser]
    public class AlertsServiceVariablesController : BaseApiController
    {
        APISettings _settings;
        public AlertsServiceVariablesController(IOptions<APISettings> settings)
        {
            _settings = settings.Value;
        }

        [HttpPost("GetServiceVariables")]
        public async Task<IActionResult> AlertsServiceVariablesList([FromBody] AlertsServiceVariablesDTO alertsServiceVariablesDTO)
        {

            AlertsServiceVariablesList response = new AlertsServiceVariablesList();
            response = await mediator.Send(new AlertsServiceVariablesCommand
            {
                alertsServiceVariablesDTO = alertsServiceVariablesDTO
            });

            if (response == null)
                return Ok(APIResponse<string>.Unauthorized("Please check login credentials"));

            return Ok(response);
        }

        [HttpPost("UpdateStatusServiceVariables")]
        public async Task<IActionResult> UpdateStatusServiceVariables([FromBody] AlertsServiceVariablesDTO alertsServiceVariablesDTO)
        {
            AlertsServiceVariablesDTO response = new AlertsServiceVariablesDTO();
            response = await mediator.Send(new UpdateStatusServiceVariableCommand
            {
                alertsServiceVariablesDTO = alertsServiceVariablesDTO
            });
            return Ok(response);
        }
    }
}
