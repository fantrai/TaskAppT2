using TaskAppT2._Servise;
using TaskAppT2._ViewModels.Study.FlashCards.LearnCardSet;
using TaskAppT2.MainMenu.Study.FlashCards;

namespace TaskAppT2._Views.Study.FlashCards.LearnCardSet;

public partial class LearnFlashCardPan : ContentView
{
	public LearnFlashCardPan()
	{
		InitializeComponent();

        HandlerChanged += (s, e) => {
            BindingContext = Handler?.MauiContext?.Services.GetService<LearnCardSetVM>();
        };
    }

    private void OnRotate(object sender, EventArgs e)
    {
        var butt = ((Button)sender);
        butt.FontAttributes = butt.FontAttributes == FontAttributes.Bold ? FontAttributes.None : FontAttributes.Bold; 
    }
}