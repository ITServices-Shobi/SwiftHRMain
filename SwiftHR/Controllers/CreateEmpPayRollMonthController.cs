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
using System.Web.WebPages.Html;
using System.Data;

namespace SwiftHR.Controllers
{
    public class CreateEmpPayRollMonthController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();

        public CreateEmpPayRollMonthController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            CreateEmpPayRollMonthU EmpCreateMonthPayRollMaster = new CreateEmpPayRollMonthU();

            EmpCreateMonthPayRollMaster = GetMonthName();
            return View("Index", EmpCreateMonthPayRollMaster);
        }

        private CreateEmpPayRollMonthU GetMonthName()
        {
            CreateEmpPayRollMonthU MonthNameList = new CreateEmpPayRollMonthU();

            List<LookUpDetailsM> LoanEariningList = new List<LookUpDetailsM>();
            LoanEariningList = _context.LookUpDetailsM.Where(e => e.LookUpId == Convert.ToInt32("5")).ToList();

            MonthNameList.MonthNameList = LoanEariningList;

            return MonthNameList;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCreatePayRollMonth(IFormCollection collection)
        {
            try
            {
                CreateEmpPayRollMonthU EmpCreatePayRollMonthMaster = new CreateEmpPayRollMonthU();

                UspEmpReimbursementDetailsViewModel ABC = new UspEmpReimbursementDetailsViewModel();
                ViewBag.Message = EmpCreatePayRollMonthMaster.AddEmpPayRollMonthDetails(collection);
                EmpCreatePayRollMonthMaster = GetMonthName();

                var EmployeeId = "";
                var ReimbId = "";
                //EmpCreatePayRollMonthMaster = GetEmpReimbursementDetailsSP(EmployeeId, ReimbId);

                return View("Index", EmpCreatePayRollMonthMaster);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return RedirectToAction("Index", "EmpReimbursementU");
            }
        }
    }
}
