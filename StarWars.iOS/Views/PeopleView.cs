namespace StarWars.iOS.Views
{
	using Core.ViewModels;
	using Foundation;
	using Core.Models;

	[Register("PeopleView")]
	public class PeopleView : BaseRefreshableView<PeopleViewModel, PersonViewModel, Person>
    {
	}
}