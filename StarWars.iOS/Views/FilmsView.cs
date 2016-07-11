namespace StarWars.iOS.Views
{
	using Core.ViewModels;
	using Foundation;
	using Core.Models;

	[Register("FilmsView")]
	public class FilmsView : BaseRefreshableView<FilmsViewModel, FilmViewModel, Film>
    {
	}
}