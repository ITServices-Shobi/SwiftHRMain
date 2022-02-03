using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SwiftHR.Models;
using SwiftHR.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Controllers
{
    public class EmpArrearDetailsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();

        public EmpArrearDetailsController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            EmpArrearDetailsUtility arrearDetailsUtility = new EmpArrearDetailsUtility();

            List<EmpArrearDetails> EmpArrearDetails = new List<EmpArrearDetails>();

            EmpArrearDetails = _context.EmpArrearDetails.Where(x => x.IsActive == true).ToList();
            ViewBag.ArrearsList = EmpArrearDetails;

            return View("Index", arrearDetailsUtility);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddArrearDetails(IFormCollection collection)
        {
            try
            {
                EmpArrearDetailsUtility arrearDetailsUtility = new EmpArrearDetailsUtility();

                ViewBag.Message = arrearDetailsUtility.AddEmpArrearDetails(collection);

                List<EmpArrearDetails> ArrearsList = new List<EmpArrearDetails>();

                ArrearsList = _context.EmpArrearDetails.Where(x => x.IsActive == true).ToList();
                ViewBag.ArrearsList = ArrearsList;

                return View("Index", arrearDetailsUtility);

                //return RedirectToAction("AddPolicy", "AttandancePolicy");
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return RedirectToAction("Index", "arrearDetailsUtility");
            }
        }

        [HttpPost("DeleteRecordArrears")]
        public ActionResult DeleteRecordArrears(string EMPARREARID)
        {
            EmpArrearDetailsUtility arrearDetailsUtility = new EmpArrearDetailsUtility();

            EmpArrearDetailsUtility empArrearDetailsUtility = new EmpArrearDetailsUtility();
            ViewBag.WarningMessage = empArrearDetailsUtility.DeleteArrearsRecord(Convert.ToInt32(EMPARREARID));

            List<EmpArrearDetails> ArrearsList = new List<EmpArrearDetails>();

            ArrearsList = _context.EmpArrearDetails.Where(x => x.IsActive == true).ToList();
            ViewBag.ArrearsList = ArrearsList;
            return View("Index", arrearDetailsUtility);

        }

        public ActionResult SearchRecord(string EMPARREARID)
        {
            EmpArrearDetails empArrearDetails = new EmpArrearDetails();
            //var result = policyMaster.EditPolicyMaster(Convert.ToInt32(policyId));

            var result = (from a in _context.EmpArrearDetails
                          where a.ID == Convert.ToInt32(EMPARREARID)
                          select new {
                              Id = a.ID,
                              EmployeeId = a.EmployeeID,
                              EmployeeNumber = a.EmployeeNumber,
                              EmployeeName = a.EmployeeName,
                              PayrollMonth = a.PayrollMonth,
                              EffectiveDateFrom = string.Format("{0:yyyy-MM-dd}", a.EffectiveDateFrom),
                              Amount = a.Amount,
                              Remarks = a.Remarks

                          }).ToList();

            return new JsonResult(result);
            //ViewBag.Message = result;

            //return View("AddPolicy", policyMaster);
        }


        public ActionResult GetEmpName(string EmpNo)
        {
            Employee ArrearMaster = new Employee();
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
