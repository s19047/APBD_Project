using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AdvertApi.ExceptionHandling
{
	public class UserAlreadyExistsException : Exception
	{
		public UserAlreadyExistsException()
		{
		}

		public UserAlreadyExistsException(string message) : base(message)
		{
		}

		public UserAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected UserAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
