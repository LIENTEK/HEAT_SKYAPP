using Base.ViewModels;
using DevExpress.Maui.DataGrid;
using DevExpress.Maui.Scheduler.Internal;
using Plugin.Maui.Audio;

namespace Base.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		HomeViewModel vm;
		public HomePage()
		{
			InitializeComponent();
			vm = new HomeViewModel();
			BindingContext = vm;
		}

		async protected override void OnAppearing()
		{
			lkestablo.SelectedValue = 0;
			lkpropiedad.SelectedValue = 0;
			await Task.Delay(100);
			vm.OnAppearing();
			lkestablo.IsDropDownOpen = false;
			lkestablo.Unfocus();
			lkpropiedad.IsDropDownOpen = false;
			lkpropiedad.Unfocus();
			base.OnAppearing();
		}

		void DataGridView_CustomCellAppearance(object sender, CustomCellAppearanceEventArgs e)
		{
			if (e.RowHandle % 2 == 0)
				e.BackgroundColor = Color.FromArgb("#F7F7F7");
			e.FontColor = Color.FromArgb("#55575C");

			//if (e.FieldName == "Valor" )
			//{
			//	double value = (double)grid.GetCellValue(e.RowHandle, e.FieldName);
			//	if (value > 7)
			//		e.BackgroundColor = Color.FromArgb("#00AE00");
			//	else if (value < 4000000)
			//		e.BackgroundColor = Color.FromArgb("#FF5458");
			//}
		}

	}
}