using TaskAppT2._Servise;
using TaskAppT2._ViewModels.Study.FlashCards.LearnCardSet;

namespace TaskAppT2._Views.Study.FlashCards.LearnCardSet;

public partial class LearnDescroptionWithTermPan : ContentView
{
	public LearnDescroptionWithTermPan()
	{
		InitializeComponent();
        HandlerChanged += (s, e) => {
            BindingContext = Handler?.MauiContext?.Services.GetService<LearnCardSetVM>();
        };
    }
}