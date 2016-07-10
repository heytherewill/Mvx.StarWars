namespace StarWars.Core.ViewModels
{
	using MvvmCross.Core.ViewModels;
	using PropertyChanged;

	[ImplementPropertyChanged]
	public class BaseViewModel : MvxViewModel
	{
		public BaseViewModel(string title = "")
		{
			Title = title;
		}

		public bool IsBusy { get; set; }

		public string Title { get; set; }
	}
}

