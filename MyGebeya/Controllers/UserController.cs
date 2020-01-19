using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using MyGebeya.Models;
using MyGebeya.ViewModels;


namespace MyGebeya.Controllers
{
    public class UserController : Controller
    {
        MyGebeyaEntities Db = new MyGebeyaEntities();
       
        [HttpGet]
        public ActionResult Register()
        {
            Models.User usr = new User();
            return View(usr);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string filename = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
                    string extension = Path.GetExtension(user.ImageFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    user.profile_picture_path = "~/Profile_Pictures/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Profile_Pictures/"), filename);
                    user.ImageFile.SaveAs(filename);
                    Db.Users.Add(user);
                    Db.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("Login","User");
                }
                catch (Exception e)
                {
                    return View();
                }
                   

            }
            return View();

        }

        // GET: User/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                if (!user.email.Equals(null) && !user.password.Equals(null))
                {
                    User current_user = Db.Users.Single(u => u.email.Equals(user.email) && u.password.Equals(user.password));

                    if (current_user != null)
                    {
                        Session["UserId"] = current_user.user_id.ToString();
                        Session["UserName"] = current_user.user_name;
                        Session["user"] = current_user;
                        return RedirectToAction("UserDashboard");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "User");
            }
       
          
            
        }
        public ActionResult UserDashboard()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    User user = (User)Session["user"];
                    var sales = new List<sale>();

                    var itemsForSale = Db.Items_for_sale.ToList();
                    var carsForSale = Db.Cars.ToList();
                    var houseForSale = Db.Houses.ToList();

                    foreach (var item in itemsForSale)
                    {
                        sales.Add(item);
                    }
                    foreach (var car in carsForSale)
                    {
                        sales.Add(car);
                    }
                    foreach (var house in houseForSale)
                    {
                        sales.Add(house);
                    }

                    List<sale> sortedSales = sales.OrderBy(o => o.sales_id).ToList();

                    List<CommentViewModel> cm = new List<CommentViewModel>();

                    foreach (var mitem in sortedSales)
                    {
                        List<Comment> comments = Db.Comments.Where(c => c.sales_id == mitem.sales_id).ToList();

                        cm.Add(new CommentViewModel()
                        {
                            item = mitem,
                           
                        });
                    }

                    var saleViewModel = new SalesViewModel
                    {
                        User = user,
                        ItemsForSale = sortedSales
                        
                    };

                    return View(saleViewModel);
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Login");
            }
           
           
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login");
        }




//        // GET: User/Details/5
//        public ActionResult Details(int id)
//        {
//            return View();
//        }
//
//       
//
//       
        // GET: User/Edit/5
        public ActionResult Edit()
        {
            try
            {
                User usr = (User)Session["user"];
                User currentUser = Db.Users.Single(u => u.user_id == usr.user_id);

                BaseViewModel baseViewModel = new BaseViewModel();
                baseViewModel.User = currentUser;
                return View(baseViewModel);
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "User");
            }
          
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                User usr = (User)Session["user"];
                User currentUser = Db.Users.Single(u => u.user_id == usr.user_id);

                
                    string filename = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
                    string extension = Path.GetExtension(user.ImageFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    currentUser.profile_picture_path = "~/Profile_Pictures/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Profile_Pictures/"), filename);
                    user.ImageFile.SaveAs(filename);

                

                currentUser.first_name = user.first_name;
                currentUser.last_name = user.last_name;
                currentUser.user_name = user.user_name;
                currentUser.email = user.email;
                currentUser.password = user.password;
                currentUser.phone_no = user.phone_no;
                Db.SaveChanges();
                Session["user"] = currentUser;

                return RedirectToAction("UserDashboard");
            }
            catch
            {
                return View();
            }
        }
//
//        // GET: User/Delete/5
//        public ActionResult Delete(int id)
//        {
//            return View();
//        }
//
//        // POST: User/Delete/5
//        [HttpPost]
//        public ActionResult Delete(int id, FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add delete logic here
//
//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

      
    }
}
