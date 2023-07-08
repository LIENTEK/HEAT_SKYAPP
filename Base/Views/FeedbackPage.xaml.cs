using Base.ViewModels;

namespace Base.Views;

public partial class FeedbackPage : ContentPage
{
	FeedbackViewModel vm;
	public FeedbackPage()
	{
		InitializeComponent();
		vm = new FeedbackViewModel();
		BindingContext=vm;
	}
	protected override void OnAppearing()
	{
		vm.ChecarVersion();
		base.OnAppearing();
	}

}