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
    public class EmpPFESICDetailsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();

        public EmpPFESICDetailsController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            EmpPFESICDetailsUtility EmpPFESICUtility = new EmpPFESICDetailsUtility();

            List<EmpPFESICDetails> EmpPFESICDetails = new List<EmpPFESICDetails>();

            EmpPFESICDetails = _context.EmpPFESICDetails.Where(x => x.IsActive == true).ToList();
            ViewBag.PFESICList = EmpPFESICDetails;

            return View("Index", EmpPFESICUtility);
        }

        public ActionResult GetEmpName(string EmpNo)
        {
            Employee EmployeeMaster = new Employee();
            //var result = policyMaster.EditPolicyMaster(Convert.ToInt32(policyId));

            var result = (from a in _context.Employees
                          where a.EmployeeNumber == Convert.ToInt32(EmpNo)
                          select a).ToList();

            return new JsonResult(result);
            //ViewBag.Message = result;

            //return View("AddPolicy", policyMaster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddPFESICDetails(IFormCollection collection)
        {
            try
            {
                EmpPFESICDetailsUtility PFESICDetailsUtility = new EmpPFESICDetailsUtility();

                ViewBag.Message = PFESICDetailsUtility.AddEmpPFESICDetails(collection);

                List<EmpPFESICDetails> PFESICList = new List<EmpPFESICDetails>();

                PFESICList = _context.EmpPFESICDetails.Where(x => x.IsActive == true).ToList();
                ViewBag.PFESICList = PFESICList;

                return View("Index", PFESICDetailsUtility);

                //return RedirectToAction("AddPolicy", "AttandancePolicy");
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return RedirectToAction("Index", "PFESICDetailsUtility");
            }
        }

        [HttpPost("DeleteRecordPFESIC")]
        public ActionResult DeleteRecordPFESIC(string EMPPFESICID)
        {
            EmpPFESICDetailsUtility PFESICDetailsUtility = new EmpPFESICDetailsUtility();

            EmpPFESICDetailsUtility empPFESICDetailsUtility = new EmpPFESICDetailsUtility();
            ViewBag.WarningMessage = empPFESICDetailsUtility.DeletePFESICRecord(Convert.ToInt32(EMPPFESICID));

            List<EmpPFESICDetails> PFESICList = new List<EmpPFESICDetails>();

            PFESICList = _context.EmpPFESICDetails.Where(x => x.IsActive == true).ToList();
            ViewBag.PFESICList = PFESICList;
            return View("Index", PFESICDetailsUtility);

        }


        public ActionResult SearchRecord(string EMPPFID)
        {
            EmpPFESICDetails empPFESICDetails = new EmpPFESICDetails();
            //var result = policyMaster.EditPolicyMaster(Convert.ToInt32(policyId));

            var result = (from a in _context.EmpPFESICDetails
                          where a.ID == Convert.ToInt32(EMPPFID)
                          select new
                          {
                              Id = a.ID,
                              EmployeeId = a.EmployeeID,
                              EmployeeNumber = a.EmployeeNumber,
                              EmployeeName = a.EmployeeName,
                              BankID = a.BankID,
                              BankBranch = a.BankBranch,
                              AccountTypeID = a.AccountTypeID,
                              AccountNo = a.AccountNo,
                              IFSCCode = a.IFSCCode,
                              EmployeeNameAsBankRecords = a.EmployeeNameAsBankRecords,
                              IBAN = a.IBAN,
                              PaymentMethod = a.PaymentMethod,
                              ESICIsApplicable = a.ESICIsApplicable,
                              ESICAccountNo = a.ESICAccountNo,
                              PFIsApplicable = a.PFIsApplicable,
                              AllowEPFExcessContribution = a.AllowEPFExcessContribution,
                              AllowEPSExcessContribution = a.AllowEPSExcessContribution,
                              PFAccountNo = a.PFAccountNo,
                              UAN = a.UAN,
                              StartDate = string.Format("{0:yyyy-MM-dd}", a.StartDate)

                          }).ToList();

            return new JsonResult(result);
            //ViewBag.Message = result;

            //return View("AddPolicy", policyMaster);
        }

    }
}
