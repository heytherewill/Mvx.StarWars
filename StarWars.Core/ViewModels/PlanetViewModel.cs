namespace StarWars.Core.ViewModels
{
	using Models;
	using PropertyChanged;

	[ImplementPropertyChanged]
	public class PlanetViewModel : BaseViewModel
	{
		private int _index;

		public void Init(IndexParameters parameters)
			=> _index = parameters.Index;

		public override async void Start()
			=> Planet = await Api.GetPlanet(_index);
	
		public Planet Planet { get; set; }
	}
}