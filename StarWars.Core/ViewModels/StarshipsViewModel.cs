namespace StarWars.Core.ViewModels
{
	using Models;
	using System.Threading.Tasks;

	public class StarshipsViewModel : BaseRefreshableViewModel<Starship, StarshipViewModel>
	{
		public StarshipsViewModel()
			: base(Resources.Starships) { }

		protected override Task<ResponseEntity<Starship>> FetchMoreItems(int page) => Api.GetStarships(page);
	}
}