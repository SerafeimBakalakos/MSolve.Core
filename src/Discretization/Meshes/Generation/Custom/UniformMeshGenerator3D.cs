namespace MGroup.MSolve.Discretization.Meshes.Generation.Custom
{
	using System.Collections.Generic;
	using System.Linq;

	using MGroup.MSolve.Discretization.Entities;
	using MGroup.MSolve.Discretization.Meshes.Structured;

	/// <summary>
	/// Creates 3D meshes based on uniform rectilinear grids: the distance between two consecutive vertices for the same axis is 
	/// constant. This distance may be different for each axis though. For now the cells are hexahedral with 8 vertices. 
	/// (bricks in particular).
	/// </summary>
	public class UniformMeshGenerator3D<TNode> : IMeshGenerator<TNode> where TNode : INode
	{
		private readonly UniformCartesianMesh3D mesh;

		public UniformMeshGenerator3D(double minX, double minY, double minZ, double maxX, double maxY, double maxZ,
			int cellsPerX, int cellsPerY, int cellsPerZ)
		{
			double[] minCoords = { minX, minY, minZ };
			double[] maxCoords = { maxX, maxY, maxZ };
			int[] numCells = { cellsPerX, cellsPerY, cellsPerZ };
			var meshBuilder = new UniformCartesianMesh3D.Builder(minCoords, maxCoords, numCells);
			meshBuilder.SetMajorMinorAxis(0, 2);
			meshBuilder.SetFirstNodeID(0);
			meshBuilder.SetFirstElementID(0);
			meshBuilder.SetElementNodeOrderDefault();
			mesh = meshBuilder.BuildMesh();
		}

		public UniformMeshGenerator3D(UniformCartesianMesh3D mesh)
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
				vertices[nodeID] = createNode(nodeID, nodeCoords[0], nodeCoords[1], nodeCoords[2]);
			}

			return vertices;
		}

		private CellConnectivity<TNode>[] CreateElements(TNode[] allVertices)
		{
			var cells = new CellConnectivity<TNode>[mesh.NumElementsTotal];
			foreach ((int cellID, int[] verticesOfCell) in mesh.EnumerateElements())
			{
				cells[cellID] = new CellConnectivity<TNode>(CellType.Hexa8,
					verticesOfCell.Select(idx => allVertices[idx]).ToArray());
			}

			return cells;
		}
	}
}
