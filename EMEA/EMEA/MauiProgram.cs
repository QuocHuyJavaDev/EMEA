﻿using UraniumUI;
using CommunityToolkit.Maui;
using Syncfusion.Maui.Core.Hosting;

namespace EMEA;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.ConfigureSyncfusionCore();
        builder
            .UseUraniumUI()
            .UseUraniumUIMaterial() 
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Inter-Black.ttf", "InterBlack");
                fonts.AddFont("Inter-Bold.ttf", "InterBold");
                fonts.AddFont("Inter-ExtraBold.ttf", "InterExtraBold");
                fonts.AddFont("Inter-ExtraLight.ttf", "InterExtraLight");
                fonts.AddFont("Inter-Light.ttf", "InterLight");
                fonts.AddFont("Inter-Medium.ttf", "InterMedium");
                fonts.AddFont("Inter-Regular.ttf", "InterRegular");
                fonts.AddFont("Inter-SemiBold.ttf", "InterSemiBold");
                fonts.AddFont("Inter-Thin.ttf", "InterThin");
                fonts.AddMaterialIconFonts();
                fonts.AddFontAwesomeIconFonts();
            }).UseMauiCommunityToolkit(options =>
            {
                options.SetShouldSuppressExceptionsInConverters(false);
                options.SetShouldSuppressExceptionsInBehaviors(false);
                options.SetShouldSuppressExceptionsInAnimations(false);
            }); 

        return builder.Build();
    }
}