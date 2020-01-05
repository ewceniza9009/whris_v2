using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace whris_v2.Services
{
    public class Security
    {
        public Security(string userForm)
        {
            _UserForm = userForm;
        }

        string _UserForm;

        Data.whrisDataContext whris;
        public int CurrentUserId 
        {
            get 
            {
                whris = new Data.whrisDataContext();

                var userIdentityName = HttpContext.Current.User.Identity.Name;
                var userIdentityId = whris.AspNetUsers.FirstOrDefault(x => x.Email == userIdentityName).Id;
                var userId = whris.MstUsers.FirstOrDefault(x => x.AspNetUserId == userIdentityId).Id;

                return userId;
            }
        }
        public Models.MstUserLine CurrentUserForms
        {
            get 
            {
                whris = new Data.whrisDataContext();

                var userIdentityName = HttpContext.Current.User.Identity.Name;
                var userIdentityId = whris.AspNetUsers.FirstOrDefault(x => x.Email == userIdentityName).Id;
                var userId = whris.MstUsers.FirstOrDefault(x => x.AspNetUserId == userIdentityId).Id;

                var result = whris.MstUserForms
                    .Where(x => x.UserId == userId && x.SysForm.FormName == _UserForm)
                    .Select(x => new Models.MstUserLine 
                    {
                        FormId = x.FormId,
                        CanAdd = x.CanAdd,
                        CanEdit = x.CanEdit,
                        CanDelete = x.CanDelete,
                        CanLock = x.CanUnlock,
                        CanUnlock = x.CanUnlock,
                        CanPreview = x.CanPreview,
                        CanView = x.CanView,
                        CanPrint = x.CanPrint
                    })
                    .FirstOrDefault();

                return result;
            }
        }
    }
}