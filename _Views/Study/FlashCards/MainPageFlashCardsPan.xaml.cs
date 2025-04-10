using TaskAppT2._Models;
using TaskAppT2._ViewModels.Study.FlashCards;
using TaskAppT2._ViewModels.Study.FlashCards.LearnCardSet;

namespace TaskAppT2.MainMenu.Study.FlashCards;

public partial class MainPageFlashCardsPan : ContentView
{
	public MainPageFlashCardsPan()
	{
		InitializeComponent();

        HandlerChanged += (s, e) => { 
            BindingContext = Handler?.MauiContext?.Services.GetService<MainPageFlashCardsPanVM>(); 
        };

        StartCarhegory.Loaded += (s, e) => {
            if (CathegoryCV.SelectedItem == null)
                VisualStateManager.GoToState(StartCarhegory, SELECT);
        };

        CathegoryCV.SelectionChanged += (s, e) => { 
            if (CathegoryCV.SelectedItem == null)
            {
                CathegoryCV.ScrollTo(0);
                VisualStateManager.GoToState(StartCarhegory, SELECT);
                AddNewCardSet.IsVisible = true;
                AddCardSetInGroup.IsVisible = false;
                OnChangeSelectCathegory?.Invoke(null);
            }
            else
            {
                VisualStateManager.GoToState(StartCarhegory, NO_SELECT);
                CathegoryCV.ScrollTo(CathegoryCV.SelectedItem, position: ScrollToPosition.Center, animate: true);
                AddNewCardSet.IsVisible = false;
                AddCardSetInGroup.IsVisible = true;
                OnChangeSelectCathegory?.Invoke((CardSetCathegory)CathegoryCV.SelectedItem);
            }
        };

        var tapGesture = new TapGestureRecognizer() { NumberOfTapsRequired = 1 };
        tapGesture.Tapped += (s, e) => { OnGoCreateNewCardSet?.Invoke(); };
        AddNewCardSet.GestureRecognizers.Add(tapGesture);

        tapGesture = new TapGestureRecognizer() { NumberOfTapsRequired = 1 };
        tapGesture.Tapped += async (s, e) => {
            if (OnGoCreateNewCathegory != null)
            {
                await OnGoCreateNewCathegory.Invoke();
                NewCathegoryPageVM.OnUseCathegory?.Invoke((CardSetCathegory)CathegoryCV.SelectedItem);
            }
        };
        AddCardSetInGroup.GestureRecognizers.Add(tapGesture);

    }

    public static Func<Task>? OnGoCreateNewCathegory { get; set; }
    public static Action? OnGoCreateNewCardSet { get; set; }
    public static Action? OnGoCardSetPan { get; set; }
    public static Action<CardSet>? OnDeleteCardSet { get; set; }
    public static Action<CardSetCathegory?>? OnChangeSelectCathegory { get; set; }
    public static Action<string>? OnFindText { get; set; }

    const string SELECT = "Select";
    const string NO_SELECT = "NoSelect";

    private void OnClickAddNewCathegory(object sender, EventArgs e)
    {
        OnGoCreateNewCathegory?.Invoke();
    }

    private void OnAllSets(object sender, EventArgs e)
    {
        CathegoryCV.SelectedItem = null;
    }

    private void OnClickEditCardSet(object sender, EventArgs e)
    {
        OnGoCreateNewCardSet?.Invoke();
        var cardSet = (CardSet)((Button)sender).BindingContext;
        NewCardSetPanVM.OnUseCardSet?.Invoke(cardSet);
    }

    private void OnClickedDeleteCardSet(object sender, EventArgs e)
    {
        var cardSet = (CardSet)((Button)sender).BindingContext;
        OnDeleteCardSet?.Invoke(cardSet);
    }

    private void OnChangeFind(object sender, TextChangedEventArgs e)
    {
        OnFindText?.Invoke(((Entry)sender).Text);
    }

    private void OnClickOpenCardSet(object sender, EventArgs e)
    {
        OnGoCardSetPan?.Invoke();
        var cardSet = (CardSet)((Button)sender).BindingContext;
        CardSetPanVM.OnUseCardSet?.Invoke(cardSet);
    }
}