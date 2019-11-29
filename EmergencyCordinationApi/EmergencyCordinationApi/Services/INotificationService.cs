using EmergencyCordinationApi.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmergencyCordinationApi.Services
{
    public interface INotificationService
    {
         void NotifyAll(NotificationViewModel data);
        void NotifyAll(NotificationType type);
    }
    public class NotificationService: INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void NotifyAll(NotificationViewModel data)
        {
            _hubContext.Clients.All.SendAsync("notify", data);
        }
        public void NotifyAll(NotificationType type)
        {
            _hubContext.Clients.All.SendAsync("notify", new NotificationViewModel { Type = type });
        }
    }

    public class NotificationViewModel
    {
        public NotificationType Type { get; set; }
    }
    public enum NotificationType
    {
        EventUpdate=1,
        ShelterUpdate=2
    }
}
