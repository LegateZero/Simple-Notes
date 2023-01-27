using System;
using Simple_Notes.Infrastructure.Commands;
using System.Windows.Input;
using Simple_Notes.ViewModels.Base;
using Windows.UI.Xaml.Controls;
using Simple_Notes.Services;
using SimpleNotes.BAL.Services.Interfaces;
using SimpleNotes.DAL.Entities;

namespace Simple_Notes.ViewModels
{
    internal class NotePageViewModel : ViewModel
    {
        private readonly INoteService _noteService;


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
                    Title = "Unsaved changes",
                    Content = "You have unsaved changes, are you sure want to exit and delete them?",
                    PrimaryButtonText = "Ok",
                    CloseButtonText = "Cancel"
                };
            if (!IsTextHasUnsavedChanges || await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                NavigationService.GoBack();
            }
        }

        public NotePageViewModel(INoteService noteService, Note note = null)
        {
            _noteService = noteService;
            if (note != null)
            {
                Header = note.Header;
                Body = note.Body;
            }

            CurrentNote = note;
        }
        


        private Note _currentNote;

        public Note CurrentNote
        {
            get => _currentNote;
            set => SetField(ref _currentNote, value);
        }


    }
}
