using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml.Input;
using Simple_Notes.Infrastructure.Commands;
using Simple_Notes.Services;
using Simple_Notes.ViewModels.Base;
using Simple_Notes.Views;
using SimpleNotes.BAL.Services.Interfaces;
using SimpleNotes.DAL.Entities;

namespace Simple_Notes.ViewModels
{
    enum SortType
    {
        Ascending,
        Descending
    }

    internal class MainPageViewModel : ViewModel
    {

        private readonly INoteService _noteService;

        private Note _selectedNote;

        public Note SelectedNote
        {
            get => _selectedNote;
            set => SetField(ref _selectedNote, value);
        }

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

        private readonly ObservableCollection<Note> _notes;

        public ObservableCollection<Note> Notes => 
            Sort == SortType.Descending
                ? new ObservableCollection<Note>(_notes.Where(note => note.Header.Contains(Filter)).OrderBy(note => note.Header))
                : new ObservableCollection<Note>(_notes.Where(note => note.Header.Contains(Filter)).OrderByDescending(note => note.Header));

        private ICommand _sortAscendingCommand;

        public ICommand SortAscendingCommand =>
            _sortAscendingCommand 
            ?? (_sortAscendingCommand = new LambdaCommand(OnSortAscendingCommandExecuted, CanSortAscendingCommandExecute));

        private bool CanSortAscendingCommandExecute(object p) => 
            Sort == SortType.Descending;

        private void OnSortAscendingCommandExecuted(object p) =>
            Sort = SortType.Ascending;


        private ICommand _sortDescendingCommand;

        public ICommand SortDescendingCommand =>
            _sortDescendingCommand
            ?? (_sortDescendingCommand = new LambdaCommand(OnSortDescendingCommandExecuted, CanSortDescendingCommandExecute));

        private bool CanSortDescendingCommandExecute(object p) =>
            Sort == SortType.Ascending;

        private void OnSortDescendingCommandExecuted(object p) =>
            Sort = SortType.Descending;


        private ICommand _openNoteCommand;

        public ICommand OpenNoteCommand =>
            _openNoteCommand 
            ?? (_openNoteCommand = new LambdaCommand(OnOpenNoteCommandExecuted, CanOpenNoteCommandExecute));

        private bool CanOpenNoteCommandExecute(object p) =>
            true;

        private void OnOpenNoteCommandExecuted(object p)
        {
            if (p is KeyRoutedEventArgs args && args.Key != VirtualKey.Enter) return;
            NavigationService.Navigate(p.GetType(), SelectedNote);
        }


        private ICommand _deleteNoteCommand;

        public ICommand DeleteNoteCommand =>
            _deleteNoteCommand ?? (_deleteNoteCommand = new LambdaCommand(OnDeleteNoteCommandExecuted, CanDeleteNoteCommandExecute));

        private bool CanDeleteNoteCommandExecute(object p) => 
            true;

        private void OnDeleteNoteCommandExecuted(object p)
        {
            for (int i = 0; i < _notes.Count; i++)
            {
                if (_notes[i].NoteId == ((Note)p).NoteId)
                {
                    _notes.Remove(_notes[i]);
                    break;
                }
            }

            _noteService.DeleteNote(((Note)p).NoteId);
            OnPropertyChanged(nameof(Notes));
        }


        private ICommand _createNoteCommand;

        public ICommand CreateNoteCommand =>
            _createNoteCommand 
            ?? (_createNoteCommand = new LambdaCommand(OnCreateNoteCommandExecuted, CanCreateNoteCommandExecute));

        private bool CanCreateNoteCommandExecute(object p) => 
            true;

        private void OnCreateNoteCommandExecuted(object p)
        {
            NavigationService.Navigate(p.GetType());
        }

        public MainPageViewModel(INoteService noteService)
        {
            _noteService = noteService;
            _notes = new ObservableCollection<Note>(noteService.GetAllNotes());
        }
    }
}
