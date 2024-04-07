namespace MGroup.MSolve.Core.Discretization.Meshes.Boundaries
{
	using MGroup.MSolve.Geometry.Coordinates;

	public class Brick3DBoundary : IDomainBoundary
	{
		private readonly double minX, minY, minZ;
		private readonly double maxX, maxY, maxZ;

		public Brick3DBoundary(double minX, double minY, double minZ, double maxX, double maxY, double maxZ)
		{
			this.minX = minX;
			this.minY = minY;
			this.minZ = minZ;
			this.maxX = maxX;
			this.maxY = maxY;
			this.maxZ = maxZ;
		}

		public bool IsInside(CartesianPoint point)
		{
			if ((point.X > minX) && (point.X < maxX)
				&& (point.Y > minY) && (point.Y < maxY)
				&& (point.Z > minZ) && (point.Z < maxZ))
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
