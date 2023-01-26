using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Notes.ViewModels.Base;

namespace Simple_Notes.ViewModels
{
    internal class NotePageViewModel : ViewModel
    {
        #region Header : string - Note header

        private string _header;

        public string Header
        {
            get => _header;
            set => SetField(ref _header, value);
        }

        #endregion

        #region Body : string - Note body

        private string _body;

        public string Body
        {
            get => _body;
            set => SetField(ref _body, value);
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


    }
}
