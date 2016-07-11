namespace StarWars.Droid.Controls
{
	using System;
	using Android.Content;
	using Android.Runtime;
	using Android.Support.V7.Widget;
	using Android.Util;
	using MvvmCross.Core.ViewModels;
	
	public class InfiniteScrollRecyclerView : HeaderFooterRecyclerView
	{
		private readonly InfiniteScrollListener _scrollListener;

		public InfiniteScrollRecyclerView(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
		}

		public InfiniteScrollRecyclerView(Context context, IAttributeSet attrs)
			: this(context, attrs, 0)
		{
		}

		public InfiniteScrollRecyclerView(Context context, IAttributeSet attrs, int defStyle)
			: this(context, attrs, defStyle, new HeaderFooterRecyclerAdapter())
		{
		}

		public InfiniteScrollRecyclerView(Context context, IAttributeSet attrs, int defStyle, HeaderFooterRecyclerAdapter adapter)
			: base(context, attrs, defStyle, adapter)
		{
			_scrollListener = new InfiniteScrollListener();
			LayoutManager = new LinearLayoutManager(context);
		}

		protected override void OnAttachedToWindow()
		{
			base.OnAttachedToWindow();
			AddOnScrollListener(_scrollListener);
		}

		protected override void OnDetachedFromWindow()
		{
			RemoveOnScrollListener(_scrollListener);
			base.OnDetachedFromWindow();
		}

		public int LastIndexedItem
		{
			get { return _scrollListener.LastIndexedItem; }
			set { _scrollListener.LastIndexedItem = value; }
		}

		public IMvxAsyncCommand FetchMoreItemsCommand
		{
			get { return _scrollListener.FetchMoreItemsCommand; }
			set { _scrollListener.FetchMoreItemsCommand = value; }
		}

		public new LayoutManager LayoutManager
		{
			get
			{
				return GetLayoutManager();
			}
			set
			{
				SetLayoutManager(value);
				_scrollListener.LayoutManager = value;
			}
		}

		private class InfiniteScrollListener : OnScrollListener
		{
			internal int LastIndexedItem { get; set; }

			internal LayoutManager LayoutManager { private get; set; }

			internal IMvxAsyncCommand FetchMoreItemsCommand { get; set; }

			public override async void OnScrolled(RecyclerView recyclerView, int dx, int dy)
			{
				if (dy <= 0) return;

				var shouldFetchMoreItems = false;

				var layoutManager = LayoutManager as LinearLayoutManager;
				if (layoutManager == null) return;

				var lastCompletelyVisibleItemPosition = layoutManager.FindLastCompletelyVisibleItemPosition();
				shouldFetchMoreItems = lastCompletelyVisibleItemPosition == LastIndexedItem - 1;
			
				if (!shouldFetchMoreItems) return;
				if (!FetchMoreItemsCommand?.CanExecute() ?? true) return;
				await FetchMoreItemsCommand?.ExecuteAsync();
			}
		}
	}
}