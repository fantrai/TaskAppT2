using TaskAppT2._ViewModels.Study.Subjects;

namespace TaskAppT2._Views.Study.Subjects;

public partial class SelectTeacherPage : ContentPage
{
	public SelectTeacherPage()
	{
		InitializeComponent();
        HandlerChanged += (s, e) => {
            BindingContext = Handler?.MauiContext?.Services.GetService<SelectTeacherPageVM>();
        };
    }
}