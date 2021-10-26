using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPL.WEB.Models
{
    public class LogInMo
    {
        //public LogInMo()
        //{
        //    //ListOrderMaster = new List<Mdl_SalesOrder>();
        //    ListuserLevel = new List<LoginUserLevel>();

        //}
        public string password1 { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Required(ErrorMessage = "Enter User name")]
        [Display(Name = "User Name")]
        public string username { get; set; }

        [Required(ErrorMessage = "Enter User Level")]
        [Display(Name = "User Level")]
        public int userLevel { get; set; }
        
    
  

    }
}