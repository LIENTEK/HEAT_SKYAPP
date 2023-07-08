namespace Base.Views;
using Base.ViewModels;

public partial class NotificacionesPage : ContentPage
{
	NotificacionesViewModel vm;
	public NotificacionesPage()
	{
		InitializeComponent();
		vm =  new NotificacionesViewModel();
		BindingContext = vm;
	}
}