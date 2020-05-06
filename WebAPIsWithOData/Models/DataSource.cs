using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIsWithOData.Models
{
    public static class DataSource
    {
        private static IList<Book> _books { get; set; }

        static DataSource()
        {
            if (_books == null)
            {
                _books = new List<Book>();

                SeedBooStoreData();
            }
        }

        /// <summary>
        /// seeding method
        /// </summary>
        public static void SeedBooStoreData()
        {
            // book #1
            Book book = new Book
            {
                Id = 1,
                ISBN = "978-0-321-87758-1",
                Title = "Essential C#5.0",
                Author = "Mark Michaelis",
                Price = 59.99m,
                Address = new Address { City = "Redmond", Street = "156TH AVE NE" },
                Press = new Press
                {
                    Id = 1,
                    Name = "Addison-Wesley",
                    Category = Category.Book,
                    Email = "test@test.com"
                }
            };
            _books.Add(book);

           foreach(var id in Enumerable.Range(2, 20))
            { 
                var pressNameExtension = id % 2 == 0 ? $@" - {id}" : "";
                // book #2
                book = new Book
                {
                    Id = id,
                    ISBN = $"063-6-920-02371-{id}",
                    Title = $"Enterprise Games -{id}",
                    Author = "Michael Hugos",
                    Price = 49.99m + id*2,
                    Address = new Address { City = "Bellevue", Street = "Main ST" },
                    Press = new Press
                    {
                        Id = 2 * id,
                        Name = $@"O'Reilly {pressNameExtension}" ,
                        Category = Category.EBook,
                        Email = $"test{id}@test.com"
                    }
                };
                _books.Add(book);

            }
        }

        /// <summary>
        /// Gets book list
        /// </summary>
        /// <returns></returns>
        public static IList<Book> GetBooks()
        {
            if (_books != null)
            {
                return _books;
            }
            else
            {
                _books = new List<Book>();
                return _books;
            }
        }
    }
}
