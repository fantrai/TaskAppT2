using System.Threading.Tasks;
using TaskAppT2._Models;
using TaskAppT2._ViewModels.Study.FlashCards;

namespace TaskAppT2._Views.Study.FlashCards;

public partial class NewCardSetPan : ContentView
{
	public NewCardSetPan()
	{
		InitializeComponent();

        HandlerChanged += (s, e) => {
            BindingContext = Handler?.MauiContext?.Services.GetService<NewCardSetPanVM>();
        };

        var tapGesture = new TapGestureRecognizer() { NumberOfTapsRequired = 1 };
        tapGesture.Tapped += (s, e) => { OnCreateNewCard?.Invoke(); };
        AddCardLb.GestureRecognizers.Add(tapGesture);
    }

    public static Action? OnCreateNewCard { get; set; }
    public static Action<Card>? OnRemoveCard { get; set; }

    private void OnDeleteCard(object sender, EventArgs e)
    {
        var card = (Card)((ImageButton)sender).BindingContext;
        OnRemoveCard?.Invoke(card);
    }
}