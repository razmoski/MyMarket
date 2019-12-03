using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SellMyStuff.Models;
using System.Data.Entity;


namespace SellMyStuff.Models
{
    public class NavbarItem
    {
        public int    Id { get; set; }
        public string nameOption { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public bool   havingImageClass { get; set; }
        public string cssClass { get; set; }
        public int    parentId { get; set; }
        public bool   isParent { get; set; }
    }

    public class Navbar
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<NavbarItem> NavbarTop()
        {
            var topNav = new List<NavbarItem>();

            topNav.Add(new NavbarItem() { Id = 0, action = "Index"  , nameOption = "Search by Category", controller = "ArticlesGroups", isParent = true, parentId = -1 });
            
            var parentgroups = db.ArticlesGroups.ToList();

            //foreach (var item in parentgroups.Where(item => item.ParentId == 0))
            //{
            //    topNav.Add(new NavbarItem()
            //    {
            //        Id = item.Id,
            //        action = "SubCategory",
            //        nameOption = item.Name,
            //        controller = "ArticlesGroups",
            //        isParent = false,
            //        parentId = 1
            //    });
            //}

            foreach (var item in parentgroups)
            {
                topNav.Add(new NavbarItem()
                {
                    Id = item.Id,
                    action = "SubCategory",
                    nameOption = item.Name,
                    controller = "ArticlesGroups",
                    isParent = (item.ParentId == 0),
                    parentId = (item.ParentId == 0 ? 0 : 1)
                });
            }

            // drop down Menu 
            //topNav.Add(new NavbarItem() { Id = 3, action = "Reports", nameOption = "Reports", controller = "ReportGen", isParent = true, parentId = -1 });
            //topNav.Add(new NavbarItem() { Id = 4, action = "SummaryReport", nameOption = "Overall Summary", controller = "ReportGen", isParent = false, parentId = 3 });

            // End drop down Menu
            //topNav.Add(new NavbarItem() { Id = 7, action = "Action", nameOption = "Other action", controller = "Home", isParent = false, parentId = -1 });
            return topNav;
        }
    }

}