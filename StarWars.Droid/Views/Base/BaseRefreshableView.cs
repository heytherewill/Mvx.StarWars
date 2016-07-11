namespace StarWars.Droid.Views
{
	using Core.ViewModels;

	public class BaseRefreshableView<TViewModel> : BaseView<TViewModel>
		where TViewModel : BaseViewModel
	{
		protected override int LayoutId => Resource.Layout.BaseView;
	}
}

