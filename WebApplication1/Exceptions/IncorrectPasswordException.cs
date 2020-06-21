using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AdvertApi.ExceptionHandling
{
	public class IncorrectPasswordException : Exception
	{
		public IncorrectPasswordException()
		{
		}

		public IncorrectPasswordException(string message) : base(message)
		{
		}

		public IncorrectPasswordException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected IncorrectPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
