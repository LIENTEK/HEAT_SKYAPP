namespace Base.Views;
using Base.ViewModels;

public partial class ResetearContraseñaPage : ContentPage
{
	ResetearContraseñaViewModel vm;
	public ResetearContraseñaPage()
	{
		InitializeComponent();
		vm = new ResetearContraseñaViewModel();
		BindingContext = vm;
	}
	protected override void OnAppearing()
	{
		vm.ChecarVersion();
		base.OnAppearing();
		animexBrincos();
	}
	void lientek(Object e, EventArgs s)
	{
		animexBrincos();
	}

	async void animexBrincos()
	{
		int a = 0;
		while (a < 4)
		{
			await btnLientek.TranslateTo(0, 10, 50);
			await btnLientek.TranslateTo(0, -10, 50);
			a++;
		}

	}

}