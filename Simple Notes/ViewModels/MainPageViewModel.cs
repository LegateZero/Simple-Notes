using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Simple_Notes.Infrastructure.Commands;
using Simple_Notes.Services.Interfaces;
using Simple_Notes.ViewModels.Base;
using SimpleNotes.DAL.Context;

namespace Simple_Notes.ViewModels
{
    enum SortType
    {
        Ascending,
        Descending
    }

    internal class MainPageViewModel : ViewModel
    {
        #region Title : string - Page title

        private string _title = "Проверка";

        public string Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }

        #endregion


        #region Counter : int - Counter

        private int _counter;

        public int Counter
        {
            get => _counter;
            set => SetField(ref _counter, value);
        }

        #endregion


        #region Sort : SortType - Sorting Direction

        private SortType _sort = SortType.Descending;

        public SortType Sort
        {
            get => _sort;
            set => SetField(ref _sort, value);
        }

        #endregion


        private ICommand _increment;
        public ICommand Increment => _increment ?? (_increment = new LambdaCommand(OnIncrementCommandExecuted, CanIncrementCommandExecuted));

        private void OnIncrementCommandExecuted()
        {
            Counter++;
        }

        private bool CanIncrementCommandExecuted()
        {
            return Counter < 10;
        }



        #region SortAcending - Explanation

        private ICommand _SortAcending;

        public ICommand SortAcending =>
            _SortAcending ?? (_SortAcending =  new LambdaCommand(OnSortAcendingExecuted, CanSortAcendingExecute));

        private bool CanSortAcendingExecute(object p) => Sort == SortType.Descending;

        private void OnSortAcendingExecuted(object p)
        {
            Sort = SortType.Ascending;
        }

        #endregion

        #region SortDescending - Explanation

        private ICommand _SortDescending;

        public ICommand SortDescending =>
            _SortDescending ?? (_SortDescending = new LambdaCommand(OnSortDescendingExecuted, CanSortDescendingExecute));

        private bool CanSortDescendingExecute(object p) => Sort == SortType.Ascending;

        private void OnSortDescendingExecuted(object p)
        {
            Sort = SortType.Descending;
        }

        #endregion

        private CollectionViewSource _notesSource = new CollectionViewSource();

        public ICollectionView Notes => _notesSource.View;


        public MainPageViewModel()
        {
            _notesSource.Source = Enumerable.Range(1, 20).Select(i => new Note(){ Body = $"Body of {i} note", Header = $"Header of {i} note", NoteId = i});
        }
    }
}
