namespace StarWars.Core
{
	using Api;
	using Refit;
	using Helpers;
	using ViewModels;
	using MvvmCross.Platform;
	using MvvmCross.Core.ViewModels;

    public class App : MvxApplication
    {
		public override void Initialize()
		{
			var swApi = RestService.For<IStarWarsApi>(ConstantsHelper.ApiUrl);
			Mvx.RegisterSingleton(swApi);
			
			RegisterAppStart<MainViewModel>();
		}
    }
}