using Base.Models;
using Base.ViewModels;

namespace Base.Views;

public partial class AntenasPage : ContentPage
{
	AntenasViewModel vm;
	public AntenasPage()
	{
		vm = new AntenasViewModel();
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