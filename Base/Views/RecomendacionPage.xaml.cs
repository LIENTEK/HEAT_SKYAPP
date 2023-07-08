using Base.ViewModels;
using DevExpress.Maui.DataGrid;

namespace Base.Views;

public partial class RecomendacionPage : ContentPage
{
	RecomendacionViewModel vm;
	public RecomendacionPage()
	{
		InitializeComponent();
		vm = new RecomendacionViewModel();
		BindingContext = vm;
	}

	async protected override void OnAppearing()
	{
		lkestablo.SelectedValue = 0;
		await Task.Delay(100);
		vm.OnAppearing();
		lkestablo.IsDropDownOpen = false;
		lkestablo.Unfocus();
		base.OnAppearing();
	}

}