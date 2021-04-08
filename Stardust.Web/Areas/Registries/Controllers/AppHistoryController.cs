﻿using System;
using System.Collections.Generic;
using System.Reflection;
using NewLife.Cube;
using NewLife.Web;
using Stardust.Data;
using XCode;
using XCode.Membership;

namespace Stardust.Web.Areas.Registries.Controllers
{
    [RegistryArea]
    public class AppHistoryController : EntityController<AppHistory>
    {
        static AppHistoryController() => MenuOrder = 93;

        protected override IEnumerable<AppHistory> Search(Pager p)
        {
            PageSetting.EnableAdd = false;
            PageSetting.EnableNavbar = false;

            var appId = p["appId"].ToInt(-1);

            var start = p["dtStart"].ToDateTime();
            var end = p["dtEnd"].ToDateTime();

            if (start.Year < 2000) start = DateTime.Today;

            return AppHistory.Search(appId, start, end, p["Q"], p);
        }

        /// <summary>菜单不可见</summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        protected override IDictionary<MethodInfo, Int32> ScanActionMenu(IMenu menu)
        {
            if (menu.Visible)
            {
                menu.Visible = false;
                (menu as IEntity).Update();
            }

            return base.ScanActionMenu(menu);
        }
    }
}