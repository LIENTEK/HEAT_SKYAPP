using Base.ViewModels;

namespace Base.Views;

public partial class TiempoRealPage : ContentPage
{
	TiempoRealViewModel vm;
	public TiempoRealPage()
	{
		vm = new TiempoRealViewModel();
		BindingContext = vm;
		InitializeComponent();
		
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