namespace MGroup.MSolve.Discretization.Meshes.Unstructured
{
	using System.Collections.Generic;

	public interface IUnstructuredMesh
	{
		/// <summary>
		/// Enumerates the global IDs and coordinates of the nodes. The nodes are returned in increasing order of their id.
		/// </summary>
		IEnumerable<(int nodeID, double[] coordinates)> EnumerateNodes();

		/// <summary>
		/// For each element returns its global ID, cell type and the global IDs of its nodes. The elements are returned in 
		/// increasing order of their id.
		/// </summary>
		IEnumerable<(int elementID, CellType cellType, int[] nodeIDs)> EnumerateElements();

		/// <summary>
		/// Finds all elements that contain the node with id = <paramref name="nodeId"/>. The ids of these elements are returned,
		/// same as retuned by <see cref="EnumerateElements"/>.
		/// </summary>
		/// <param name="nodeId">The id of the node, same as returned by <see cref="EnumerateNodes"/></param>
		/// <returns>The ids of the elements containing the target node.</returns>
		ISet<int> FindElementsWithNode(int nodeId);
	}
}
