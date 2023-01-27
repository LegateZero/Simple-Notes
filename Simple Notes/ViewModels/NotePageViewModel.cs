using System;
using Simple_Notes.Infrastructure.Commands;
using System.Windows.Input;
using Simple_Notes.ViewModels.Base;
using Windows.UI.Xaml.Controls;
using Simple_Notes.Services;
using SimpleNotes.BAL.Services.Interfaces;
using SimpleNotes.DAL.Entities;
using System.Collections.ObjectModel;

namespace Simple_Notes.ViewModels
{
    internal class NotePageViewModel : ViewModel
    {
        private readonly INoteService _noteService;


        private Note _currentNote;

        public Note CurrentNote
        {
            get => _currentNote;
            set => SetField(ref _currentNote, value);
        }

        private string _header = string.Empty;
        public string Header
        {
            get => _header;
            set {
                SetField(ref _header, value);
                IsTextHasUnsavedChanges = true;
            }
        }


        private string _body = string.Empty;
        public string Body
        {
            get => _body;
            set
            {
                SetField(ref _body, value);
                IsTextHasUnsavedChanges = true;
            }
        }


        private bool _isTextHasUnsavedChanges = false;
        public bool IsTextHasUnsavedChanges
        {
            get => _isTextHasUnsavedChanges;
            set
            {
                SetField(ref _isTextHasUnsavedChanges, value);
                OnPropertyChanged(nameof(SaveChangesCommand));
            }
        }




        private ICommand _saveChangesCommand;

        public ICommand SaveChangesCommand =>
            _saveChangesCommand 
            ?? (_saveChangesCommand = new LambdaCommand(OnSaveChangesCommandExecuted, CanSaveChangesCommandExecute));

        private bool CanSaveChangesCommandExecute(object p) => 
            IsTextHasUnsavedChanges && Header.Length > 0 && Body.Length > 0;

        private void OnSaveChangesCommandExecuted(object p)
        {
            if (CurrentNote == null)
            {
                CurrentNote = new Note()
                {
                    Header = this.Header,
                    Body = this.Body,
                };
                _noteService.AddNote(CurrentNote);
            }
            else
            {
                CurrentNote.Header = this.Header;
                CurrentNote.Body = this.Body;
                _noteService.UpdateNote(CurrentNote);
            }

            IsTextHasUnsavedChanges = false;
        }


        private ICommand _goBackCommand;

        public ICommand GoBackCommand => 
            _goBackCommand 
            ?? (_goBackCommand = new LambdaCommand(OnGoBackCommandExecuted));

        private async void OnGoBackCommandExecuted(object p)
        {
            var dialog = new ContentDialog()
                {
                    Title = "Несохраненные изменения",
                    Content = "Вы уверены, что хотите покинуть страницу? Все несохраненные изменения будут потеряны.",
                    PrimaryButtonText = "Уйти",
                    CloseButtonText = "Отмена"
                };
            if (!IsTextHasUnsavedChanges || await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                NavigationService.GoBack();
            }
        }


        private ICommand _loadedCommand;

        public ICommand LoadedCommand =>
            _loadedCommand ?? (_loadedCommand = new LambdaCommand(OnLoadedCommandExecuted));

        private void OnLoadedCommandExecuted(object p)
        {
            if (p is Note note)
            {
                Header = note.Header;
                Body = note.Body;
                CurrentNote = note;
                IsTextHasUnsavedChanges = false;
            }
        }


        public NotePageViewModel(INoteService noteService)
        {
            _noteService = noteService;
        }

    }
}
