using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyGebeya.Models;

namespace MyGebeya.ViewModels
{
    public class CommentForRentViewModel:BaseViewModel
    {
        public Rent item { get; set; }
        public Comment_for_rent comment { get; set; }
    }
}