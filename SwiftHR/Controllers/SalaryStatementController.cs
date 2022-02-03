using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SwiftHR.Models;
using SwiftHR.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;


namespace SwiftHR.Controllers
{
    public class SalaryStatementController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();

        public SalaryStatementController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }
        public IActionResult Index()
        {

            AddSalaryUtility addSalaryUtility = new AddSalaryUtility();
            //LookUpMaster = GetLookupName();

            //List<SalaryHeader> SalaryHeaderList = new List<SalaryHeader>();
            //List<SalaryDetails> SalaryDetailsList = new List<SalaryDetails>();

            //SalaryHeaderList = _context.SalaryHeaders.Where(x => x.IsActive == true).ToList();
            //ViewBag.SalaryHeaderList = SalaryHeaderList;
            //
            //SalaryDetailsList = _context.SalaryDetails.Where(x => x.IsActive == true).ToList();
            //ViewBag.SalaryDetailsList = SalaryDetailsList;
            ViewBag.SalaryStatementList = null;
            GetSalStatementFromSP("");
            return View("Index", addSalaryUtility);

        }

        public void GetSalStatementFromSP(string PayMonth)
        {
            AddSalaryUtility salaryUtility = new AddSalaryUtility();
            DataTable response = new DataTable();
            List<SalaryMonthlyStatement> employeeList = new List<SalaryMonthlyStatement>();
            SalaryMonthlyStatement details = new SalaryMonthlyStatement();

            string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
            response = salaryUtility.GetSalayStatementDetails(PayMonth, connstring).Tables[0];

            employeeList = (from DataRow dr in response.Rows
                            select new SalaryMonthlyStatement()
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                EmployeeID = Convert.ToInt32(dr["EmployeeID"]),
                                EmployeeNumber = dr["EmployeeNumber"].ToString(),
                                EmployeeName = dr["EmployeeName"].ToString(),
                                DOJ = dr["DOJ"].ToString(),
                                DaysInMonth = dr["DaysInMonth"].ToString(),
                                LOPDays = dr["LOPDays"].ToString(),
                                TotalWorkingDays = dr["TotalWorkingDays"].ToString(),
                                PayoutMonth = dr["PayoutMonth"].ToString(),
                                Basic = Convert.ToDecimal(dr["Basic"]),
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
                                MonthlyNetPay = Convert.ToDecimal(dr["MonthlyNetPay"]),
                                MonthlyGrossPay = Convert.ToDecimal(dr["MonthlyGrossPay"]),
                                TotalDeduction = Convert.ToDecimal(dr["TotalDeduction"]),
                                NetPay = Convert.ToDecimal(dr["NetPay"]),
                            }).ToList();

            ViewBag.SalaryStatementList = employeeList;

            //using (ExcelPackage package = new ExcelPackage())
            //{
            //    ExcelWorksheet ws = package.Workbook.Worksheets.Add("Your Sheet Name");
            //    int rowNumber = 1;
            //    foreach (DataTable dt in response.Rows)
            //    {
            //        ws.Cells["A" + rowNumber].LoadFromDataTable(dt, true);
            //        rowNumber += dt.Rows.Count + 2; // to create 2 empty rows
            //    }

            //    package.SaveAs(new FileInfo(@"C:\SalaryStatementDoc\SalaryStatementDoc.xlsx"));
            //}
            if (!string.IsNullOrEmpty(PayMonth))
            {
                ExportDataSetToExcel(PayMonth);
            }
        }

        private void ExportDataSetToExcel(string PayMonth)
        {
            AddSalaryUtility salaryUtility = new AddSalaryUtility();
            DataTable response = new DataTable();
            List<SalaryMonthlyStatement> employeeList = new List<SalaryMonthlyStatement>();
            SalaryMonthlyStatement details = new SalaryMonthlyStatement();

            string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
            //response = salaryUtility.GetSalayStatementDetails(PayMonth, connstring).Tables[0];
            //DataTable dt = new DataTable();
            //dt = salaryUtility.GetSalayStatementDetails(PayMonth, connstring).Tables;

            //Creae an Excel application instance
            Excel.Application excelApp = new Excel.Application();

            //Create an Excel workbook instance and open it from the predefined location
            Excel.Workbook excelWorkBook = excelApp.Workbooks.Open(@"C:\SalaryStatementDoc\SalaryStatementDoc_2901.xlsx");

            foreach (DataTable table in salaryUtility.GetSalayStatementDetails(PayMonth, connstring).Tables)
            {
                //Add a new worksheet to workbook with the Datatable name
                Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = table.TableName;

                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }

                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    }
                }
            }

            excelWorkBook.Save();
            excelWorkBook.Close();
            excelApp.Quit();

        }

    }
}
