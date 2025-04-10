using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;

namespace TaskAppT2._Servise
{
    class PopUI
    {
        public static void ShowSnackErr(string text)
        {
            Snackbar.Make(text, visualOptions: new CommunityToolkit.Maui.Core.SnackbarOptions()
            {
                BackgroundColor = Color.FromArgb("#DA4B4B"),
            }).Show();
        }
    }
}
