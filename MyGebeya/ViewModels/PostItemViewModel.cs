using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyGebeya.Models;

namespace MyGebeya.ViewModels
{
    public class PostItemViewModel:BaseViewModel
    {
        public Items_for_sale Item { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}