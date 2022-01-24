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
    [QueryProperty(nameof(DeskBookId), nameof(DeskBookId))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailDeskPage : ContentPage
    {
        public string DeskBookId { get; set; }
        public DetailDeskPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(DeskBookId, out var result);

            BindingContext = await DeskBookService.GetDeskBook(result);
        }
    }
}