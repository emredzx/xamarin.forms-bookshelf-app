using BookShelf.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Services
{
    public static class BookshelfBookService
    {
        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
                return;
            // Get an absolute path to the database file
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BookData.db");

            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<BookshelfBook>();
        }

        public static async Task AddBookshelfBook(string name, string author, int totalpagenumber, int rating, string description)
        {
            await Init();

            var bookshelfBook = new BookshelfBook
            {
                Name = name,
                Author = author,
                TotalPageNumber = totalpagenumber,
                Rating = rating,
                Description = description,
            };
            await db.InsertAsync(bookshelfBook);
        }

        public static async Task UpdateBookshelfBook(int id, string name, string author, int totalpagenumber, int rating, string description)
        {
            await Init();

            var bookshelfBook = new BookshelfBook
            {
                Id = id,
                Name = name,
                Author = author,
                TotalPageNumber = totalpagenumber,
                Rating = rating,
                Description = description,
            };
            await db.UpdateAsync(bookshelfBook);
        }

        public static async Task RemoveBookshelfBook(int id)
        {
            await Init();

            await db.DeleteAsync<BookshelfBook>(id);
        }

        public static async Task<IEnumerable<BookshelfBook>> GetBookshelfBook()
        {
            await Init();
            var bookshelfBook = await db.Table<BookshelfBook>().ToListAsync();
            return bookshelfBook;
        }

        public static async Task<BookshelfBook> GetBookshelfBook(int id)
        {
            await Init();
            var bookshelfBook = await db.Table<BookshelfBook>().FirstOrDefaultAsync(b => b.Id == id);
            return bookshelfBook;
        }

        public static async Task<int> GetTotalPage()
        {
            await Init();
            var totalpage = await db.ExecuteScalarAsync<int>("SELECT SUM(TotalPageNumber) FROM BookshelfBook");
            return totalpage;
        }

        public static async Task<int> GetTotalBook()
        {
            await Init();
            var totalpage = await db.ExecuteScalarAsync<int>("SELECT COUNT(TotalPageNumber) FROM BookshelfBook");
            return totalpage;
        }
    }
}
