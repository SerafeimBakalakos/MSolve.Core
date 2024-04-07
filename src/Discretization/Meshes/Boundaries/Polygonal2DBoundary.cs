namespace MGroup.MSolve.Core.Discretization.Meshes.Boundaries
{
	using System.Collections.Generic;

	using MGroup.MSolve.Geometry.Coordinates;
	using MGroup.MSolve.Geometry.Shapes;

	public class Polygonal2DBoundary : IDomainBoundary
	{
		private readonly ConvexPolygon2D polygon;

		public Polygonal2DBoundary(IReadOnlyList<CartesianPoint> vertices)
		{
			polygon = ConvexPolygon2D.Create(vertices, false);
		}

		public bool IsInside(CartesianPoint point)
		{
			var pos = polygon.FindRelativePositionOfPoint(point);
			if (pos == PolygonPointPosition.Inside)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
