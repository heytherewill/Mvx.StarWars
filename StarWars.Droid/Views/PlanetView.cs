namespace StarWars.Droid.Views
{
	using Android.App;
	using Core.ViewModels;
	
	[Activity(Theme = "@style/StarWarsTheme")]
    public class PlanetView : BaseView<PlanetViewModel>
    {
		protected override int LayoutId => Resource.Layout.PlanetView;
    }
}