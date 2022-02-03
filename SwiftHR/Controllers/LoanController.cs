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
    public class LoanController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();

        public LoanController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
           
            LoanDetailsU LoanMasters = new LoanDetailsU();
            LoanMasters = GetMonthName();
            
            List<LoanHeader> LoanHeaderList = new List<LoanHeader>();
            GetLoanDataFromSP("6");
            //LoanHeaderList = _context.LoanHeader.Where(x => x.IsActive == false).ToList();
            //ViewBag.LoanHList = LoanHeaderList;

            return View("Index", LoanMasters);
        }

        public void GetLoanDataFromSP(string EmployeeId)
        {
            LoanDetailsU LoanUMaster = new LoanDetailsU();
            DataTable response = new DataTable();
            List<UspLoanDetailsViewModel> employeeList = new List<UspLoanDetailsViewModel>();
            UspLoanDetailsViewModel details = new UspLoanDetailsViewModel();

            string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
            response = LoanUMaster.GetListOfLoanDetails(EmployeeId, connstring).Tables[0];

            employeeList = (from DataRow dr in response.Rows
                            select new UspLoanDetailsViewModel()
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                Date = dr["date"].ToString(),
                                LoanType = Convert.ToInt32(dr["loanType"]),
                                Amount = Convert.ToDecimal(dr["amount"]),
                                ToPrincipale = Convert.ToDecimal(dr["toPrincipale"]),
                                ToInterest = Convert.ToDecimal(dr["toInterest"]),
                                ActualInterest = Convert.ToDecimal(dr["actualInterest"]),
                                ActualPrincipal = Convert.ToDecimal(dr["actualPrincipal"]),
                                Remarks = dr["remarks"].ToString(),
                            }).ToList();

            ViewBag.LoanHList = employeeList;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddLoanHeaderDetails(IFormCollection collection)
        {
            try
            {
                LoanDetailsU LoanMaster = new LoanDetailsU();

                ViewBag.Message = LoanMaster.AddLoanHeader(collection);

                //List<EmpReimbursement> ReimbursementList = new List<EmpReimbursement>();

                //ReimbursementList = _context.EmpReimbursement.Where(x => x.IsActive == false).ToList();
                //ViewBag.ReimbursementList = ReimbursementList;

                List<LookUpDetailsM> LoanTypeList = new List<LookUpDetailsM>();
                LoanTypeList = _context.LookUpDetailsM.Where(x => x.IsActive == false && x.LookUpId == 6).ToList();
                ViewBag.LoanTypeList = LoanTypeList;

                return View("Index", LoanMaster);

                //return RedirectToAction("AddPolicy", "AttandancePolicy");
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return RedirectToAction("Index", "LoanMaster");
            }
        }

        private LoanDetailsU GetMonthName()
        {
            LoanDetailsU empLopDetails = new LoanDetailsU();

            List<LookUpDetailsM> LoanTypeList = new List<LookUpDetailsM>();
            LoanTypeList = _context.LookUpDetailsM.Where(e => e.LookUpId == Convert.ToInt32("6")).ToList();

            empLopDetails.LoanTypeList = LoanTypeList;

            return empLopDetails;
        }

    }
}
