using Horus.Models;
using Horus.Services.LoginServices;
using Horus.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Horus.ViewModels
{
	public class LoginPageViewModel : ViewModelBase
	{
		private readonly INavigationService _navigationService;
		private readonly IPageDialogService _dialogService;
		private readonly ILoginService _loginService;
		private DelegateCommand _loginCommand;
		private DelegateCommand _showPasswordCommand;
		private DelegateCommand<string> _showAlertCommand;
		private string _user;
		private string _password;
		private bool _isLoading;
		private bool _isPassword;
		public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILoginService loginService)
			: base(navigationService)
		{
			_navigationService = navigationService;
			_dialogService = pageDialogService;
			_loginService = loginService;
			IsLoading = false;
			IsPassword = true;
		}
		public string User { get => _user; set { SetProperty(ref _user, value); } }
		public string Password { get => _password; set { SetProperty(ref _password, value); } }
		public bool IsLoading { get => _isLoading; set { SetProperty(ref _isLoading, value); } }
		public bool IsPassword { get => _isPassword; set { SetProperty(ref _isPassword, value); } }
		public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(ValidateUser));
		public DelegateCommand ShowPasswordCommand => _showPasswordCommand ?? (_showPasswordCommand = new DelegateCommand(ShowPassword));
		public DelegateCommand<string> ShowAlertCommand => _showAlertCommand ?? (_showAlertCommand = new DelegateCommand<string>(ShowAlert));

		private async void ValidateUser()
		{
			IsLoading = true;
			var response = await _loginService.Login<User>(User, Password);
			if (!response.IsSuccess)
			{
				await _dialogService.DisplayAlertAsync("Error", response.Message, "Aceptar");
				IsLoading = false;
				return;
			}
			IsLoading = false;
			User = string.Empty;
			Password = string.Empty;
			await SaveAuthToken(((User)response.Result).authorizationToken);
			await _navigationService.NavigateAsync(nameof(ChallengePage));
		}
		private async Task SaveAuthToken(string token)
		{
			if (!Application.Current.Properties.ContainsKey("token"))
			{
				Application.Current.Properties["token"] = token;
				await Application.Current.SavePropertiesAsync();
			}
		}
		private void ShowPassword()
		{
			IsPassword = !IsPassword;
		}
		private void ShowAlert(string message)
		{
			_dialogService.DisplayAlertAsync("", message, "Aceptar");
		}
		public override void OnNavigatedTo(INavigationParameters parameters)
		{
			base.OnNavigatedTo(parameters);
			IsPassword = true;
		}
	}
}
