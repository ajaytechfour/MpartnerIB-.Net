using LuminousMpartnerIB.EF;
using LuminousMpartnerIB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TVS;

namespace LuminousMpartnerIB.Controllers
{
    public class HomeController : Controller
    {
        datautility dut = new datautility();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        LuminousMpartnerIBEntities db;
        List<AllPage> sideBarMenuLst = new List<AllPage>();

        public HomeController()
        {
            db = new LuminousMpartnerIBEntities();
        }

        public ActionResult LuminousMpartnerIB()
        {
            sideBarMenuLst = new List<AllPage>();
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                SqlParameter[] pram ={
                    new SqlParameter("@userid",Session["ctype"]),
                      new SqlParameter("@requestType","WEB"),
                     };

                ds = dut.ExecuteDSProcedure("SP_GetSidebarMenu_Permission", pram);

                List<AllPage> objData = SqlHelper.ConvertDataTable<AllPage>(ds.Tables[0]);

                //var ss = from al in objData
                //         select new SideBarMenuModel
                //         {
                //             Id = al.Id,
                //             PageName = al.PageName,
                //             ModuleName = al.ModuleName,
                //             RequestType = al.RequestType,
                //             RootNode = al.RootNode,
                //             IsTopMenu = al.IsTopMenu,
                //             Status = al.Status,
                //             DisplayOrder = al.DisplayOrder,
                //             Image = "/images/" + al.Image
                //         };

                Session["sidebarmenu"] = objData;
                return View();
            }
        }

    }
}
