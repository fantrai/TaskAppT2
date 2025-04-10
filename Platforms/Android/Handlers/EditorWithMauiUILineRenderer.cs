using AndroidX.AppCompat.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAppT2
{
    class EditorWithMauiUILineRenderer : Microsoft.Maui.Handlers.EditorHandler
    {
        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            platformView.SetBackgroundResource(Resource.Drawable.entry_with_maui_ui_line);
            base.ConnectHandler(platformView);
        }
    }
}