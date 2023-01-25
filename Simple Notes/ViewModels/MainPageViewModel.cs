using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Notes.ViewModels.Base;

namespace Simple_Notes.ViewModels
{
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

    }
}
