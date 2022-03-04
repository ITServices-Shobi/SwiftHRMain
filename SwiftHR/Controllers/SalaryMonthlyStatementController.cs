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

      //  [HttpGet("GetFixHolidays")]

        [HttpPost]
        public ActionResult EmployeesSalaryProcess(int curMonthNo, int curYear)
        {
            List<USpSalaryMonthlyStatement> result = null;
            try
            {

                bool saturday, sunday;
                bool.TryParse(_configuration["WeekOfHolidays:saturday"], out saturday);
                bool.TryParse(_configuration["WeekOfHolidays:sunday"], out sunday);

                DateTime today = DateTime.Today;
                DateTime endOfMonth = new DateTime(curYear, curMonthNo, DateTime.DaysInMonth(curYear, curMonthNo));
                int day = endOfMonth.Day;

                DateTime now = DateTime.Now;
                int WeekHolidaycount;
                WeekHolidaycount = 0;
                for (int i = 0; i < day; ++i)
                {
                    DateTime d = new DateTime(curYear, curMonthNo, i + 1);

                    

                    if (saturday == true)
                    {
                        if (d.DayOfWeek == DayOfWeek.Saturday)
                        {

                            WeekHolidaycount = WeekHolidaycount + 1;
                        }
                    }
                    if (sunday == true)
                    {
                        if (d.DayOfWeek == DayOfWeek.Sunday)
                        {
                            WeekHolidaycount = WeekHolidaycount + 1;
                        }
                    }
                }


                List<string> fixHolidays = _configuration.GetSection("FixedHolidays")?.GetChildren()?.Select(x => x.Path.Substring(x.Path.IndexOf(":") + 1))?.ToList();

                List<List<String>> holidayMatrix = new List<List<String>>();

                int FixHolidaycounter = 0;

                foreach (var fxholidays in fixHolidays)
                {
                    string[] holi = fxholidays.Split("/");
                    string holidayString = curYear + "-" + holi[1] + "-" + holi[0];
                    DateTime holidaydt = new DateTime(curYear, Convert.ToInt32(holi[1]), Convert.ToInt32(holi[0]));
                    string holidayday = holidaydt.DayOfWeek.ToString();

                    if (saturday == true && sunday == true )
                    {
                        if (holidayday.ToLower() != "saturday" && holidayday.ToLower() != "sunday")
                        {
                            if (holi[1] == curMonthNo.ToString("D2"))
                            {
                                holidayMatrix.Add(new List<String>());

                                holidayMatrix[FixHolidaycounter].Add(fxholidays + "/" + curYear);

                                holidayMatrix[FixHolidaycounter].Add(_configuration["FixedHolidays:" + fxholidays]);

                                FixHolidaycounter++;
                            }
                        }

                    }
                    else if (saturday == true && sunday == false)
                    {
                        if (holidayday.ToLower() != "saturday")
                        {
                            if (holi[1] == curMonthNo.ToString("D2"))
                            {
                                holidayMatrix.Add(new List<String>());

                                holidayMatrix[FixHolidaycounter].Add(fxholidays + "/" + curYear);

                                holidayMatrix[FixHolidaycounter].Add(_configuration["FixedHolidays:" + fxholidays]);

                                FixHolidaycounter++;
                            }
                        }

                    }
                    else if (saturday == false && sunday == true)
                    {
                        if (holidayday.ToLower() != "sunday")
                        {
                            if (holi[1] == curMonthNo.ToString("D2"))
                            {
                                holidayMatrix.Add(new List<String>());

                                holidayMatrix[FixHolidaycounter].Add(fxholidays + "/" + curYear);

                                holidayMatrix[FixHolidaycounter].Add(_configuration["FixedHolidays:" + fxholidays]);

                                FixHolidaycounter++;
                            }
                        }

                    }
                    else 
                    {                      
                            if (holi[1] == curMonthNo.ToString("D2"))
                            {
                                holidayMatrix.Add(new List<String>());

                                holidayMatrix[FixHolidaycounter].Add(fxholidays + "/" + curYear);

                                holidayMatrix[FixHolidaycounter].Add(_configuration["FixedHolidays:" + fxholidays]);

                                FixHolidaycounter++;
                            }                       

                    }
                }

                var TotalHolidays = FixHolidaycounter + WeekHolidaycount;

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
                    response = SalProcessDataMaster.ProcessData(Convert.ToInt32(curMonthNo), Convert.ToInt32(curYear), Convert.ToInt32(TotalHolidays), connstring).Tables[0];

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
