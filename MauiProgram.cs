using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TaskAppT2._Models;
using TaskAppT2._ViewModels.Study.FlashCards;
using TaskAppT2._ViewModels.Study.FlashCards.LearnCardSet;
using TaskAppT2._ViewModels.Study.Subjects;
using TaskAppT2._ViewModels.Timer;
using TaskAppT2._ViewModels.Writes;
using TaskAppT2.CustomUI;
using Xe.AcrylicView;

namespace TaskAppT2;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseAcrylicView()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("Inter.ttf", "Inter");
			})
            .ConfigureMauiHandlers(h =>
			{
#if ANDROID
                h.AddHandler<Shell, CustomShellRenderer>();
                h.AddHandler<EntryWithoutLine, EntryWithoutLineRenderer>();
                h.AddHandler<EntryWithGreenLine, EntryWithGreenLineRenderer>();
                h.AddHandler<EntryWithMauiUILine, EntryWithMauiUILineRenderer>();
                h.AddHandler<EditorWithGreenLine, EditorWithGreenLineRenderer>();
                h.AddHandler<EditorWithMauiUILine, EditorWithMauiUILineRenderer>();
#endif
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<LocalDataBase>();
        builder.Services.AddSingleton<MainPageFlashCardsPanVM>();
		builder.Services.AddSingleton<NewCathegoryPageVM>();
		builder.Services.AddSingleton<NewCardSetPanVM>();
		builder.Services.AddSingleton<CardSetPanVM>();
		builder.Services.AddSingleton<LearnCardSetVM>();
		builder.Services.AddSingleton<MainPageSubjectsPanVM>();
		builder.Services.AddSingleton<NewSubjectPanVM>();
		builder.Services.AddSingleton<TimerPageVM>();
		builder.Services.AddSingleton<WritesPageVM>();
		return builder.Build();
	}
}
