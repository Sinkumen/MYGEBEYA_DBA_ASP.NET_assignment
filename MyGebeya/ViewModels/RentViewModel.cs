using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyGebeya.Models;

namespace MyGebeya.ViewModels
{
    public class RentViewModel:BaseViewModel
    {
        public Comment_for_rent comment { get; set; }
        public List<Rent> ItemsForRents { get; set; }
    }
}