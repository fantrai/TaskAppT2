using System.Collections.Specialized;
using TaskAppT2._ViewModels.Study.FlashCards.LearnCardSet;
using TaskAppT2.MainMenu.Study.FlashCards;

namespace TaskAppT2._Views.Study.FlashCards.LearnCardSet;

public partial class LearnCrossingPan : ContentView
{
	public LearnCrossingPan()
	{
		InitializeComponent();
        HandlerChanged += (s, e) => {
            BindingContext = Handler?.MauiContext?.Services.GetService<LearnCardSetVM>();
        };
    }

    void ResetPan()
    {
        FlashCardsPan.OnStartGoBack -= ResetPan;
        CardsCV.IsVisible = false;
        StartButt.IsVisible = true;
    }

    private void OnStart(object sender, EventArgs e)
    {
        FlashCardsPan.OnStartGoBack += ResetPan;
        CardsCV.IsVisible = true;
        StartButt.IsVisible = false;
    }

    private void OnNewSelected(object sender, SelectionChangedEventArgs e)
    {
        if (CardsCV.SelectedItems.Count >= 2)
        {
            LearnCardSetVM.OnCheckCross?.Invoke(((CrossCardView)CardsCV.SelectedItems[0]).Text, ((CrossCardView)CardsCV.SelectedItems[1]).Text);
            CardsCV.SelectedItems = null;
        }
    }
}