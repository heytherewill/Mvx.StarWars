namespace StarWars.Droid.Extensions
{
	using Android.Content;
	using Android.Graphics;
	using Android.Support.V4.Content;

	public static class ColorExtensions
	{
		public static Color ToColor(this int self, Context context)
			=> new Color(ContextCompat.GetColor(context, self));
	}
}