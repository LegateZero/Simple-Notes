using Microsoft.Extensions.DependencyInjection;
using Simple_Notes.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SimpleNotes.DAL.Context;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Simple_Notes.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class NotePage : Page
    {

        public NotePage()
        {
            Debug.WriteLine("Test1");
            this.InitializeComponent();
            
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = (Window.Current.Content as Frame);
            rootFrame.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Debug.WriteLine("Test2");
            var viewModel = ((NotePageViewModel)(e.Parameter));
            Debug.WriteLine(viewModel);
            var containter = ((App)App.Current).Container;
            DataContext = viewModel;
        }
    }
}
