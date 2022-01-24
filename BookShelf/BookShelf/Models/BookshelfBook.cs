using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BookShelf.Models
{
    public class BookshelfBook
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int TotalPageNumber { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
    }
}
