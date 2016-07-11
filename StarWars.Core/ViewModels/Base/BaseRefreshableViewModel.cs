namespace StarWars.Core.ViewModels
{
	using Models;
	using Extensions;
	using System.Threading.Tasks;
	using MvvmCross.Core.ViewModels;
	using System.Collections.ObjectModel;

	public abstract class BaseRefreshableViewModel<TModelType, TViewModelType> : BaseViewModel
		where TModelType : BaseModel
		where TViewModelType : BaseViewModel
	{
		private int _currentPage = 1;
		private bool _canFetchMoreItems = true;

		protected BaseRefreshableViewModel()
			: this("")
		{
		}

		protected BaseRefreshableViewModel(string title)
			: base(title)
		{
			RefreshCommand = new MvxAsyncCommand(RefreshCommandExecute);
			ItemClickCommand = new MvxCommand<TModelType>(ItemClickCommandExecute);
			FetchMoreItemsCommand = new MvxAsyncCommand(FetchMoreItemsCommandExecute);
		}

		public override async void Start() 
		{
			await FetchMoreItemsCommandExecute();
			await FetchMoreItemsCommandExecute();
		}
	
		public ObservableCollection<TModelType> Items { get; } = new ObservableCollection<TModelType>();

		public IMvxCommand ItemClickCommand { get; }

		public IMvxAsyncCommand RefreshCommand { get; }

		public IMvxAsyncCommand FetchMoreItemsCommand { get; }

		private void ItemClickCommandExecute(TModelType item)
			=> ShowViewModel<TViewModelType>(IndexParameters.FromUrl(item.Url));

		private async Task RefreshCommandExecute()
		{
			Items.Clear();
			_currentPage = 1;
			_canFetchMoreItems = true;
			await FetchMoreItemsCommandExecute();
			await FetchMoreItemsCommandExecute();
		}

		private async Task FetchMoreItemsCommandExecute()
		{
			if (!_canFetchMoreItems) return;

			var apiResult = await FetchMoreItems(_currentPage).WithBusyIndicator(this);
			if (!apiResult.Success) return;

			_currentPage++;
			_canFetchMoreItems = apiResult.Data.Next != null;
			Items.AddRange(apiResult.Data.Results);
		}

		protected abstract Task<ResponseEntity<TModelType>> FetchMoreItems(int page);
	}
}