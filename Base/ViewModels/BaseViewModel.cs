using Base.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Base.Models;
using Microsoft.Maui.ApplicationModel;

namespace Base.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		string amb = clsUriWs.Ambiente;
		string name = clsUriWs.Name;
		bool pressed =false;
		bool isOne = true;
		bool isBusy = false;
		bool isReady = true;
		bool showPopUp = false;
		string versionApp = AppInfo.VersionString;
		string title = string.Empty;
		string user = Preferences.Get("User", "Inicie Sesion");
		string company = clsUriWs.Company;
		bool logged = Preferences.Get("Logged", false);

		bool showPopErrorWs=false;
		string errorPopWsMsg = string.Empty;
		bool showErrorGeneral = false;
		string errorGeneralMsg = string.Empty;

		bool showPopExitoWs = false;
		string exitoPopWsMsg = string.Empty;
		bool showExitoGeneral = false;
		string exitoGeneralMsg = string.Empty;


		public Thread Version { get; set; }
		public Thread HideError { get; set; }


		public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

		public INavigationService Navigation => DependencyService.Get<INavigationService>();

		public string Company
		{
			get { return this.company; }
			set { SetProperty(ref this.company, value); }
		}
		public string User
		{
			get { return this.user; }
			set { SetProperty(ref this.user, value); }
		}

		public string MsgLientek
		{
			get => clsUriWs.CatalogoLientek;
		}
		public string VersionApp
		{
			get { return this.versionApp; }
		}

		public string Name
		{
			get { return this.name; }
		}

		public string Amb
		{
			get { return this.amb; }
		}

		public bool Pressed
		{
			get { return this.pressed; }
			set { SetProperty(ref this.pressed, value); }
		}

		public bool Logged
		{
			get { return this.logged; }
			set { SetProperty(ref this.logged, value); }
		}

		public bool IsOne
		{
			get { return this.isOne; }
			set { SetProperty(ref this.isOne, value); }
		}

		public bool IsBusy
		{
			get { return this.isBusy; }
			set { SetProperty(ref this.isBusy, value); }
		}

		public bool IsReady
		{
			get { return this.isReady; }
			set { SetProperty(ref this.isReady, value); }
		}

		public bool ShowPopUp
		{
			get { return this.showPopUp; }
			set { SetProperty(ref this.showPopUp, value); }
		}
		public string Title
		{
			get { return this.title; }
			set { SetProperty(ref this.title, value); }
		}
		public string ErrorGeneralMsg
		{
			get => this.errorGeneralMsg;
			set => SetProperty(ref this.errorGeneralMsg, value);
		}
		public bool ShowErrorGeneral
		{
			get => this.showErrorGeneral;
			set => SetProperty(ref this.showErrorGeneral, value);
		}

		public bool ShowPopErrorWs
		{
			get => this.showPopErrorWs;
			set => SetProperty(ref this.showPopErrorWs, value);
		}
		public string ErrorPopWsMsg
		{
			get => this.errorPopWsMsg;
			set => SetProperty(ref this.errorPopWsMsg, value);
		}

		public string ExitoGeneralMsg
		{
			get => this.exitoGeneralMsg;
			set => SetProperty(ref this.exitoGeneralMsg, value);
		}
		public bool ShowExitoGeneral
		{
			get => this.showExitoGeneral;
			set => SetProperty(ref this.showExitoGeneral, value);
		}

		public bool ShowPopExitoWs
		{
			get => this.showPopExitoWs;
			set => SetProperty(ref this.showPopExitoWs, value);
		}
		public string ExitoPopWsMsg
		{
			get => this.exitoPopWsMsg;
			set => SetProperty(ref this.exitoPopWsMsg, value);
		}

		public event PropertyChangedEventHandler PropertyChanged;


		public virtual Task InitializeAsync(object parameter)
		{
			return Task.CompletedTask;
		}

		protected bool SetProperty<T>(ref T backingStore, T value,
			[CallerMemberName] string propertyName ="",
			Action onChanged = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return false;

			backingStore = value;
			onChanged?.Invoke();
			OnPropertyChanged(propertyName);
			return true;
		}

		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			var changed = PropertyChanged;
			if (changed == null)
				return;

			changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public bool BlockButton()
		{
			if (!Pressed)
			{
				return true;
			}
			return false;
		}
		public void ChecarVersion()
		{
			Version= new Thread(new ThreadStart(GetVersionApp));
			Version.Start();
		}

		async void GetVersionApp()
		{
			try
			{
				var rq = new clsConsultas();
				var strrq = rq.VersionActual(DeviceInfo.Current.Platform.ToString(), amb);//corregir este metodo cuando el api este bien hecha

				if (strrq.Contains(clsUriWs.ErrorHttp))
				{
					strrq = strrq.Replace(clsUriWs.ErrorHttp, "");
					ShowErrorGeneral = true;
					ErrorGeneralMsg = strrq;
					HideError = new Thread(new ThreadStart(hide));
					HideError.Start();
					return;
				}else if (strrq.Contains(clsUriWs.ErrorHttpLientek))
				{
					strrq = strrq.Replace(clsUriWs.ErrorHttpLientek, "");
					ShowErrorGeneral = true;
					ErrorGeneralMsg = strrq;
					HideError = new Thread(new ThreadStart(hide));
					HideError.Start();
					return;
				}

				

				if (!strrq.Equals(AppInfo.VersionString))
				{
					if (DeviceInfo.Current.Platform.Equals(clsUriWs.Android))
					{
						await Launcher.OpenAsync(clsUriWs.StoreAppAndroid);
					}
					else if (DeviceInfo.Current.Platform.Equals(clsUriWs.iOS))
					{
						MainThread.BeginInvokeOnMainThread(() =>
						{
							Uri uri = new Uri(clsUriWs.StoreAppiOS);
							Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
						});
					}
				}
			}
			catch (Exception ex)
			{
				ShowErrorGeneral = true;
				ErrorGeneralMsg = ex.Message;
				HideError = new Thread(new ThreadStart(hide));
				HideError.Start();
			}

		}

		async void hide()
		{
			if (ErrorGeneralMsg.Length > 40)
			{
				await Task.Delay(7000);
			}
			else
			{
				await Task.Delay(5000);
			}

			ShowErrorGeneral = false;
			ErrorGeneralMsg = string.Empty;
		}
	}
}