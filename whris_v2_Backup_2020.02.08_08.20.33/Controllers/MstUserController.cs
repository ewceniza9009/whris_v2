using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.DynamicLinq;
using Filter = Kendo.DynamicLinq.Filter;

namespace whris_v2.Controllers
{
    public class MstUserController : Controller
    {
        public Data.whrisDataContext whris;

        #region View
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region Parent
        public ActionResult GetUserList()
        {
            whris = new Data.whrisDataContext();

            var result = whris.MstUsers
                .Select(i => new Models.MstUser
                {
                    Id = i.Id,
                    Username = i.UserName,
                    FullName = i.FullName,
                    IsLocked = i.IsLocked
                });

            return Json(result.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult MstUserDetail(int modelId)
        {
            whris = new Data.whrisDataContext();

            var result = whris.MstUsers
                .Where(x => x.Id == modelId)
                .Select(i => new Models.MstUser
                {
                    Id = i.Id,
                    Username = i.UserName,
                    FullName = i.FullName,
                    Password = i.Password,
                    IsLocked = i.IsLocked
                }).FirstOrDefault();

            if (modelId == 0)
            {
                result = new Models.MstUser
                {
                    Id = 0,
                    Username = "NA",
                    FullName = "NA",
                    Password = "NA",
                    IsLocked = false
                };
            }

            return View(result);
        }

        [HttpPost]
        public ActionResult SaveUser(Models.MstUser model)
        {
            using (whris = new Data.whrisDataContext())
            {
                Data.MstUser user = new Data.MstUser();

                if (model.Id == 0)
                {
                    user.UserName = model.Username;
                    user.Password = model.Password;
                    user.FullName = model.FullName;
                    user.IsLocked = model.IsLocked;
                    user.EntryUserId = 1;
                    user.EntryDateTime = DateTime.Now;
                    user.UpdateUserId = 1;
                    user.UpdateDateTime = DateTime.Now;

                    whris.MstUsers.InsertOnSubmit(user);
                }
                else
                {
                    user = whris.MstUsers.SingleOrDefault(x => x.Id == model.Id);

                    if (user != null)
                    {
                        user.UserName = model.Username;
                        user.Password = model.Password;
                        user.FullName = model.FullName;
                        user.IsLocked = model.IsLocked;
                        user.EntryUserId = 1;
                        user.EntryDateTime = DateTime.Now;
                        user.UpdateUserId = 1;
                        user.UpdateDateTime = DateTime.Now;
                    }
                }

                whris.SubmitChanges();
            };

            return PartialView("MstUserDetail");
        }

        public JsonResult DeleteUser(int modelId)
        {
            bool isDeleted = false;

            using (whris = new Data.whrisDataContext())
            {
                if (modelId != 0)
                {
                    var user = whris.MstUsers.SingleOrDefault(x => x.Id == modelId);

                    if (user != null) whris.MstUsers.DeleteOnSubmit(user);

                    whris.SubmitChanges();

                    isDeleted = true;
                }
            }

            return Json(isDeleted, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Child
        [HttpPost]
        public ActionResult Read(int take, int skip, IEnumerable<Sort> sort, Filter filter, int modelFilterId)
        {
            //int userFilterId = int.Parse(Request.QueryString["userId"]);

            using (whris = new Data.whrisDataContext())
            {
                var result = whris
                    .MstUserForms
                    .Where(w => w.UserId == modelFilterId)
                    .OrderBy(x => x.FormId)
                    .Select(y => new Models.MstUserLine()
                    {
                        Id = y.Id,
                        UserId = y.UserId,
                        FormId = y.FormId,
                        CanAdd = y.CanAdd,
                        CanEdit = y.CanEdit,
                        CanDelete = y.CanDelete,
                        CanLock = y.CanLock,
                        CanUnlock = y.CanUnlock,
                        CanPreview = y.CanPreview,
                        CanPrint = y.CanPrint,
                        CanView = y.CanView
                    })
                    .ToDataSourceResult(take, skip, sort, filter);

                return Json(result);
            }
        }
        [HttpPost]
        public ActionResult Create(IEnumerable<Models.MstUserLine> model)
        {
            var result = new List<Data.MstUserForm>();

            using (var whris = new Data.whrisDataContext())
            {
                foreach (var line in model)
                {
                    var userLine = new Data.MstUserForm()
                    {
                        UserId = line.UserId,
                        FormId = line.FormId,
                        CanAdd = line.CanAdd,
                        CanEdit = line.CanEdit,
                        CanDelete = line.CanDelete,
                        CanLock = line.CanLock,
                        CanUnlock = line.CanUnlock,
                        CanPreview = line.CanPreview,
                        CanPrint = line.CanPrint,
                        CanView = line.CanView
                    };

                    result.Add(userLine);

                    whris.MstUserForms.InsertAllOnSubmit(result);
                }

                whris.SubmitChanges();
            }

            return Json(result.Select(y => new Models.MstUserLine()
            {
                UserId = y.UserId,
                FormId = y.FormId,
                CanAdd = y.CanAdd,
                CanEdit = y.CanEdit,
                CanDelete = y.CanDelete,
                CanLock = y.CanLock,
                CanUnlock = y.CanUnlock,
                CanPreview = y.CanPreview,
                CanPrint = y.CanPrint,
                CanView = y.CanView
            })
            .ToList());
        }
        [HttpPost]
        public ActionResult Update(IEnumerable<Models.MstUserLine> model)
        {
            using (var whris = new Data.whrisDataContext())
            {
                foreach (var line in model)
                {
                    var userLine = whris.MstUserForms.FirstOrDefault(x => x.Id == line.Id);

                    if (userLine != null)
                    {
                        userLine.UserId = line.UserId;
                        userLine.FormId = line.FormId;
                        userLine.CanAdd = line.CanAdd;
                        userLine.CanEdit = line.CanEdit;
                        userLine.CanDelete = line.CanDelete;
                        userLine.CanLock = line.CanLock;
                        userLine.CanUnlock = line.CanUnlock;
                        userLine.CanPreview = line.CanPreview;
                        userLine.CanPrint = line.CanPrint;
                        userLine.CanView = line.CanView;
                    }
                }

                whris.SubmitChanges();
            }

            return Json(null);
        }
        [HttpPost]
        public ActionResult Destroy(IEnumerable<Models.MstUserLine> model)
        {
            using (whris = new Data.whrisDataContext())
            {
                var userLine = new List<Data.MstUserForm>();

                foreach (var line in model)
                {
                    var uLine = whris.MstUserForms.FirstOrDefault(x => x.UserId == line.Id);

                    userLine.Add(uLine);
                }

                whris.MstUserForms.DeleteAllOnSubmit(userLine);

                whris.SubmitChanges();
            }

            return null;
        }
        #endregion

        #region ComboBox
        public JsonResult GetCmbForms()
        {
            var cmbForms = new List<Models.ComboBox.MstUser.CmbForms>();
            using (whris = new Data.whrisDataContext())
            {
                cmbForms = whris.SysForms.Select(x => new Models.ComboBox.MstUser.CmbForms
                {
                    Id = x.Id,
                    FormName = x.Remarks

                }).ToList();
            }

            return Json(cmbForms, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}