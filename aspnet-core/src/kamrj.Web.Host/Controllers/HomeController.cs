using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp;
using Abp.Extensions;
using Abp.Notifications;
using Abp.Timing;
using Abp.Web.Security.AntiForgery;
using kamrj.Controllers;

namespace kamrj.Web.Host.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class HomeController : kamrjControllerBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly INotificationPublisher _notificationPublisher;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public HomeController(INotificationPublisher notificationPublisher)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _notificationPublisher = notificationPublisher;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IActionResult Index()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return Redirect("/swagger");
        }

        /// <summary>
        /// This is a demo code to demonstrate sending notification to default tenant admin and host admin uers.
        /// Don't use this code in production !!!
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<ActionResult> TestNotification(string message = "")
        {
            if (message.IsNullOrEmpty())
            {
                message = "This is a test notification, created at " + Clock.Now;
            }

            var defaultTenantAdmin = new UserIdentifier(1, 2);
            var hostAdmin = new UserIdentifier(null, 1);

            await _notificationPublisher.PublishAsync(
                "App.SimpleMessage",
                new MessageNotificationData(message),
                severity: NotificationSeverity.Info,
                userIds: new[] { defaultTenantAdmin, hostAdmin }
            );

            return Content("Sent notification: " + message);
        }
    }
}
