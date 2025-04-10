using TaskAppT2._ViewModels.Timer;
using TaskAppT2._ViewModels.Writes;

namespace TaskAppT2._Views.Writes;

public partial class AddNewNoteModalPage : ContentPage
{
	public AddNewNoteModalPage()
	{
		InitializeComponent();
        HandlerChanged += (s, e) => {
            BindingContext = Handler?.MauiContext?.Services.GetService<WritesPageVM>();
        };
    }
}