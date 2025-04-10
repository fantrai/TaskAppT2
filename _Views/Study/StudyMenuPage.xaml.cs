using Microsoft.Maui.Controls;

namespace TaskAppT2.MainMenu.Study;

public partial class StudyMenuPage : ContentPage
{
	public StudyMenuPage()
	{
        InitializeComponent();
        SwitchTab(GROUP_NAME, 0);
        CustomUI.UpTabBarButton.OnNewTab += SwitchTab;
    }

    static readonly string GROUP_NAME = "StudyMainMenuPage";

    void SwitchTab(string group, int num)
    {
        if (group == GROUP_NAME)
        {
            var items = Tabs.Children.Cast<ContentView>().ToList();

            for (int i = 0; i < items.Count; i++)
            {
                if (i == num) items[i].IsVisible = true;
                else items[i].IsVisible = false;
            }
        }
    }
}