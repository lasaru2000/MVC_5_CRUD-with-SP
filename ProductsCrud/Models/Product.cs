using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductsCrud.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName{ get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Qty{ get; set; }
        [Required]
        public string Remarks { get; set; }

    }
}