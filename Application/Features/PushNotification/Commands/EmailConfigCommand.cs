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
    public class EmailConfigCommand : IRequest<EmailConfigurationList>
    {
        public EmailConfigDTO emailConfigDTO { get; set; }
    }

    internal class EmailConfigHandler : IRequestHandler<EmailConfigCommand, EmailConfigurationList>
    {
        protected readonly IEmailConfig _email;
        public EmailConfigHandler(IEmailConfig email)
        {
            _email = email;
        }

        public async Task<EmailConfigurationList> Handle(EmailConfigCommand request, CancellationToken cancellationToken)
        {
            return await _email.GetEmailConfigurationList(request.emailConfigDTO);
        }



    }
}
