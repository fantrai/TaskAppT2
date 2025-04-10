using TaskAppT2._ViewModels.Study.Subjects;

namespace TaskAppT2._Views.Study.Subjects;

public partial class NewSubjectPan : ContentView
{
	public NewSubjectPan()
	{
		InitializeComponent();
        HandlerChanged += (s, e) => {
            BindingContext = Handler?.MauiContext?.Services.GetService<NewSubjectPanVM>();
        };
    }
}