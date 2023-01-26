using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Notes.Infrastructure.Commands;
using System.Windows.Input;
using Simple_Notes.ViewModels.Base;
using Simple_Notes.Views;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using SimpleNotes.DAL.Context;

namespace Simple_Notes.ViewModels
{
    internal class NotePageViewModel : ViewModel
    {
        #region Header : string - Note header

        private string _header;

        public string Header
        {
            get => _header;
            set {
                SetField(ref _header, value);
                IsTextHasUnsavedChanges = true;
            }
        }

        #endregion

        #region Body : string - Note body

        private string _body;

        public string Body
        {
            get => _body;
            set
            {
                SetField(ref _body, value);
                IsTextHasUnsavedChanges = true;
            }
        }

        #endregion


        public NotePageViewModel()
        {

        }

        public NotePageViewModel(string header, string body)
        {
            _header = header;
            _body = body;
        }

        #region IsTexthasUnsafedChanges : bool - Changes Indicator

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

        #endregion

        #region SortDescending - Explanation

        private ICommand _saveChangesCommand;

        public ICommand SaveChangesCommand =>
            _saveChangesCommand ?? (_saveChangesCommand = new LambdaCommand(OnSaveChangesCommandExecuted, CanSaveChangesCommandExecute));

        private bool CanSaveChangesCommandExecute(object p) => IsTextHasUnsavedChanges;

        private void OnSaveChangesCommandExecuted(object p)
        {
            Debug.WriteLine((string)p);
            IsTextHasUnsavedChanges = false;
        }

        #endregion

        


        public NotePageViewModel(Note note)
        {
            Header = note.Header;
            Body = note.Body;
        }

    }
}
