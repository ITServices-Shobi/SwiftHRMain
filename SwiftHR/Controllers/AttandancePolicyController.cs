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

namespace SwiftHR.Controllers
{
    public class AttandancePolicyController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        public AttandancePolicyController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }
        public IActionResult AddPolicy()
        {
            PolicyMaster policyMaster = new PolicyMaster();

            policyMaster = GetAttandancePolicyName();
            List<AttandancePolicy> PolicyList = new List<AttandancePolicy>();

            PolicyList = _context.AttandancePolicies.Where(x => x.IsActive == false).ToList();
            ViewBag.PolicyList = PolicyList;

            if (IsAllowPageAccess("AttendancePolicySetup"))
                return View("AddPolicy", policyMaster);
            else
                return RedirectToAction("AccessDenied", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPolicyDetails(IFormCollection collection)
        {
            try
            {
                PolicyMaster policyMaster = new PolicyMaster();
                PolicyMaster PolicyAttMasters = new PolicyMaster();
                policyMaster = GetAttandancePolicyName();

                if (ModelState.IsValid)
                {
                    if (IsAllowPageAccess("AddEmployee"))
                    {
                        ViewBag.Message = PolicyAttMasters.AddPolicyMaster(collection);
                    }
                }

                List<AttandancePolicy> PolicyList = new List<AttandancePolicy>();

                PolicyList = _context.AttandancePolicies.Where(x => x.IsActive == false).ToList();
                ViewBag.PolicyList = PolicyList;

                return View("AddPolicy", policyMaster);
                //return RedirectToAction("AddPolicy", "AttandancePolicy");
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return RedirectToAction("AddPolicy", "AttandancePolicy");
            }
        }

        [HttpPost("DeleteRecord")]
        public ActionResult DeleteRecord(string policyId)
        {
            PolicyMaster policyM = new PolicyMaster();

            PolicyMaster PolicyAttMasters = new PolicyMaster();
            ViewBag.WarningMessage = PolicyAttMasters.DeletePolicyMaster(Convert.ToInt32(policyId));

            List<AttandancePolicy> PolicyList = new List<AttandancePolicy>();

            PolicyList = _context.AttandancePolicies.Where(x => x.IsActive == false).ToList();
            ViewBag.PolicyList = PolicyList;
            
            return View(policyM);          
        }
        public ActionResult SearchRecord(string policyId)
        {
            PolicyMaster policyMaster = new PolicyMaster();
            //var result = policyMaster.EditPolicyMaster(Convert.ToInt32(policyId));

            var result = (from a in _context.AttandancePolicies
                          where a.AttandancePolicyId == Convert.ToInt32(policyId)
                          select a).ToList();

            return new JsonResult(result);
            //ViewBag.Message = result;

            //return View("AddPolicy", policyMaster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPolicyCategory(IFormCollection collection)
        {
            try
            {
                PolicyMaster policyMaster = new PolicyMaster();
                PolicyMaster PolicyAttMasters = new PolicyMaster();
                policyMaster = GetAttandancePolicyName();

                if (ModelState.IsValid)
                {
                    if (IsAllowPageAccess("AddEmployee"))
                    {
                        ViewBag.Message = PolicyAttMasters.AddPolicyCategory(collection);
                    }
                }

                List<AttandancePolicyRulesCategory> PolicyList = new List<AttandancePolicyRulesCategory>();

                PolicyList = _context.AttandancePolicyRulesCategories.Where(x => x.IsActive == false).ToList();
                ViewBag.PolicyList = PolicyList;

                return View("AddPolicy", policyMaster);
                //return RedirectToAction("AddPolicy", "AttandancePolicy");
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return RedirectToAction("AddPolicy", "AttandancePolicy");
            }
        }
        public ActionResult AddPolicyRule(IFormCollection collection)
        {
            try
            {
                PolicyMaster policyMaster = new PolicyMaster();
                PolicyMaster PolicyAttMasters = new PolicyMaster();
                policyMaster = GetAttandancePolicyName();

                if (ModelState.IsValid)
                {
                    if (IsAllowPageAccess("AddEmployee"))
                    {
                        ViewBag.Message = PolicyAttMasters.AddPoliRule(collection);
                    }
                }
                List<AttandancePolicy> PolicyList = new List<AttandancePolicy>();
                PolicyList = _context.AttandancePolicies.Where(x => x.IsActive == false).ToList();
                ViewBag.PolicyList = PolicyList;

                return View("AddPolicy", policyMaster);
                //return RedirectToAction("AddPolicy", "AttandancePolicy");
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return RedirectToAction("AddPolicy", "AttandancePolicy");
            }
        }
        private PolicyMaster GetAttandancePolicyName()
        {
            PolicyMaster policyMaster = new PolicyMaster();

            List<AttandancePolicy> GetPolicyListName = new List<AttandancePolicy>();
            GetPolicyListName = _context.AttandancePolicies.Where(e => e.IsActive==false).ToList();

            List<AttandancePolicyRulesCategory> GetPolicyCategoryName = new List<AttandancePolicyRulesCategory>();
            GetPolicyCategoryName = _context.AttandancePolicyRulesCategories.Where(e => e.IsActive == false).ToList();

            policyMaster.PolicyList = GetPolicyListName;
            policyMaster.PolicyCategoryList = GetPolicyCategoryName;
            return policyMaster;
        }
        public string GetCookies(string key)
        {
            string cookieValue = string.Empty;
            cookieValue = Request.Cookies[key];
            return cookieValue;
        }
        public int GetLoggedInUserRoleId()
        {
            int roleId = 0;
            string rid = GetCookies("rid");

            if (!string.IsNullOrEmpty(rid))
            {
                string rlId = DataSecurity.DecryptString(rid);

                if (!string.IsNullOrEmpty(rlId))
                    roleId = Convert.ToInt32(rlId);
            }

            return roleId;
        }
        private bool IsAllowPageAccess(string pageName)
        {
            bool IsAllowAccess = false;
            int roleId = GetLoggedInUserRoleId();

            if (roleId > 0)
                IsAllowAccess = IsPageAccessAllowed(roleId, pageName);

            return IsAllowAccess;
        }
        public bool IsPageAccessAllowed(int roleId, string pageName)
        {
            bool IsAllowedAccess = false;
            IsAllowedAccess = Convert.ToBoolean((from a in _context.PageAccessSetups
                                                 join c in _context.PageModules on a.PageModuleId equals c.PageModuleId
                                                 where a.RoleId == roleId & c.PageModuleName == pageName
                                                 select a.IsAllow).SingleOrDefault());
            return IsAllowedAccess;
        }
    }
}
