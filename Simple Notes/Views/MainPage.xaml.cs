using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Simple_Notes.ViewModels;
using Simple_Notes.Views;

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


            //ApplicationView.GetForCurrentView().Title = "Notes";
            this.InitializeComponent();
            var containter = ((App)App.Current).Container;
            ViewModel = (MainPageViewModel)ActivatorUtilities.GetServiceOrCreateInstance(containter, typeof(MainPageViewModel));
            DataContext = ViewModel;
            //var context = this.DataContext as MainPageViewModel;
            //if (context == null) return;
            //ApplicationView.GetForCurrentView().Title = "Notes";
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(NotePage), null);
        }
    }
}
