namespace StarWars.Droid.Controls
{
	using System;
	using System.Collections;
	using System.Collections.Specialized;
	using System.Windows.Input;
	using Android.Content;
	using Android.Runtime;
	using Android.Support.V7.Widget;
	using Android.Util;
	using Android.Views;
	using MvvmCross.Binding;
	using MvvmCross.Binding.Attributes;
	using MvvmCross.Binding.Droid.BindingContext;
	using MvvmCross.Binding.ExtensionMethods;
	using MvvmCross.Core.ViewModels;
	using MvvmCross.Droid.Support.V7.RecyclerView;
	using MvvmCross.Platform;
	using MvvmCross.Platform.Exceptions;
	using MvvmCross.Platform.Platform;
	using MvvmCross.Platform.WeakSubscription;
	using Object = Java.Lang.Object;
	using Resource = Resource;

	public class HeaderFooterRecyclerView : RecyclerView
	{
		protected HeaderFooterRecyclerView(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer) { }

		public HeaderFooterRecyclerView(Context context, IAttributeSet attrs)
			: this(context, attrs, 0, new HeaderFooterRecyclerAdapter()) { }

		public HeaderFooterRecyclerView(Context context, IAttributeSet attrs, int defStyle)
			: this(context, attrs, defStyle, new HeaderFooterRecyclerAdapter()) { }

		public HeaderFooterRecyclerView(Context context, IAttributeSet attrs, int defStyle, HeaderFooterRecyclerAdapter adapter)
			: base(context, attrs, defStyle)
		{
			SetLayoutManager(new LinearLayoutManager(context));

			Adapter = adapter;

			var styledAttributes = Context.ObtainStyledAttributes(attrs, Resource.Styleable.HeaderRecyclerView);
			var count = styledAttributes.IndexCount;

			for (var i = 0; i < count; ++i)
			{
				var attr = styledAttributes.GetIndex(i);
				switch (attr)
				{
					case Resource.Styleable.HeaderRecyclerView_ItemTemplate:
						adapter.ItemTemplateId = styledAttributes.GetResourceId(attr, 0);
						break;
					case Resource.Styleable.HeaderRecyclerView_HeaderTemplate:
						adapter.HeaderTemplateId = styledAttributes.GetResourceId(attr, 0);
						break;
					case Resource.Styleable.HeaderRecyclerView_FooterTemplate:
						adapter.FooterTemplateId = styledAttributes.GetResourceId(attr, 0);
						break;
				}
			}

			styledAttributes.Recycle();
		}

		public new HeaderFooterRecyclerAdapter Adapter
		{
			get { return GetAdapter() as HeaderFooterRecyclerAdapter; }
			set
			{
				var existing = Adapter;

				if (existing == value)
					return;

				// Support lib doesn't seem to have anything similar to IListAdapter yet
				// hence cast to Adapter.
				if (value != null && existing != null)
				{
					value.ItemsSource = existing.ItemsSource;
					value.ItemClick = existing.ItemClick;
					value.ItemLongClick = existing.ItemLongClick;

					SwapAdapter(value, false);
				}
				else
				{
					SetAdapter(value);
				}

				if (existing != null)
				{
					existing.ItemsSource = null;
				}
			}
		}

		public IMvxViewModel ViewModel
		{
			get
			{
				return Adapter.ViewModel;
			}
			set
			{
				Adapter.ViewModel = value;
			}
		}

		[MvxSetToNullAfterBinding]
		public IEnumerable ItemsSource
		{
			get { return Adapter.ItemsSource; }
			set { Adapter.ItemsSource = value; }
		}

		public ICommand ItemClick
		{
			get { return Adapter.ItemClick; }
			set { Adapter.ItemClick = value; }
		}

		public ICommand ItemLongClick
		{
			get { return Adapter.ItemLongClick; }
			set { Adapter.ItemLongClick = value; }
		}

		public ICommand HeaderItemClick
		{
			get { return Adapter.HeaderItemClick; }
			set { Adapter.HeaderItemClick = value; }
		}

		public ICommand HeaderItemLongClick
		{
			get { return Adapter.HeaderItemLongClick; }
			set { Adapter.HeaderItemLongClick = value; }
		}

		public ICommand FooterItemClick
		{
			get { return Adapter.FooterItemClick; }
			set { Adapter.FooterItemClick = value; }
		}

		public ICommand FooterItemLongClick
		{
			get { return Adapter.FooterItemLongClick; }
			set { Adapter.FooterItemLongClick = value; }
		}
	}

	public enum ItemListType
	{
		Header,
		Footer,
		Item
	}

	public class HeaderFooterRecyclerAdapter : RecyclerView.Adapter
	{
		public event EventHandler DataSetChanged;

		private readonly IMvxAndroidBindingContext _bindingContext;

		private ICommand _itemClick, _itemLongClick, _headerItemClick, _headerItemLongClick, _footerItemClick, _footerItemLongClick;
		private IEnumerable _itemsSource;
		private IDisposable _subscription;

		protected IMvxAndroidBindingContext BindingContext => _bindingContext;

		public HeaderFooterRecyclerAdapter() : this(MvxAndroidBindingContextHelpers.Current()) { }
		public HeaderFooterRecyclerAdapter(IMvxAndroidBindingContext bindingContext)
		{
			_bindingContext = bindingContext;
		}

		public bool ReloadOnAllItemsSourceSets { get; set; }

		public ICommand ItemClick
		{
			get { return _itemClick; }
			set
			{
				if (ReferenceEquals(_itemClick, value)) return;

				if (_itemClick != null)
				{
					MvxTrace.Warning("Changing ItemClick may cause inconsistencies where some items still call the old command.");
				}

				_itemClick = value;
			}
		}

		public ICommand ItemLongClick
		{
			get { return _itemLongClick; }
			set
			{
				if (ReferenceEquals(_itemLongClick, value)) return;

				if (_itemLongClick != null)
				{
					MvxTrace.Warning("Changing ItemLongClick may cause inconsistencies where some items still call the old command.");
				}

				_itemLongClick = value;
			}
		}

		public ICommand HeaderItemClick
		{
			get { return _headerItemClick; }
			set
			{
				if (ReferenceEquals(_headerItemClick, value)) return;

				if (_headerItemClick != null)
				{
					MvxTrace.Warning("Changing HeaderItemClick may cause inconsistencies where some items still call the old command.");
				}

				_headerItemClick = value;
			}
		}

		public ICommand HeaderItemLongClick
		{
			get { return _headerItemLongClick; }
			set
			{
				if (ReferenceEquals(_headerItemLongClick, value)) return;

				if (_headerItemLongClick != null)
				{
					MvxTrace.Warning("Changing HeaderItemLongClick may cause inconsistencies where some items still call the old command.");
				}

				_headerItemLongClick = value;
			}
		}

		public ICommand FooterItemClick
		{
			get { return _footerItemClick; }
			set
			{
				if (ReferenceEquals(_footerItemClick, value)) return;

				if (_footerItemClick != null)
				{
					MvxTrace.Warning("Changing HeaderItemClick may cause inconsistencies where some items still call the old command.");
				}

				_footerItemClick = value;
			}
		}

		public ICommand FooterItemLongClick
		{
			get { return _footerItemLongClick; }
			set
			{
				if (ReferenceEquals(_footerItemLongClick, value)) return;

				if (_footerItemLongClick != null)
				{
					MvxTrace.Warning("Changing HeaderItemLongClick may cause inconsistencies where some items still call the old command.");
				}

				_footerItemLongClick = value;
			}
		}

		[MvxSetToNullAfterBinding]
		public virtual IEnumerable ItemsSource
		{
			get { return _itemsSource; }
			set { SetItemsSource(value); }
		}

		public override void OnViewAttachedToWindow(Object holder)
		{
			base.OnViewAttachedToWindow(holder);

			var viewHolder = (IMvxRecyclerViewHolder)holder;
			viewHolder.OnAttachedToWindow();
		}

		public override void OnViewDetachedFromWindow(Object holder)
		{
			base.OnViewDetachedFromWindow(holder);

			var viewHolder = (IMvxRecyclerViewHolder)holder;
			viewHolder.OnDetachedFromWindow();
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			var itemBindingContext = new MvxAndroidBindingContext(parent.Context, _bindingContext.LayoutInflaterHolder);

			var clickHandler = ItemClick;
			var longClickHandler = ItemClick;

			var itemListType = (ItemListType)viewType;
			if (itemListType == ItemListType.Header)
			{
				clickHandler = HeaderItemClick;
				longClickHandler = HeaderItemLongClick;
			}
			else if (itemListType == ItemListType.Footer)
			{
				clickHandler = FooterItemClick;
				longClickHandler = FooterItemLongClick;
			}

			return new MvxRecyclerViewHolder(InflateViewForHolder(parent, viewType, itemBindingContext), itemBindingContext)
			{
				Click = clickHandler,
				LongClick = longClickHandler
			};
		}

		public override int ItemCount
			=> ItemsSource.Count() + (HeaderTemplateId > 0 ? 1 : 0) + (FooterTemplateId > 0 ? 1 : 0);

		public IMvxViewModel ViewModel { get; set; }

		public int HeaderTemplateId { get; set; }

		public int FooterTemplateId { get; set; }

		public int ItemTemplateId { get; set; }

		public override int GetItemViewType(int position)
		{
			if (IsHeader(position))
			{
				return (int)ItemListType.Header;
			}

			if (IsFooter(position))
			{
				return (int)ItemListType.Footer;
			}

			return (int)ItemListType.Item;
		}

		protected virtual View InflateViewForHolder(ViewGroup parent, int viewType, IMvxAndroidBindingContext bindingContext)
		{
			int templateId;

			var itemListType = (ItemListType)viewType;
			switch (itemListType)
			{
				case ItemListType.Header:
					templateId = HeaderTemplateId;
					break;

				case ItemListType.Footer:
					templateId = FooterTemplateId;
					break;

				default:
					templateId = ItemTemplateId;
					break;
			}

			return bindingContext.BindingInflate(templateId, parent, false);
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			((IMvxRecyclerViewHolder)holder).DataContext = GetItem(position);
		}

		public virtual object GetItem(int position)
		{
			if (IsHeader(position) || IsFooter(position))
			{
				return ViewModel;
			}

			return _itemsSource.ElementAt(position - (HeaderTemplateId > 0 ? 1 : 0));
		}

		protected virtual bool IsHeader(int position)
			=> HeaderTemplateId > 0 && position == 0;

		protected virtual bool IsFooter(int position)
			=> FooterTemplateId > 0 && position == ItemCount - 1;

		protected virtual void SetItemsSource(IEnumerable value)
		{
			if (ReferenceEquals(_itemsSource, value) && !ReloadOnAllItemsSourceSets)
			{
				return;
			}

			if (_subscription != null)
			{
				_subscription.Dispose();
				_subscription = null;
			}

			_itemsSource = value;

			if (_itemsSource != null && !(_itemsSource is IList))
			{
				MvxBindingTrace.Trace(MvxTraceLevel.Warning,
					"Binding to IEnumerable rather than IList - this can be inefficient, especially for large lists");
			}

			var newObservable = _itemsSource as INotifyCollectionChanged;
			if (newObservable != null)
			{
				_subscription = newObservable.WeakSubscribe(OnItemsSourceCollectionChanged);
			}

			NotifyAndRaiseDataSetChanged();
		}

		protected virtual void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			NotifyDataSetChanged(e);
		}

		public virtual void NotifyDataSetChanged(NotifyCollectionChangedEventArgs e)
		{
			try
			{
				switch (e.Action)
				{
					case NotifyCollectionChangedAction.Add:
						NotifyItemRangeInserted(e.NewStartingIndex + 1, e.NewItems.Count);
						RaiseDataSetChanged();
						break;
					case NotifyCollectionChangedAction.Move:
						for (var i = 0; i < e.NewItems.Count; i++)
						{
							var oldItem = e.OldItems[i];
							var newItem = e.NewItems[i];

							NotifyItemMoved(ItemsSource.GetPosition(oldItem), ItemsSource.GetPosition(newItem));
							RaiseDataSetChanged();
						}
						break;
					case NotifyCollectionChangedAction.Replace:
						NotifyItemRangeChanged(e.NewStartingIndex, e.NewItems.Count);
						RaiseDataSetChanged();
						break;
					case NotifyCollectionChangedAction.Remove:
						NotifyItemRangeRemoved(e.OldStartingIndex, e.OldItems.Count);
						RaiseDataSetChanged();
						break;
					case NotifyCollectionChangedAction.Reset:
						NotifyAndRaiseDataSetChanged();
						break;
				}
			}
			catch (Exception exception)
			{
				Mvx.Warning(
					"Exception masked during Adapter RealNotifyDataSetChanged {0}. Are you trying to update your collection from a background task? See http://goo.gl/0nW0L6",
					exception.ToLongString());
			}
		}

		private void RaiseDataSetChanged()
		{
			var handler = DataSetChanged;
			handler?.Invoke(this, EventArgs.Empty);
		}

		private void NotifyAndRaiseDataSetChanged()
		{
			RaiseDataSetChanged();
			NotifyDataSetChanged();
		}
	}
}