using BookShelf.Services;
using BookShelf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookShelf.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordsPage : ContentPage
    {
        public RecordsPage()
        {
            InitializeComponent();
        }

        
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var pageCount = await BookshelfBookService.GetTotalPage();
            pageCount += await DeskBookService.GetTotalPage();
            var bookshelfbookCount = await BookshelfBookService.GetTotalBook();
            var deskbookCount = await DeskBookService.GetTotalBook();
            totalPages.Text = pageCount.ToString();
            bookshelfBook.Text = bookshelfbookCount.ToString();
            deskbookBook.Text = deskbookCount.ToString();
        }
    }

}