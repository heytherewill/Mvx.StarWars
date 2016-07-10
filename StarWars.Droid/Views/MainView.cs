namespace StarWars.Droid.Views
{
	using Android.App;
	
    [Activity(Theme = "@style/StarWarsTheme", Label = "View for FirstViewModel")]
    public class MainView : BaseView
    {
		protected override int LayoutId => Resource.Layout.MainView;
    }
}