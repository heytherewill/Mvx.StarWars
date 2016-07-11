namespace StarWars.Core.ViewModels
{
	using Models;
	using System.Threading.Tasks;

	public class PlanetsViewModel : BaseRefreshableViewModel<Planet, PlanetViewModel>
	{
		public PlanetsViewModel()
			: base(Resources.Planets) { }

		protected override Task<ResponseEntity<Planet>> FetchMoreItems(int page) => Api.GetPlanets(page);
	}
}