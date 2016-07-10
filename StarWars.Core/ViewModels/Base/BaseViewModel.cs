namespace StarWars.Core.ViewModels
{
	using MvvmCross.Core.ViewModels;
	using PropertyChanged;

	[ImplementPropertyChanged]
	public class BaseViewModel : MvxViewModel
	{
		public bool IsBusy { get; set; }
	}
}

