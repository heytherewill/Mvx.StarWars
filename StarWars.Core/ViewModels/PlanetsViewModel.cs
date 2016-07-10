namespace StarWars.Core.ViewModels
{
	using Models;
	using System.Collections.ObjectModel;
	using MvvmCross.Core.ViewModels;

	public class PlanetsViewModel : BaseViewModel
	{
		public PlanetsViewModel()
			: base(Resources.Planets)
		{
			PlanetCommand = new MvxCommand<Planet>(PlanetCommandExecute);
		}

		public override async void Start()
		{
			var result = await Api.GetPlanets();
			Planets.AddRange(result.Results);
		}

		public ObservableCollection<Planet> Planets { get; } = new ObservableCollection<Planet>();

		public IMvxCommand PlanetCommand { get; }

		private void PlanetCommandExecute(Planet planet)
			=> ShowViewModel<PlanetViewModel>(IndexParameters.FromUrl(planet.Url));
	}
}