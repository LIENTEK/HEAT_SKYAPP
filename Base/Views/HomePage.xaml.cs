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
			
			try
			{
				var X = vm.Items;
				double value = (double)grid.GetCellValue(e.RowHandle, "Valor1");
				if (value >= 39.5)
				{
					e.FontColor = Color.FromRgb(255, 199, 206);
				}
				if (value >= 39 && value < 39.5)
				{
					e.FontColor = Color.FromRgb(255, 235, 156);
				}

				//if (vm.SelPropiedad.Id.Equals("Temperatura °C"))
				//{
				//		double value = (double)grid.GetCellValue(e.RowHandle, e.FieldName);
				//		if (value >= 39.5)
				//		{
				//			e.FontColor = Color.FromRgb(255, 199, 206);
				//		}
				//		if (value >= 39 && value < 39.5)
				//		{
				//			e.FontColor = Color.FromRgb(255, 235, 156);
				//		}

				//}
				//else if (vm.SelPropiedad.Id.Equals("ITH"))
				//{
				//		double value = (double)grid.GetCellValue(e.RowHandle, e.FieldName);
				//		if (value >= 80)
				//		{
				//			e.FontColor = Color.FromRgb(255, 117, 117);
				//		}
				//		if (value >= 74 && value < 80)
				//		{
				//			e.FontColor = Color.FromRgb(255, 204, 102);
				//		}
				//		if (value >= 60 && value < 74)
				//		{
				//			e.FontColor = Color.FromRgb(255, 255, 153);
				//		}
				//}
				//else
				//{
				//	e.FontColor = Colors.Black;
				//}


			}
			catch(Exception ex)
			{
				e.FontColor = Colors.Black;
			}
		}

		public void DataGridView_CustomCellAppearance1(object sender, CustomCellAppearanceEventArgs e)
		{
			//if (e.RowHandle % 2 == 0)
			//	e.BackgroundColor = Color.FromArgb("#F7F7F7");
			//e.FontColor = Color.FromArgb("#55575C");

			//vm.SelPropiedad = new Models.clsPropiedades()
			//{
			//	Id = "Temperatura °C",
			//	Propiedad = "Temperatura °C"
			//};


			//if (vm.SelPropiedad.Id.Equals("Temperatura °C"))
			//{
			//	if (e.FieldName == "Valor1")
			//	{
			//		double value = (double)grid.GetCellValue(e.RowHandle, e.FieldName);
			//		if (value >= 39.5)
			//		{
			//			e.FontColor = Color.FromRgb(255, 199, 206);
			//		}
			//		if (value >= 39 && value < 39.5)
			//		{
			//			e.FontColor = Color.FromRgb(255, 235, 156);
			//		}
			//	}
			//}
			//else if (vm.SelPropiedad.Id.Equals("ITH"))
			//{
			//	if (e.FieldName == "Valor1")
			//	{
			//		double value = (double)grid.GetCellValue(e.RowHandle, e.FieldName);
			//		if (value >= 80)
			//		{
			//			e.FontColor = Color.FromRgb(255, 117, 117);
			//		}
			//		if (value >= 74 && value < 80)
			//		{
			//			e.FontColor = Color.FromRgb(255, 204, 102);
			//		}
			//		if (value >= 60 && value < 74)
			//		{
			//			e.FontColor = Color.FromRgb(255, 255, 153);
			//		}
			//	}
			//}
			//else
			//{
			//	if (e.FieldName == "Valor1")
			//	{
			//		e.FontColor = Colors.Black;
			//	}
			//}
		}
	}
}