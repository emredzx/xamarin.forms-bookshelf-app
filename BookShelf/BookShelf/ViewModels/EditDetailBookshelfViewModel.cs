using BookShelf.Services;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BookShelf.ViewModels
{

    [QueryProperty(nameof(Id), nameof(Id))]
    [QueryProperty(nameof(Name), nameof(Name))]
    [QueryProperty(nameof(Author), nameof(Author))]
    [QueryProperty(nameof(TotalPageNumber), nameof(TotalPageNumber))]
    [QueryProperty(nameof(Rating), nameof(Rating))]
    [QueryProperty(nameof(Description), nameof(Description))]
    public class EditDetailBookshelfViewModel : ViewModelBase
    {
        string name, author, description;
        int id, totalpagenumber, rating;

        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Author { get => author; set => SetProperty(ref author, value); }
        public int TotalPageNumber { get => totalpagenumber; set => SetProperty(ref totalpagenumber, value); }
        public int Rating { get => rating; set => SetProperty(ref rating, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }

        public AsyncCommand UpdateCommand { get; }

        public EditDetailBookshelfViewModel()
        {
            Title = "Edit Bookshelf Book";
            UpdateCommand = new AsyncCommand(Update);
        }

        async Task Update()
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(author) ||
                string.IsNullOrWhiteSpace(description) || totalpagenumber == 0 || rating < 1 || rating > 5)
            {
                return;
            }

            await BookshelfBookService.UpdateBookshelfBook(id, name, author, totalpagenumber,rating,description);

            await Shell.Current.GoToAsync("..");
        }
    }
}
