namespace MGroup.MSolve.Discretization.Meshes.Output.VTK
{
	using System.Collections.Generic;

	public interface IVtkMesh
	{
		IReadOnlyList<VtkCell> VtkCells { get; } //TODO: Make this a dictionary

		IReadOnlyList<VtkPoint> VtkPoints { get; } //TODO: Make this a dictionary
	}
}
