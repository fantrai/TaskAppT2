using TaskAppT2._ViewModels.Timer;

namespace TaskAppT2._Views.Timer;

public partial class EndIntervalModalPage : ContentPage
{
	public EndIntervalModalPage()
	{
		InitializeComponent();
        HandlerChanged += (s, e) => {
            BindingContext = Handler?.MauiContext?.Services.GetService<TimerPageVM>();
        };
    }
}