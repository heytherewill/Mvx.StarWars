namespace StarWars.iOS.Views
{
	using Core.ViewModels;
	using Foundation;
	using Core.Models;

	[Register("VehiclesView")]
	public class VehiclesView : BaseRefreshableView<VehiclesViewModel, VehicleViewModel, Vehicle>
    {
	}
}