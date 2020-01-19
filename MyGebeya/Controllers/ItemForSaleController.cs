using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyGebeya.Models;
using MyGebeya.ViewModels;

namespace MyGebeya.Controllers
{
    public class ItemForSaleController : Controller
    {
        MyGebeyaEntities Db = new MyGebeyaEntities();

        public ActionResult ShopItem()
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
                    ViewBag.ItemTypeId = new SelectList(Db.Item_type, "item_type_id", "item_type_name");
                    return View(saleViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "User");
            }
            
        }

        [HttpPost]
        public ActionResult ShopItem(string search, int ItemTypeId)
        {
            try
            {
            if (Session["UserId"] != null)
            {
                User user = (User)Session["user"];
                var sales = new List<sale>();

                var itemsForSale = Db.Items_for_sale.Where(i =>  (i.item_name.Contains(search) || search == null) && i.item_type_id == ItemTypeId).ToList();
                var carsForSale = Db.Cars.Where(c =>  (c.model.Contains(search) || search == null) && c.car_type_id == ItemTypeId).ToList();
                var houseForSale = Db.Houses.Where(h => (h.house_name.Contains(search) || search == null) && h.house_type_id == ItemTypeId).ToList();

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
                ViewBag.ItemTypeId = new SelectList(Db.Item_type, "item_type_id", "item_type_name");
                return View(saleViewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
            }
            catch (Exception e)
            {
                return RedirectToAction("Login","User");
            }
            
        }

        // GET: ItemForSale
        public ActionResult FindItemForSale()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    User user = (User)Session["user"];
                    var sales = new List<sale>();

                    var itemsForSale = Db.Items_for_sale.Where(i=>i.user_id == user.user_id).ToList();
                    var carsForSale = Db.Cars.Where(c => c.user_id == user.user_id ).ToList();
                    var houseForSale = Db.Houses.Where(h => h.user_id == user.user_id).ToList();

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
                   ViewBag.ItemTypeId = new SelectList(Db.Item_type, "item_type_id", "item_type_name");
                    return View(saleViewModel);
                }
                else
                {
                    return RedirectToAction("Login","User");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Login","User");
            }
           
        }

        [HttpPost]
        public ActionResult FindItemForSale(string search,int ItemTypeId)
        {
            //            try
            //            {
            if (Session["UserId"] != null)
            {
                User user = (User)Session["user"];
                var sales = new List<sale>();

                var itemsForSale = Db.Items_for_sale.Where(i => i.user_id == user.user_id && (i.item_name.Contains(search) || search == null) && i.item_type_id == ItemTypeId).ToList();
                var carsForSale = Db.Cars.Where(c => c.user_id == user.user_id && (c.model.Contains(search) || search == null) && c.car_type_id == ItemTypeId).ToList();
                var houseForSale = Db.Houses.Where(h => h.user_id == user.user_id && (h.house_name.Contains(search) || search == null)&& h.house_type_id == ItemTypeId).ToList();

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
                ViewBag.ItemTypeId = new SelectList(Db.Item_type, "item_type_id", "item_type_name");
                return View(saleViewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
            //            }
            //            catch (Exception e)
            //            {
            //                return RedirectToAction("Login","User");
            //            }
            return View();
        }

        // GET: ItemForSale/Create
            public ActionResult PostItemForSale()
        {
            if (Session["UserId"] != null)
            {
                var types = Db.Item_type.ToList();
                Items_for_sale item = new Items_for_sale();
                item.item_Types = types;
                PostItemViewModel model = new PostItemViewModel
                {

                    User = (User)Session["user"],
                    Item = item
                };
                return View(model);
                ;
            }

            return RedirectToAction("Login", "User");

        }

        // POST: ItemForSale/Create
        [HttpPost]
        public ActionResult PostItemForSale(Items_for_sale item)
        {

            try
            {
                string extension = Path.GetExtension(item.ImageFile.FileName);
                if (item.ImageFile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(item.ImageFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    item.item_image_path = "~/UploadedPictures/Items_Pictures/" + filename;
                    filename = Path.Combine(Server.MapPath("~/UploadedPictures/Items_Pictures/"), filename);
                    item.ImageFile.SaveAs(filename);
                }
                else
                {
                    var types = Db.Item_type.ToList();
                    Items_for_sale mItem = new Items_for_sale();
                    mItem.item_Types = types;
                    PostItemViewModel model = new PostItemViewModel
                    {

                        User = (User)Session["user"],
                        Item = mItem
                    };
                    return View(model);
                }

                sale newSale = new sale();
                sale added = Db.sales.Add(newSale);
                User user = (User)Session["user"];
                item.user_id = user.user_id;
                item.availability = true;
                item.sales_id = added.sales_id;
                item.date_added = DateTime.UtcNow.Date;
                Db.Items_for_sale.Add(item);
                Db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("UserDashboard","User");
            }
            catch (Exception e)
            {
                var types = Db.Item_type.ToList();
                Items_for_sale mItem = new Items_for_sale();
                mItem.item_Types = types;
                PostItemViewModel model = new PostItemViewModel
                {

                    User = (User)Session["user"],
                    Item = mItem
                };
                return View(model);
            }
                   
               
                
            
        }

       
        // GET: ItemForSale/Edit/5
        public ActionResult Edit(int id)
        {
            var types = Db.Item_type.ToList();
            Items_for_sale mItem = Db.Items_for_sale.Single(i=>i.item_id==id);
            mItem.item_Types = types;
            PostItemViewModel model = new PostItemViewModel
            {

                User = (User)Session["user"],
                Item = mItem
            };
            return View(model);
        }

       

        // POST: ItemForSale/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Items_for_sale item)
        {
            Items_for_sale myItem = Db.Items_for_sale.Single(i => i.item_id == id);
            try
            {
                

                if (item.ImageFile != null) {
                string filename = Path.GetFileNameWithoutExtension(item.ImageFile.FileName);
                string extension = Path.GetExtension(item.ImageFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                myItem.item_image_path = "~/UploadedPictures/Items_Pictures/" + filename;
                filename = Path.Combine(Server.MapPath("~/UploadedPictures/Items_Pictures/"), filename);
                item.ImageFile.SaveAs(filename);
                }
                myItem.availability = item.availability;
                myItem.item_name = item.item_name;
                myItem.item_description = item.item_description;
                myItem.item_type_id = item.item_type_id;
                myItem.price_per_item = item.price_per_item;
                
                Db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("FindItemForSale", "ItemForSale");
            }
            catch (Exception e)
            {
                var types = Db.Item_type.ToList();
                Items_for_sale mItem = new Items_for_sale();
                mItem.item_Types = types;
                PostItemViewModel model = new PostItemViewModel
                {

                    User = (User) Session["user"],
                    Item = mItem
                };
                return View(model);
            }
        }

      

        // GET: ItemForSale/Delete/5
        public ActionResult Delete(int id)
        {
            Items_for_sale item = null;
            Car car = null;
            House house = null;

            try { item = Db.Items_for_sale.Single(i => i.item_id== id); }
            catch (Exception e) { }

            try { car = Db.Cars.Single(i => i.car_id == id); }
            catch (Exception e) { }

            try { house = Db.Houses.Single(i => i.house_id == id); }
            catch (Exception e) { }

            if (item != null)
            {
                sale sale = Db.sales.Single(s => s.sales_id == item.sales_id);
                List<Comment> itemComments = Db.Comments.Where(c => c.sales_id == item.sales_id).ToList();
                List<Likes_for_sale_item> likesComments = Db.Likes_for_sale_item.Where(c => c.sales_id == item.sales_id).ToList();
                List<Dislikes_for_sale_item> dislikeComments = Db.Dislikes_for_sale_item.Where(c => c.sales_id == item.sales_id).ToList();

                foreach (var comment in itemComments)
                {
                    Db.Comments.Remove(comment);
                }
                foreach (var like in likesComments)
                {
                    Db.Likes_for_sale_item.Remove(like);
                }
                foreach (var dislike in dislikeComments)
                {
                    Db.Dislikes_for_sale_item.Remove(dislike);
                }
                Db.sales.Remove(sale);
                Db.Items_for_sale.Remove(item);
            }
            else if (house != null)
            {
                sale sale = Db.sales.Single(s => s.sales_id == house.sales_id);
                Db.sales.Remove(sale);
                Db.Houses.Remove(house);
            }
            else if(car!= null)
            {
                sale sale = Db.sales.Single(s => s.sales_id == car.sales_id);
                Db.sales.Remove(sale);
                Db.Cars.Remove(car);
            }

            Db.SaveChanges();
            return RedirectToAction("FindItemForSale");
        }

        
    }
}
