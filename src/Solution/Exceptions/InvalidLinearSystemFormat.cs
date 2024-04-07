namespace MGroup.MSolve.Solution.Exceptions
{
	using System;

	public class InvalidLinearSystemFormat : ArgumentException
	{
		public InvalidLinearSystemFormat()
		{
		}

		public InvalidLinearSystemFormat(string message)
			: base(message)
		{
		}

		public InvalidLinearSystemFormat(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
