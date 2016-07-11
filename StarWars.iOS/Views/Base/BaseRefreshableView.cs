namespace StarWars.iOS.Views
{
	using System;
	using Core.Models;
	using Core.ViewModels;
	using MvvmCross.Binding.BindingContext;
	using MvvmCross.Binding.iOS.Views;
	using MvvmCross.Core.ViewModels;
	using UIKit;

	public class BaseRefreshableView<TViewModel, TViewModelType, TModelType> : BaseView<TViewModel>
		where TViewModel : BaseRefreshableViewModel<TModelType, TViewModelType>
		where TViewModelType : BaseViewModel
		where TModelType : BaseModel
	{
		private const int ReloadDistance = 10;

		protected override void InitializeBindings()
		{
			var refreshControl = new UIRefreshControl();
			refreshControl.AddTarget(OnRefresh, UIControlEvent.ValueChanged);
			RefreshControl = refreshControl;

			var source = new MvxInfiniteTableViewSource(TableView, "TitleText Name")
			{
				FetchMoreItemsCommand = ViewModel.FetchMoreItemsCommand,
			};

			TableView.Source = source;

			var set = this.CreateBindingSet<BaseRefreshableView<TViewModel, TViewModelType, TModelType>, BaseRefreshableViewModel<TModelType, TViewModelType>> ();
			set.Bind(source).To(vm => vm.Items);
			set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.ItemClickCommand);
			set.Apply();

			TableView.ReloadData();
		}

		private async void OnRefresh(object sender, EventArgs e)
		{
			await ViewModel.RefreshCommand.ExecuteAsync();
			var refreshControl = sender as UIRefreshControl;
			refreshControl?.EndRefreshing();
		}

		private class MvxInfiniteTableViewSource : MvxStandardTableViewSource
		{
			private bool _isFetchingMore;

			private readonly UIActivityIndicatorView _footer = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);

			public IMvxAsyncCommand FetchMoreItemsCommand { get; set; }

			public MvxInfiniteTableViewSource(UITableView tableView, string bindingText)
				: base(tableView, bindingText)
			{
				_footer.StartAnimating();
			}

			public override async void Scrolled(UIScrollView scrollView)
			{
				var offset = scrollView.ContentOffset;
				var bounds = scrollView.Bounds;
				var size = scrollView.ContentSize;
				var inset = scrollView.ContentInset;

				var y = offset.Y + bounds.Size.Height - inset.Bottom;
				var h = size.Height;

				if (y <= h + ReloadDistance) return;

				if (_isFetchingMore) return;

				_isFetchingMore = true;
				_footer.Hidden = false;
				await FetchMoreItemsCommand.ExecuteAsync();
				_isFetchingMore = false;
				_footer.Hidden = true;
			}

			public override UIView GetViewForFooter(UITableView tableView, nint section) => _footer;

			public override nfloat EstimatedHeightForFooter(UITableView tableView, nint section)
				=> 40.0f;
		}
	}
}