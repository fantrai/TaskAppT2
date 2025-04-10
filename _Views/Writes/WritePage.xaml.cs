using TaskAppT2._Models;
using TaskAppT2._ViewModels.Writes;

namespace TaskAppT2._Views.Writes;

public partial class WritePage : ContentPage
{
	public WritePage()
	{
		InitializeComponent();
        HandlerChanged += (s, e) => {
            BindingContext = Handler?.MauiContext?.Services.GetService<WritesPageVM>();
        };
    }

    public static Action<string>? OnNewFindText { get; set; }

    private void OnChangeFindText(object sender, TextChangedEventArgs e)
    {
        string str = ((Entry)sender).Text;
        OnNewFindText?.Invoke(str);
    }
}