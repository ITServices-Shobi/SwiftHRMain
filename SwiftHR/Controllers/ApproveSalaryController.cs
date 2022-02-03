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
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Configuration;
using ConfigurationManager = SwiftHR.Models.ConfigurationManager;
using System.Xml;

namespace SwiftHR.Controllers
{
    public class ApproveSalaryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        public ApproveSalaryController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }
        public IActionResult Index()    
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetListOfSalaryHeader()
        {
            AddSalaryUtility SalaryHeaderMaster = new AddSalaryUtility();
            DataTable response = new DataTable();
            List<SpGetSalaryHeaderViewModel> SalaryHeaderList = new List<SpGetSalaryHeaderViewModel>();
            SpGetSalaryHeaderViewModel details = new SpGetSalaryHeaderViewModel();
            string json = "";
            //var SalaruListrecord = System.Text.Json.JsonSerializer.Deserialize<SpGetTimeSheetDetailsViewModel>();
            long flag = 0;
            try
            {
                string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
                response = SalaryHeaderMaster.GetListOfSalaryHeader(connstring).Tables[0];

                SalaryHeaderList = (from DataRow dr in response.Rows
                                select new SpGetSalaryHeaderViewModel()
                                {
                                    SalHeaderId = Convert.ToInt32(dr["salHeaderId"]),
                                    EmployeeID= Convert.ToInt32(dr["employeeID"]),
                                    EmployeeNumber= Convert.ToInt32(dr["employeeNumber"]),
                                    EmployeeName = dr["employeeName"].ToString(),
                                    EmployeeType=Convert.ToInt32(dr["employeeType"]),
                                    PFAvailability= Convert.ToInt32(dr["pFAvailability"]),
                                    DOJ= dr["dOJ"].ToString(),
                                    DOB = dr["dOB"].ToString(),
                                    LastPayrollProceesedDate = dr["lastPayrollProceesedDate"].ToString(),
                                    PayoutMonth = Convert.ToInt32(dr["payoutMonth"]),
                                    Remarks = dr["remarks"].ToString(),
                                    EffectiveEndDate= dr["effectiveEndDate"].ToString(),
                                    EffectiveStartDate = dr["effectiveStartDate"].ToString(),
                                }).ToList();
                
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            //return Newtonsoft.Json.JsonConvert.SerializeObject(response.Tables[0]);

            return Json(new { Result = SalaryHeaderList });
        }

        public JsonResult GetListOfSalaryDetails(string SalHeaderId)
        {
            AddSalaryUtility SalaryDetailsMaster = new AddSalaryUtility();
            DataTable response = new DataTable();
            List<SpGetSalaryHeaderViewModel> SalaryDetailsList = new List<SpGetSalaryHeaderViewModel>();
            SpGetSalaryHeaderViewModel details = new SpGetSalaryHeaderViewModel();
            string json = "";
            try
            {
                string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
                response = SalaryDetailsMaster.GetListOfSalaryDetails(Convert.ToInt32(SalHeaderId), connstring).Tables[0];

                SalaryDetailsList = (from DataRow dr in response.Rows
                                select new SpGetSalaryHeaderViewModel()
                                {
                                    SingalColName = dr["singalColName"].ToString(),
                                    ColValue = dr["colValue"].ToString(),
                                }).ToList();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return Json(new { Result = SalaryDetailsList });
        }

        [HttpPost("RejectSalary")]
        public ActionResult RejectSalary(string salHeaderId)
        {
            AddSalaryUtility RejectMaster = new AddSalaryUtility();

            AddSalaryUtility RejDetailsMasters = new AddSalaryUtility();
            ViewBag.WarningMessage = RejDetailsMasters.RejectRecord(Convert.ToInt32(salHeaderId));

            GetListOfSalaryHeader();
            return View("Index", RejectMaster);

        }

        public ActionResult ApproveSalaryHeader(string salHeaderId)
        {
            AddSalaryUtility RejectMaster = new AddSalaryUtility();

            AddSalaryUtility RejDetailsMasters = new AddSalaryUtility();
            ViewBag.WarningMessage = RejDetailsMasters.ApproveRecord(Convert.ToInt32(salHeaderId));

            //GetListOfSalaryHeader();
            return View("Index", RejectMaster);

        }

    }
}
