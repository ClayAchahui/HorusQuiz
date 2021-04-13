using Horus.Models;
using Horus.Services.ChallengesServices;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Horus.ViewModels
{
	public class ChallengePageViewModel : BindableBase
	{
		private readonly INavigationService _navigationService;
		private readonly IPageDialogService _dialogService;
		private readonly IChallengeService _challengeService;
		private ObservableCollection<Challenge> _challenges;
		private DelegateCommand _goBackCommand;
		private DelegateCommand<Challenge> _challengeTappedCommand;
		private int _totalCompleted;
		private bool _loadingData;

		public ChallengePageViewModel(INavigationService navigationService,IPageDialogService pageDialogService,IChallengeService challengeService)
		{
			_navigationService = navigationService;
			_dialogService = pageDialogService;
			_challengeService = challengeService;
			GetChallenges();

		}
		public int TotalCompleted { get => _totalCompleted; set { SetProperty(ref _totalCompleted,value); } }
		public bool LoadingData { get => _loadingData; set { SetProperty(ref _loadingData,value); } }
		public DelegateCommand<Challenge> ChallengeTappedCommand => _challengeTappedCommand ?? (_challengeTappedCommand  = new DelegateCommand<Challenge>(ShowItem));
		public ObservableCollection<Challenge> Challenges { get => _challenges; set => SetProperty(ref _challenges,value); }
		public DelegateCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new DelegateCommand(CloseSession));


		private async void ShowItem(Challenge obj)
		{
			await _dialogService.DisplayAlertAsync(obj.title,obj.description,"Aceptar");
		}
		private async void GetChallenges()
		{
			LoadingData = true;
			var response=await _challengeService.GetChallenges<List<Challenge>>();
			if (!response.IsSuccess)
			{
				await _dialogService.DisplayAlertAsync("Error",response.Message,"Aceptar");
				LoadingData = false;
				return;
			}
			LoadingData = false;
			Challenges = new ObservableCollection<Challenge>((List<Challenge>)response.Result);
			TotalCompleted = Challenges.Count(a=>a.currentPoints==a.totalPoints);
		}
		private async void CloseSession()
		{
			var response = await _dialogService.DisplayAlertAsync("Sesión", "¿Desea cerrar su sesión?", "Si", "No");
			if (!response) return;
			Application.Current.Properties.Clear();
			await _navigationService.GoBackAsync();
		}
	}
}
