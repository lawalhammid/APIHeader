using Microsoft.AspNetCore.Mvc;
using MobileInternetBankingWebAPI.BAL.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIHeaderImplementation.Controllers
{
  //  [APIKey]
    public class HeaderApiController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
