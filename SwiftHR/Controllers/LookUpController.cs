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

namespace SwiftHR.Controllers
{
    public class LookUpController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        public LookUpController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }
        public IActionResult Index()
        {

            LookUpMasterU LookUpMaster = new LookUpMasterU();
            LookUpMaster = GetLookupName();

            List<LookUpM> LookUpList = new List<LookUpM>();
            List<LookUpDetailsM> LookUpDetailsList = new List<LookUpDetailsM>();

            LookUpList = _context.LookUpM.Where(x => x.IsActive == false).ToList();
            ViewBag.LookUpList = LookUpList;

            LookUpDetailsList = _context.LookUpDetailsM.Where(x => x.IsActive == false).ToList();
            ViewBag.LookUpDetailsList = LookUpDetailsList;

            return View("Index", LookUpMaster);

        }
        private LookUpMasterU GetLookupName()
        {
            LookUpMasterU LookUpMaster = new LookUpMasterU();

            List<LookUpM> GetPolicyListName = new List<LookUpM>();
            GetPolicyListName = _context.LookUpM.Where(e => e.IsActive == false).ToList();

            LookUpMasterU LookUpDetailsMaster = new LookUpMasterU();

            List<LookUpDetailsM> GetLookUpDetailsListName = new List<LookUpDetailsM>();
            GetLookUpDetailsListName = _context.LookUpDetailsM.Where(e => e.IsActive == false).ToList();
            
            LookUpDetailsMaster.LookUpDetailsList = GetLookUpDetailsListName;
            LookUpMaster.LookUpList = GetPolicyListName;
            
            return LookUpMaster;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddLookUp(IFormCollection collection)
        {
            try
            {
                LookUpMasterU LookUpMaster = new LookUpMasterU();
                LookUpMasterU LooKupMasters = new LookUpMasterU();
                LookUpMaster = GetLookupName();
                ViewBag.Message = LooKupMasters.AddLookUp(collection);

                List<LookUpM> LookUpList = new List<LookUpM>();

                LookUpList = _context.LookUpM.Where(x => x.IsActive == false).ToList();
                ViewBag.LookUpList = LookUpList;

                return View("Index", LookUpMaster);
                //return RedirectToAction("AddPolicy", "AttandancePolicy");
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return RedirectToAction("AddPolicy", "AttandancePolicy");
            }
        }

        public ActionResult AddLookUpDetails(IFormCollection collection)
        {
            try
            {
                LookUpMasterU LookUpMaster = new LookUpMasterU();
                LookUpMasterU LooKupDetailsMasters = new LookUpMasterU();

                LookUpMaster = GetLookupName();
                ViewBag.Message = LooKupDetailsMasters.AddLookUpDeatisl(collection);

                List<LookUpDetailsM> LookUpList = new List<LookUpDetailsM>();

                LookUpList = _context.LookUpDetailsM.Where(x => x.IsActive == false).ToList();
                ViewBag.LookUpDetailsList = LookUpList;

                return View("Index", LookUpMaster);
                //return RedirectToAction("AddPolicy", "AttandancePolicy");
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return RedirectToAction("AddPolicy", "AttandancePolicy");
            }
        }

        public ActionResult SearchRecordLookUp(string LookUpId)
        {
            PolicyMaster policyMaster = new PolicyMaster();
            //var result = policyMaster.EditPolicyMaster(Convert.ToInt32(policyId));

            var result = (from a in _context.LookUpM
                          where a.LookUpId == Convert.ToInt32(LookUpId)
                          select a).ToList();

            return new JsonResult(result);
            //ViewBag.Message = result;

            //return View("AddPolicy", policyMaster);
        }

        [HttpPost("DeleteLookUpRecord")]
        public ActionResult DeleteLookUpRecord(string LookUpId)
        {
            LookUpMasterU LookUpM = new LookUpMasterU();

            LookUpMasterU LookUpMasters = new LookUpMasterU();
            ViewBag.WarningMessage = LookUpMasters.DeleteLookUp(Convert.ToInt32(LookUpId));

            List<LookUpM> LookUpList = new List<LookUpM>();

            LookUpList = _context.LookUpM.Where(x => x.IsActive == false).ToList();
            ViewBag.LookUpList = LookUpList;

            return View(LookUpM);
        }
        public ActionResult SearchRecordLookUpDetails(string LookUpDetailsId)
        {
            LookUpMasterU LookUpDetailsMaster = new LookUpMasterU();
            //var result = policyMaster.EditPolicyMaster(Convert.ToInt32(policyId));

            var result = (from a in _context.LookUpDetailsM
                          where a.LookUpDetailsId == Convert.ToInt32(LookUpDetailsId)
                          select a).ToList();

            return new JsonResult(result);
            //ViewBag.Message = result;

            //return View("AddPolicy", policyMaster);
        }

        [HttpPost("DeleteLookUpDeyailsData")]
        public ActionResult DeleteLookUpDetailsRecord(string LookUpDetailsId)
        {
            LookUpMasterU LookUpM = new LookUpMasterU();

            LookUpMasterU LookUpMasters = new LookUpMasterU();
            ViewBag.WarningMessage = LookUpMasters.DeleteLookUpDetails(Convert.ToInt32(LookUpDetailsId));

            List<LookUpDetailsM> LookUpDetailsList = new List<LookUpDetailsM>();

            LookUpDetailsList = _context.LookUpDetailsM.Where(x => x.IsActive == false).ToList();
            ViewBag.LookUpDetailsList = LookUpDetailsList;

            return View(LookUpM);
        }

    }
}
