namespace StarWars.Droid.Views
{
	using Android.App;
	using Core.ViewModels;
	
	[Activity(Theme = "@style/StarWarsTheme")]
    public class FilmsView : BaseRefreshableView<FilmsViewModel>
    {
		protected override int LayoutId => Resource.Layout.FilmsView;
    }
}	