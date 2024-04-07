namespace MGroup.MSolve.Core.Discretization.Meshes.Boundaries
{
	using MGroup.MSolve.Geometry.Coordinates;

	/// <summary>
	/// Encloses all points.
	/// </summary>
	public class NoBoundary : IDomainBoundary
	{
		public bool IsInside(CartesianPoint point) => true;
	}
}
