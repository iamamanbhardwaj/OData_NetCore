using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIsWithOData.Models
{
    // Book Entity
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public virtual Address Address { get; set; }
        public virtual Press Press { get; set; }
    }

    // Press Entity
    public class Press
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Category Category { get; set; }
    }

    // Category Enum 
    public enum Category
    {
        Book,
        Magazine,
        EBook
    }

    // Address Complex Type
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}
