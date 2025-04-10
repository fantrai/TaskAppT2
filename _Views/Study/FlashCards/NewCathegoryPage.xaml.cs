using TaskAppT2._Models;
using TaskAppT2._ViewModels.Study.FlashCards;

namespace TaskAppT2._Views.Study.FlashCards;

public partial class NewCathegoryPage : ContentPage
{
	public NewCathegoryPage()
	{
		InitializeComponent();
        HandlerChanged += (s, e) =>
            { 
                BindingContext = Handler?.MauiContext?.Services.GetService<NewCathegoryPageVM>();
         };

        NewCathegoryPageVM.OnUseCathegory += (e) => { DelButt.IsEnabled = true; };
        NewCathegoryPageVM.OnResetCathegoryPan += () => { DelButt.IsEnabled = false; };
    }

    public static Action<CardSet, bool>? OnChangeSetCathegory { get; set; }

    protected override bool OnBackButtonPressed()
    {
        NewCathegoryPageVM.OnResetCathegoryPan?.Invoke();
        return base.OnBackButtonPressed();
    }

    private void OnCheckedSetCathegory(object sender, CheckedChangedEventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        OnChangeSetCathegory?.Invoke((CardSet)cb.BindingContext, cb.IsChecked);
    }
}