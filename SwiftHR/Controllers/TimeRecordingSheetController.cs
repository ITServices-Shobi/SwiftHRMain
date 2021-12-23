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
using System.Data;
using System.Web.WebPages.Html;

namespace SwiftHR.Controllers
{
    public class TimeRecordingSheetController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        public TimeRecordingSheetController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }

        public IActionResult Index(string empId)
        {

            String localEmpId;
            if (!string.IsNullOrEmpty(empId))
                localEmpId = empId;
            else
                localEmpId = GetLoggedInEmpId().ToString();

            EmployeeOnboardingDetails empOnboardingDetails = new EmployeeOnboardingDetails(localEmpId);
            empOnboardingDetails = GetEmployeeProfileDetails(localEmpId);

            var EmployeeName = (from c in _context.Employees where c.EmployeeId ==Convert.ToInt32(localEmpId) && c.IsActive==true select new { c.FirstName ,c.MiddleName,c.LastName }).FirstOrDefault();


            string EmpName = EmployeeName.FirstName + EmployeeName.MiddleName + EmployeeName.LastName;

            TimeRecordingSheetMaster TimeSheetRecMaster = new TimeRecordingSheetMaster();

            //TimeSheetRecMaster = GetTimeSheetData();
            List<AttandancePolicy> PolicyList = new List<AttandancePolicy>();

            PolicyList = _context.AttandancePolicies.Where(x => x.IsActive == false).ToList();
            ViewBag.PolicyList = PolicyList;

            ViewBag.PresenceList = GetPresenceList();
            ViewBag.EmployeeList = GetEmpNameList();
            ViewBag.EmpName = EmpName;
            ViewBag.EmpId = localEmpId;
            ViewBag.CountOfDay = DateTime.Now.Day;
            ViewBag.NameOfDay = DateTime.Now.DayOfWeek;
            ViewBag.NoOfDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            List<string> weekName = new List<string>();
            weekName.Add("");
            DateTime eachDate;
            eachDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            for (var i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
            {
                weekName.Add(eachDate.DayOfWeek.ToString());
                eachDate = eachDate.AddDays(1);
            }
            ViewBag.weekDayName = weekName;

            var Month = DateTime.Now.Month.ToString();
            //string Month = String.Format("{0:MMMM}", DateTime.Now);
            var result = _context.TimeRecordingSheetDetails.Where(x => x.IsActive == false && x.EmpId == Convert.ToInt32(localEmpId) && x.Month ==Convert.ToInt32(Month)).ToList();
            ViewBag.TimeSheetRecordingDetails = result;
            return View();
        }

        public List<SelectListItem> GetEmpNameList()
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "0", Text = "--Select--" });
            var getdata = (dynamic)null;
            try
            {
                using (SHR_SHOBIGROUP_DBContext db = new SHR_SHOBIGROUP_DBContext())
                {
                    getdata = (from m in db.Employees where m.IsActive == true select new { m.EmployeeId, m.FirstName, m.MiddleName, m.LastName }).ToList();

                    foreach (var data in getdata)
                    {
                        selectList.Add(new SelectListItem { Value = data.EmployeeId.ToString(), Text = data.FirstName.ToString() + ' ' + data.LastName.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                if (getdata != null)
                {
                    getdata = null;
                }
            }
            return selectList;
        }

        public List<SelectListItem> GetPresenceList()
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "0", Text = "--Select--" });
            var getdata = (dynamic)null;
            try
            {
                using (SHR_SHOBIGROUP_DBContext db = new SHR_SHOBIGROUP_DBContext())
                {
                    getdata = (from m in db.Presences where m.IsActive == true select new { m.PresenceId, m.PresenceName }).ToList();

                    foreach (var data in getdata)
                    {
                        selectList.Add(new SelectListItem { Value = data.PresenceId.ToString(), Text = data.PresenceName.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                if (getdata != null)
                {
                    getdata = null;
                }
            }
            return selectList;
        }

        private EmployeeOnboardingDetails GetEmployeeProfileDetails(string empId)
        {
            EmployeeOnboardingDetails empOnboardingDetails = new EmployeeOnboardingDetails(empId);
            return empOnboardingDetails;

        }

        [HttpPost]
        public ActionResult InsertWorkOrder(string TextIn, string TextOut, string TextTotal, string TextBreak, string TextNet, string TextPresence, int EmpId, int DayNo, string Day, int Month, string Date)
        {
            List<USpInsertTimeSheetRecord> result = null;
            try
            {
                TimeRecordingSheetMaster TimeSheetMaster = new TimeRecordingSheetMaster();
                ViewBag.TimeSheetMessage = TimeSheetMaster.InsertTimeSheetData(TextIn, TextOut, TextTotal, TextBreak, TextNet, TextPresence, EmpId, DayNo, Day,Month, Convert.ToDateTime(Date));
                // result = await TimeSheetMaster.InsertTimeSheetData(TextIn, TextOut, TextTotal, TextBreak, TextNet, Convert(TextPresence);
            }
            catch (Exception ex)
            {

            }

            return Json(new { Result = result });
        }

        public ActionResult SubmitRecord(int HiddenRecodId)
        {
            List<USpInsertTimeSheetRecord> result = null;
            try
            {
                TimeRecordingSheetMaster TimeSheetMaster = new TimeRecordingSheetMaster();
                ViewBag.Message = TimeSheetMaster.SubmitRecord(HiddenRecodId);

                //result = _context.TimeRecordingSheetDetails.Where(e => e.IsActive == false).ToList();
                // result = await TimeSheetMaster.InsertTimeSheetData(TextIn, TextOut, TextTotal, TextBreak, TextNet, Convert(TextPresence);
            }
            catch (Exception ex)
            {

            }
            return Json(new { Result = result });
        }

        public ActionResult InsertAllData(string workorderjson)
        {
            int EmployeeId = 0,Month=0;
            List<TimeRecordingSheetDetails> result = null;
            try
            {
                TimeRecordingSheetMaster TimeSheetMaster = new TimeRecordingSheetMaster();
                TimeRecordingSheetDetailsModel details = new TimeRecordingSheetDetailsModel();
                
                var timerecord = System.Text.Json.JsonSerializer.Deserialize<List<TimeRecordingSheetDetails>>(workorderjson);
                DateTime TimeSheetDate=DateTime.Now;
                foreach (var record in timerecord)
                {
                    string createddate = Convert.ToDateTime(record.Date).ToString("yyyy-MM-dd");
                    EmployeeId = record.EmpId;
                    Month = record.Month;
                    TimeSheetDate = Convert.ToDateTime(createddate);
                    ViewBag.Message = TimeSheetMaster.InsertTimeSheetData(record.EmpIn, record.EmpOut, record.Total, record.EmpBreak, record.Net, record.Presence, record.EmpId, record.DayNo, record.Day,record.Month, TimeSheetDate);
                }

                result = _context.TimeRecordingSheetDetails.Where(e => e.IsActive == false).Where(e => e.EmpId == EmployeeId).Where(e => e.Month== Month).Where(e => e.Date.Year == TimeSheetDate.Year).ToList();

                //ViewBag.TimeSheetRecordingDetails = result;
                // ViewBag.Message = TimeSheetMaster.InsertTimeSheetData(workorderjson);
                // result = await TimeSheetMaster.InsertTimeSheetData(TextIn, TextOut, TextTotal, TextBreak, TextNet, Convert(TextPresence);
            }
            catch (Exception ex)
            {

            }
            return Json(new { Result = result });
        }

        private TimeRecordingSheetMaster GetTimeSheetData()
        {
            TimeRecordingSheetMaster TimeSheetMaster = new TimeRecordingSheetMaster();

            List<TimeRecordingSheetDetails> GetTimeSheetListName = new List<TimeRecordingSheetDetails>();
            GetTimeSheetListName = _context.TimeRecordingSheetDetails.Where(e => e.IsActive == false && e.Date != null).ToList();

            ViewBag.TimeSheetListData = GetTimeSheetListName;
            TimeSheetMaster.TimeSheetList = GetTimeSheetListName;
            return TimeSheetMaster;
        }

        public string GetCookies(string key)
        {
            string cookieValue = string.Empty;
            cookieValue = Request.Cookies[key];
            return cookieValue;
        }
        public int GetLoggedInEmpId()
        {
            int employeeId = 0;
            string eid = GetCookies("eid");
            string empId = DataSecurity.DecryptString(eid);

            if (!string.IsNullOrEmpty(empId))
                employeeId = Convert.ToInt32(empId);

            return employeeId;
        }

        [HttpPost]
        public JsonResult GetMonthAndDay(string empId, string year, string month,string NewDate)
        {

            var result = _context.TimeRecordingSheetDetails.Where(e => e.IsActive == false).Where(e => e.EmpId == Convert.ToInt32(empId)).Where(e=> e.Month== Convert.ToInt32(month)).Where(e => e.Date.Year == Convert.ToInt32(year)).ToList();

            List<string> weekName = new List<string>();
            weekName.Add("");
            DateTime eachDate;
            eachDate = new DateTime(Convert.ToDateTime(NewDate).Year, Convert.ToDateTime(NewDate).Month, 1);
            for (var i = 1; i <= DateTime.DaysInMonth(Convert.ToDateTime(NewDate).Year, Convert.ToDateTime(NewDate).Month); i++)
            {
                weekName.Add(eachDate.DayOfWeek.ToString());
                eachDate = eachDate.AddDays(1);
            }
            ViewBag.weekDayName = weekName;

            return Json(new { Result = result, weekName= weekName });

        }

    }
}
