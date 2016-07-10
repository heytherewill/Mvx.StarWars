namespace StarWars.UnitTests.ViewModels
{
	using Core.ViewModels;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class MainViewModelTests : BaseViewModelTests<MainViewModel>
	{
		protected override MainViewModel CreateViewModel() => new MainViewModel();

		[Test]
		public void TheMainViewModelHasSixCategories()
			=> ViewModel.Categories.Count.Should().Be(6);

		[Test]
		public void SelectingTheFirstCategoryShowsTheFilmsViewModel()
			=> CallCategoryCommandAndAssertViewModelWasShown<FilmsViewModel>(0);

		[Test]
		public void SelectingTheSecondCategoryShowsThePeopleViewModel()
			=> CallCategoryCommandAndAssertViewModelWasShown<PeopleViewModel>(1);

		[Test]
		public void SelectingTheThirdCategoryShowsThePlanetsViewModel()
			=> CallCategoryCommandAndAssertViewModelWasShown<PlanetsViewModel>(2);

		[Test]
		public void SelectingTheFourthCategoryShowsTheSpeciesViewModel()
			=> CallCategoryCommandAndAssertViewModelWasShown<SpeciesViewModel>(3);

		[Test]
		public void SelectingTheFifthCategoryShowsTheVehiclesViewModel()
			=> CallCategoryCommandAndAssertViewModelWasShown<VehiclesViewModel>(4);

		[Test]
		public void SelectingTheSixthCategoryShowsTheStarshipsViewModel()
			=> CallCategoryCommandAndAssertViewModelWasShown<StarshipsViewModel>(5);

		private void CallCategoryCommandAndAssertViewModelWasShown<TType>(int index)
			where TType : BaseViewModel
		{
			var category = ViewModel.Categories[index];
			ViewModel.CategoryCommand.Execute(category);
			AssertViewModelWasShown<TType>();
		}
	}
}