using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuminousMpartnerIB.EF;
using System.Data;
using System.IO;
namespace LuminousMpartnerIB.Controllers
{
    public class ConnectAssistController : Controller
    {
        //
        // GET: /ConnectAssist/

        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();

        private string PageUrl = "/ConnectAssist/Index";

        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {

                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {

                    return View();

                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }

            }
        }

        public JsonResult getTicketList(string status)
        {
           
                var getticket = db.ConnectAssists.Where(c=>c.Flag==status).Select(c => new { c.Id, c.Srno, c.Flag, c.CreatedOn, c.CreatedBy }).ToList();


                return Json(getticket, JsonRequestBehavior.AllowGet);
            
          


        }

        public ActionResult Edit(int id)
        {

            if (id!=0)
            {
                Session["AssistID"] = id.ToString();
            }

            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/ConnectAssist/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    try
                    {

                        var connectassist = db.ExecuteStoreQuery<ConAssist>("select conassist.Srno as Serialno,mc.CreatedOn as Createdon,ul.Dis_Name as Username,mc.CreatedBy,mc.Description as Text,mc.Attachment as Attachment,conassist.Flag   from ConnectAssist conassist join MapConnectAssist_Comments mc on conassist.Id=mc.ConnectAssistId join UsersList ul on conassist.Userid=ul.UserId where conassist.Id='" + id + "' order by conassist.CreatedOn").ToList();


                        //var connectassist =(from conassist in db.ConnectAssists
                        //                     join map_comment in db.MapConnectAssist_Comments on conassist.Id equals map_comment.ConnectAssistId join ul in db.UsersLists
                        //                     on conassist.Userid equals ul.UserId where conassist.Id==id
                        //         orderby conassist.CreatedOn
                        //         select new
                        //         {
                        //           Serialno = conassist.Srno,
                        //            Createdon= conassist.CreatedOn,
                        //            CreatedBy= ul.Dis_Name,
                        //            Text= map_comment.Description,
                        //            Attachment=conassist.Attachment
                                     
                        //         }).ToList();

                        //List<ConAssist> con = new List<ConAssist>();

                        //foreach(var data in connectassist)
                        //{
                        //    ConAssist cAssist = new ConAssist();
                        //    cAssist.Serialno = data.Serialno;
                        //    cAssist.Createdon = Convert.ToDateTime(data.Createdon);
                        //    cAssist.Text = data.Text;
                        //    cAssist.Attachment = data.Attachment;
                        //    con.Add(cAssist);
                        //}

                        return View(connectassist.ToList());
                    }
                    catch
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

          [HttpPost]
        public ActionResult Update(string comments, string ticketstatus, HttpPostedFileBase postedFile)
        {
            string filename = "";
            string Imagename = "";
              if(postedFile!=null)
              {
                   filename = Path.GetFileNameWithoutExtension(postedFile.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(postedFile.FileName);
                  Imagename = filename.Replace(" ", string.Empty);
                  string str = Path.Combine(Server.MapPath("~/ConnectAssist/"), Imagename);
                  postedFile.SaveAs(str);
              }
            MapConnectAssist_Comments mpcomments = new MapConnectAssist_Comments();

            mpcomments.Description = comments;
            mpcomments.ConnectAssistId = Convert.ToInt32(Session["AssistID"]);
            mpcomments.CreatedOn = DateTime.Now;
            mpcomments.CreatedBy = Session["Id"].ToString();
            mpcomments.Attachment = Imagename;
            mpcomments.filename = filename;
            db.MapConnectAssist_Comments.AddObject(mpcomments);
            db.SaveChanges();
            db.MapConnectAssist_Comments.Detach(mpcomments);
            //db.AddToMapConnectAssist_Comments(new MapConnectAssist_Comments
            //{

            //   ConnectAssistId=Convert.ToInt32(Session["AssistID"]),
            //   Description=comments,
            //   CreatedBy=Session["Id"].ToString(),
            //   CreatedOn=DateTime.no

            //});
              int assist_id=Convert.ToInt32(Session["AssistID"]);
              var updateConnectAssist = db.ConnectAssists.Where(c => c.Id == assist_id).SingleOrDefault();

              updateConnectAssist.Flag = ticketstatus;

              if(ticketstatus=="Resolved")
              {
                  updateConnectAssist.ResolvedDate = DateTime.Now;
              }
              if (ticketstatus == "InProcess")
              {
                  var ticketInprocess = db.ConnectAssists.Where(c => c.Id == assist_id && c.InProcessDate == null).ToList();
                  if (ticketInprocess.Count() != 0)
                  {
                      updateConnectAssist.InProcessDate = DateTime.Now;
                  }
              }
              db.ObjectStateManager.ChangeObjectState(updateConnectAssist, System.Data.EntityState.Modified);
             // db.ConnectAssists.Attach(updateConnectAssist);

             // entity.User.Attach(model);
            
             // entity.SaveChanges();


             // db.ConnectAssists.AddObject(updateConnectAssist);
              db.SaveChanges();

            
            //db.ExecuteStoreCommand("Update ConnectAssist set Flag='" + ticketstatus + "' where id='" + Session["AssistID"] + "'");
            //if (ticketstatus=="Resolved")
            //{
            //    db.ExecuteStoreCommand("Update ConnectAssist set ResolvedDate='" + DateTime.Now + "' where id='" + Session["AssistID"] + "'");
            //}
              //CAST('03/28/2011 18:03:40' AS DATETIME)
            return RedirectToAction("Index");
        }


    }
}
