using AspNetCore.Reporting;
using ClosedXML.Excel;
using Fingers10.ExcelExport.ActionResults;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using SwiftHR.Models;
using SwiftHR.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace SwiftHR.Controllers
{
    public class MonthlyEmployeeMailSendingController : Controller
    {


        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();

        public MonthlyEmployeeMailSendingController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index(string curMonthNo, string curYear)
        {
            SalaryMonthlyStatementU MailSendUtility = new SalaryMonthlyStatementU();
           // MailSendUtility = GetEmployeeProcessSalary(curMonthNo, curYear);
            return View("Index", MailSendUtility);
        }
        public JsonResult SendSalarySlipToEmployees(string EmPID, int curMonthNo, int curYear)
        {
            string status = "success";
            List<int> numlist = new List<int>();
            try
            {
                foreach (string number in EmPID.TrimStart(',').Split(','))
                    numlist.Add(Int32.Parse(number));

                for (int i = 0; i < numlist.Count; i++)
                {
                    var empMailId = (from Emp in _context.Employees where Emp.EmployeeId == Convert.ToInt32(numlist[i]) select Emp.Email).FirstOrDefault();
                    byte[] fc;
                    EmailService ems = new EmailService();
                    fc = Print(numlist[i], curMonthNo, curYear);
                    try
                    {
                        ems.SendSalaryPdf("swifthrsoft@gmail.com", empMailId.ToString(), "test", "sent test mail with salary slip", fc);
                    }
                    catch(Exception ex)
                    {
                        status = "Failed";
                    }
                    if(status != "Failed")
                    {

                        SalaryMonthlyStatementU UpdateStatus = new SalaryMonthlyStatementU();

                        ViewBag.WarningMessage = UpdateStatus.UpdateMailStatus(Convert.ToInt32(numlist[i]), Convert.ToInt32(curMonthNo), Convert.ToInt32(curYear));
                    }

                }
            }
            catch(Exception ex)
            {
                status = "Failed "+ex.Message;
            }
            return Json(new { Result = status });
        }

        
        public byte[] Print(int EmPID, int curMonthNo, int curYear)
        {
            string mintype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\PaySlip\\PaySlipReport.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>() ;

            System.Data.DataTable response = new System.Data.DataTable();
            SalaryMonthlyStatementU SalProcessDataMaster = new SalaryMonthlyStatementU();
            List<USpPrintSalaryLipViewModel> pm = new List<USpPrintSalaryLipViewModel>();

            string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
            response = SalProcessDataMaster.SendPaySlip(EmPID, curMonthNo, curYear, connstring).Tables[0];
            pm = (from DataRow dr in response.Rows
                  select new USpPrintSalaryLipViewModel()
                  {
                      PayoutMonth = dr["payoutMonth"].ToString(),
                      PayoutYR = dr["payoutYR"].ToString(),
                      Name = dr["name"].ToString(),
                      DOJ = dr["dOJ"].ToString(),
                      Designation = dr["designation"].ToString(),
                      Department = dr["department"].ToString(),
                      LocationName = dr["locationName"].ToString(),
                      TotalWorkingDays = dr["totalWorkingDays"].ToString(),
                      LOPDays = dr["lOPDays"].ToString(),
                      EmployeeNumber = dr["employeeNumber"].ToString(),
                      BankName = dr["bankName"].ToString(),
                      BankAccountNo = dr["bankAccountNo"].ToString(),
                      PANNumber = dr["pANNumber"].ToString(),
                      PFNumber = dr["pFNumber"].ToString(),
                      UANNumber = dr["uANNumber"].ToString(),
                      Basic = dr["basic"].ToString(),
                      HRA = dr["hRA"].ToString(),
                      Bonus = dr["bonus"].ToString(),
                      OtherAllowance = dr["otherAllowance"].ToString(),
                      VARIABLEBONUS = dr["vARIABLEBONUS"].ToString(),
                      PF = dr["pF"].ToString(),
                      ESI = dr["eSI"].ToString(),
                      PROFTAX = dr["pROFTAX"].ToString(),
                      TotalEarningsINR = dr["totalEarningsINR"].ToString(),
                      TotalDeductionsINR = dr["totalDeductionsINR"].ToString(),
                      NetPay = dr["netPay"].ToString(),
                      NetPayInWord = dr["netPayInWord"].ToString(),

                  }).ToList();

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("PaySlipDataSet", pm);
            
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mintype);
            return result.MainStream;

        }


        public FileResult OpenSalarySlipPdf(int EmPID, int curMonthNo, int curYear)
        {
            string mintype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\PaySlip\\PaySlipReport.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            System.Data.DataTable response = new System.Data.DataTable();
            SalaryMonthlyStatementU SalProcessDataMaster = new SalaryMonthlyStatementU();
            List<USpPrintSalaryLipViewModel> pm = new List<USpPrintSalaryLipViewModel>();
            List<USpPrintSalaryEaringViewModel> ER = new List<USpPrintSalaryEaringViewModel>();
            List<USpPrintSalaryEaringViewModel> DE = new List<USpPrintSalaryEaringViewModel>();

            string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
            response = SalProcessDataMaster.SendPaySlip(EmPID, curMonthNo, curYear, connstring).Tables[0];
            pm = (from DataRow dr in response.Rows
                  select new USpPrintSalaryLipViewModel()
                  {
                      PayoutMonth = dr["payoutMonth"].ToString(),
                      PayoutYR = dr["payoutYR"].ToString(),
                      Name = dr["name"].ToString(),
                      DOJ = dr["dOJ"].ToString(),
                      Designation = dr["designation"].ToString(),
                      Department = dr["department"].ToString(),
                      LocationName = dr["locationName"].ToString(),
                      TotalWorkingDays = dr["totalWorkingDays"].ToString(),
                      LOPDays = dr["lOPDays"].ToString(),
                      EmployeeNumber = dr["employeeNumber"].ToString(),
                      BankName = dr["bankName"].ToString(),
                      BankAccountNo = dr["bankAccountNo"].ToString(),
                      PANNumber = dr["pANNumber"].ToString(),
                      PFNumber = dr["pFNumber"].ToString(),
                      UANNumber = dr["uANNumber"].ToString(),
                      Basic = dr["basic"].ToString(),
                      HRA = dr["hRA"].ToString(),
                      Bonus = dr["bonus"].ToString(),
                      OtherAllowance = dr["otherAllowance"].ToString(),
                      VARIABLEBONUS = dr["vARIABLEBONUS"].ToString(),
                      PF = dr["pF"].ToString(),
                      ESI = dr["eSI"].ToString(),
                      PROFTAX = dr["pROFTAX"].ToString(),
                      TotalEarningsINR = dr["totalEarningsINR"].ToString(),
                      TotalDeductionsINR = dr["totalDeductionsINR"].ToString(),
                      NetPay = dr["netPay"].ToString(),
                      NetPayInWord = dr["netPayInWord"].ToString(),

                  }).ToList();

            response = SalProcessDataMaster.EarningDataPaySlip(EmPID, curMonthNo, curYear, connstring).Tables[0];
            ER = (from DataRow dr in response.Rows
                  select new USpPrintSalaryEaringViewModel()
                  {
                      
                      Name = dr["name"].ToString(),
                      Amount = dr["Amount"].ToString(),

                  }).ToList();

            response = SalProcessDataMaster.DeductionDataPaySlip(EmPID, curMonthNo, curYear, connstring).Tables[0];
            DE = (from DataRow dr in response.Rows
                  select new USpPrintSalaryEaringViewModel()
                  {

                      Name = dr["name"].ToString(),
                      Amount = dr["Amount"].ToString(),

                  }).ToList();

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("PaySlipDataSet", pm);
            localReport.AddDataSource("EarningDataSet", ER);
            localReport.AddDataSource("DeductionDataSet", DE);

            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mintype);
            return File(result.MainStream,"application/pdf");

        }



        [HttpPost]
        public JsonResult GetEmployeeProcessSalary(string curMonthNo, string curYear)
        {
            List<USpSendMailSalaryMonthlyStatementViewModel> result = null;

            SalaryMonthlyStatementU salaryUtility = new SalaryMonthlyStatementU();
            System.Data.DataTable response = new System.Data.DataTable();
            List<USpSendMailSalaryMonthlyStatementViewModel> employeeList = new List<USpSendMailSalaryMonthlyStatementViewModel>();
            USpSendMailSalaryMonthlyStatementViewModel details = new USpSendMailSalaryMonthlyStatementViewModel();

            try
            {
                
                try
                {
                    string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
                    response = salaryUtility.GetEmployeeSalDetails(curMonthNo, curYear, connstring).Tables[0];

                    employeeList = (from DataRow dr in response.Rows
                                    select new USpSendMailSalaryMonthlyStatementViewModel()
                                    {
                                        EmployeeId = Convert.ToInt32(dr["employeeId"]),
                                        ID = Convert.ToInt32(dr["iD"]),
                                        EmployeeName = dr["employeeName"].ToString(),
                                        DOJ = dr["DOJ"].ToString(),
                                        DaysInMonth = Convert.ToInt32(dr["daysInMonth"]),
                                        LOP = Convert.ToDecimal(dr["lOP"]),
                                        TotalWorkingDays = Convert.ToDecimal(dr["totalWorkingDays"]),
                                        Month = dr["month"].ToString(),
                                        Basic = Convert.ToDecimal(dr["basic"]),
                                        HRA = Convert.ToDecimal(dr["HRA"]),
                                        Bonus = Convert.ToDecimal(dr["Bonus"]),
                                        OtherAllowance = Convert.ToDecimal(dr["OtherAllowance"]),
                                        Overttime = Convert.ToDecimal(dr["Overttime"]),
                                        ProfTax = Convert.ToDecimal(dr["ProfTax"]),
                                        Arrears = Convert.ToDecimal(dr["Arrears"]),
                                        Reimbursement = Convert.ToDecimal(dr["Reimbursement"]),
                                        Loan = Convert.ToDecimal(dr["Loan"]),
                                        AdvanceSalary = Convert.ToDecimal(dr["AdvanceSalary"]),
                                        MonthlyPF = Convert.ToDecimal(dr["MonthlyPF"]),
                                        MonthlyESIC = Convert.ToDecimal(dr["MonthlyESIC"]),
                                        TotalDeduction = Convert.ToDecimal(dr["TotalDeduction"]),
                                        NetPay = Convert.ToDecimal(dr["netPay"]),
                                    }).ToList();
                    ViewBag.EmployeeProcessSalaryDetails = employeeList;

                    salaryUtility.SalProcessMaliSendingListData = employeeList;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { Result = employeeList });
        }

    }
}
