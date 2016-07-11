namespace StarWars.Droid.Views
{
	using Extensions;
	using Android.Graphics;
	using Android.OS;
	using Android.Support.V4.Content;
	using Android.Support.V7.Widget;
	using Core.ViewModels;
	using MvvmCross.Droid.Support.V7.AppCompat;
	using Android.Views;
	using System.ComponentModel;

	public abstract class BaseView<TViewModel> : MvxAppCompatActivity<TViewModel>
		where TViewModel : BaseViewModel
	{
		private bool _isReturning;

		protected abstract int LayoutId { get; }

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(LayoutId);

			if (ViewModel == null) return;
			SetupToolbar();
		}

		protected override void OnResume()
		{
			if (!_isReturning)
			{
				_isReturning = true;
				OverridePendingTransition(Resource.Animation.SlideInFromRight, Resource.Animation.SlideOutFromRight);
			}
			else
			{
				OverridePendingTransition(Resource.Animation.SlideInFromLeft, Resource.Animation.SlideOutFromLeft);
			}

			base.OnResume();
		}
		
		private void SetupToolbar()
		{
			var toolbar = FindViewById<Toolbar>(Resource.Id.Toolbar);
			if (toolbar == null) return;

			SetSupportActionBar(toolbar);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetDisplayShowHomeEnabled(true);

			var color = Android.Resource.Color.White.ToColor(this);
			var arrow = ContextCompat.GetDrawable(this, Resource.Drawable.abc_ic_ab_back_mtrl_am_alpha).Mutate();
			arrow.SetColorFilter(color, PorterDuff.Mode.SrcIn);
			SupportActionBar.SetHomeAsUpIndicator(arrow);

			SupportActionBar.Title = ViewModel.Title;

			toolbar.SetTitleTextColor(color);
			toolbar.NavigationClick += OnNavigationClick;

			ViewModel.PropertyChanged += OnPropertyChanged;

			if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop) return;

			var window = Window;
			window.ClearFlags(WindowManagerFlags.TranslucentStatus);
			window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
			window.SetStatusBarColor(Resource.Color.PrimaryDark.ToColor(this));
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != nameof(ViewModel.Title)) return;

			SupportActionBar.Title = ViewModel.Title;
		}

		private void OnNavigationClick(object sender, Toolbar.NavigationClickEventArgs e)
			=> ViewModel.BackCommand.Execute();
	}
}