namespace StarWars.Droid
{
	using Android.Content;
	using MvvmCross.Droid.Platform;
	using MvvmCross.Core.ViewModels;
	using MvvmCross.Platform.Platform;
	
    public class Setup : MvxAndroidSetup
    {
		public Setup(Context applicationContext)
			: base(applicationContext) { }

        protected override IMvxApplication CreateApp() => new Core.App();
       
        protected override IMvxTrace CreateDebugTrace() => new DebugTrace();
    }
}