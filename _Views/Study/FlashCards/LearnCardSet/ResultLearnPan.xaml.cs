using Microsoft.Maui.Graphics.Text;
using TaskAppT2._Servise;
using TaskAppT2._ViewModels.Study.FlashCards;
using TaskAppT2._ViewModels.Study.FlashCards.LearnCardSet;
using TaskAppT2.MainMenu.Study.FlashCards;

namespace TaskAppT2._Views.Study.FlashCards.LearnCardSet;

public partial class ResultLearnPan : ContentView
{
	public ResultLearnPan()
	{
		InitializeComponent();

        HandlerChanged += (s, e) => {
            BindingContext = Handler?.MauiContext?.Services.GetService<LearnCardSetVM>();
        };

    }

    private void OnRetrySet(object sender, EventArgs e)
    {
        FlashCardsPan.OnStartGoBack?.Invoke();
        FlashCardsPan.OnUseNewReturnButtonImage?.Invoke(UseImage.Cross);
        LearnCardSetVM.OnReplayLernSet?.Invoke();
    }

    private void OnBackToSet(object sender, EventArgs e)
    {
        FlashCardsPan.OnStartGoBack?.Invoke();
        FlashCardsPan.OnStartGoBack?.Invoke();
    }
}