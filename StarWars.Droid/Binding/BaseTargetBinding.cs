namespace StarWars.Droid.Bindings
{
	using System;
	using Android.App;
	using Android.Views;
	using MvvmCross.Binding;
	using MvvmCross.Binding.Bindings.Target;
	using MvvmCross.Platform;
	using MvvmCross.Platform.Droid.Platform;

	public abstract class BaseTargetBinding<TView, TType> : MvxTargetBinding
		where TView : View
	{
		public BaseTargetBinding(TView target)
			: base(target)
		{
		}

		public sealed override void SetValue(object value)
		{
			var convertedValue = Convert(value);
			TypedSetValue(convertedValue);
		}

		protected Activity Activity => Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;

		protected TView View => Target as TView;

		protected virtual TType Convert(object value) => (TType)value;

		protected abstract void TypedSetValue(TType value);

		public sealed override Type TargetType => typeof(TType);

		public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
	}
}