using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SwiftHR.Models;
using SwiftHR.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Controllers
{
    public class SalaryMonthlyStatementController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();

        public SalaryMonthlyStatementController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
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
        public ActionResult EmployeesSalaryProcess(int curMonthNo, int curYear)
        {
            List<USpSalaryMonthlyStatement> result = null;
            try
            {




                SalaryMonthlyStatementU SalProcessDataMaster = new SalaryMonthlyStatementU();
                DataTable response = new DataTable();

                List<USpSalaryMonthlyStatement> ReimbursementList = new List<USpSalaryMonthlyStatement>();
                USpSalaryMonthlyStatement details = new USpSalaryMonthlyStatement();

                string json = "";
                //var timerecord = System.Text.Json.JsonSerializer.Deserialize<SpGetEmpReimbursementApprovedViewModel>(recordList);
                long flag = 0;
                try
                {
                    string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
                    response = SalProcessDataMaster.ProcessData(Convert.ToInt32(curMonthNo), Convert.ToInt32(curYear), connstring).Tables[0];

                    ReimbursementList = (from DataRow dr in response.Rows
                                         select new USpSalaryMonthlyStatement()
                                         {
                                             Id = Convert.ToInt32(dr["Id"]),
                                             
                                         }).ToList();
                    result = ReimbursementList.ToList();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            catch (Exception ex)
            {

            }

            return Json(new { Result = result });
        }


    }
}
