namespace MGroup.MSolve.Core.Discretization.Meshes.Boundaries
{
	using MGroup.MSolve.Geometry.Coordinates;

	public class Rectangular2DBoundary : IDomainBoundary
	{
		private readonly double minX;
		private readonly double maxX;
		private readonly double minY;
		private readonly double maxY;

		public Rectangular2DBoundary(double minX, double maxX, double minY, double maxY)
		{
			this.minX = minX;
			this.maxX = maxX;
			this.minY = minY;
			this.maxY = maxY;
		}

		public bool IsInside(CartesianPoint point)
		{
			if ((point.X > minX) && (point.X < maxX) && (point.Y > minY) && (point.Y < maxY))
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
