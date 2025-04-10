using AndroidX.AppCompat.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAppT2
{
    class EntryWithMauiUILineRenderer : Microsoft.Maui.Handlers.EntryHandler
    {
        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            platformView.SetBackgroundResource(Resource.Drawable.entry_with_maui_ui_line);
            base.ConnectHandler(platformView);
        }
    }
}