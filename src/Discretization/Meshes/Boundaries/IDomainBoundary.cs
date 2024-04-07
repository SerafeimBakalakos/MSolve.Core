namespace MGroup.MSolve.Core.Discretization.Meshes.Boundaries
{
	using MGroup.MSolve.Geometry.Coordinates;

	public interface IDomainBoundary
	{
		/// <summary>
		/// Return true if <paramref name="point"/> is inside the boundary, but not on the boundary exactly.
		/// </summary>
		/// <param name="point">The point of interest</param>
		/// <returns>True/false</returns>
		bool IsInside(CartesianPoint point);
	}
}
