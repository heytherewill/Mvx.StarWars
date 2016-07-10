namespace StarWars.UnitTests.ViewModels
{
	using NUnit.Framework;
	using Core.ViewModels;
	using MvvmCross.Test.Core;
	using MvvmCross.Core.Views;
	using MvvmCross.Platform.Core;
	using System.Linq;
	using System;
	using FluentAssertions;

	public abstract class BaseViewModelTests<TViewModel> : MvxIoCSupportingTest
		where TViewModel : BaseViewModel
	{
		protected TViewModel ViewModel { get; private set; }

		protected MockDispatcher MockDispatcher { get; private set; }

		protected abstract TViewModel CreateViewModel();

		protected void AssertViewModelWasShown<TType>() where TType : BaseViewModel
			=> AssertViewModelWasShown(typeof(TType));

		protected void AssertViewModelWasShown(Type type)
			=> MockDispatcher.Requests.Any(r => r.ViewModelType == type).Should().BeTrue();

		protected override void AdditionalSetup()
		{
			MockDispatcher = new MockDispatcher();
			Ioc.RegisterSingleton<IMvxViewDispatcher>(MockDispatcher);
			Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(MockDispatcher);

			ViewModel = CreateViewModel();
		}

		[SetUp]
		private void Init() => Setup();
	}
}