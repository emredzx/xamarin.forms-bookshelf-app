using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BookShelf.Models
{
    public class DeskBook
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int PageNumber { get; set; }
        public int TotalPageNumber { get; set; }
    }
}
