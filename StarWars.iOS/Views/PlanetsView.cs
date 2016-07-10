namespace StarWars.iOS.Views
{
	using Core.ViewModels;
	using Foundation;
	using MvvmCross.Binding.BindingContext;
	using MvvmCross.Binding.iOS.Views;

	[Register("PlanetsView")]
	public class PlanetsView : BaseView<PlanetsViewModel>
    {
        protected override void InitializeBindings()
        {
			var source = new MvxStandardTableViewSource(TableView, "TitleText Name");
			TableView.Source = source;

            var set = this.CreateBindingSet<PlanetsView, PlanetsViewModel>();
			set.Bind(source).To(vm => vm.Planets);
			set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.PlanetCommand);
            set.Apply();

			TableView.ReloadData();
        }
	}
}