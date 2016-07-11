namespace StarWars.Core.ViewModels
{
	using Models;
	using System.Threading.Tasks;

	public class PeopleViewModel : BaseRefreshableViewModel<Person, PersonViewModel>
	{
		public PeopleViewModel()
			: base(Resources.People) { }

		protected override Task<ResponseEntity<Person>> FetchMoreItems(int page) => Api.GetPeople(page);
	}
}