using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AdvertApi.ExceptionHandling
{
	public class UserDoesNotExistException : Exception
	{
		public UserDoesNotExistException()
		{
		}

		public UserDoesNotExistException(string message) : base(message)
		{
		}

		public UserDoesNotExistException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected UserDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
