namespace StarWars.Core.ViewModels
{
	using Api;
	using MvvmCross.Core.ViewModels;
	using MvvmCross.Platform;
	using PropertyChanged;

	[ImplementPropertyChanged]
	public class BaseViewModel : MvxViewModel
	{
		private IStarWarsApi _api;
		protected IStarWarsApi Api => _api ?? (_api = Mvx.Resolve<IStarWarsApi>());

		public BaseViewModel()
			: this("")
		{
		}

		public BaseViewModel(string title)
		{
			Title = title;

			BackCommand = new MvxCommand(BackCommandExecute);
		}

		public bool IsBusy { get; set; }

		public string Title { get; set; }

		public IMvxCommand BackCommand { get; }

		private void BackCommandExecute()
		{
			if (!ShouldGoBack()) return;
			Close(this);
		}

		protected virtual bool ShouldGoBack() => true;
	}
}