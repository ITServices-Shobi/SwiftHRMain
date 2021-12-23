using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SwiftHR.Models;
using SwiftHR.Utility;
using System.IO;
using System.Collections;

namespace SwiftHR.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();

        public CompanyController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            CompanyMaster companyMaster = new CompanyMaster();
            return View("Index", companyMaster);
        }

        [ValidateAntiForgeryToken]
        public ActionResult AddCompanyDetails(IFormCollection collection)
        {
            try
            {
                CompanyMaster companyMaster = new CompanyMaster();



                ViewBag.Message = companyMaster.AddCompanyMaster(collection);
                
                return View("Index", companyMaster);
                //return RedirectToAction("AddPolicy", "AttandancePolicy");
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return RedirectToAction("Index", "companyMaster");
            }
        }
    }
}
