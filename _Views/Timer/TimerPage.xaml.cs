using TaskAppT2._Models;
using TaskAppT2._ViewModels.Study.FlashCards;
using TaskAppT2._ViewModels.Timer;

namespace TaskAppT2._Views.Timer;

public partial class TimerPage : ContentPage
{
	public TimerPage()
	{
		InitializeComponent();
        HandlerChanged += (s, e) => {
            BindingContext = Handler?.MauiContext?.Services.GetService<TimerPageVM>();
        };
	}

	const string START = "Start";
	const string STOP = "Stop";
	bool isStart = true;

    private void OnChangeState(object? sender, EventArgs? e)
    {
        FrogImg.IsVisible = false;
        TimeText.IsVisible = true;
        if (isStart)
		{
			isStart = false;
            VisualStateManager.GoToState(TimerButt, STOP);
        }
        else
		{
            isStart = true;
            VisualStateManager.GoToState(TimerButt, START);
        }
    }

    private void OnFalseChangeState(object sender, EventArgs e)
    {
        isStart = true;
        VisualStateManager.GoToState(TimerButt, START);
        FrogImg.IsVisible = true;
        TimeText.IsVisible = false;
    }
}