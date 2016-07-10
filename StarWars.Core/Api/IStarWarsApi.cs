namespace StarWars.Core.Api
{
	using System.Threading.Tasks;
	using StarWars.Core.Models;
	using Refit;

	public interface IStarWarsApi
	{
		[Get("/api/planets/?page={page}")]
		Task<ResponseEntity<Planet>> GetPlanets(int page = 1);

		[Get("/api/planets/{id}")]
		Task<Planet> GetPlanet(int id);
	}
}