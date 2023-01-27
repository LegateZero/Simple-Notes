using Microsoft.Extensions.DependencyInjection;
using Simple_Notes.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using SimpleNotes.BAL.Services.Interfaces;
using SimpleNotes.DAL.Entities;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Simple_Notes.Views
{

    public sealed partial class NotePage : Page
    {

        public NotePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var currentNote = ((Note)(e.Parameter));
            var container = ((App)App.Current).Container;
            var noteService = container.GetService<INoteService>();
            var viewModel = new NotePageViewModel(noteService, currentNote);

            DataContext = viewModel;
            viewModel.IsTextHasUnsavedChanges = false;
        }
    }
}
