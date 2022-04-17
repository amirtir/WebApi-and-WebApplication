using MyWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyWebMain.Models
{
    public class Customer
    {

        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}

