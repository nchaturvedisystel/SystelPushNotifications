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
    public class UpdateStatusWhatsAppConfigCommand : IRequest<WhatsAppConfigDTO>
    {
        public WhatsAppConfigDTO whatsAppConfigDTO { get; set; }
    }

    internal class UpdateStatusWhatsAppConfigHandler : IRequestHandler<UpdateStatusWhatsAppConfigCommand, WhatsAppConfigDTO>

    {
        protected readonly IWhatsAppConfig _whatsApp;
        public UpdateStatusWhatsAppConfigHandler(IWhatsAppConfig whatsApp)
        {
            _whatsApp = whatsApp;
        }

        public async Task<WhatsAppConfigDTO> Handle(UpdateStatusWhatsAppConfigCommand request, CancellationToken cancellationToken)
        {
            return await _whatsApp.WhatsAppConfigurationListStatusUpdate(request.whatsAppConfigDTO);
        }

    }

}
