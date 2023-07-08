using Base.ViewModels;

namespace Base.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class EstresCaloricoPage : ContentPage
{
	EstresCaloricoViewModel vm;
	public EstresCaloricoPage()
	{
		InitializeComponent();
		vm = new EstresCaloricoViewModel();
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