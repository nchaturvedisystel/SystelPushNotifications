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
    public class WhatsAppConfigCommand : IRequest<WhatsAppConfigurationList>
    {
        public WhatsAppConfigDTO whatsAppConfigDTO { get; set; }
    }

    internal class WhatsAppConfigHandler : IRequestHandler<WhatsAppConfigCommand, WhatsAppConfigurationList>
    {
        protected readonly IWhatsAppConfig _whatsApp;
        public WhatsAppConfigHandler(IWhatsAppConfig whatsApp)
        {
            _whatsApp = whatsApp;
        }

        public async Task<WhatsAppConfigurationList> Handle(WhatsAppConfigCommand request, CancellationToken cancellationToken)
        {
            return await _whatsApp.GetWhatsAppConfigurationList(request.whatsAppConfigDTO);
        }



    }

}
