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
    [QueryProperty(nameof(PageNumber), nameof(PageNumber))]
    [QueryProperty(nameof(TotalPageNumber), nameof(TotalPageNumber))]
    public class EditDetailDeskViewModel : ViewModelBase
    {
        string name, author;
        int id, pagenumber, totalpagenumber;

        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Author { get => author; set => SetProperty(ref author, value); }
        public int PageNumber { get => pagenumber; set => SetProperty(ref pagenumber, value); }
        public int TotalPageNumber { get => totalpagenumber; set => SetProperty(ref totalpagenumber, value); }

        public AsyncCommand UpdateCommand { get; }

        public EditDetailDeskViewModel()
        {
            Title = "Edit Desk Book";
            UpdateCommand = new AsyncCommand(Update);
        }

        async Task Update()
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(author) ||
                totalpagenumber == 0 || totalpagenumber < pagenumber ||
                totalpagenumber < 0 || pagenumber < 0)
            {
                return;
            }

            await DeskBookService.UpdateDeskBook(id, name, author, pagenumber, totalpagenumber);

            await Shell.Current.GoToAsync("..");
        }
    }
}
