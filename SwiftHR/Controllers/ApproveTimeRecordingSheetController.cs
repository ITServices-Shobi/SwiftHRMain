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
    public class ApproveTimeRecordingSheetController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        public ApproveTimeRecordingSheetController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }

        public IActionResult Index()
        {

            ViewBag.CountOfDay = DateTime.Now.Day;
            ViewBag.NameOfDay = DateTime.Now.DayOfWeek;
            ViewBag.NoOfDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            ViewBag.EmployeeList = GetEmpNameList();

            return View();
        }
        public List<SelectListItem> GetEmpNameList()
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "0", Text = "--Select--" });
            var getdata=(dynamic)null;
            try
            {
                using(SHR_SHOBIGROUP_DBContext db =new SHR_SHOBIGROUP_DBContext())
                {
                    getdata = (from m in db.Employees where m.IsActive == true select new { m.EmployeeId, m.FirstName,m.MiddleName,m.LastName }).ToList();

                    foreach(var data in getdata)
                    {
                        selectList.Add(new SelectListItem { Value = data.EmployeeId.ToString(), Text = data.FirstName.ToString()+' '+ data.LastName.ToString() });
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

        public JsonResult GetMonthAndDay()
        {
            var result = _context.TimeRecordingSheetDetails.Where(e => e.IsActive == false).ToList();
            return Json(new { Result = result });
        }

        public string ConvertObjectToXMLString(object classObject)
        {
            string xmlString = null;
            XmlSerializer xmlSerializer = new XmlSerializer(classObject.GetType());
            using(MemoryStream memoryStream=new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, classObject);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            return xmlString;
        }

        public long GetListOfTimeSheetDetails(SpGetTimeSheetDetailsViewModel recordList, string connstring)
        {
            int flag = 0;
            try
            {
                DataSet ds = new DataSet();
                SqlConnection con = new SqlConnection(connstring);
                string xmlString = ConvertObjectToXMLString(connstring);
                XElement xElement = XElement.Parse(xmlString);
                con.Open();
                SqlCommand cmd = new SqlCommand("RPT_GetTimeSheetDetalis", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Parameters", SqlDbType.Xml).Value = Convert.ToString(xmlString);
                cmd.CommandTimeout = 150000;
                using(var da =new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds);
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
                flag = 0;
            }
            return flag;
        }

        [HttpPost]
        public JsonResult GetListOfTimeSheetDetailsFromEmployee(string recordList)
        {
            TimeRecordingSheetMaster TimeSheetMaster = new TimeRecordingSheetMaster();
            DataTable response = new DataTable();
            List<SpGetTimeSheetDetailsViewModel> employeeList = new List<SpGetTimeSheetDetailsViewModel>();
            SpGetTimeSheetDetailsViewModel details = new SpGetTimeSheetDetailsViewModel();
            string json = "";
            var timerecord = System.Text.Json.JsonSerializer.Deserialize<SpGetTimeSheetDetailsViewModel>(recordList);
            long flag = 0;
            try
            {
                string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
                response = TimeSheetMaster.GetListOfTimeSheetDetails(timerecord, connstring).Tables[0];

                employeeList = (from DataRow dr in response.Rows
                               select new SpGetTimeSheetDetailsViewModel()
                               {
                                   year = Convert.ToInt32(dr["year"]),
                                   Month = Convert.ToInt32(dr["Month"]),
                                   EmpId = Convert.ToInt32(dr["EmpId"]),
                                   EmpName = dr["EmpName"].ToString(),
                                   InFrom = dr["InFrom"].ToString(),
                                   OutTill = dr["OutTill"].ToString(),
                                   Net = dr["Net"].ToString(),
                                   Total = dr["Total"].ToString(),
                               }).ToList();
                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(response.Tables[0].ToString());

                //json = JsonConvert.SerializeXmlNode(doc);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            //return Newtonsoft.Json.JsonConvert.SerializeObject(response.Tables[0]);

            return Json(new { Result = employeeList });
        }

        [HttpPost]
        public JsonResult TimeRecordingSheetList(string empId,string year,string month)
        {
            TimeRecordingSheetMaster TimeSheetMaster = new TimeRecordingSheetMaster();
            DataTable response = new DataTable();
            List<TimeRecordingSheetDetails> employeeList = new List<TimeRecordingSheetDetails>();
            SpGetTimeSheetDetailsViewModel details = new SpGetTimeSheetDetailsViewModel();
            try
            {
                string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
                response = TimeSheetMaster.GetListOfEmployeeTimeSheetDetails(Convert.ToInt32(empId),Convert.ToInt32(month),Convert.ToInt32(year), connstring).Tables[0];

                employeeList = (from DataRow dr in response.Rows
                                select new TimeRecordingSheetDetails()
                                {
                                    RecTimeSheetDetailsId = Convert.ToInt32(dr["recTimeSheetDetailsId"]),
                                    DayNo = Convert.ToInt32(dr["dayNo"]),                                    
                                    Day = dr["day"].ToString(),
                                    EmpIn = dr["empIn"].ToString(),
                                    EmpOut = dr["empOut"].ToString(),
                                    EmpBreak = dr["empBreak"].ToString(),
                                    Net = dr["net"].ToString(),
                                    Total = dr["total"].ToString(),
                                    Presence = dr["presence"].ToString(),
                                    IsApprove = Convert.ToBoolean(dr["isApprove"]),
                                }).ToList();
                
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            
            return Json(new { Result = employeeList });
        }

        [HttpPost]
        public JsonResult ApproveTimeRecordingSheetList(string timeSheetRecId)
        {
            TimeRecordingSheetMaster TimeSheetMaster = new TimeRecordingSheetMaster();
            DataTable response = new DataTable();
            List<TimeRecordingSheetDetails> employeeList = new List<TimeRecordingSheetDetails>();
            SpGetTimeSheetDetailsViewModel details = new SpGetTimeSheetDetailsViewModel();
            try
            {
                string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
                
                ViewBag.Message = TimeSheetMaster.ApproveRecord(Convert.ToInt32(timeSheetRecId));

                response = TimeSheetMaster.GetListOfEmployeeTimeSheetDetailsByRecordId(Convert.ToInt32(timeSheetRecId), connstring).Tables[0];

                employeeList = (from DataRow dr in response.Rows
                                select new TimeRecordingSheetDetails()
                                {
                                    RecTimeSheetDetailsId = Convert.ToInt32(dr["recTimeSheetDetailsId"]),
                                    DayNo = Convert.ToInt32(dr["dayNo"]),
                                    Day = dr["day"].ToString(),
                                    EmpIn = dr["empIn"].ToString(),
                                    EmpOut = dr["empOut"].ToString(),
                                    EmpBreak = dr["empBreak"].ToString(),
                                    Net = dr["net"].ToString(),
                                    Total = dr["total"].ToString(),
                                    Presence = dr["presence"].ToString(),
                                    IsApprove = Convert.ToBoolean(dr["isApprove"]),
                                }).ToList();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return Json(new { Result = employeeList });
        }

        [HttpPost]
        public JsonResult ApproveAllTimeRecordingSheetList(string timeSheetDetailsIds)
        {
            TimeRecordingSheetMaster TimeSheetMaster = new TimeRecordingSheetMaster();
            DataTable response = new DataTable();
            List<TimeRecordingSheetDetails> employeeList = new List<TimeRecordingSheetDetails>();
            SpGetTimeSheetDetailsViewModel details = new SpGetTimeSheetDetailsViewModel();
            try
            {
                string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
                timeSheetDetailsIds = timeSheetDetailsIds.TrimEnd(',');
                timeSheetDetailsIds = timeSheetDetailsIds.TrimStart(',');
                ViewBag.Message = TimeSheetMaster.ApproveAllRecord(timeSheetDetailsIds, connstring);

                response = TimeSheetMaster.GetListOfEmployeeTimeSheetDetailsByRecordId(Convert.ToInt32(timeSheetDetailsIds), connstring).Tables[0];

                employeeList = (from DataRow dr in response.Rows
                                select new TimeRecordingSheetDetails()
                                {
                                    RecTimeSheetDetailsId = Convert.ToInt32(dr["recTimeSheetDetailsId"]),
                                    DayNo = Convert.ToInt32(dr["dayNo"]),
                                    Day = dr["day"].ToString(),
                                    EmpIn = dr["empIn"].ToString(),
                                    EmpOut = dr["empOut"].ToString(),
                                    EmpBreak = dr["empBreak"].ToString(),
                                    Net = dr["net"].ToString(),
                                    Total = dr["total"].ToString(),
                                    Presence = dr["presence"].ToString(),
                                    IsApprove = Convert.ToBoolean(dr["isApprove"]),
                                }).ToList();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return Json(new { Result = employeeList });
        }
    }
}
