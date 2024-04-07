//TODO: Extend this to 3D cells
namespace MGroup.MSolve.Core.Discretization.Meshes.Generation.GMSH
{
	using MGroup.MSolve.Discretization;

	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Converts cell types and the order of their vertices from GMSH to MSolve.
	/// </summary>
	internal class GmshCellFactory
	{
		private static readonly IReadOnlyDictionary<int, CellType> gmshCellCodes;

		// Vertex order for cells. Index = gmsh order, value = MSolve order.
		private static readonly IReadOnlyDictionary<CellType, int[]> gmshCellConnectivity;

		static GmshCellFactory()
		{
			var codes = new Dictionary<int, CellType>();
			codes.Add(2, CellType.Tri3);
			codes.Add(3, CellType.Quad4);
			codes.Add(9, CellType.Tri6);
			codes.Add(10, CellType.Quad9);
			codes.Add(16, CellType.Quad8);
			gmshCellCodes = codes;

			var connectivity = new Dictionary<CellType, int[]>();
			connectivity.Add(CellType.Tri3, new int[] { 0, 1, 2 });
			connectivity.Add(CellType.Quad4, new int[] { 0, 1, 2, 3 });
			connectivity.Add(CellType.Tri6, new int[] { 0, 1, 2, 3, 4, 5 });
			connectivity.Add(CellType.Quad9, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
			connectivity.Add(CellType.Quad8, new int[] { 0, 1, 2, 3, 4, 5, 6, 7 });
			gmshCellConnectivity = connectivity;
		}

		private readonly IReadOnlyList<double[]> allNodes;

		public GmshCellFactory(IReadOnlyList<double[]> allNodes)
		{
			this.allNodes = allNodes;
		}

		/// <summary>
		/// Returns the nodes of an element in the order used by MSolve given the nodes in the order used by GMSH.
		/// </summary>
		public int[] ChangeOrderOfElementNodes(CellType cellType, int[] gmshNodeIDs)
		{
			var msolveNodeIDs = Permute(gmshNodeIDs, gmshCellConnectivity[cellType]);
			var nodeCoords = msolveNodeIDs.Select(n => allNodes[n]).ToList();
			var fixedNodeOrder = EnforceCounterClockwiseOrder(nodeCoords);
			return Permute(msolveNodeIDs, fixedNodeOrder);
		}

		public bool TryGetMSolveCellType(int gmshCellCode, out CellType cellType)
			=> gmshCellCodes.TryGetValue(gmshCellCode, out cellType);

		/// <summary>
		/// If the order is clockwise, it is reversed.
		/// </summary>
		/// <param name="cellVertices"></param>
		private static int[] EnforceCounterClockwiseOrder(IList<double[]> cellVertices)
		{
			//TODO: This only works for 2D cells. Not sure if it sufficient or required for second order elements.
			// The area of a cell with clockwise vertices is negative.
			var cellArea = 0.0; // Actually double the area will be computed, but we only care about the sign here
			for (var i = 0; i < cellVertices.Count; ++i)
			{
				var vertex1 = cellVertices[i];
				var vertex2 = cellVertices[(i + 1) % cellVertices.Count];
				cellArea += vertex1[0] * vertex2[1] - vertex2[0] * vertex1[1];
			}

			var order = Enumerable.Range(0, cellVertices.Count).ToArray();
			if (cellArea < 0)
			{
				Array.Reverse(order);
			}

			return order;
		}

		private static T[] Permute<T>(T[] originalAray, int[] permutation)
		{
			var result = new T[originalAray.Length];
			for (var i = 0; i < originalAray.Length; ++i)
			{
				result[permutation[i]] = originalAray[i];
			}

			return result;
		}
	}
}
