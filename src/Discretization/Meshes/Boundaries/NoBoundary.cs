namespace MGroup.MSolve.Core.Discretization.Meshes.Boundaries
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	using MGroup.MSolve.Geometry.Coordinates;

	/// <summary>
	/// Encloses all points.
	/// </summary>
	public class NoBoundary : IDomainBoundary
	{
		public bool IsInside(CartesianPoint point) => true;
	}
}
