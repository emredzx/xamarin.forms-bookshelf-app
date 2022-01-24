using BookShelf.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Services
{
    public static class DeskBookService
    {
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (db != null)
                return;
            // Get an absolute path to the database file
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BookData.db");

            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<DeskBook>();
        }
        public static async Task AddDeskBook(string name, string author, int pagenumber, int totalpagenumber)
        {
            await Init();

            var deskBook = new DeskBook
            {
                Name = name,
                Author = author,
                PageNumber = pagenumber,
                TotalPageNumber = totalpagenumber,
            };
            await db.InsertAsync(deskBook);
        }

        public static async Task UpdateDeskBook(int id, string name, string author, int pagenumber, int totalpagenumber)
        {
            await Init();

            var deskBook = new DeskBook
            {
                Id = id,
                Name = name,
                Author = author,
                PageNumber = pagenumber,
                TotalPageNumber = totalpagenumber,
            };
            await db.UpdateAsync(deskBook);
        }

        public static async Task RemoveDeskBook(int id)
        {
            await Init();

            await db.DeleteAsync<DeskBook>(id);
        }
        public static async Task<IEnumerable<DeskBook>> GetDeskBook()
        {
            await Init();
            var deskBook = await db.Table<DeskBook>().ToListAsync();
            return deskBook;
        }
        public static async Task<DeskBook> GetDeskBook(int id)
        {
            await Init();
            var deskBook = await db.Table<DeskBook>().FirstOrDefaultAsync(d => d.Id == id);
            return deskBook;
        }

        public static async Task<int> GetTotalPage()
        {
            await Init();
            var totalpage = await db.ExecuteScalarAsync<int>("SELECT SUM(PageNumber) FROM DeskBook");
            return totalpage;
        }

        public static async Task<int> GetTotalBook()
        {
            await Init();
            var totalpage = await db.ExecuteScalarAsync<int>("SELECT COUNT(PageNumber) FROM DeskBook");
            return totalpage;
        }

    }
}
