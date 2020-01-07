using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace whris_v2.Services
{
    public class Menu
    {
        Data.whrisDataContext whris;

        public List<MenuModel> AllActiveMenus 
        { get 
            {
                whris = new Data.whrisDataContext();

                return whris.WebMenus.Select(x => new MenuModel
                {
                    Category = x.Category,
                    Menu = x.Menu,
                    Action = x.Action,
                    Controller = x.Controller
                }).ToList();
            } 
        }
    }

    public class MenuModel 
    {
        public string Category { get; set; }
        public string Menu { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }
}