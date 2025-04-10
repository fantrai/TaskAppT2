using TaskAppT2._ViewModels.Study.FlashCards.LearnCardSet;

namespace TaskAppT2._Views.Study.FlashCards.LearnCardSet;

public partial class LearnTermWithDescryptionPan : ContentView
{
	public LearnTermWithDescryptionPan()
	{
		InitializeComponent();
        HandlerChanged += (s, e) => {
            BindingContext = Handler?.MauiContext?.Services.GetService<LearnCardSetVM>();
        };
    }

    private void OnSendRequest(object sender, EventArgs e)
    {
        ResultText.Text = string.Empty;
    }
}