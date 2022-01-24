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
    public partial class DeskPage : ContentPage
    {
        public DeskPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = (DeskViewModel)BindingContext;
            await vm.RefreshCommand.ExecuteAsync();
        }
    }
}