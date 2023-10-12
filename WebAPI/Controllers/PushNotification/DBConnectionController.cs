using Application.DTOs.PushNotification;
using Application.Features.PushNotification.Commands;
using Application;
using Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Application.DTOs.User;
using Application.Features.Users.Commands;

namespace WebAPI.Controllers.PushNotification
{
    [Route("DBConnection")]
    //[AuthorizeUser]
    public class DBConnectionController : BaseApiController
    {
        APISettings _settings;
        public DBConnectionController(IOptions<APISettings> settings)
        {
            _settings = settings.Value;
        }

        [HttpPost("GetDBConnection")]
        public async Task<IActionResult> GetDBConnectionList([FromBody] DBConnectionDTO dbConnectionDTO)
        {

            DBConnectionList response = new DBConnectionList();
            response = await mediator.Send(new DBConnectionCommand
            {
                dBConnectionDTO = dbConnectionDTO
            });

            if (response == null)
                return Ok(APIResponse<string>.Unauthorized("Please check login credentials"));

            return Ok(response);
        }


        [HttpPost("UpdateStatusDBConnection")]
        public async Task<IActionResult> UpdateStatusDBConnection([FromBody] DBConnectionDTO dbConnectionDTO)
        {
            DBConnectionDTO response = new DBConnectionDTO();
            response = await mediator.Send(new UpdateStatusDBConnCommand
            {
                dBConnectionDTO = dbConnectionDTO
            });
            return Ok(response);
        }


    }
}
