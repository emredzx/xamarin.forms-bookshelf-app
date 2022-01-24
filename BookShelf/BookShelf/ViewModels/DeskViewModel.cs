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
    public class DeskViewModel : ViewModelBase
    {
        
        public ObservableRangeCollection<DeskBook> deskBooks { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<DeskBook> RemoveCommand { get; }
        public AsyncCommand<DeskBook> UpdateCommand { get; }
        public AsyncCommand<DeskBook> BookshelfCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }
        public DeskViewModel()
        {
            Title = "Desk";

            deskBooks = new ObservableRangeCollection<DeskBook>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<DeskBook>(Remove);
            UpdateCommand = new AsyncCommand<DeskBook>(Update);
            BookshelfCommand = new AsyncCommand<DeskBook>(BookShelf);
            SelectedCommand = new AsyncCommand<object>(Selected);
        }

        async Task Update(DeskBook deskBook)
        {
            if (deskBook == null)
                return;

            SelectedDeskBook = null;

            var route = $"{nameof(EditDetailDeskPage)}?Id={deskBook.Id}&Name={deskBook.Name}&Author={deskBook.Author}&PageNumber={deskBook.PageNumber}&TotalPageNumber={deskBook.TotalPageNumber}";
            await Shell.Current.GoToAsync(route);
        }

        async Task BookShelf (DeskBook deskBook)
        {
            if (deskBook == null)
                return;

            SelectedDeskBook = null;

            var route = $"{nameof(AddBookshelfDeskPage)}?Id={deskBook.Id}&Name={deskBook.Name}&Author={deskBook.Author}&TotalPageNumber={deskBook.TotalPageNumber}";
            await Shell.Current.GoToAsync(route);
        }

        DeskBook selectedDeskBook;
        
        public DeskBook SelectedDeskBook
        {
            get => selectedDeskBook;
            set => SetProperty(ref selectedDeskBook, value);
        }

        async Task Selected(object args)
        {
            var deskBook = args as DeskBook;
            if (deskBook == null)
                return;

            SelectedDeskBook = null;
            
            var route = $"{nameof(DetailDeskPage)}?DeskBookId={deskBook.Id}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Add()
        {
            var route = $"{nameof(AddDeskPage)}";
            await Shell.Current.GoToAsync(route);
        }


        async Task Remove(DeskBook deskbook)
        {
            await DeskBookService.RemoveDeskBook(deskbook.Id);
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(500);

            deskBooks.Clear();

            var books = await DeskBookService.GetDeskBook();

            deskBooks.AddRange(books);

            IsBusy = false;
        }
    }

}
