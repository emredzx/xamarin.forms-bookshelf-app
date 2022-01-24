using BookShelf.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BookShelf
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddDeskPage), typeof(AddDeskPage));

            Routing.RegisterRoute(nameof(DetailDeskPage), typeof(DetailDeskPage));

            Routing.RegisterRoute(nameof(AddBookshelfPage), typeof(AddBookshelfPage)); 

            Routing.RegisterRoute(nameof(AddBookshelfDeskPage), typeof(AddBookshelfDeskPage));

            Routing.RegisterRoute(nameof(DetailBookshelfPage), typeof(DetailBookshelfPage));

            Routing.RegisterRoute(nameof(EditDetailDeskPage), typeof(EditDetailDeskPage));

            Routing.RegisterRoute(nameof(EditDetailBookshelfPage), typeof(EditDetailBookshelfPage));
        }
    }
}
