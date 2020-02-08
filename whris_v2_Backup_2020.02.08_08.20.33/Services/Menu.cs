using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace whris_v2.Services
{
    public class Menu
    {
        Data.whrisDataContext whris;
        string _search;
        public Menu(string search)
        {
            _search = search;
        }

        public List<MenuModel> AllActiveMenus 
        { get 
            {
                whris = new Data.whrisDataContext();

                var result = new List<MenuModel>();

                if (_search != null || _search != "")
                {
                    result = whris.WebMenus
                    .Where(m => m.Menu.Contains(_search))
                    .Select(x => new MenuModel
                    {
                        Category = x.Category,
                        Menu = x.Menu,
                        Action = x.Action,
                        Controller = x.Controller
                    }).ToList();
                }
                else 
                {
                    result = whris.WebMenus
                    .Select(x => new MenuModel
                    {
                        Category = x.Category,
                        Menu = x.Menu,
                        Action = x.Action,
                        Controller = x.Controller
                    }).ToList();
                }

                return result;
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