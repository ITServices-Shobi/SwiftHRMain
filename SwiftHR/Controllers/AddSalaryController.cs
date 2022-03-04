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
    public class AddSalaryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();

        public AddSalaryController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }
        public IActionResult Index()
        {

            AddSalaryUtility addSalaryUtility = new AddSalaryUtility();
            //LookUpMaster = GetLookupName();

            List<SalaryHeader> SalaryHeaderList = new List<SalaryHeader>();
            List<SalaryDetails> SalaryDetailsList = new List<SalaryDetails>();

            SalaryHeaderList = _context.SalaryHeaders.Where(x => x.IsActive == true).ToList();
            ViewBag.SalaryHeaderList = SalaryHeaderList;

            SalaryDetailsList = _context.SalaryDetails.Where(x => x.IsActive == true).ToList();
            ViewBag.SalaryDetailsList = SalaryDetailsList;

            return View("Index", addSalaryUtility);

        }

        public ActionResult GetEmpName(string EmpNo)
        {
            Employee EmployeeMaster = new Employee();
            AddSalaryUtility salaryUtility = new AddSalaryUtility();
            DataTable response = new DataTable();
            List<SALEmployeeDetails> employeeList = new List<SALEmployeeDetails>();
            SALEmployeeDetails details = new SALEmployeeDetails();

            string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
            response = salaryUtility.GetSalEmpDetails(EmpNo, connstring).Tables[0];

            employeeList = (from DataRow dr in response.Rows
                            select new SALEmployeeDetails()
                            {
                                EmployeeId = Convert.ToInt32(dr["EmployeeId"]),

                                EmployeeNumber = dr["EmployeeNumber"].ToString(),
                                FirstName = dr["FirstName"].ToString(),
                                MiddleName = dr["MiddleName"].ToString(),
                                LastName = dr["LastName"].ToString(),
                                
                                Gender = dr["Gender"].ToString(),
                                DateOfJoining = dr["DateOfJoining"].ToString(),
                                DateOfBirth = dr["DateOfBirth"].ToString(),
                                Location = dr["Location"].ToString(),
                                
                                PFIsApplicable = Convert.ToBoolean(dr["PFIsApplicable"]),
                                VersionNumber = dr["VersionNumber"].ToString(),
                                Basic = Convert.ToDecimal(dr["Basic"]),
                                HRA = Convert.ToDecimal(dr["HRA"]),
                                Bonus = Convert.ToDecimal(dr["Bonus"]),
                                OtherAllowance = Convert.ToDecimal(dr["OtherAllowance"]),
                                Overttime = Convert.ToDecimal(dr["Overttime"]),
                                ProfTax = Convert.ToDecimal(dr["ProfTax"]),
                                Loan = Convert.ToDecimal(dr["Loan"]),
                                AdvanceSalary = Convert.ToDecimal(dr["AdvanceSalary"]),
                                EmployeeContributionPF = Convert.ToDecimal(dr["EmployeeContributionPF"]),
                                EmployeeContributionESIC = Convert.ToDecimal(dr["EmployeeContributionESIC"]),
                                EmployerContributionPF = Convert.ToDecimal(dr["EmployerContributionPF"]),
                                EmployerContributionESIC = Convert.ToDecimal(dr["EmployerContributionESIC"]),
                                MonthlyNetPay = Convert.ToDecimal(dr["MonthlyNetPay"]),
                                MonthlyGrossPay = Convert.ToDecimal(dr["MonthlyGrossPay"]),
                                AnnualGrossSalary = Convert.ToDecimal(dr["AnnualGrossSalary"]),
                                AnnualGrossCTC = Convert.ToDecimal(dr["AnnualGrossCTC"]),

                            }).ToList();

            //var result = (from a in _context.Employees
            //              where a.EmployeeNumber == Convert.ToInt32(EmpNo)
            //              select new
            //              {
            //                  EmployeeId = a.EmployeeId,
            //                  EmployeeNumber = a.EmployeeNumber,
            //                  FirstName = a.FirstName,
            //                  MiddleName = a.MiddleName,
            //                  LastName = a.LastName,
            //                  DateOfBirth = string.Format("{0:yyyy-MM-dd}", a.DateOfBirth),
            //                  DateOfJoining = string.Format("{0:yyyy-MM-dd}", a.DateOfJoining),
            //                  Gender = a.Gender

            //              }).ToList();

            return new JsonResult(employeeList);
            //ViewBag.Message = result;

            //return View("AddPolicy", policyMaster);
        }

        public ActionResult CalculateSalary(string GrossPay)
        {
            Employee EmployeeMaster = new Employee();
            //var result = policyMaster.EditPolicyMaster(Convert.ToInt32(policyId));

            decimal BasicPer, HRAPer, BonusPer, EmployeeESIPer, EmployerESIPer, EmployeePFPer, EmployerPFAcc10Per, EmployerPFEPSPer;

            decimal.TryParse(_configuration["SalaryInputPercentages:Basic"], out BasicPer);
            decimal.TryParse(_configuration["SalaryInputPercentages:HRA"], out HRAPer);
            decimal.TryParse(_configuration["SalaryInputPercentages:Bonus"], out BonusPer);
            decimal.TryParse(_configuration["SalaryInputPercentages:EmployeeESI"], out EmployeeESIPer);
            decimal.TryParse(_configuration["SalaryInputPercentages:EmployerESI"], out EmployerESIPer);
            decimal.TryParse(_configuration["SalaryInputPercentages:EmployeePF"], out EmployeePFPer);
            decimal.TryParse(_configuration["SalaryInputPercentages:EmployerPFAcc10"], out EmployerPFAcc10Per);
            decimal.TryParse(_configuration["SalaryInputPercentages:EmployerPFEPS"], out EmployerPFEPSPer);
            //_configuration["LeaveStatus:Approved"];

            decimal MonthlyGrossPay = 0, BasicAmt = 0, HRAAmt = 0, BonusAmt = 0, EmployeeESIAmt = 0, EmployerESIAmt = 0, EmployeePFAmt = 0, EmployerPFAcc10Amt = 0, EmployerPFEPSAmt = 0, OtherAllowanceAmt = 0, OverttimeAmt = 0, ProfTaxAmt = 0, LoanAmt = 0,
                AdvanceSalaryAmt = 0, EmployeeContributionPFAmt = 0, EmployeeContributionESICAmt = 0, EmployerContributionPFAmt = 0, EmployerContributionESICAmt = 0, MonthlyNetPayAmt = 0, AnnualGrossSalaryAmt = 0, AnnualGrossCTCAmt = 0;

            if (!String.IsNullOrEmpty(GrossPay))
            {
                decimal.TryParse(GrossPay, out MonthlyGrossPay);

                BasicAmt = (MonthlyGrossPay / 100) * BasicPer;
                HRAAmt = (BasicAmt / 100) * HRAPer;
                if(BasicAmt >= 7001 && BasicAmt <= 21000)
                    BonusAmt = (BasicAmt / 100) * BonusPer;
                OtherAllowanceAmt = (MonthlyGrossPay) - (BasicAmt + HRAAmt + BonusAmt);
                ProfTaxAmt = 200;
                if (BasicAmt < 15000)
                {
                    EmployeeContributionPFAmt = (BasicAmt / 100) * EmployeePFPer;
                }
                else
                {
                    EmployeeContributionPFAmt = (15000 / 100) * EmployeePFPer;
                }
                EmployerContributionPFAmt = EmployeeContributionPFAmt;
                if (MonthlyGrossPay < 21000)
                {
                    EmployeeContributionESICAmt = (MonthlyGrossPay / 100) * EmployeeESIPer;
                    EmployerContributionESICAmt = (MonthlyGrossPay / 100) * EmployerESIPer;
                }
                MonthlyNetPayAmt = (MonthlyGrossPay) - (ProfTaxAmt + EmployeeContributionPFAmt + EmployeeContributionESICAmt);
                AnnualGrossSalaryAmt = MonthlyGrossPay * 12;
                AnnualGrossCTCAmt = (MonthlyGrossPay * 12)+(EmployerContributionPFAmt * 12)+(EmployerContributionESICAmt*12);
            }
            var result = new { Basic = BasicAmt, HRA = HRAAmt, Bonus = BonusAmt, OtherAllowance = OtherAllowanceAmt, ProfTax = ProfTaxAmt, EmployeeContributionPF = EmployeeContributionPFAmt,
                EmployerContributionPF = EmployerContributionPFAmt, EmployeeContributionESIC = EmployeeContributionESICAmt, EmployerContributionESIC = EmployerContributionESICAmt, 
                MonthlyNetPay = MonthlyNetPayAmt, AnnualGrossCTC = AnnualGrossCTCAmt, AnnualGrossSalary = AnnualGrossSalaryAmt };

            return new JsonResult(result);
            //ViewBag.Message = result;

            //return View("AddPolicy", policyMaster);
        }

        public ActionResult CalculateBasicSalary(string GrossPay)
        {
            Employee EmployeeMaster = new Employee();
            //var result = policyMaster.EditPolicyMaster(Convert.ToInt32(policyId));

            decimal BasicPer, HRAPer, BonusPer, EmployeeESIPer, EmployerESIPer, EmployeePFPer, EmployerPFAcc10Per, EmployerPFEPSPer;

            decimal.TryParse(_configuration["SalaryInputPercentages:Basic"], out BasicPer);
            decimal.TryParse(_configuration["SalaryInputPercentages:HRA"], out HRAPer);
            decimal.TryParse(_configuration["SalaryInputPercentages:Bonus"], out BonusPer);
            decimal.TryParse(_configuration["SalaryInputPercentages:EmployeeESI"], out EmployeeESIPer);
            decimal.TryParse(_configuration["SalaryInputPercentages:EmployerESI"], out EmployerESIPer);
            decimal.TryParse(_configuration["SalaryInputPercentages:EmployeePF"], out EmployeePFPer);
            decimal.TryParse(_configuration["SalaryInputPercentages:EmployerPFAcc10"], out EmployerPFAcc10Per);
            decimal.TryParse(_configuration["SalaryInputPercentages:EmployerPFEPS"], out EmployerPFEPSPer);
            //_configuration["LeaveStatus:Approved"];

            decimal MonthlyGrossPay = 0, BasicAmt = 0, HRAAmt = 0, BonusAmt = 0, EmployeeESIAmt = 0, EmployerESIAmt = 0, EmployeePFAmt = 0, EmployerPFAcc10Amt = 0, EmployerPFEPSAmt = 0, OtherAllowanceAmt = 0, OverttimeAmt = 0, ProfTaxAmt = 0, LoanAmt = 0,
                AdvanceSalaryAmt = 0, EmployeeContributionPFAmt = 0, EmployeeContributionESICAmt = 0, EmployerContributionPFAmt = 0, EmployerContributionESICAmt = 0, MonthlyNetPayAmt = 0, AnnualGrossSalaryAmt = 0, AnnualGrossCTCAmt = 0;

            if (!String.IsNullOrEmpty(GrossPay))
            {
                decimal.TryParse(GrossPay, out BasicAmt);

                //BasicAmt = (MonthlyGrossPay / 100) * BasicPer;
                HRAAmt = (BasicAmt / 100) * HRAPer;
                if (BasicAmt >= 7001 && BasicAmt <= 21000)
                    BonusAmt = (BasicAmt / 100) * BonusPer;
                OtherAllowanceAmt = (MonthlyGrossPay) - (BasicAmt + HRAAmt + BonusAmt);
                ProfTaxAmt = 200;
                if (BasicAmt < 15000)
                {
                    EmployeeContributionPFAmt = (BasicAmt / 100) * EmployeePFPer;
                }
                else
                {
                    EmployeeContributionPFAmt = (15000 / 100) * EmployeePFPer;
                }
                EmployerContributionPFAmt = EmployeeContributionPFAmt;
                //if (MonthlyGrossPay < 21000)
                //{
                //    EmployeeContributionESICAmt = (MonthlyGrossPay / 100) * EmployeeESIPer;
                //    EmployerContributionESICAmt = (MonthlyGrossPay / 100) * EmployerESIPer;
                //}
                //MonthlyNetPayAmt = (MonthlyGrossPay) - (ProfTaxAmt + EmployeeContributionPFAmt + EmployerContributionPFAmt + EmployeeContributionESICAmt + EmployerContributionESICAmt);
                //AnnualGrossSalaryAmt = MonthlyGrossPay * 12;
                //AnnualGrossCTCAmt = MonthlyNetPayAmt * 12;
            }
            var result = new
            {
                Basic = BasicAmt,
                HRA = HRAAmt,
                Bonus = BonusAmt,
                OtherAllowance = OtherAllowanceAmt,
                ProfTax = ProfTaxAmt,
                EmployeeContributionPF = EmployeeContributionPFAmt,
                EmployerContributionPF = EmployerContributionPFAmt,
                EmployeeContributionESIC = EmployeeContributionESICAmt,
                EmployerContributionESIC = EmployerContributionESICAmt,
                MonthlyNetPay = MonthlyNetPayAmt,
                AnnualGrossCTC = AnnualGrossCTCAmt,
                AnnualGrossSalary = AnnualGrossSalaryAmt
            };

            return new JsonResult(result);
            //ViewBag.Message = result;

            //return View("AddPolicy", policyMaster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddSalaryDetails(IFormCollection collection)
        {
            try
            {
                AddSalaryUtility addSalaryUtility = new AddSalaryUtility();

                ViewBag.Message = addSalaryUtility.AddSalaryDetails(collection);

                List<SalaryHeader> salaryHeadersList = new List<SalaryHeader>();

                salaryHeadersList = _context.SalaryHeaders.Where(x => x.IsActive == true).ToList();
                ViewBag.salaryHeadersList = salaryHeadersList;

                return View("Index", addSalaryUtility);

                //return RedirectToAction("AddPolicy", "AttandancePolicy");
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return RedirectToAction("Index", "AddSalary");
            }
        }

    }
}
