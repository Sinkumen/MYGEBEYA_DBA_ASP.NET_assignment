using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyGebeya.Models;
using MyGebeya.ViewModels;

namespace MyGebeya.Controllers
{
    public class CommentForRentController : Controller
    {
        MyGebeyaEntities Db = new MyGebeyaEntities();
        // GET: Comment
        [HttpPost]
        public ActionResult AddComment(Comment_for_rent comment)
        {
            //            try
            //            {
            User usr = (User)Session["user"];
            comment.user_id = usr.user_id;
            comment.date_added = DateTime.UtcNow.Date;
            Rent item = null;
            Rent car = null;
            Rent hotel = null;

            try { item = Db.Items_for_rent.Single(i => i.rent_id == comment.rent_id); }
            catch (Exception e) { }

            try { car = Db.Car_for_rent.Single(i => i.rent_id == comment.rent_id); }
            catch (Exception e) { }

            try { hotel = Db.House_for_rent.Single(i => i.rent_id == comment.rent_id); }
            catch (Exception e) { }


            int itemType;
            if (item != null)
            {
                itemType = 1;
            }
            else if (hotel != null)
            {
                itemType = 2;
            }
            else
            {
                itemType = 3;
            }
            Db.Comment_for_rent.Add(comment);
            Db.SaveChanges();
            return RedirectToAction("ViewComment", "CommentForRent", new { id = comment.rent_id, type = itemType });
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
                CommentForRentViewModel cm = new CommentForRentViewModel();
                User user = (User)Session["user"];
                if (type == 1)
                {
                    Items_for_rent item = Db.Items_for_rent.Single(i => i.rent_id == id);
                    cm.item = item;
                    cm.User = user;
                    Comment_for_rent comment = new Comment_for_rent();
                    cm.comment = comment;

                }
                else if (type == 2)
                {
                    Car_for_rent item = Db.Car_for_rent.Single(i => i.rent_id == id);
                    cm.item = item;
                    cm.User = user;
                    Comment_for_rent comment = new Comment_for_rent();
                    cm.comment = comment;

                }
                else
                {
                    House_for_rent item = Db.House_for_rent.Single(i => i.rent_id == id);
                    cm.item = item;
                    cm.User = user;

                }
//
              return View(cm);

            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "User");
            }

        }
    }
}