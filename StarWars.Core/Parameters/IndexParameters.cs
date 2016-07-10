namespace StarWars.Core
{
	using System;
	using System.Text.RegularExpressions;

	public class IndexParameters
	{
		public int Index { get; set; }

		internal static IndexParameters FromUrl(string url)
			=> new IndexParameters { Index = Convert.ToInt32(GetFirstMatchOrDefault(url)) };

		private static string GetFirstMatchOrDefault(string url)
		{
			const string regex = "([0-9]+?)";
			var match = Regex.Match(url, regex);
			var result = match?.Captures?[0]?.Value;
			return result;
		}
	}
}