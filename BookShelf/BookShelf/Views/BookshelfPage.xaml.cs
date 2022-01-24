using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelf.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookShelf.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookshelfPage : ContentPage
    {
        public BookshelfPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = (BookshelfViewModel)BindingContext;
            await vm.RefreshCommand.ExecuteAsync();
        }
    }
}