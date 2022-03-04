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

namespace SwiftHR.Controllers
{
    public class EmpReimbursementController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();

        public EmpReimbursementController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }
        public IActionResult Index()
        {

            EmpReimbursementU EmpReimbursementMaster = new EmpReimbursementU();

            List<EmpReimbursement> ReimbursementDetailsList = new List<EmpReimbursement>();

            ReimbursementDetailsList = _context.EmpReimbursement.Where(x => x.IsActive == true)
                .ToList();

            var EmployeeId = "";
            var ReimbId = "";

            EmpReimbursementMaster = GetEarningsComponents();
            GetEmpReimbursementDetailsSP(EmployeeId,ReimbId);

            //ViewBag.ReimbursementList = ReimbursementDetailsList;

            return View("Index", EmpReimbursementMaster);
        }

        private EmpReimbursementU GetEarningsComponents()
        {
            EmpReimbursementU EarningscomponentsList = new EmpReimbursementU();

            List<LookUpDetailsM> LoanEariningList = new List<LookUpDetailsM>();
            LoanEariningList = _context.LookUpDetailsM.Where(e => e.LookUpId == Convert.ToInt32("1002")).ToList();

            EarningscomponentsList.EarningscomponentsList = LoanEariningList;

            return EarningscomponentsList;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmpReimbursementDetails(IFormCollection collection)
        {
            try
            {
                EmpReimbursementU EmpReimbursementMaster = new EmpReimbursementU();

                UspEmpReimbursementDetailsViewModel ABC = new UspEmpReimbursementDetailsViewModel();
                ViewBag.Message = EmpReimbursementMaster.AddReimbursementDetails(collection);
                EmpReimbursementMaster = GetEarningsComponents();
                
                var EmployeeId = "";
                var ReimbId = "";
                EmpReimbursementMaster = GetEmpReimbursementDetailsSP(EmployeeId, ReimbId);
                
                return View("Index", EmpReimbursementMaster);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return RedirectToAction("Index", "EmpReimbursementU");
            }
        }
        private EmpReimbursementU GetEmpReimbursementDetailsSP(string EmployeeId,string ReimbId)
        {
            EmpReimbursementU EmpReimbursementUMaster = new EmpReimbursementU();
            DataTable response = new DataTable();
            EmpReimbursementU EmpReimbursementDetailsData = new EmpReimbursementU();
            List<UspEmpReimbursementDetailsViewModel> EmpReimbursementList = new List<UspEmpReimbursementDetailsViewModel>();
            UspEmpReimbursementDetailsViewModel details = new UspEmpReimbursementDetailsViewModel();

            string connstring = ConfigurationManager.AppSetting.GetConnectionString("SHR_Client_DBConnection");
            response = EmpReimbursementUMaster.GetReimbursementDetails(EmployeeId, ReimbId, connstring).Tables[0];

            EmpReimbursementList = (from DataRow dr in response.Rows
                                    select new UspEmpReimbursementDetailsViewModel()
                                    {
                                        Id = Convert.ToInt32(dr["iD"]),
                                        EmployeeId = Convert.ToInt32(dr["employeeId"]),
                                        EmployeeNumber = Convert.ToInt32(dr["employeeNumber"]),
                                        EmployeeName = dr["employeeName"].ToString(),
                                        EarningsTypeFromLookUp = Convert.ToInt32(dr["earningsTypeFromLookUp"]),
                                        EarningsType = dr["earningsType"].ToString(),
                                        Date = dr["date"].ToString(),
                                        Amount = Convert.ToDecimal(dr["amount"]),
                                        PaymentEffectedDate = dr["paymentEffectedDate"].ToString(),
                                        Remarks = dr["remarks"].ToString(),
                                        Status = dr["status"].ToString(),
                                    }).ToList();

            ViewBag.ReimbursementList = EmpReimbursementList;
            EmpReimbursementUMaster = GetEarningsComponents();
            EmpReimbursementUMaster.EarningscomponentsListData = EmpReimbursementList;

            return EmpReimbursementUMaster;
        }

        public ActionResult SearchRecord(string EmpReimbId,string ReimbId)
        {
            EmpReimbursementU EmpReimbursementMaster = new EmpReimbursementU();

            //var result = policyMaster.EditPolicyMaster(Convert.ToInt32(policyId));
            
            EmpReimbursementMaster = GetEmpReimbursementDetailsSP("0",ReimbId);
            

            return new JsonResult(EmpReimbursementMaster);
            //ViewBag.Message = result;

            //return View("AddPolicy", policyMaster);
        }

        [HttpPost("DeleteReimbRecord")]
        public ActionResult DeleteReimbRecord(string ReimbId)
        {
            EmpReimbursementU EmpReimbursementMaster = new EmpReimbursementU();
            List<UspEmpReimbursementDetailsViewModel> EmpReimbursementList = new List<UspEmpReimbursementDetailsViewModel>();
            EmpReimbursementU DeleteMasters = new EmpReimbursementU();
            ViewBag.WarningMessage = DeleteMasters.DeleteReimbusment(Convert.ToInt32(ReimbId));
            var EmployeeId = "";
            var ReimbusmentId = "";
            EmpReimbursementMaster = GetEmpReimbursementDetailsSP(EmployeeId, ReimbusmentId);

            ViewBag.ReimbursementList = EmpReimbursementList;
            
            return View("Index", EmpReimbursementMaster);
        }
    }
}
