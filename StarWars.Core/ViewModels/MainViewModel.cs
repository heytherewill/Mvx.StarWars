namespace StarWars.Core.ViewModels
{
	using Models;
	using MvvmCross.Core.ViewModels;
	using System.Collections.ObjectModel;

	public class MainViewModel : BaseViewModel
	{
		public MainViewModel()
			: base(Resources.Categories)
		{
			CategoryCommand = new MvxCommand<Category>(CategoryCommandExecute);
		}

		public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>
		{
			new Category { Name = Resources.Films, Type = CategoryType.Films },
			new Category { Name = Resources.People, Type = CategoryType.People },
			new Category { Name = Resources.Planets, Type = CategoryType.Planets },
			new Category { Name = Resources.Species, Type = CategoryType.Species },
			new Category { Name = Resources.Vehicles, Type = CategoryType.Vehicles },
			new Category { Name = Resources.Starships, Type = CategoryType.Starships }
		};

		public IMvxCommand CategoryCommand { get; }

		private void CategoryCommandExecute(Category category)
		{
			switch (category.Type)
			{
				case CategoryType.Films:
					ShowViewModel<FilmsViewModel>();
					break;
				case CategoryType.People:
					ShowViewModel<PeopleViewModel>();
					break;
				case CategoryType.Planets:
					ShowViewModel<PlanetsViewModel>();
					break;
				case CategoryType.Species:
					ShowViewModel<SpeciesViewModel>();
					break;
				case CategoryType.Vehicles:
					ShowViewModel<VehiclesViewModel>();
					break;
				case CategoryType.Starships:
					ShowViewModel<StarshipsViewModel>();
					break;
			}
		}
	}
}