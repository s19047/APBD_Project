using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AdvertApi.Exceptions
{
	public class BuildingsOnDifferentStreetException : Exception
	{
		public BuildingsOnDifferentStreetException()
		{
		}

		public BuildingsOnDifferentStreetException(string message) : base(message)
		{
		}

		public BuildingsOnDifferentStreetException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected BuildingsOnDifferentStreetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
