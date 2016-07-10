namespace StarWars.Core
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	public static class EnumerableExtensions
	{
		public static void AddRange<T>(this ObservableCollection<T> self, IEnumerable<T> toAdd)
		{
			foreach (var item in toAdd)
			{
				self.Add(item);
			}
		}
	}
}