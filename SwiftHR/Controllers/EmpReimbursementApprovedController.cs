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
    public class EmpReimbursementApprovedController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        public EmpReimbursementApprovedController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }
        public IActionResult Index(string EmpNo)
        {
            //var EmpNo = "Sunny";
            ViewBag.EmployeeList = GetEmpNameList();
            ViewBag.MonthList = GetMonthNameList();
            List<Employee> EmployeeDetailsList = new List<Employee>();

            EmployeeDetailsList = _context.Employees.Where(x => x.FirstName == EmpNo).ToList();
            
            EmpReimbursementU empReiMasters = new EmpReimbursementU();
            ///empReiMasters = SearchEmpNameDetails(EmpNo);
            ViewBag.EmpList = EmployeeDetailsList;

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

        public List<SelectListItem> GetMonthNameList()
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "0", Text = "--Select--" });
            var getdata = (dynamic)null;
            try
            {
                using (SHR_SHOBIGROUP_DBContext db = new SHR_SHOBIGROUP_DBContext())
                {
                    getdata = (from m in db.LookupDetailsM where m.IsActive == false && m.LookUpId == 5 select new { m.LookUpDetailsId, m.Description, m.Name }).ToList();

                    foreach (var data in getdata)
                    {
                        selectList.Add(new SelectListItem { Value = data.Name.ToString(), Text = data.Description.ToString() });
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
        [HttpPost]
        public JsonResult GetListOfEmpReimbursementApproved(string EmpId, string EmpCode, string MonthId)
        {
            EmpReimbursementU ReimbursementMaster = new EmpReimbursementU();
            DataTable response = new DataTable();

            List<SpGetEmpReimbursementApprovedViewModel> ReimbursementList = new List<SpGetEmpReimbursementApprovedViewModel>();
            SpGetEmpReimbursementApprovedViewModel details = new SpGetEmpReimbursementApprovedViewModel();
            
            string json = "";
           // var timerecord = System.Text.Json.JsonSerializer.Deserialize<SpGetEmpReimbursementApprovedViewModel>(recordList);
            long flag = 0;
            try
            {
                string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
                response = ReimbursementMaster.GetListOfReimbursementDetails(Convert.ToInt32(EmpId),EmpCode,Convert.ToInt32(MonthId), connstring).Tables[0];

                ReimbursementList = (from DataRow dr in response.Rows
                                select new SpGetEmpReimbursementApprovedViewModel()
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    EmployeeNumber= dr["EmployeeNumber"].ToString(),
                                    EmployeeName = dr["EmployeeName"].ToString(),
                                    Year = Convert.ToInt32(dr["Year"]),
                                    Month = Convert.ToInt32(dr["Month"]),
                                    Date= dr["Date"].ToString(),
                                    Amount = Convert.ToDecimal(dr["Amount"]),
                                    PaymentEffectedDate= dr["Date"].ToString(),
                                    Remarks= dr["Remarks"].ToString(),
                                }).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return Json(new { Result = ReimbursementList });
        }
        public ActionResult SearchEmpNameDetails(string EmpNo)
        {
            //var result = policyMaster.EditPolicyMaster(Convert.ToInt32(policyId));
            EmpReimbursementU empReimbursementDetails = new EmpReimbursementU();

            List<Employee> EmpDetailsList = new List<Employee>();
            if(EmpNo !=null)
            {
                var result = (from a in _context.Employees where a.FirstName == EmpNo select a).ToList();
                EmpDetailsList = _context.Employees.Where(e => e.FirstName == EmpNo || e.MiddleName == EmpNo || e.LastName == EmpNo || e.EmployeeNumber.ToString() == EmpNo).ToList();
                ViewBag.EmpList = EmpDetailsList;
                empReimbursementDetails.EmployeeDetailsList = EmpDetailsList;
            }

            return new JsonResult(EmpDetailsList);
            //return empReimbursementDetails;
            //ViewBag.Message = result;
            //return View("AddPolicy", policyMaster);
        }

        public ActionResult ApproveRecord(int HiddenRecodId)
        {            
            try
            {
                EmpReimbursementU ReimbursementMaster = new EmpReimbursementU();
                ViewBag.Message = ReimbursementMaster.ApproveRecord(HiddenRecodId);

            }
            catch (Exception ex)
            {

            }
            return Json(new { Result = "" });
        }
        [HttpPost]
        public JsonResult ApproveAllList(string timeSheetDetailsIds)
        {
            EmpReimbursementU ReimbursementMaster = new EmpReimbursementU();
            DataTable response = new DataTable();
            List<EmpReimbursement> ReimbursementList = new List<EmpReimbursement>();
            
            try
            {
                string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
                timeSheetDetailsIds = timeSheetDetailsIds.TrimEnd(',');
                timeSheetDetailsIds = timeSheetDetailsIds.TrimStart(',');
                ViewBag.Message = ReimbursementMaster.ApproveAllRecord(timeSheetDetailsIds, connstring);

               // response = ReimbursementMaster.GetListOfReimbursementDetailsById(Convert.ToInt32(timeSheetDetailsIds), connstring).Tables[0];
                //ReimbursementList = (from DataRow dr in response.Rows
                //                     select new SpGetEmpReimbursementApprovedViewModel()
                //                     {
                //                         Id = Convert.ToInt32(dr["Id"]),
                //                         EmployeeNumber = dr["EmployeeNumber"].ToString(),
                //                         EmployeeName = dr["EmployeeName"].ToString(),
                //                         Year = Convert.ToInt32(dr["Year"]),
                //                         Month = Convert.ToInt32(dr["Month"]),
                //                         Date = dr["Date"].ToString(),
                //                         Amount = Convert.ToDecimal(dr["Amount"]),
                //                         PaymentEffectedDate = dr["Date"].ToString(),
                //                         Remarks = dr["Remarks"].ToString(),
                //                     }).ToList();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return Json(new { Result = ReimbursementList });
        }
    }
}
