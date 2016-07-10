namespace StarWars.iOS.Views
{
	using MvvmCross.iOS.Views;
	using Core.ViewModels;
	using UIKit;

	public abstract class BaseView<TViewModel> : MvxTableViewController<TViewModel>
		where TViewModel : BaseViewModel
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			if (ViewModel == null) return;

			var navigationBar = NavigationController.NavigationBar;

			navigationBar.BarStyle = UIBarStyle.Default;
			navigationBar.BarTintColor = UIColor.FromRGB(255, 152, 0);

			navigationBar.ClipsToBounds = false;
			navigationBar.TintColor = UIColor.White;

			navigationBar.SetBackgroundImage(new UIImage(), UIBarPosition.Any, UIBarMetrics.Default);
			navigationBar.ShadowImage = new UIImage();

			navigationBar.TitleTextAttributes = new UIStringAttributes {  ForegroundColor = UIColor.White };
			navigationBar.Translucent = false;

			Title = ViewModel.Title;
			InitializeBindings();
		}

		protected abstract void InitializeBindings();
	}
}