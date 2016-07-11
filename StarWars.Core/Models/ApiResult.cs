namespace StarWars.Core.Models
{
	using System.Net;

	public static class ApiResult
	{
		public static ApiResult<T> Create<T>(T data)
			=> new ApiResult<T>(data, true, HttpStatusCode.OK);

		internal static ApiResult<T> Create<T>(T data, bool status, HttpStatusCode statusCode)
			=> new ApiResult<T>(data, status, statusCode);
	}

	public class ApiResult<T>
	{
		public ApiResult(T data, bool success, HttpStatusCode statusCode)
		{
			Data = data;
			Success = success;
			StatusCode = statusCode;
		}

		public T Data { get; }

		public bool Success { get; }

		public HttpStatusCode StatusCode { get; }
	}
}

