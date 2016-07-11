namespace StarWars.iOS.Views
{
	using Core.ViewModels;
	using Foundation;
	using Core.Models;

	[Register("SpeciesView")]
	public class SpeciesView : BaseRefreshableView<SpeciesViewModel, SpeciesDetailViewModel, Species>
    {
	}
}