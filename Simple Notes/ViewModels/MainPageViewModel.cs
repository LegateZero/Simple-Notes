using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Simple_Notes.Infrastructure.Commands;
using Simple_Notes.Services.Interfaces;
using Simple_Notes.ViewModels.Base;
using Simple_Notes.Views;
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

        #region Note : Note - SelectedNote

        private Note _selectedNote;

        public Note SelectedNote
        {
            get => _selectedNote;
            set => SetField(ref _selectedNote, value);
        }

        #endregion


        #region Sort : SortType - Sorting Direction

        private SortType _sort = SortType.Descending;

        public SortType Sort
        {
            get => _sort;
            set
            {
                SetField(ref _sort, value);
                OnPropertyChanged(nameof(Notes));
            }
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


        #region SortDescending - Explanation

        private ICommand _openNoteCommand;

        public ICommand OpenNoteCommand =>
            _openNoteCommand ?? (_openNoteCommand = new LambdaCommand(OnOpenNoteCommandExecuted, CanOpenNoteCommandExecute));

        private bool CanOpenNoteCommandExecute(object p) => true;

        private void OnOpenNoteCommandExecuted(object p)
        {
            Frame frame = Window.Current.Content as Frame;
            var notePageViewModel = new NotePageViewModel(SelectedNote);
            frame.Navigate(typeof(NotePage), notePageViewModel);
        }

        #endregion

        #region DeleteNoteCommand - Note deletion command

        private ICommand _deleteNoteCommand;

        public ICommand DeleteNoteCommand =>
            _deleteNoteCommand ?? (_deleteNoteCommand = new LambdaCommand(OnDeleteNoteCommandExecuted, CanDeleteNoteCommandExecute));

        private bool CanDeleteNoteCommandExecute(object p) => true;

        private void OnDeleteNoteCommandExecuted(object p)
        {
            _notesSource.Source = _notesSource.View.Where(note => ((Note)note).NoteId != ((Note)p).NoteId);
            OnPropertyChanged(nameof(Notes));
        }

        #endregion


        public int DoSomething()
        {
            return 2;
        }



        private CollectionViewSource _notesSource = new CollectionViewSource();

        public ICollectionView Notes
        {
            get
            {
                var filtered = new CollectionViewSource();
                filtered.Source = Sort == SortType.Ascending 
                    ? _notesSource.View.OrderBy(note => ((Note)note).Header) 
                    : _notesSource.View.OrderByDescending(note => ((Note)note).Header);
                if (Filter.Length == 0)
                {
                    return filtered.View;
                }

                
                filtered.Source = filtered.View.Where(note => ((Note)note).Header.Contains(Filter));
                return filtered.View;
            }
        }


        public ICollectionView SortedNotes
        {
            get
            {
                if (Filter.Length == 0)
                {
                    return _notesSource.View;
                }

                var filtered = new CollectionViewSource();
                filtered.Source = _notesSource.View.Where(note => ((Note)note).Header.Contains(Filter));
                return filtered.View;
            }
        }


        #region Filter : string - Text filter

        private string _filter = string.Empty;

        public string Filter
        {
            get => _filter;
            set
            {
                SetField(ref _filter, value);
                OnPropertyChanged(nameof(Notes));
            }
        }

        #endregion





        public MainPageViewModel()
        {
            _notesSource.Source = Enumerable.Range(1, 20).Select(i => new Note(){ Body = $"Body of {i} note", Header = $"Header of {i} note", NoteId = i});
        }
    }
}
