using Application.DTOs.PushNotification;
using Application.Interfaces.PushNotification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PushNotification.Commands
{
    public class UpdateStatusEmailConfigCommand : IRequest<EmailConfigDTO>
    {
        public EmailConfigDTO emailConfigDTO { get; set; }
    }
    internal class UpdateStatusEmailConfigHandler : IRequestHandler<UpdateStatusEmailConfigCommand, EmailConfigDTO>
    
    {
        protected readonly IEmailConfig _email;
        public UpdateStatusEmailConfigHandler(IEmailConfig email)
        {
            _email = email;
        }

        public async Task<EmailConfigDTO> Handle(UpdateStatusEmailConfigCommand request, CancellationToken cancellationToken)
        {
            return await _email.EmailConfigurationListStatusUpdate(request.emailConfigDTO);
        }

    }
}
