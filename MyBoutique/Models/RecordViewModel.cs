using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBoutique.Models
{
    public class RecordViewModel
    {

        public string NameOfVendor { get; set; }
        [Display(Name = "Vendor Id")]
        public string Vendor_Id { get; set; }
        [Required]
        [Display(Name = "Article Number*")]

        public string Article_No { get; set; }
        [Required]
        [Display(Name = "Number Of Suits*")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Number of Suits should be in Digits")]
        public int No_Of_Suits { get; set; }
        [Required]
        [Display(Name = "Number of Colors*")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Number of Colors should be in Digits")]
        public int Colors { get; set; }

        [Required]
        [Display(Name = "Paid Amount*")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Paid Amount should be in Digits")]
        public int Paid_Amount { get; set; }


        [Required]
        [Display(Name = "Price for Each*")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Price should be in Digits")]
        public int Price { get; set; }
        [Display(Name = "Date Of Recieving")]
        [DataType(DataType.Date)]
        public DateTime Added_On { get; set; }
        [Display(Name = "Add Image")]
        public HttpPostedFileBase Image { get; set; }
        [Display(Name = "Article Image")]
        public string ImagePath { get; set; }
    }
}