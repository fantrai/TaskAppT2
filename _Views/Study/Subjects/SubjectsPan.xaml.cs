using TaskAppT2._Servise;
using TaskAppT2._ViewModels.Study.Subjects;

namespace TaskAppT2._Views.Study.Subjects;

public partial class SubjectsPan : ContentView
{
	public SubjectsPan()
	{
		InitializeComponent();
        OnUseNewReturnButtonImage += ChangeReturnButtonImage;
        MainPageSubjectsPan.OnGoNewSubjectPan += Go<NewSubjectPan>;
        NewSubjectPanVM.OnGoSelectTeacher += GoModalPage<SelectTeacherPage>;
        OnStartGoBack += GoBack;
        Go<MainPageSubjectsPan>();
    }

    ~SubjectsPan()
    {
        OnUseNewReturnButtonImage -= ChangeReturnButtonImage;
        MainPageSubjectsPan.OnGoNewSubjectPan -= Go<NewSubjectPan>;
        NewSubjectPanVM.OnGoSelectTeacher -= GoModalPage<SelectTeacherPage>;
        OnStartGoBack -= GoBack;
    }

    public static Action? OnStartGoBack { get; set; }
    public static Action<UseImage>? OnUseNewReturnButtonImage { get; set; }

    static readonly Dictionary<string, ContentView> pages = [];
    static readonly Dictionary<string, Page> modalPages = [];
    static readonly Stack<ContentView> navStack = [];

    void ChangeReturnButtonImage(UseImage image)
    {
        ReturnButt.Source = ReturnImage.ReturnStringPath(image);
    }

    void ReturnButtonUpdate()
    {
        ReturnButt.IsVisible = navStack.Count > 1;
    }

    public void Go<T>() where T : ContentView, new()
    {
        ContentView page;
        if (pages.TryGetValue(typeof(T).ToString(), out ContentView? value))
        {
            page = value;
        }
        else
        {
            page = new T();
            pages.Add(typeof(T).ToString(), page);
            PanViews.Add(page);
        }

        if (navStack.Count > 0)
        {
            if (navStack.Peek() == page) return;
            navStack.Peek().IsVisible = false;
        }

        navStack.Push(page);
        page.IsVisible = true;
        ReturnButtonUpdate();
    }
    public async Task GoModalPage<T>() where T : Page, new()
    {
        Page page;
        if (modalPages.TryGetValue(typeof(T).ToString(), out Page? value))
        {
            page = value;
        }
        else
        {
            page = new T();
            modalPages.Add(typeof(T).ToString(), page);
        }

        await Navigation.PushModalAsync(page);
    }

    public void GoBack()
    {
        ChangeReturnButtonImage(UseImage.ArrayLeft);
        if (Navigation.ModalStack.Count > 0)
        {
            Navigation.PopModalAsync();
            if (Navigation.ModalStack.Count == 0) navStack.Peek().IsVisible = true;
        }
        else
        {
            navStack.Pop().IsVisible = false;
            navStack.Peek().IsVisible = true;
            ReturnButtonUpdate();
        }
    }

    private void OnGoBack(object sender, EventArgs e)
    {
        OnStartGoBack?.Invoke();
    }
}