namespace StarWars.UnitTests.ViewModels
{
	using Core.ViewModels;
	using Core.Models;
	using Moq;
	using System.Collections.Generic;
	using NUnit.Framework;
	using FluentAssertions;
	using System.Linq;

	[TestFixture]
	public class PlanetsViewModelTests : BaseViewModelTests<PlanetsViewModel>
	{
		private static readonly List<Planet> Planets = new List<Planet>
		{
			new Planet { Name = "Tatooine", Url = "http://swapi.co/api/planets/2" },
			new Planet { Name = "Alderaan", Url = "http://swapi.co/api/planets/3" }
		};

		private static readonly ResponseEntity<Planet> FirstResponse = new ResponseEntity<Planet>
		{
			Next = "http://swapi.co/api/planets/?page=2",
			Results = Planets
		};

		private static readonly ResponseEntity<Planet> LastResponse = new ResponseEntity<Planet>
		{
			Next = null,
			Results = Planets
		};

		[Test]
		public void TheStartMethodInitializesThePlanetList()
		{
			SetupApiAndStart();

			ViewModel.Planets.Count.Should().Be(2);
		}

		[Test]
		public void ThePlanetCommandShowsThePlanetViewModel()
		{
			SetupApiAndStart();

			var planet = Planets.First();
			ViewModel.PlanetCommand.Execute(planet);
			AssertViewModelWasShown<PlanetViewModel>();
		}

		[Test]
		public void ThePlanetViewModelIsShownWithProperParameters()
		{
			SetupApiAndStart();

			var planet = Planets.First();
			ViewModel.PlanetCommand.Execute(planet);
			MockDispatcher.Requests.First().ParameterValues.First().Value.Should().Be("2");
		}

		private void SetupApiAndStart()
		{
			Api.Setup(a => a.GetPlanets(1)).ReturnsAsync(FirstResponse);
			ViewModel.Start();
		}
	}
}