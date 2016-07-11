namespace StarWars.Droid
{
	using Bindings;
	using Android.Content;
	using MvvmCross.Droid.Platform;
	using MvvmCross.Core.ViewModels;
	using MvvmCross.Platform.Platform;
	using MvvmCross.Binding.Bindings.Target.Construction;
	using Android.Support.V4.Widget;

	public class Setup : MvxAndroidSetup
    {
		public Setup(Context applicationContext)
			: base(applicationContext) { }

        protected override IMvxApplication CreateApp() => new Core.App();
       
        protected override IMvxTrace CreateDebugTrace() => new DebugTrace();

		protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
		{
			base.FillTargetFactories(registry);

			registry.RegisterCustomBindingFactory<SwipeRefreshLayout>("RefreshCommand", refreshLayout => new RefreshLayoutRefreshCommandTargetBinding(refreshLayout));
		}
    }
}