using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Microsoft.Extensions.DependencyInjection;
using Simple_Notes.ViewModels;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace Simple_Notes
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        internal MainPageViewModel ViewModel { get; set; }
        public MainPage()
        {
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = false;
            
            this.InitializeComponent();
            var container = ((App)App.Current).Container;
            ViewModel = (MainPageViewModel)ActivatorUtilities.GetServiceOrCreateInstance(container, typeof(MainPageViewModel));
            DataContext = ViewModel;
            
        }

        private void KeyboardAccelerator_OnInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            if (this.ViewList.FocusState == FocusState.Unfocused)
                this.ViewList.Focus(FocusState.Keyboard);
        }
    }
}
