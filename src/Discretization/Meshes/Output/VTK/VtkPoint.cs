namespace MGroup.MSolve.Discretization.Meshes.Output.VTK
{
	using MGroup.MSolve.Discretization.Entities;

	/// <summary>
	/// Vertex used to represent VTK grids.
	/// </summary>
	public class VtkPoint
	{
		public VtkPoint(int id, double x, double y = 0, double z = 0)
		{
			this.ID = id;
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public VtkPoint(int id, INode node)
		{
			this.ID = id;
			this.X = node.X;
			this.Y = node.Y;
			this.Z = node.Z;
		}

		public int ID { get; }

		public double X { get; }
		public double Y { get; }
		public double Z { get; }
	}
}
