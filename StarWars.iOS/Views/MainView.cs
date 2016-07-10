namespace StarWars.iOS.Views
{
	using Core.ViewModels;
	using Foundation;
	using MvvmCross.Binding.BindingContext;
	using MvvmCross.Binding.iOS.Views;

	[Register("MainView")]
	public class MainView : BaseView
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			var source = new MvxStandardTableViewSource(TableView, "TitleText Name");
			TableView.Source = source;

            var set = this.CreateBindingSet<MainView, MainViewModel>();
			set.Bind(source).To(vm => vm.Categories);
			set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.CategoryCommand);
            set.Apply();

			TableView.ReloadData();
        }
	}
}
