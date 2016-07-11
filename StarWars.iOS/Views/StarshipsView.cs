namespace StarWars.iOS.Views
{
	using Core.ViewModels;
	using Foundation;
	using Core.Models;

	[Register("StarshipsView")]
	public class StarshipsView : BaseRefreshableView<StarshipsViewModel, StarshipViewModel, Starship>
    {
	}
}