using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DarulAman.Models;

namespace DarulAman.Utills
{
    public class BaseHelper
    {
        public static tbl_Admin Currentdonation { get; set; }
        public static tbl_Event Currentevent { get; set; }
       
        public static tbl_Team Currentteam { get; set; }
         
         

   }
}