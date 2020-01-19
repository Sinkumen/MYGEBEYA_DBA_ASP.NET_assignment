using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyGebeya.Models;

namespace MyGebeya.ViewModels
{
    public class CommentViewModel:BaseViewModel
    {
        public sale item { get; set; }
        public Comment comment { get; set; }

    }
}