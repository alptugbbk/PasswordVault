using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.Utilities.Outcome
{
	public class ErrorDetail
	{
		public List<string> Errors { get; private set; }
		public bool IsShow { get; private set; }

		public ErrorDetail(string error, bool isShow)
		{
			Errors = new List<string> { error };
			IsShow = isShow;
		}

		public ErrorDetail(List<string> errors, bool isShow)
		{
			Errors = errors ?? new List<string>();
			IsShow = isShow;
		}

	}
}
