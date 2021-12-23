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
    public class EmpLOPDetailsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();

        public EmpLOPDetailsController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            EmpLOPDetailsU EmpLOPDetailsMaster= new EmpLOPDetailsU();

            List<EmpLOPDetails> LOPDetailsList = new List<EmpLOPDetails>();

            LOPDetailsList = _context.EmpLOPDetails.Where(x => x.IsActive == false).ToList();
            ViewBag.LOPList = LOPDetailsList;

            return View("Index",EmpLOPDetailsMaster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddLOPDetails(IFormCollection collection)
        {
            try
            {
                EmpLOPDetailsU LOPMaster = new EmpLOPDetailsU();
                
                ViewBag.Message = LOPMaster.AddEmpLOPDetails(collection);

                List<EmpLOPDetails> LOPList = new List<EmpLOPDetails>();

                LOPList = _context.EmpLOPDetails.Where(x => x.IsActive == false).ToList();
                ViewBag.LOPList = LOPList;

                return View("Index", LOPMaster);

                //return RedirectToAction("AddPolicy", "AttandancePolicy");
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return RedirectToAction("Index", "LOPMaster");
            }
        }

        [HttpPost("DeleteRecordLOP")]
        public ActionResult DeleteRecordLOP(string EMPLOPID)
        {
            EmpLOPDetailsU LOPMaster = new EmpLOPDetailsU();

            EmpLOPDetailsU LOPDetailsMasters = new EmpLOPDetailsU();
            ViewBag.WarningMessage = LOPDetailsMasters.DeleteLOPRecord(Convert.ToInt32(EMPLOPID));

            List<EmpLOPDetails> LOPList = new List<EmpLOPDetails>();

            LOPList = _context.EmpLOPDetails.Where(x => x.IsActive == false).ToList();
            ViewBag.LOPList = LOPList;
            return View("Index", LOPMaster);

        }

        public ActionResult SearchRecord(string EMPLOPID)
        {
            EmpLOPDetails LOPMaster = new EmpLOPDetails();
            //var result = policyMaster.EditPolicyMaster(Convert.ToInt32(policyId));

            var result = (from a in _context.EmpLOPDetails
                          where a.ID == Convert.ToInt32(EMPLOPID)
                          select a).ToList();

            return new JsonResult(result);
            //ViewBag.Message = result;

            //return View("AddPolicy", policyMaster);
        }

        public ActionResult GetEmpName(string EmpNo)
        {
            Employee LOPMaster = new Employee();
            //var result = policyMaster.EditPolicyMaster(Convert.ToInt32(policyId));

            var result = (from a in _context.Employees
                          where a.EmployeeNumber == Convert.ToInt32(EmpNo)
                          select a).ToList();

            return new JsonResult(result);
            //ViewBag.Message = result;

            //return View("AddPolicy", policyMaster);
        }

    }
}
