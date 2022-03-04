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
using AspNetCore.Reporting;

namespace SwiftHR.Controllers
{
    public class EmployeePaySlipController : Controller
    {


        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        public EmployeePaySlipController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        public IActionResult Index()
        {
            return View();
        }

       

        [HttpPost]
        public JsonResult GenerateEmployeesPaySlip(int EmPID,int curMonthNo, int curYear)
        {
            List<SpGetEmployeePaySlipViewModel> result = null;

            SalaryMonthlyStatementU SalProcessDataMaster = new SalaryMonthlyStatementU();
            DataTable response = new DataTable();

            List<SpGetEmployeePaySlipViewModel> ReimbursementList = new List<SpGetEmployeePaySlipViewModel>();
            SpGetEmployeePaySlipViewModel details = new SpGetEmployeePaySlipViewModel();

            try
            {

                string json = "";
                //var timerecord = System.Text.Json.JsonSerializer.Deserialize<SpGetEmpReimbursementApprovedViewModel>(recordList);
                long flag = 0;
                try
                {
                    string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
                    response = SalProcessDataMaster.PaySlip(Convert.ToInt32(EmPID), Convert.ToInt32(curMonthNo), Convert.ToInt32(curYear), connstring).Tables[0];

                    ReimbursementList = (from DataRow dr in response.Rows
                                         select new SpGetEmployeePaySlipViewModel()
                                         {
                                             EmpDetails = (dr["empDetails"]).ToString(),
                                             EmpDetailsVale = (dr["empDetailsVale"]).ToString(),
                                             EmpDetails1 = (dr["empDetails1"]).ToString(),
                                             EmpDetailsVale1 = (dr["empDetailsVale1"]).ToString(),

                                         }).ToList();
                    
                }

                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            catch (Exception ex)
            {
                
            }

            return Json(new { Result = ReimbursementList });
        }


    }
}
