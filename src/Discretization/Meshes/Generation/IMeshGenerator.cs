//TODO: Also Mesh generation should be moved to Geometry project.

namespace MGroup.MSolve.Discretization.Meshes.Generation
{
	using System.Collections.Generic;

	using MGroup.MSolve.Discretization.Entities;

	/// <summary>
	/// Creates 2D and 3D meshes for use in FEM or similar methods.
	/// </summary>
	/// <typeparam name="TNode">The type of node/vertex</typeparam>
	public interface IMeshGenerator<TNode> where TNode : INode
	{
		/// <summary>
		/// Generates the mesh in terms of its nodes/vertices and elements/cells.
		/// </summary>
		/// <param name="createNode">A delegate that initializes a new object of the specific node type.</param>
		/// <returns>The vertices and cells of the mesh</returns>
		(IReadOnlyList<TNode> nodes, IReadOnlyList<CellConnectivity<TNode>> elements) CreateMesh(CreateNode<TNode> createNode);
	}

	/// <summary>
	/// Initializes a new instance of some class that implements <see cref="INode"/>.
	/// </summary>
	/// <param name="id">The unique identifier of the node.</param>
	/// <param name="x">The x coordinate of the node.</param>
	/// <param name="y">The y coordinate of the node. Leave it 0.0 for 1D problems.</param>
	/// <param name="z">The z coordinate of the node. Leave it 0.0 for 1D, 2D problems.</param>
	public delegate TNode CreateNode<TNode>(int id, double x, double y, double z) where TNode : INode;
}
