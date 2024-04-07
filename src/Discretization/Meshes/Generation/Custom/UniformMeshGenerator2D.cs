//TODO: perhaps the origin should be (0.0, 0.0) and the meshes could then be transformed. Abaqus does something similar with its
//      meshed parts during assembly
namespace MGroup.MSolve.Discretization.Meshes.Generation.Custom
{
	using System.Collections.Generic;
	using System.Linq;

	using MGroup.MSolve.Discretization.Entities;
	using MGroup.MSolve.Discretization.Meshes.Structured;

	/// <summary>
	/// Creates 2D meshes based on uniform rectilinear grids: the distance between two consecutive vertices for the same axis is 
	/// constant. This distance may be different for each axis though. For now the cells are quadrilateral with 4 vertices 
	/// (rectangles in particular).
	/// </summary>
	public class UniformMeshGenerator2D<TNode> : IMeshGenerator<TNode> where TNode : INode
	{
		private readonly UniformCartesianMesh2D mesh;

		public UniformMeshGenerator2D(double minX, double minY, double maxX, double maxY, int cellsPerX, int cellsPerY)
		{
			double[] minCoords = { minX, minY };
			double[] maxCoords = { maxX, maxY };
			int[] numCells = { cellsPerX, cellsPerY };
			var meshBuilder = new UniformCartesianMesh2D.Builder(minCoords, maxCoords, numCells);
			meshBuilder.SetMajorAxis(0);
			meshBuilder.SetFirstNodeID(0);
			meshBuilder.SetFirstElementID(0);
			mesh = meshBuilder.BuildMesh();
		}

		public UniformMeshGenerator2D(UniformCartesianMesh2D mesh)
		{
			this.mesh = mesh;
		}

		/// <inheritdoc />
		public (IReadOnlyList<TNode> nodes, IReadOnlyList<CellConnectivity<TNode>> elements)
			CreateMesh(CreateNode<TNode> createNode)
		{
			TNode[] nodes = CreateNodes(createNode);
			CellConnectivity<TNode>[] elements = CreateElements(nodes);
			return (nodes, elements);
		}

		private TNode[] CreateNodes(CreateNode<TNode> createNode)
		{
			var vertices = new TNode[mesh.NumNodesTotal];
			foreach ((int nodeID, double[] nodeCoords) in mesh.EnumerateNodes())
			{
				vertices[nodeID] = createNode(nodeID, nodeCoords[0], nodeCoords[1], 0.0);
			}

			return vertices;
		}

		private CellConnectivity<TNode>[] CreateElements(TNode[] allVertices)
		{
			var cells = new CellConnectivity<TNode>[mesh.NumElementsTotal];
			foreach ((int cellID, int[] verticesOfCell) in mesh.EnumerateElements())
			{
				cells[cellID] = new CellConnectivity<TNode>(CellType.Quad4, 
					verticesOfCell.Select(idx => allVertices[idx]).ToArray());
			}

			return cells;
		}
	}
}
