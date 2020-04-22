using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuminousMpartnerIB.EF;
using System.Web.Security;
using System.Threading;
using System.Data;

namespace TVS.Controllers
{
    public class pageaccessController : Controller
    {
        //
        // GET: /pageaccess/
        DataTable dt = new DataTable();
        LuminousMpartnerIBEntities db;
        public pageaccessController()
        {
            db = new LuminousMpartnerIBEntities();
        }


       public ActionResult pageaccess()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {

                dt = Session["permission"] as DataTable;

                string pageUrl2 = "/pageaccess/pageaccess";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result.Length > 0)
                {
                    string str = result[0]["createrole"].ToString() + "," + result[0]["editrole"].ToString() + "," + result[0]["deleterole"].ToString() + "," + result[0]["uview"].ToString();

                    if (str == "1,1,1,1")
                    {
                        Loadstate();
                        return View();
                       
                    }
                    else
                    {
                        return RedirectToAction("snotallowed", "snotallowed");   
                    }
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }


            }


        }

        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Showstate()
        {

            return Json(db.allpages.ToList(), JsonRequestBehavior.AllowGet);
        }




        public List<SelectListItem> Loadstate()
        {
            var myResults = (from data in db.allprofiles
                             select new
                             {
                                 Text = data.profilename,
                                 Value = data.id

                             }
          ).ToList();


            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (var dd in myResults)
            {
                li.Add(new SelectListItem { Text = dd.Text, Value = Convert.ToString(dd.Value) });

            }


            ViewData["country"] = li;

            return li;
        }



        public JsonResult savedata(int pofileid, string pageid, string createpage, string editpage, string deletepage, string userview)
        {

            db.Connection.Open();
            using (System.Data.Common.DbTransaction transaction = db.Connection.BeginTransaction())
            {
                try
                {


                    pageid = pageid.Replace("undefinednull,", "").Replace("undefined", "").TrimEnd(',');
                    createpage = createpage.Replace("undefinednull,", "").Replace("undefined", "").TrimEnd(',');
                    editpage = editpage.Replace("undefinednull,", "").Replace("undefined", "").TrimEnd(',');
                    deletepage = deletepage.Replace("undefinednull,", "").Replace("undefined", "").TrimEnd(',');
                    userview = userview.Replace("undefinednull,", "").Replace("undefined", "").TrimEnd(',');

                    int counter = 0;
                    string[] pagesid = pageid.Split(',');
                    string[] crep = createpage.Split(',');
                    string[] editp = editpage.Split(',');
                    string[] dep = deletepage.Split(',');
                    string[] uv = userview.Split(',');
                    foreach (string word in pagesid)
                    {
                        int pid = Convert.ToInt32(word);
                        int cid = Convert.ToInt32(crep[counter]);
                        int eid = Convert.ToInt32(editp[counter]);
                        int did = Convert.ToInt32(dep[counter]);
                        int uve = Convert.ToInt32(uv[counter]);

                        var query = db.pageallows.Count(em => em.pageid == pid && em.profileid == pofileid);
                        var total = Convert.ToInt32(query) + 1;
                        if (Convert.ToInt32(total) > 1)
                        {



                            pageallow Emp = db.pageallows.First(em => em.pageid == pid && em.profileid == pofileid);
                            Emp.pageid = pid;
                            Emp.createrole = cid;
                            Emp.editrole=eid;
                            Emp.deleterole=did;
                            Emp.uview = uve;
                            counter = counter + 1;
                         
                        }
                        else
                        {


                            db.AddTopageallows(new pageallow
                            {

                                profileid = pofileid,
                                pageid = pid,
                                createrole = cid,
                                editrole = eid,
                                deleterole = did,
                                uview = uve

                            });
                            counter = counter + 1;
                        }
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    //TVPTable Empbill1 = db.TVPTables.First(em => em.id == billid);
                    //Empbill.billstatus = "approved";

                    //db.SaveChanges();

                    return Json("Profile saved successfully", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    transaction.Dispose();
                    return Json("Profile not saved successfully ", JsonRequestBehavior.AllowGet);
                }
                finally
                {

                    db.Connection.Close();
                    db.Connection.Dispose();
                }
            }
        }



        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Showstatenew(int profileid)
        {

            var lines =
    from tl in db.allpages
    join j in db.pageallows.Where(em=>em.profileid==profileid) on tl.id equals j.pageid into tl_j
    

    from j in tl_j.DefaultIfEmpty()
    select new
    {

        id = tl.id,
        pagename = tl.pagename,
        profileid = j.profileid,
        createrole = j.createrole,
        editrole = j.editrole,
        deleterole = j.deleterole,
        userview=j.uview
    };






            return Json(lines, JsonRequestBehavior.AllowGet);
        }
    }
}
