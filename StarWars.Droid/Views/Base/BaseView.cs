namespace StarWars.Droid.Views
{
	using Android.OS;
	using MvvmCross.Droid.Support.V7.AppCompat;

	public abstract class BaseView : MvxAppCompatActivity
	{
		protected abstract int LayoutId { get; }

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(LayoutId);
		}
	}
}