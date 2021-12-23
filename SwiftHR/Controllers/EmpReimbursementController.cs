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


namespace SwiftHR.Controllers
{
    public class EmpReimbursementController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();

        public EmpReimbursementController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }
        public IActionResult Index()
        {

            EmpReimbursementU EmpReimbursementMaster = new EmpReimbursementU();

            List<EmpReimbursement> ReimbursementDetailsList = new List<EmpReimbursement>();

            ReimbursementDetailsList = _context.EmpReimbursement.Where(x => x.IsActive == false).ToList();
            ViewBag.ReimbursementList = ReimbursementDetailsList;

            return View("Index", EmpReimbursementMaster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddEmpReimbursementDetails(IFormCollection collection)
        {
            try
            {
                EmpReimbursementU ReimbursementMaster = new EmpReimbursementU();

                ViewBag.Message = ReimbursementMaster.AddReimbursementDetails(collection);

                List<EmpReimbursement> ReimbursementList = new List<EmpReimbursement>();

                ReimbursementList = _context.EmpReimbursement.Where(x => x.IsActive == false).ToList();
                ViewBag.ReimbursementList = ReimbursementList;

                return View("Index", ReimbursementMaster);

                //return RedirectToAction("AddPolicy", "AttandancePolicy");
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return RedirectToAction("Index", "EmpReimbursementU");
            }
        }
        public ActionResult SearchReimbursementRecord(string EMPReimbID)
        {
            EmpReimbursement ReimbursementMaster = new EmpReimbursement();
            //var result = policyMaster.EditPolicyMaster(Convert.ToInt32(policyId));
            var result = (from a in _context.EmpReimbursement
                          where a.Id == Convert.ToInt32(EMPReimbID)
                          select new {
                              Id = a.Id,
                              EmployeeId = a.EmployeeId,
                              EmployeeNumber = a.EmployeeNumber,
                              EmployeeName = a.EmployeeName,
                              Date = string.Format("{0:yyyy-MM-dd}", a.Date),
                                       Amount = a.Amount,
                              Remarks = a.Remarks

                          }).ToList();

            return new JsonResult(result);
            //ViewBag.Message = result;

            //return View("AddPolicy", policyMaster);
        }

        [HttpPost("ReimbursementDelete")]
        public ActionResult ReimbursementDelete(string EMPReimbID)
        {
            EmpReimbursementU ReimbursementMaster = new EmpReimbursementU();

            EmpReimbursementU ReimbursementList = new EmpReimbursementU();
            ViewBag.WarningMessage = ReimbursementList.DeleteReimbursementRecord(Convert.ToInt32(EMPReimbID));

            List<EmpReimbursement> ReimbursementDetailsList = new List<EmpReimbursement>();

            ReimbursementDetailsList = _context.EmpReimbursement.Where(x => x.IsActive == false).ToList();
            ViewBag.ReimbursementList = ReimbursementDetailsList;
            return View("Index", ReimbursementMaster);

        }

    }
}
