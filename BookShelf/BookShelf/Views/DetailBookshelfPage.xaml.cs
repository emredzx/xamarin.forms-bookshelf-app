using BookShelf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookShelf.Views
{
    [QueryProperty(nameof(BookshelfbookId), nameof(BookshelfbookId))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailBookshelfPage : ContentPage
    {
        public string BookshelfbookId { get; set; }
        public DetailBookshelfPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(BookshelfbookId, out var result);

            BindingContext = await BookshelfBookService.GetBookshelfBook(result);
        }
    }
}