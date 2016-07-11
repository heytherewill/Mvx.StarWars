namespace StarWars.Core.ViewModels
{
	using Models;
	using System.Threading.Tasks;

	public class FilmsViewModel : BaseRefreshableViewModel<Film, FilmViewModel>
	{
		public FilmsViewModel()
			: base(Resources.Films) { }

		protected override Task<ResponseEntity<Film>> FetchMoreItems(int page) => Api.GetFilms(page);
	}
}