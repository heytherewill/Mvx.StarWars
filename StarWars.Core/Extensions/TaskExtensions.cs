namespace StarWars.Core.Extensions
{
	using Models;
	using ViewModels;
	using System.Threading.Tasks;
	using Refit;
	using MvvmCross.Platform;
	using MvvmCross.Platform.Platform;
	using System.Net;

	public static class TaskExtensions
	{
		public static async Task<ApiResult<T>> WithBusyIndicator<T>(this Task<T> self, BaseViewModel vm)
		{
			vm.IsBusy = true;

			try
			{
				var result = await self.ConfigureAwait(false);
				return ApiResult.Create(result);
			}
			catch (ApiException ex)
			{
				Mvx.Trace(MvxTraceLevel.Error, ex.Message);
				return ApiResult.Create(default(T), false, ex.StatusCode);
			}
			catch (WebException ex)
			{
				Mvx.Trace(MvxTraceLevel.Error, ex.Message);
				return ApiResult.Create(default(T), false, HttpStatusCode.ServiceUnavailable);
			}
			finally
			{
				vm.IsBusy = false;
			}
		}
	}
}