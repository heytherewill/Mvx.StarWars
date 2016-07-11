namespace StarWars.Core.ViewModels
{
	using Models;
	using System.Threading.Tasks;

	public class SpeciesViewModel : BaseRefreshableViewModel<Species, SpeciesDetailViewModel>
	{
		public SpeciesViewModel()
			: base(Resources.Species) { }

		protected override Task<ResponseEntity<Species>> FetchMoreItems(int page) => Api.GetSpecies(page);
	}
}