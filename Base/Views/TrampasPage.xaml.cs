using Base.ViewModels;

namespace Base.Views;

public partial class TrampasPage : ContentPage
{
	TrampasViewModel vm;
	public TrampasPage()
	{
		vm = new TrampasViewModel();
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