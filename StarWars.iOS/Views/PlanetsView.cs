namespace StarWars.iOS.Views
{
	using Core.ViewModels;
	using Foundation;
	using Core.Models;

	[Register("PlanetsView")]
	public class PlanetsView : BaseRefreshableView<PlanetsViewModel, PlanetViewModel, Planet>
    {
	}
}