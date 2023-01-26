using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Notes.Services.Interfaces
{
    internal interface INavigationService
    {
        void NavigateTo(string page, object parameter);
        void GoBack();

    }
}
