using Microsoft.Extensions.DependencyInjection;
using Simple_Notes.ViewModels;
using SimpleNotes.BAL.Services.Interfaces;

namespace Simple_Notes.Services
{
    internal class ViewModelLocator
    {
        private MainPageViewModel _mainPage;
        public MainPageViewModel MainPage => 
            _mainPage 
            ?? (_mainPage = ActivatorUtilities.GetServiceOrCreateInstance<MainPageViewModel>(((App)App.Current).Container));

        public NotePageViewModel NotePage =>
            ActivatorUtilities.GetServiceOrCreateInstance<NotePageViewModel>(((App)App.Current).Container);
    }
}
