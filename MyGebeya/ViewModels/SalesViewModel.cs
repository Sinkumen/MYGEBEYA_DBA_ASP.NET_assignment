using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyGebeya.Models;

namespace MyGebeya.ViewModels
{
    public class SalesViewModel:BaseViewModel
    {
        public Comment comment { get; set; }
        public List<sale> ItemsForSale { get; set; }

    }
}