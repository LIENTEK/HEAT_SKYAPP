using Base.ViewModels;
using CommunityToolkit.Maui;
using DevExpress.Maui;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Plugin.LocalNotification;
using Syncfusion.Maui.Core.Hosting;



namespace Base
{
	
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				//.UseMauiCommunityToolkitMediaElement()
				.ConfigureSyncfusionCore()
				//.UseLocalNotification()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("roboto-regular.ttf", "Roboto");
					fonts.AddFont("roboto-medium.ttf", "Roboto-Medium");
					fonts.AddFont("roboto-bold.ttf", "Roboto-Bold");
					fonts.AddFont("univia-pro-regular.ttf", "Univia-Pro");
					fonts.AddFont("univia-pro-medium.ttf", "Univia-Pro Medium");
				})
				.ConfigureEffects((effects) =>
				{
					effects.AddCompatibilityEffects(AppDomain.CurrentDomain.GetAssemblies());
				})
				.UseDevExpress(useLocalization: true);
				DevExpress.Maui.Charts.Initializer.Init();
				DevExpress.Maui.CollectionView.Initializer.Init();
				DevExpress.Maui.Controls.Initializer.Init();
				DevExpress.Maui.Editors.Initializer.Init();
				DevExpress.Maui.DataGrid.Initializer.Init();
				DevExpress.Maui.Scheduler.Initializer.Init();



			return builder.Build();
		}


	}
}