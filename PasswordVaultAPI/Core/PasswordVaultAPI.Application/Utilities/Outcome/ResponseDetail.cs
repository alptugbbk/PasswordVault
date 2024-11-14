using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.Utilities.Outcome
{
	public class ResponseDetail<T> where T : class
	{
		public T Data { get; private set; }
		public int StatusCode { get; private set; }
		public bool IsSuccessful { get; private set; }
		public ErrorDetail Error { get; private set; }

		public static ResponseDetail<T> Success(T data, int statusCode)
		{
			return new ResponseDetail<T>
			{
				Data = data,
				StatusCode = statusCode,
				IsSuccessful = true
			};
		}

		public static ResponseDetail<T> Success(int statusCode)
		{
			return new ResponseDetail<T>
			{
				StatusCode = statusCode,
				IsSuccessful = true
			};
		}

		public static ResponseDetail<T> Fail(string message, int statusCode, bool isShow)
		{
			var errorDetail = new ErrorDetail(message, isShow);
			return new ResponseDetail<T>
			{
				Error = errorDetail,
				StatusCode = statusCode,
				IsSuccessful = false
			};
		}

		public static ResponseDetail<T> Fail(List<string> errors, int statusCode, bool isShow)
		{
			var errorDetail = new ErrorDetail(errors, isShow);
			return new ResponseDetail<T>
			{
				Error = errorDetail,
				StatusCode = statusCode,
				IsSuccessful = false
			};
		}
	}
}
