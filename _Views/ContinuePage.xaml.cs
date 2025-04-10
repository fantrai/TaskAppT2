using TaskAppT2._Servise;

namespace TaskAppT2._Views;

public partial class ContinuePage : ContentPage
{
    public ContinuePage()
    {
        InitializeComponent();
    }

    public static Action? ActionWithContinue { get; set; }

    static ContinuePage? pan;

    string Description { set { TextL.Text = value; } }
    string TitleText { set { TitleL.Text = value; } }
    UseImage ImageSourse { set { Img.Source = ReturnImage.ReturnStringPath(value); } }

    public static async Task OpenPan(string title, string text, UseImage img = UseImage.Trash)
    {
        pan ??= new ContinuePage();
        pan.TitleText = title;
        pan.Description = text;
        pan.ImageSourse = img;
        await Shell.Current.Navigation.PushModalAsync(pan);
    }

    private void OnClosePan(object? sender, EventArgs? e)
    {
        ActionWithContinue = null;
        Navigation.PopModalAsync(true);
    }

    private void OnContinue(object sender, EventArgs e)
    {
        ActionWithContinue?.Invoke();
        OnClosePan(null, null);
    }
}