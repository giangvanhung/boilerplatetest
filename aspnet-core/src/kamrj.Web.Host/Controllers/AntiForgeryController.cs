using System.Threading.Tasks;
using Abp.Web.Security.AntiForgery;
using Microsoft.AspNetCore.Antiforgery;
using kamrj.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace kamrj.Web.Host.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AntiForgeryController : kamrjControllerBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly IAntiforgery _antiforgery;
        private readonly IAbpAntiForgeryManager _antiForgeryManager;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public AntiForgeryController(IAntiforgery antiforgery, IAbpAntiForgeryManager antiForgeryManager)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _antiforgery = antiforgery;
            _antiForgeryManager = antiForgeryManager;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public void GetToken()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public void SetCookie()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _antiForgeryManager.SetCookie(HttpContext);
        }
    }
}
