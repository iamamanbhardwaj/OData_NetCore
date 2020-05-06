using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIsWithOData.Models
{
    public class SqlBookStoreDbContext : DbContext
    {

        public SqlBookStoreDbContext() : base() { }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Press> Presses { get; set; }


    }
}
