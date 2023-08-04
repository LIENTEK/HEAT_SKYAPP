using Base.ViewModels;

namespace Base.Views;

public partial class ReportePage : ContentPage
{
	ReporteViewModel vm;
	public ReportePage()
	{
		InitializeComponent();
		vm = new ReporteViewModel();
		BindingContext = vm;

	}

	async protected override void OnAppearing()
	{
		lkestablo.SelectedValue = 0;
		await Task.Delay(100);
		vm.OnAppearing();
		lkestablo.Unfocus();
		base.OnAppearing();
	}

}