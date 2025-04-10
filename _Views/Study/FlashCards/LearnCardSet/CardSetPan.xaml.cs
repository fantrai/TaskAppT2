using TaskAppT2._Models;
using TaskAppT2._Servise;
using TaskAppT2._ViewModels.Study.FlashCards.LearnCardSet;
using TaskAppT2.MainMenu.Study.FlashCards;

namespace TaskAppT2._Views.Study.FlashCards.LearnCardSet;

public partial class CardSetPan : ContentView
{
	public CardSetPan()
	{
		InitializeComponent();
        HandlerChanged += (s, e) => {
            BindingContext = Handler?.MauiContext?.Services.GetService<CardSetPanVM>();
        };
    }

    public static Action? OnGoLearnFlashCardPan {  get; set; }
    public static Action? OnGoLearnTermWithDescryptionPan {  get; set; }
    public static Action? OnGoLearnDescryptionWithTermPan {  get; set; }
    public static Action? OnGoLearnCrossingPan {  get; set; }

    private void SwitchView(object sender, EventArgs e)
    {
        var butt = (Button)sender;
        var card = (Card)butt.BindingContext;
        if (butt.FontAttributes == FontAttributes.Bold)
        {
            butt.FontAttributes = FontAttributes.None;
            butt.Text = card.Description;
        }
        else
        {
            butt.FontAttributes = FontAttributes.Bold;
            butt.Text = card.Term;
        }
    }

    private void OnStartLearnFlashCards(object sender, EventArgs e)
    {
        OnGoLearnFlashCardPan?.Invoke();
        var cards = ((CardSetPanVM)((Button)sender).BindingContext).Cards;
        LearnCardSetVM.OnUseCards?.Invoke([.. cards]);
        FlashCardsPan.OnUseNewReturnButtonImage?.Invoke(UseImage.Cross);
    }

    private void OnStartLearnTermForDescripton(object sender, EventArgs e)
    {
        OnGoLearnTermWithDescryptionPan?.Invoke();
        var cards = ((CardSetPanVM)((Button)sender).BindingContext).Cards;
        LearnCardSetVM.OnUseCards?.Invoke([.. cards]);
        FlashCardsPan.OnUseNewReturnButtonImage?.Invoke(UseImage.Cross);
    }

    private void OnStartLearnDescriptionWithTerm(object sender, EventArgs e)
    {
        OnGoLearnDescryptionWithTermPan?.Invoke();
        var cards = ((CardSetPanVM)((Button)sender).BindingContext).Cards;
        LearnCardSetVM.OnUseCards?.Invoke([.. cards]);
        FlashCardsPan.OnUseNewReturnButtonImage?.Invoke(UseImage.Cross);
    }

    private void OnStartLearnCrossing(object sender, EventArgs e)
    {
        OnGoLearnCrossingPan?.Invoke();
        var cards = ((CardSetPanVM)((Button)sender).BindingContext).Cards;
        LearnCardSetVM.OnUseCards?.Invoke([.. cards]);
        FlashCardsPan.OnUseNewReturnButtonImage?.Invoke(UseImage.Cross);
    }
}