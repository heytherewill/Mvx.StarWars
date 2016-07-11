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
		Task<Planet> GetPlanetById(int id);

		[Get("/api/vehicles/?page={page}")]
		Task<ResponseEntity<Vehicle>> GetVehicles(int page = 1);

		[Get("/api/vehicles/{id}")]
		Task<Vehicle> GetVehicleById(int id);

		[Get("/api/films/?page={page}")]
		Task<ResponseEntity<Film>> GetFilms(int page = 1);

		[Get("/api/films/{id}")]
		Task<Film> GetFilmById(int id);

		[Get("/api/people/?page={page}")]
		Task<ResponseEntity<Person>> GetPeople(int page = 1);

		[Get("/api/planets/{id}")]
		Task<Person> GetPersonById(int id);

		[Get("/api/starships/?page={page}")]
		Task<ResponseEntity<Starship>> GetStarships(int page = 1);

		[Get("/api/starships/{id}")]
		Task<Planet> GetStarshipById(int id);

		[Get("/api/species/?page={page}")]
		Task<ResponseEntity<Species>> GetSpecies(int page = 1);

		[Get("/api/species/{id}")]
		Task<Species> GetSpecieById(int id);
	}
}