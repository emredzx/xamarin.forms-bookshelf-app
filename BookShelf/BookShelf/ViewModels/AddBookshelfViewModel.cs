using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookShelf.Models;
using BookShelf.Services;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace BookShelf.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    [QueryProperty(nameof(Name), nameof(Name))]
    [QueryProperty(nameof(Author), nameof(Author))]
    [QueryProperty(nameof(TotalPageNumber), nameof(TotalPageNumber))]
    public class AddBookshelfViewModel : ViewModelBase
    {
        string name, author, description;
        int id, totalpagenumber, rating;

        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Author { get => author; set => SetProperty(ref author, value); }
        public int TotalPageNumber { get => totalpagenumber; set => SetProperty(ref totalpagenumber, value); }
        public int Rating { get => rating; set => SetProperty(ref rating, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }

        public AsyncCommand SaveCommand { get; }

        public AddBookshelfViewModel()
        {
            Title = "Add to Bookshelf";
            SaveCommand = new AsyncCommand(Save);
        }

        async Task Save()
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(author) || 
                string.IsNullOrWhiteSpace(description) || totalpagenumber == 0 || rating < 1 || rating > 5)
            {
                return;
            }

            await BookshelfBookService.AddBookshelfBook(name, author, totalpagenumber, rating, description);

            await DeskBookService.RemoveDeskBook(Id);

            await Shell.Current.GoToAsync("..");
        }
    }
}
