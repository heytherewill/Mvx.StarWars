namespace StarWars.Core.ViewModels
{
	using Models;
	using Extensions;
	using PropertyChanged;

	[ImplementPropertyChanged]
	public class PlanetViewModel : BaseViewModel
	{
		private int _index;

		public void Init(IndexParameters parameters)
			=> _index = parameters.Index;

		public override async void Start()
		{
			var apiResult = await Api.GetPlanetById(_index).WithBusyIndicator(this);
			if (!apiResult.Success) return;

			Planet = apiResult.Data;
			Title = Planet.Name;
		}

		public Planet Planet { get; set; }
	}
}