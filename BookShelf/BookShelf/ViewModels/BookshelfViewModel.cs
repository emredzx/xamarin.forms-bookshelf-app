using BookShelf.Models;
using BookShelf.Services;
using BookShelf.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BookShelf.ViewModels
{
    public class BookshelfViewModel : ViewModelBase
    {
        public ObservableRangeCollection<BookshelfBook> bookshelfBooks { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<BookshelfBook> UpdateCommand { get; }
        public AsyncCommand<BookshelfBook> RemoveCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }

        public BookshelfViewModel()
        {
            Title = "Bookshelf";

            bookshelfBooks = new ObservableRangeCollection<BookshelfBook>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            UpdateCommand = new AsyncCommand<BookshelfBook>(Update);
            RemoveCommand = new AsyncCommand<BookshelfBook>(Remove);
            SelectedCommand = new AsyncCommand<object>(Selected);
        }

        BookshelfBook selectedBookshelfBook;
        public BookshelfBook SelectedBookshelfBook
        {
            get => selectedBookshelfBook;
            set => SetProperty(ref selectedBookshelfBook, value);
        }

        async Task Selected(object args)
        {
            var bookshelfBook = args as BookshelfBook;
            if (bookshelfBook == null)
                return;

            SelectedBookshelfBook = null;

            var route = $"{nameof(DetailBookshelfPage)}?BookshelfbookId={bookshelfBook.Id}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Update(BookshelfBook bookshelfBook)
        {
            if (bookshelfBook == null)
                return;

            SelectedBookshelfBook = null;

            var route = $"{nameof(EditDetailBookshelfPage)}?Id={bookshelfBook.Id}&Name={bookshelfBook.Name}&Author={bookshelfBook.Author}&TotalPageNumber={bookshelfBook.TotalPageNumber}" +
                $"&Rating={bookshelfBook.Rating}&Description={bookshelfBook.Description}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Add()
        {
            var route = $"{nameof(AddBookshelfPage)}";
            await Shell.Current.GoToAsync(route);
        }
        async Task Remove(BookshelfBook bookshelfbook)
        {
            await BookshelfBookService.RemoveBookshelfBook(bookshelfbook.Id);
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(500);

            bookshelfBooks.Clear();

            var books = await BookshelfBookService.GetBookshelfBook();

            bookshelfBooks.AddRange(books);

            IsBusy = false;
        }
    }
}
