namespace StarWars.UnitTests.ViewModels
{
	using Moq;
	using NUnit.Framework;
	using Core.Api;
	using Core.ViewModels;
	using MvvmCross.Test.Core;
	using MvvmCross.Core.Views;
	using MvvmCross.Platform.Core;
	using System.Linq;
	using System;
	using FluentAssertions;
	using MvvmCross.Core.ViewModels;

	public class BaseViewModelTests<TViewModel> : MvxIoCSupportingTest
		where TViewModel : BaseViewModel, new()
	{
		protected TViewModel ViewModel { get; private set; }

		protected Mock<IStarWarsApi> Api { get; private set; }

		protected MockDispatcher MockDispatcher { get; private set; }

		protected void AssertViewModelWasShown<TType>() where TType : BaseViewModel
			=> AssertViewModelWasShown(typeof(TType));

		protected void AssertViewModelWasShown(Type type)
			=> MockDispatcher.Requests.Any(r => r.ViewModelType == type).Should().BeTrue();

		[SetUp]
		private void Init() => Setup();
	
		protected override void AdditionalSetup()
		{
			MockDispatcher = new MockDispatcher();
			Ioc.RegisterSingleton<IMvxViewDispatcher>(MockDispatcher);
			Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(MockDispatcher);

			Api = new Mock<IStarWarsApi>();
			Ioc.RegisterSingleton(Api.Object);

			ViewModel = new TViewModel();
			ViewModel.Start();
		}
	}

	[TestFixture]
	public class BaseViewModelTests : BaseViewModelTests<BaseViewModel>
	{
		[Test]
		public void TheStringConstructorSetsTheTitle()
		{
			const string title = "The Title";
			var vm = new BaseViewModel(title);
			vm.Title.Should().Be(title);
		}
	}
}