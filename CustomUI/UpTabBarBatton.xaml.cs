namespace TaskAppT2.CustomUI;

public partial class UpTabBarButton : ContentView
{
    public UpTabBarButton()
    {
        InitializeComponent();
        OnNewTab += SetNoActiveState;
        SetNoActiveState(groupName, NumberReturnOnClick);
    }

    public static Action<string, int>? OnNewTab { get; set; }

    const string ACTIVE_STATE_NAME = "Active";
    const string NOACTIVE_STATE_NAME = "NoActive";

    string groupName = "";
    int numberReturnOnClick = 0;
    bool selected = false;

    public string GroupName
    {
        get { return groupName; }
        set { groupName = value; }
    }
    public int NumberReturnOnClick
    {
        get { return numberReturnOnClick; }
        set { numberReturnOnClick = value; }
    }
    public string Text
    {
        get { return butt.Text; }
        set { butt.Text = value; }
    }
    public bool Selected
    {
        get { return selected; }
        set
        {
            selected = value;
            if (selected) NewTabSelected();
        }
    }

    private void SetNoActiveState(string groupName, int numb)
    {
        if (groupName == this.groupName)
        {
            VisualStateManager.GoToState(this, NOACTIVE_STATE_NAME);
            VisualStateManager.GoToState(this, NOACTIVE_STATE_NAME);
            butt.FontAttributes = FontAttributes.None;
        }
    }

    private void SetActiveState(string groupName)
    {
        if (groupName == this.groupName)
        {
            VisualStateManager.GoToState(this, ACTIVE_STATE_NAME);
            VisualStateManager.GoToState(this, ACTIVE_STATE_NAME);
            butt.FontAttributes = FontAttributes.Bold;
        }
    }

    private void NewTabSelected()
    {
        OnNewTab?.Invoke(groupName, numberReturnOnClick);
        SetActiveState(groupName);
    }

    private void OnClick(object sender, EventArgs e)
    {
        NewTabSelected();
    }
}