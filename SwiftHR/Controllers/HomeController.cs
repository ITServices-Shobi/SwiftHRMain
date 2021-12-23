using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SwiftHR.Models;
using SwiftHR.Utility;


namespace SwiftHR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        
        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Login
        public IActionResult Login(UserDetail userDetail)
        {
            if (ModelState.IsValid)
            {
                string userName = userDetail.UserName;
                string password = userDetail.UserPassword;

                if (!_context.UserDetails.Any(u => u.UserName == userName && u.UserPassword == password))
                {
                    return View("Index");
                }
                else
                {
                    Employee empData = new Employee();
                    empData = GetEmployeeDetailsByEmpNumber(userName);

                    string empId = DataSecurity.EncryptString(empData.EmployeeId.ToString());

                    SetCookies("eid", empId);

                    int userId = GetUserByUserName(userName).UserId;

                    string usrId = DataSecurity.EncryptString(userId.ToString());

                    SetCookies("uid", usrId);

                    int roleId = Convert.ToInt32(GetUserByUserName(userName).RoleId);

                    string rlId = DataSecurity.EncryptString(roleId.ToString());

                    SetCookies("rid", rlId);
                    
                    bool IsAllowAccess = false;

                    if (roleId > 0)
                    {
                        List<FeedsGroup> postsGroup = new List<FeedsGroup>();
                        postsGroup = _context.FeedsGroups.ToList();

                        IsAllowAccess = IsPageAccessAllowed(roleId, "Dashboard");

                        if (IsAllowAccess)
                        {
                            return RedirectToAction("Dashboard", "Home");
                            //return View("Dashboard", postsGroup);
                        }
                        else
                        {
                            if (IsAllowAccess = IsPageAccessAllowed(roleId, "ESSDashboard"))
                            {
                                return RedirectToAction("ESSDashboard", "Home");

                                //return View("ESSDashboard", postsGroup);
                            }
                            else
                                return View("Index");
                        }
                    }
                    else
                        return View("Index");
                }

            }
            else
                return View("Index");
        }
        #endregion

        #region Dashboard
        public IActionResult Dashboard()
        {


            //List<FeedsGroup> postsGroup = new List<FeedsGroup>();
            //postsGroup = _context.FeedsGroups.ToList();


            DashBoard dBoard = new DashBoard();

            List<LeaveApplyDetail> leaveData = new List<LeaveApplyDetail>();
            leaveData = _context.LeaveApplyDetails.ToList();

            List<Employee> empData = new List<Employee>();
            empData = _context.Employees.ToList();

            List<Department> deptData = new List<Department>();
            deptData = _context.Departments.ToList();

            List <EmpOnboardingDetail> onbData = new List<EmpOnboardingDetail>();
            onbData = _context.EmpOnboardingDetails.ToList();

            dBoard.empMasterDataItems = empData;
            dBoard.leaveApplyListAll = leaveData;
            dBoard.departmentListAll = deptData;
            dBoard.empOnboardingDetails = onbData;
            

            return View("Dashboard", dBoard);
        }

        #endregion

        #region Posts
        public IActionResult Posts()
        {
            List<FeedsGroup> postsGroup = new List<FeedsGroup>();
            postsGroup = _context.FeedsGroups.ToList();
            return View("Posts", postsGroup);
        }

        public ActionResult ShowPostsData()
        {
            List<EmployeeFeed> commentsFeedsData = new List<EmployeeFeed>();
            commentsFeedsData = _context.EmployeeFeeds.ToList();
            List<EmployeeFeedsAllData> feedsAllList = new List<EmployeeFeedsAllData>();
            feedsAllList = GetFeedsAllFilteredListWithLikesAndComments(commentsFeedsData);
            feedsAllList = feedsAllList.OrderByDescending(x => x.employeeFeed.FeedsId).ToList();
            return PartialView("ShowPostsData", feedsAllList);
        }

        public ActionResult ShowGroupPostsData(string FeedsGroupId)
        {
            int postsGroupId = -1;
            List<EmployeeFeed> commentsFeedsData = new List<EmployeeFeed>();

            if (!string.IsNullOrEmpty(FeedsGroupId))
                postsGroupId = Convert.ToInt32(FeedsGroupId);

            if (postsGroupId == 0)
            {
                int employeeId = GetLoggedInEmpId();               
                commentsFeedsData = _context.EmployeeFeeds.Where(i => i.EmployeeId == employeeId).ToList();
            }
            else if(postsGroupId > 0)
                commentsFeedsData = _context.EmployeeFeeds.Where(i => i.FeedsGroupId == postsGroupId).ToList();
                      
            List<EmployeeFeedsAllData> feedsAllList = new List<EmployeeFeedsAllData>();
            feedsAllList = GetFeedsAllFilteredListWithLikesAndComments(commentsFeedsData);
            feedsAllList = feedsAllList.OrderByDescending(x => x.employeeFeed.FeedsId).ToList();
            return PartialView("ShowPostsData", feedsAllList);
        }

        public ActionResult AddPostsData()
        {
            List<FeedsGroup> postsGroup = new List<FeedsGroup>();
            postsGroup = _context.FeedsGroups.ToList();

            return PartialView("AddPostsData", postsGroup);
        }

        [HttpPost("InsertPostsData")]
        public async Task<IActionResult> InsertPostsData(IFormFile file, string PostTextData, string FeedsGroupId)
        {
            bool IsCallValid = false;

            if (file != null)
                IsCallValid = true;
            else if (!string.IsNullOrEmpty(PostTextData))
                IsCallValid = true;


            if (!string.IsNullOrEmpty(FeedsGroupId) && IsCallValid)
            {
                string fileUploadName = string.Empty;

                //var filePaths = new List<string>();

                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    long size = file.Length;
                    // full path to file in temp location
                    var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot\\FeedsImages", file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    fileUploadName = file.FileName;
                }

                string feedsDescription = PostTextData;
                int employeeId = GetLoggedInEmpId();

                EmployeeFeed empFeed = new EmployeeFeed();
                empFeed.FeedsId = 0;
                empFeed.EmployeeId = employeeId;
                empFeed.FeedsDescription = feedsDescription;
                empFeed.FeedsFileName = fileUploadName;
                empFeed.FeedsGroupId = Convert.ToInt32(FeedsGroupId);
                empFeed.VisibilityDate = string.Empty;
                empFeed.CreatedDate = CurrentDate();
                empFeed.CreatedTime = CurrentTime();

                _context.EmployeeFeeds.Add(empFeed);
                await _context.SaveChangesAsync();

            }

            List<FeedsGroup> postsGroup = new List<FeedsGroup>();
            postsGroup = _context.FeedsGroups.ToList();

            return View("Posts", postsGroup);

            //if (IsAllowPageAccess("Dashboard"))
            //    return View("Dashboard", postsGroup);
            //else if (IsAllowPageAccess("ESSDashboard"))
            //    return View("ESSDashboard", postsGroup);
            //else
            //    return View("AccessDenied");

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            //return Ok(new { count = files.Count, size, filePaths });
        }

        public List<EmployeeFeedsAllData> GetFeedsAllFilteredListWithLikesAndComments(List<EmployeeFeed> commentsFeedsData)
        {
            List<EmployeeFeedsAllData> feedsAllList = new List<EmployeeFeedsAllData>();

            foreach (EmployeeFeed empFeed in commentsFeedsData)
            {
                EmployeeFeedsAllData allfeeds = new EmployeeFeedsAllData();

                allfeeds.employeeFeed = empFeed;

                allfeeds.LikesCount = _context.FeedsCommentsAndLikes
                                .Where(o => o.FeedsId == empFeed.FeedsId & o.IsLike == true)
                                .Count();

                allfeeds.CommentsCount = _context.FeedsCommentsAndLikes
                                  .Where(o => o.FeedsId == empFeed.FeedsId & o.IsComment == true)
                                  .Count();

                string FirstName = _context.Employees
                                  .Where(o => o.EmployeeId == empFeed.EmployeeId)
                                  .Select(o => o.FirstName).SingleOrDefault();

                string MiddleName = _context.Employees
                                  .Where(o => o.EmployeeId == empFeed.EmployeeId)
                                  .Select(o => o.MiddleName).SingleOrDefault();

                string LastName = _context.Employees
                                  .Where(o => o.EmployeeId == empFeed.EmployeeId)
                                  .Select(o => o.LastName).SingleOrDefault();

                string EmployeeProfilePhoto = _context.UserDetails
                                  .Where(o => o.EmployeeId == empFeed.EmployeeId)
                                  .Select(o => o.ProfilePicturePath).SingleOrDefault();

                //string EmployeeProfilePhoto = string.Empty;

                allfeeds.EmployeeName = FirstName + " " + MiddleName + " " + LastName;

                if (!string.IsNullOrEmpty(EmployeeProfilePhoto))
                    allfeeds.EmployeeProfilePhoto = EmployeeProfilePhoto;
                else
                    allfeeds.EmployeeProfilePhoto = "default-avatar.png";



                List<FeedsCommentsAndLike> feedsCommentsAndLikes = new List<FeedsCommentsAndLike>();
                feedsCommentsAndLikes = _context.FeedsCommentsAndLikes.Where(i => i.FeedsId == empFeed.FeedsId & i.IsComment == true).ToList();
                List<FeedsAllComments> feedCommentsAllList = new List<FeedsAllComments>();

                foreach (FeedsCommentsAndLike empFeedComment in feedsCommentsAndLikes)
                {
                    FeedsAllComments feedCMD = new FeedsAllComments();
                    feedCMD.feedComments = empFeedComment;

                    string First_Name = _context.Employees
                                 .Where(o => o.EmployeeId == empFeedComment.EmployeeId)
                                 .Select(o => o.FirstName).SingleOrDefault();

                    string Middle_Name = _context.Employees
                                      .Where(o => o.EmployeeId == empFeedComment.EmployeeId)
                                      .Select(o => o.MiddleName).SingleOrDefault();

                    string Last_Name = _context.Employees
                                      .Where(o => o.EmployeeId == empFeedComment.EmployeeId)
                                      .Select(o => o.LastName).SingleOrDefault();

                    string Employee_ProfilePhoto = _context.UserDetails
                                  .Where(o => o.EmployeeId == empFeedComment.EmployeeId)
                                  .Select(o => o.ProfilePicturePath).SingleOrDefault();

                    //string Employee_ProfilePhoto = string.Empty;

                    feedCMD.CommentEmployeeName = First_Name + " " + Middle_Name + " " + Last_Name;

                    if (!string.IsNullOrEmpty(Employee_ProfilePhoto))
                        feedCMD.CommentEmployeeProfilePhoto = Employee_ProfilePhoto;
                    else
                        feedCMD.CommentEmployeeProfilePhoto = "default-avatar.png";

                    feedCommentsAllList.Add(feedCMD);
                }

                allfeeds.feedsComments = feedCommentsAllList.TakeLast(3).ToList();


                feedsAllList.Add(allfeeds);
            }

            return feedsAllList;
        }

        public ActionResult AddLikesToFeed(string FeedsId)
        {
            int employeeId = GetLoggedInEmpId();
            FeedsCommentsAndLike empFeedCL = new FeedsCommentsAndLike();
            empFeedCL.FeedsId = Convert.ToInt32(FeedsId);
            empFeedCL.EmployeeId = employeeId;
            empFeedCL.IsLike = true;
            empFeedCL.CreatedDate = Common.CurrentDate();
            empFeedCL.CreatedTime = Common.CurrentTime();

            using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
            {
                if (!EmployeeFeedsLikeExists(employeeId, Convert.ToInt32(FeedsId)))
                {
                    entities.FeedsCommentsAndLikes.Add(empFeedCL);
                    entities.SaveChanges();
                    //id = empFeedCL.FeedsClid.ToString();
                }
            }


            List<EmployeeFeed> commentsFeedsData = new List<EmployeeFeed>();
            commentsFeedsData = _context.EmployeeFeeds.ToList();
            List<EmployeeFeedsAllData> feedsAllList = new List<EmployeeFeedsAllData>();
            feedsAllList = GetFeedsAllFilteredListWithLikesAndComments(commentsFeedsData);
            feedsAllList = feedsAllList.OrderByDescending(x => x.employeeFeed.FeedsId).ToList();
            return PartialView("ShowPostsData", feedsAllList);
        }

        private bool EmployeeFeedsLikeExists(int empId, int feedsId)
        {
            return _context.FeedsCommentsAndLikes.Any(e => e.EmployeeId == empId & e.FeedsId == feedsId & e.IsLike == true);
        }

        public ActionResult AddCommentsToFeed(string FeedsId, string groupId, string feedsComment)
        {
            int employeeId = GetLoggedInEmpId();
            FeedsCommentsAndLike empFeedCL = new FeedsCommentsAndLike();
            empFeedCL.FeedsId = Convert.ToInt32(FeedsId);
            empFeedCL.EmployeeId = employeeId;
            empFeedCL.IsComment = true;
            empFeedCL.Comments = feedsComment;
            empFeedCL.CreatedDate = Common.CurrentDate();
            empFeedCL.CreatedTime = Common.CurrentTime();

            using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
            {
                entities.FeedsCommentsAndLikes.Add(empFeedCL);
                entities.SaveChanges();
                //id = employeeFeeds.FeedsClid.ToString();
            }

            int grpId = -1;

            if (!string.IsNullOrEmpty(groupId))
                grpId = Convert.ToInt32(groupId);

            List<EmployeeFeed> commentsFeedsData = new List<EmployeeFeed>();
            List<EmployeeFeedsAllData> feedsAllList = new List<EmployeeFeedsAllData>();

            if (grpId == -1)
            {
                commentsFeedsData = _context.EmployeeFeeds.ToList();
                feedsAllList = GetFeedsAllFilteredListWithLikesAndComments(commentsFeedsData);
            }
            else if (grpId == 0)
            {
                commentsFeedsData = _context.EmployeeFeeds.Where(i => i.EmployeeId == employeeId).ToList();
                feedsAllList = GetFeedsAllFilteredListWithLikesAndComments(commentsFeedsData);
            }
            else
            {
                commentsFeedsData = _context.EmployeeFeeds.Where(i => i.FeedsGroupId == grpId).ToList();
                feedsAllList = GetFeedsAllFilteredListWithLikesAndComments(commentsFeedsData);
            }

            if (feedsAllList.Count > 0)
                feedsAllList = feedsAllList.OrderByDescending(x => x.employeeFeed.FeedsId).ToList();

            //return PartialView("ShowPostsData", feedsAllList);
            string result = "true";

            return new JsonResult(result);
        }

        public ActionResult GetCommentsToFeed(string groupId)
        {
            int employeeId = GetLoggedInEmpId();
            int grpId = -1;

            if (!string.IsNullOrEmpty(groupId) && groupId != "undefined")
                grpId = Convert.ToInt32(groupId);

            List<EmployeeFeed> commentsFeedsData = new List<EmployeeFeed>();
            List<EmployeeFeedsAllData> feedsAllList = new List<EmployeeFeedsAllData>();

            if (grpId == -1)
            {
                commentsFeedsData = _context.EmployeeFeeds.ToList();
                feedsAllList = GetFeedsAllFilteredListWithLikesAndComments(commentsFeedsData);
            }
            else if (grpId == 0)
            {
                commentsFeedsData = _context.EmployeeFeeds.Where(i => i.EmployeeId == employeeId).ToList();
                feedsAllList = GetFeedsAllFilteredListWithLikesAndComments(commentsFeedsData);
            }
            else
            {
                commentsFeedsData = _context.EmployeeFeeds.Where(i => i.FeedsGroupId == grpId).ToList();
                feedsAllList = GetFeedsAllFilteredListWithLikesAndComments(commentsFeedsData);
            }

            if (feedsAllList.Count > 0)
                feedsAllList = feedsAllList.OrderByDescending(x => x.employeeFeed.FeedsId).ToList();

            return PartialView("ShowPostsData", feedsAllList);
        }


        #endregion

        #region Common

        [HttpGet]
        public IActionResult GetUserDetails()
        {
            int userId = GetLoggedInUserId();

            EmployeeUserDetails employeeUserDetails = new EmployeeUserDetails();
            UserDetail userData = _context.UserDetails.Where(i => i.UserId == userId).SingleOrDefault();

            int empId = 0;

            if(!String.IsNullOrEmpty(userData.UserId.ToString()) && userData.UserId > 0)
            {
                if (string.IsNullOrEmpty(userData.ProfilePicturePath))
                    userData.ProfilePicturePath = "default-avatar.png";

                userData.UserPassword = string.Empty;

                String role = "";

                role = Convert.ToString((from a in _context.UserDetails
                                                     join c in _context.RoleMasters on a.RoleId equals c.RoleId
                                                     where c.IsActive == true & a.EmployeeId== userData.EmployeeId
                                         select c.RoleName).SingleOrDefault());

                employeeUserDetails.userRoleName = role;
                employeeUserDetails.userDetails = userData;

                if (userData.EmployeeId > 0)
                {
                    empId = Convert.ToInt32(userData.EmployeeId);

                    Employee employee = _context.Employees.Where(i => i.EmployeeId == empId).SingleOrDefault();

                    if (employee.EmployeeId > 0)
                        employeeUserDetails.empDetails = employee;
                }

                
                employeeUserDetails.addEmployeeAccess= IsAllowPageAccess("AddEmployee");
                employeeUserDetails.employeeListAccess = IsAllowPageAccess("EmployeeList");
                employeeUserDetails.generalSettingsAccess = IsAllowPageAccess("GeneralSettings");
                employeeUserDetails.leaveTypesAccess = IsAllowPageAccess("LeaveTypes");
                employeeUserDetails.leaveRulesAccess = IsAllowPageAccess("LeaveRules");
                employeeUserDetails.leavePolicySetupAccess = IsAllowPageAccess("LeavePolicySetup");
                employeeUserDetails.notificationsAccess = IsAllowPageAccess("Notifications");
                employeeUserDetails.leaveReasonsAccess = IsAllowPageAccess("LeaveReasons");
                employeeUserDetails.leaveReportsAccess = IsAllowPageAccess("LeaveReports");
                employeeUserDetails.shiftsAccess = IsAllowPageAccess("Shifts");
                employeeUserDetails.attendancePolicySetupAccess = IsAllowPageAccess("AttendancePolicySetup");
                employeeUserDetails.attendanceSchemeAccess = IsAllowPageAccess("AttendanceScheme");
                employeeUserDetails.showLeavesAccess = IsAllowPageAccess("ShowLeaves");
                employeeUserDetails.applyLeaveAccess = IsAllowPageAccess("ApplyLeave");
                employeeUserDetails.showAttendanceAccess = IsAllowPageAccess("ShowAttendance");
                employeeUserDetails.helpDeskSetupAccess = IsAllowPageAccess("HelpDeskSetup");
                employeeUserDetails.helpDeskAnalysisAccess = IsAllowPageAccess("HelpDeskAnalysis");
                employeeUserDetails.reportsMappingAccess = IsAllowPageAccess("ReportsMapping");
                employeeUserDetails.dashboardAccess = IsAllowPageAccess("Dashboard");
                employeeUserDetails.eSSDashboardAccess = IsAllowPageAccess("ESSDashboard");

            }

            string result = JsonConvert.SerializeObject(employeeUserDetails);
            return new JsonResult(result);
        }

        public ActionResult ModifyPassword(String callingView)
        {
            int loggedUser = GetLoggedInUserId();
            if(loggedUser>0)
            {
                UserDetail usrData = new UserDetail();
                usrData = _context.UserDetails.Where(o => o.UserId == Convert.ToInt32(loggedUser)).SingleOrDefault();
                return PartialView("UserPasswordChange", usrData);
            }
            return null;
        }

        // POST: Password Data for save
        [HttpPost("UpdatePassword")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(IFormFile PicturePath, IFormCollection collection)
        {
            UserDetail usrData = new UserDetail();
            int loggedUser = GetLoggedInUserId();
            int loggedEmployee = GetLoggedInEmpId();
            if (loggedUser > 0)
            {
                usrData = _context.UserDetails.Where(o => o.UserId == Convert.ToInt32(loggedUser) && o.EmployeeId== Convert.ToInt32(loggedEmployee)).SingleOrDefault();
                if (usrData != null)
                {
                    if (!string.IsNullOrEmpty(collection["newUserPassword"]))
                        usrData.UserPassword = collection["newUserPassword"];

                    //Upload profile picture
                    string fileUploadName = string.Empty;
                    if (PicturePath != null && !string.IsNullOrEmpty(PicturePath.FileName))
                    {
                        long size = PicturePath.Length;
                        // full path to file in temp location
                        var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot\\UploadImages", PicturePath.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await PicturePath.CopyToAsync(stream);
                        }

                        fileUploadName = PicturePath.FileName;
                        usrData.ProfilePicturePath = fileUploadName;
                    }
                    _context.SaveChanges();
                }
                // return Content(@"<script>window.close();</script>", "text/html");
                //string headder=HttpContext.Request.Headers["Referer"];
                //string headder = HttpContext.Request.fo;
                //return View(headder);
                // return PartialView("UserPasswordChange", usrData);
                //Login(usrData);
                // return ModifyPassword("Self");

            }
            
            Login(usrData);
            string result = JsonConvert.SerializeObject(usrData);
            return new JsonResult(result);
            //return ModifyPassword("Self");
            //return PartialView("UserPasswordChange");
        }

        private Employee GetEmployeeDetailsByEmpNumber(string userName)
        {
            Employee empData = _context.Employees.Where(i => i.EmployeeNumber == Convert.ToInt32(userName)).Single();
            return empData;
        }

        private Department GetEmployeeDepartmentColorByDeptNumber(string deptName)
        {
            Department deptData = _context.Departments.Where(i => i.DepartmentName == deptName).Single();

            return deptData;
        }

        //public ActionResult NewJoiningEmployeeDetails(string empId)
        //{
        //    Employee empData = new Employee();
        //    empData = _context.Employees.Where(o => o.EmployeeId == Convert.ToInt32(empId)).SingleOrDefault();
        //    return PartialView("Dashboard", empData);
        //}

        private UserDetail GetUserByUserName(string userName)
        {
            UserDetail userData = _context.UserDetails.Where(i => i.UserName == userName).Single();
            return userData;
        }

        protected void SetCookies(string key, string value)
        {
            int minutes = 30;

            HttpContext.Response.Cookies.Append(key, value, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(minutes)
            });
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

        public int GetLoggedInUserId()
        {
            int userId = 0;
            string uid = GetCookies("uid");

            if (!string.IsNullOrEmpty(uid))
            {
                string usrId = DataSecurity.DecryptString(uid);

                if (!string.IsNullOrEmpty(usrId))
                    userId = Convert.ToInt32(usrId);
            }

            return userId;
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

        public static string CurrentDate()
        {
            return (TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MMM-yyyy"));
        }

        public static string CurrentTime()
        {
            return (TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("HH:mm"));
        }
        #endregion

        #region Other
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
