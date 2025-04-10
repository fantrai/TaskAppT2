using TaskAppT2._Models;
using TaskAppT2._ViewModels.Study.Subjects;

namespace TaskAppT2._Views.Study.Subjects;

public partial class MainPageSubjectsPan : ContentView
{
	public MainPageSubjectsPan()
	{
		InitializeComponent();
        HandlerChanged += (s, e) => {
            BindingContext = Handler?.MauiContext?.Services.GetService<MainPageSubjectsPanVM>();
        };
        var tapGesture = new TapGestureRecognizer() { NumberOfTapsRequired = 1 };
        tapGesture.Tapped += (s, e) => { OnGoNewSubjectPan?.Invoke(); };
        AddNewSubject.GestureRecognizers.Add(tapGesture);
    }

    public static Action? OnGoNewSubjectPan { get; set; }
    public static Action<Subject>? OnDeleteSubject { get; set; }
    public static Action<string>? OnFindText { get; set; }

    private void OnChangeFind(object sender, TextChangedEventArgs e)
    {
        OnFindText?.Invoke(((Entry)sender).Text);
    }

    private void OnEditSubject(object sender, EventArgs e)
    {
        Subject sub = (Subject)((Button)sender).BindingContext;
        OnGoNewSubjectPan?.Invoke();
        NewSubjectPanVM.OnUseSubject?.Invoke(sub);
    }
}