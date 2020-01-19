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
    public class ItemForRentController : Controller
    {
        MyGebeyaEntities Db = new MyGebeyaEntities();
        // GET: ItemForRent
        public ActionResult RentDashboard()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    User user = (User)Session["user"];
                    var rents = new List<Rent>();

                    var itemsForRent = Db.Items_for_rent.ToList();
                    var carsForRent = Db.Car_for_rent.ToList();
                    var houseForRent = Db.House_for_rent.ToList();

                    foreach (var item in itemsForRent)
                    {
                        rents.Add(item);
                    }
                    foreach (var car in carsForRent)
                    {
                        rents.Add(car);
                    }
                    foreach (var house in houseForRent)
                    {
                        rents.Add(house);
                    }

                    List<Rent> sortedSales = rents.OrderBy(o => o.rent_id).ToList();

                   

                    

                    var rentViewModel = new RentViewModel()
                    {
                        User = user,
                        ItemsForRents = sortedSales

                    };

                    return View(rentViewModel);
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

        // GET: ItemForRent/Details/5
        public ActionResult RentItems()
        {
            //            try
            //            {
            if (Session["UserId"] != null)
            {
                User user = (User)Session["user"];
                var rents = new List<Rent>();

                var itemsForRent = Db.Items_for_rent.ToList();
                var carsForRent = Db.Car_for_rent.ToList();
                var housesForRent = Db.House_for_rent.ToList();

                foreach (var item in itemsForRent)
                {
                    rents.Add(item);
                }
                foreach (var car in carsForRent)
                {
                    rents.Add(car);
                }
                foreach (var house in housesForRent)
                {
                    rents.Add(house);
                }

                List<Rent> sortedRents = rents.OrderBy(o => o.rent_id).ToList();



                var rentViewModel = new RentViewModel()
                {
                    User = user,
                    ItemsForRents = sortedRents

                };
                ViewBag.ItemTypeId = new SelectList(Db.Item_type, "item_type_id", "item_type_name");
                return View(rentViewModel);
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


        [HttpPost]
        public ActionResult RentItems(string search, int ItemTypeId)
        {
            if (Session["UserId"] != null)
            {
                User user = (User)Session["user"];
                var rents = new List<Rent>();

                var itemsForRent = Db.Items_for_rent.Where(i =>(i.item_name.Contains(search) || search == null) && i.item_type_id == ItemTypeId).ToList();
                var carsForRent = Db.Car_for_rent.Where(c =>(c.model.Contains(search) || search == null) && c.car_type_id == ItemTypeId).ToList();
                var houseForRent = Db.House_for_rent.Where(h => (h.house_name.Contains(search) || search == null) && h.house_type_id == ItemTypeId).ToList();

                foreach (var item in itemsForRent)
                {
                    rents.Add(item);
                }
                foreach (var car in carsForRent)
                {
                    rents.Add(car);
                }
                foreach (var house in houseForRent)
                {
                    rents.Add(house);
                }

                List<Rent> sortedRents = rents.OrderBy(o => o.rent_id).ToList();

                var rentViewModel = new RentViewModel()
                {
                    User = user,
                    ItemsForRents = sortedRents

                };
                ViewBag.ItemTypeId = new SelectList(Db.Item_type, "item_type_id", "item_type_name");
                return View(rentViewModel);
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

        // GET: ItemForRent/Create
            public ActionResult PostItemForRent()
        {
            if (Session["UserId"] != null)
            {
                var types = Db.Item_type.ToList();
                var paymentTypes = Db.payment_type.ToList();
                Items_for_rent item = new Items_for_rent();
                item.item_Types = types;
                item.payment_Types = paymentTypes;
                PostItemForRentViewModel model = new PostItemForRentViewModel
                {

                    User = (User)Session["user"],
                    Item = item
                };
                return View(model);
                ;
            }

            return RedirectToAction("Login", "User");

        }

        // POST: ItemForRent/Create
        [HttpPost]
        public ActionResult PostItemForRent(Items_for_rent item)
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
                    var paymentTypes = Db.payment_type.ToList();
                    Items_for_rent mItem = new Items_for_rent();
                    mItem.item_Types = types;
                    mItem.payment_Types = paymentTypes;
                    PostItemForRentViewModel model = new PostItemForRentViewModel
                    {

                        User = (User)Session["user"],
                        Item = mItem
                    };
                    return View(model);
                }

                Rent newRent = new Rent();
                Rent added = Db.Rents.Add(newRent);
                User user = (User)Session["user"];
                item.user_id = user.user_id;
                item.availablity = true;
                item.rent_id= added.rent_id;
                item.date_added = DateTime.UtcNow.Date;
                Db.Items_for_rent.Add(item);
                Db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("RentDashboard", "ItemForRent");
            }
            catch (Exception e)
            {
                var types = Db.Item_type.ToList();
                Items_for_rent mItem = new Items_for_rent();
                mItem.item_Types = types;
                var paymentTypes = Db.payment_type.ToList();
                mItem.payment_Types = paymentTypes;
                PostItemForRentViewModel model = new PostItemForRentViewModel
                {

                    User = (User)Session["user"],
                    Item = mItem
                };
                return View(model);
            }




        }

        // GET: ItemForRent/Edit/5
        public ActionResult Edit(int id)
        {
            
            var types = Db.Item_type.ToList();
            Items_for_rent mItem = Db.Items_for_rent.Single(i => i.item_id == id);
            mItem.item_Types = types;
            var paymentTypes = Db.payment_type.ToList();
            mItem.payment_Types = paymentTypes;
           PostItemForRentViewModel model = new PostItemForRentViewModel
            {

                User = (User)Session["user"],
                Item = mItem
            };
            return View(model);
        }

        // POST: ItemForRent/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Items_for_rent item)
        {
            Items_for_rent myItem = Db.Items_for_rent.Single(i => i.item_id == id);
            try
            {
                string extension = Path.GetExtension(item.ImageFile.FileName);
                if (item.ImageFile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(item.ImageFile.FileName);

                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    myItem.item_image_path = "~/UploadedPictures/Items_Pictures/" + filename;
                    filename = Path.Combine(Server.MapPath("~/UploadedPictures/Items_Pictures/"), filename);
                    item.ImageFile.SaveAs(filename);
                }
                myItem.availablity = item.availablity;
                myItem.item_name = item.item_name;
                myItem.item_description = item.item_description;
                myItem.item_type_id = item.item_type_id;
                myItem.price = item.price;

                Db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("FindItemForRent", "ItemForRent");
            }
            catch (Exception e)
            {
               
                
                Items_for_rent mItem = new Items_for_rent();
                var types = Db.Item_type.ToList();
                mItem.item_Types = types;
                var paymentTypes = Db.payment_type.ToList();
                mItem.payment_Types = paymentTypes;

                PostItemForRentViewModel model = new PostItemForRentViewModel
                {

                    User = (User)Session["user"],
                    Item = mItem
                };
                return View(model);
            }
        }

        // GET: ItemForRent/Delete/5
        public ActionResult Delete(int id)
        {
            Items_for_rent item = null;
            Car_for_rent car = null;
            House_for_rent house = null;

            try { item = Db.Items_for_rent.Single(i => i.item_id == id); }
            catch (Exception e) { }

            try { car = Db.Car_for_rent.Single(i => i.car_id == id); }
            catch (Exception e) { }

            try { house = Db.House_for_rent.Single(i => i.house_id == id); }
            catch (Exception e) { }

            if (item != null)
            {
                Rent rent = Db.Rents.Single(s => s.rent_id == item.rent_id);
                List<Comment_for_rent> itemComments = Db.Comment_for_rent.Where(c => c.rent_id == item.rent_id).ToList();
                List<Likes_for_rent_item> likesComments = Db.Likes_for_rent_item.Where(c => c.rent_id == item.rent_id).ToList();
                List<Dislike_for_rent_item> dislikeComments = Db.Dislike_for_rent_item.Where(c => c.rent_id == item.rent_id).ToList();

                foreach (var comment in itemComments)
                {
                    Db.Comment_for_rent.Remove(comment);
                }
                foreach (var like in likesComments)
                {
                    Db.Likes_for_rent_item.Remove(like);
                }
                foreach (var dislike in dislikeComments)
                {
                    Db.Dislike_for_rent_item.Remove(dislike);
                }
                Db.Rents.Remove(rent);
                Db.Items_for_rent.Remove(item);
            }
            else if (house != null)
            {
                Rent rent = Db.Rents.Single(s => s.rent_id == house.rent_id);
                Db.Rents.Remove(rent);
                Db.House_for_rent.Remove(house);
            }
            else if (car != null)
            {
                Rent rent = Db.Rents.Single(s => s.rent_id == car.rent_id);
                Db.Rents.Remove(rent);
                Db.Car_for_rent.Remove(car);
            }

            Db.SaveChanges();
            return RedirectToAction("FindItemForRent","ItemForRent");
        }

        // POST: ItemForRent/Delete/5

        public ActionResult FindItemForRent()
        {
            //            try
            //            {
            if (Session["UserId"] != null)
            {
                User user = (User)Session["user"];
                var rents = new List<Rent>();

                var itemsForRent = Db.Items_for_rent.Where(i => i.user_id == user.user_id).ToList();
                var carsForRent = Db.Car_for_rent.Where(c => c.user_id == user.user_id).ToList();
                var housesForRent = Db.House_for_rent.Where(h => h.user_id == user.user_id).ToList();

                foreach (var item in itemsForRent)
                {
                    rents.Add(item);
                }
                foreach (var car in carsForRent)
                {
                    rents.Add(car);
                }
                foreach (var house in housesForRent)
                {
                    rents.Add(house);
                }

                List<Rent> sortedRents = rents.OrderBy(o => o.rent_id).ToList();

              

                var rentViewModel = new RentViewModel()
                {
                    User = user,
                    ItemsForRents = sortedRents

                };
                ViewBag.ItemTypeId = new SelectList(Db.Item_type, "item_type_id", "item_type_name");
                return View(rentViewModel);
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
        
        [HttpPost]
        public ActionResult FindItemForRent(string search, int ItemTypeId)
        {
            if (Session["UserId"] != null)
            {
                User user = (User)Session["user"];
                var rents = new List<Rent>();

                var itemsForRent = Db.Items_for_rent.Where(i => i.user_id == user.user_id && (i.item_name.Contains(search) || search == null) && i.item_type_id == ItemTypeId).ToList();
                var carsForRent = Db.Car_for_rent.Where(c => c.user_id == user.user_id && (c.model.Contains(search) || search == null) && c.car_type_id == ItemTypeId).ToList();
                var houseForRent = Db.House_for_rent.Where(h => h.user_id == user.user_id && (h.house_name.Contains(search) || search == null) && h.house_type_id == ItemTypeId).ToList();

                foreach (var item in itemsForRent)
                {
                    rents.Add(item);
                }
                foreach (var car in carsForRent)
                {
                    rents.Add(car);
                }
                foreach (var house in houseForRent)
                {
                    rents.Add(house);
                }

                List<Rent> sortedRents = rents.OrderBy(o => o.rent_id).ToList();

                var rentViewModel = new RentViewModel()
                {
                    User = user,
                    ItemsForRents = sortedRents

                };
                ViewBag.ItemTypeId = new SelectList(Db.Item_type, "item_type_id", "item_type_name");
                return View(rentViewModel);
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
    }
}
