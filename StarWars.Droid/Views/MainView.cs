namespace StarWars.Droid.Views
{
	using Android.App;
	using Core.ViewModels;
	
    [Activity(Theme = "@style/StarWarsTheme", Label = "View for FirstViewModel")]
    public class MainView : BaseView<MainViewModel>
    {
		protected override int LayoutId => Resource.Layout.MainView;
    }
}