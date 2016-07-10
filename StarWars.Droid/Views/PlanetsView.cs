namespace StarWars.Droid.Views
{
	using Android.App;
	using Core.ViewModels;
	
	[Activity(Theme = "@style/StarWarsTheme")]
    public class PlanetsView : BaseView<PlanetsViewModel>
    {
		protected override int LayoutId => Resource.Layout.PlanetsView;
    }
}