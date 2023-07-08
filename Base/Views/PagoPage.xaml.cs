using Base.ViewModels;

namespace Base.Views;

public partial class PagoPage : ContentPage
{
	PagoViewModel vm;
	public PagoPage()
	{
		InitializeComponent();
		vm = new PagoViewModel();
		BindingContext = vm;
	}

	protected override void OnAppearing()
	{
		vm.ChecarVersion();
		base.OnAppearing();
	}
}