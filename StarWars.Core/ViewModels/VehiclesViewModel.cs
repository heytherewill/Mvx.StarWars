namespace StarWars.Core.ViewModels
{
	using Models;
	using System.Threading.Tasks;

	public class VehiclesViewModel : BaseRefreshableViewModel<Vehicle, VehicleViewModel>
	{
		public VehiclesViewModel()
			: base(Resources.Vehicles) { }

		protected override Task<ResponseEntity<Vehicle>> FetchMoreItems(int page) => Api.GetVehicles(page);
	}
}