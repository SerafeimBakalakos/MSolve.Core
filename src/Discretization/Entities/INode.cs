namespace MGroup.MSolve.Discretization.Entities
{
	using System;
	using System.Collections.Generic;

	public interface INode : IDiscretePoint, IComparable<INode>
	{
		double X { get; }
		double Y { get; }
		double Z { get; }

		Dictionary<int, IElementType> ElementsDictionary { get; }

		HashSet<int> Subdomains { get; }
	}
}
