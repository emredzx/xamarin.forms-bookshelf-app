using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookShelf.Services;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace BookShelf.ViewModels
{
    public class AddDeskViewModel : ViewModelBase
    {
        string name, author;
        int pagenumber, totalpagenumber;

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Author { get => author; set => SetProperty(ref author, value); }
        public int PageNumber { get => pagenumber; set => SetProperty(ref pagenumber, value); }
        public int TotalPageNumber { get => totalpagenumber; set => SetProperty(ref totalpagenumber, value); }

        public AsyncCommand SaveCommand { get; }

        public AddDeskViewModel()
        {
            Title = "Add to Desk";
            SaveCommand = new AsyncCommand(Save);
        }

        async Task Save()
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(author) || 
                totalpagenumber == 0 || totalpagenumber < pagenumber ||
                totalpagenumber < 0 || pagenumber < 0)
            {
                return;
            }

            await DeskBookService.AddDeskBook(name, author, pagenumber, totalpagenumber);

            await Shell.Current.GoToAsync("..");
        }
    }
}
