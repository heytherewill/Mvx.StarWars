namespace StarWars.Core.Api
{
	using System.Threading.Tasks;
	using StarWars.Core.Models;
	using Refit;

	public interface IStarWarsApi
	{
		//[Get("/api/planets/")]
		//Task<Result<List<Planet>>> GetPlanets();

		[Get("/api/planets/{id}")]
		Task<Planet> GetPlanet(int id);
	}
}