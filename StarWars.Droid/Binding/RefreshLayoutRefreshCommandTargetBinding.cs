namespace StarWars.Droid.Bindings
{
	using System;
	using Android.Support.V4.Widget;
	using MvvmCross.Core.ViewModels;

	public class RefreshLayoutRefreshCommandTargetBinding : BaseTargetBinding<SwipeRefreshLayout, IMvxAsyncCommand>
	{
		private IMvxAsyncCommand _command;

		public RefreshLayoutRefreshCommandTargetBinding(SwipeRefreshLayout target)
			: base(target)
		{
			target.Refresh += OnRefresh;
		}

		protected override void TypedSetValue(IMvxAsyncCommand value) => _command = value;

		private async void OnRefresh(object sender, EventArgs e)
		{
			if (_command == null) return;
			if (!_command.CanExecute()) return;
			await _command.ExecuteAsync();
			View.Refreshing = false;
		}

		protected override void Dispose(bool isDisposing)
		{
			if (isDisposing)
			{
				var view = View;
				if (view != null)
				{
					view.Refresh -= OnRefresh;
				}
			}

			base.Dispose(isDisposing);
		}
	}
}