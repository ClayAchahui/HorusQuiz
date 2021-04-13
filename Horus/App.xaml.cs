using Horus.Services.ChallengesServices;
using Horus.Services.LoginServices;
using Horus.ViewModels;
using Horus.Views;
using Prism;
using Prism.Ioc;
using System;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace Horus
{
	public partial class App
	{
		public App(IPlatformInitializer initializer)
			: base(initializer)
		{
		}

		protected override async void OnInitialized()
		{
			InitializeComponent();

			await NavigationService.NavigateAsync("NavigationPage/LoginPage");
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			RegisterScreens(containerRegistry);
			RegisterServices(containerRegistry);
			containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
		}

		private void RegisterServices(IContainerRegistry containerRegistry)
		{
			containerRegistry.Register(typeof(ILoginService),typeof(LoginService));
			containerRegistry.Register(typeof(IChallengeService),typeof(ChallengeService));
		}

		private void RegisterScreens(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<NavigationPage>();
			containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
			containerRegistry.RegisterForNavigation<ChallengePage, ChallengePageViewModel>();
		}
	}
}
