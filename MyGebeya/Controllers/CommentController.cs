using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyGebeya.Models;
using MyGebeya.ViewModels;

namespace MyGebeya.Controllers
{
    public class CommentController : Controller
    {
        MyGebeyaEntities Db = new MyGebeyaEntities();
        // GET: Comment
        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
//            try
//            {
                User usr = (User)Session["user"];
                comment.user_id = usr.user_id;
                comment.date_added = DateTime.UtcNow.Date;
                sale item = null;
                sale car = null;
                sale hotel = null;
                
                try { item = Db.Items_for_sale.Single(i => i.sales_id == comment.sales_id); }
                catch (Exception e){}

                try { car = Db.Cars.Single(i => i.sales_id == comment.sales_id); }
                catch (Exception e) { }

                try { hotel = Db.Houses.Single(i => i.sales_id == comment.sales_id); }
                catch (Exception e) { }

           
                int itemType;
                if (item != null)
                {
                    itemType = 1;
                }
                else if(hotel!=null)
                {
                    itemType = 2;
                }
                else
                {
                    itemType = 3;
                }
            Db.Comments.Add(comment);
                Db.SaveChanges();
                return RedirectToAction("ViewComment", "Comment", new { id = comment.sales_id, type = itemType });
//            }
//            catch (Exception e)
//            {
//                return RedirectToAction("PostItemForSale", "ItemForSale");
//            }
           
           
        }

       
        public ActionResult ViewComment(int id, int type)
        {
            try
            {
                CommentViewModel cm = new CommentViewModel();
                User user = (User)Session["user"];
                if (type == 1)
                {
                    Items_for_sale item = Db.Items_for_sale.Single(i => i.sales_id == id);
                    cm.item = item;
                    cm.User = user;
                    Comment comment = new Comment();
                    cm.comment = comment;

                }
                else if(type == 2)
                {
                    Car item = Db.Cars.Single(i => i.sales_id == id);
                    cm.item = item;
                    cm.User = user;
                    Comment comment = new Comment();
                    cm.comment = comment;

                }
                else
                {
                    House item = Db.Houses.Single(i => i.sales_id == id);
                    cm.item = item;
                    cm.User = user;

                }

                return View(cm);

            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "User");
            }
           
        }
    }
}