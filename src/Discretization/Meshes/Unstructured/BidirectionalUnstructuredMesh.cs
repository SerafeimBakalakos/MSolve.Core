namespace MGroup.MSolve.Discretization.Meshes.Unstructured
{
	using System.Collections.Generic;

	using MGroup.MSolve.Core.Discretization.Meshes.Boundaries;
	using MGroup.MSolve.Core.Discretization.Meshes.Unstructured;
	using MGroup.MSolve.Geometry.Coordinates;
	using MGroup.MSolve.Geometry.Shapes;

	public class BidirectionalUnstructuredMesh : IUnstructuredMesh, I2DInteractiveMesh
	{
		private readonly IDomainBoundary boundary;
		private IReadOnlyDictionary<int, HashSet<int>> elementsOfNodes;

		public BidirectionalUnstructuredMesh(IDomainBoundary boundary = null)
		{
			if (boundary == null)
			{
				this.boundary = new NoBoundary();
			}
			else
			{
				this.boundary = boundary;
			}
		}

		public List<double[]> Nodes { get; } = new List<double[]>();

		public List<(CellType cellType, int[] nodes)> Elements { get; } = new List<(CellType, int[])>();

		public void ConnectComponents()
		{
			// Connect cells to vertices
			var nodesToElements = new Dictionary<int, HashSet<int>>();
			for (int e = 0; e < Elements.Count; ++e)
			{
				(_, int[] nodesOfElement) = Elements[e];
				foreach (int node in nodesOfElement)
				{
					bool exists = nodesToElements.TryGetValue(node, out HashSet<int> elementsOfThisNode);
					if (!exists)
					{
						elementsOfThisNode = new HashSet<int>();
						nodesToElements.Add(node, elementsOfThisNode);
					}

					elementsOfThisNode.Add(e);
				}
			}

			this.elementsOfNodes = nodesToElements;
		}

		public IEnumerable<(int elementID, CellType cellType, int[] nodeIDs)> EnumerateElements()
		{
			for (int e = 0; e < Elements.Count; ++e)
			{
				yield return (e, Elements[e].cellType, Elements[e].nodes);
			}
		}

		public IEnumerable<(int nodeID, double[] coordinates)> EnumerateNodes()
		{
			for (int n = 0; n < Nodes.Count; ++n)
			{
				yield return (n, Nodes[n]);
			}
		}

		public int[] FindElementsContainingPoint(CartesianPoint point, int startingElement = int.MinValue)
		{
			// TODO: handle cases where the point lies on an element edge or node.
			// TODO: Do it by spreading around the starting cell, breadth-first-search
			var containingElements = new List<int>();
			for (int e = 0; e < Elements.Count; ++e)
			{
				IReadOnlyList<double[]> nodesOfElement = GetNodalCoordsOfElement(e);
				var outline = ConvexPolygon2D.CreateFromCoords(nodesOfElement);
				PolygonPointPosition pos = outline.FindRelativePositionOfPoint(point);
				if ((pos == PolygonPointPosition.Inside) || (pos == PolygonPointPosition.OnEdge)
					|| (pos == PolygonPointPosition.OnVertex))
				{
					containingElements.Add(e);
				}
			}

			return containingElements.ToArray();
		}

		public int[] FindElementsIntersectedByCircle(Circle2D circle, int startingElement = int.MinValue)
		{
			var intersectedElements = new List<int>();
			for (int e = 0; e < Elements.Count; ++e)
			{
				IReadOnlyList<double[]> nodesOfElement = GetNodalCoordsOfElement(e);
				var outline = ConvexPolygon2D.CreateFromCoords(nodesOfElement);
				CirclePolygonPosition relativePosition = outline.FindRelativePositionOfCircle(circle);
				if (relativePosition == CirclePolygonPosition.Intersecting)
				{
					intersectedElements.Add(e);
				}
			}

			return intersectedElements.ToArray();
		}

		public int[] FindElementsInsideCircle(Circle2D circle, int startingElement = int.MinValue)
		{
			var internalElements = new List<int>();
			for (int e = 0; e < Elements.Count; ++e)
			{
				IReadOnlyList<double[]> nodesOfElement = GetNodalCoordsOfElement(e);
				var outline = ConvexPolygon2D.CreateFromCoords(nodesOfElement);
				CirclePolygonPosition relativePosition = outline.FindRelativePositionOfCircle(circle);
				if ((relativePosition == CirclePolygonPosition.Intersecting) ||
					(relativePosition == CirclePolygonPosition.PolygonInsideCircle))
				{
					internalElements.Add(e);
				}
			}

			return internalElements.ToArray();
		}

		public ISet<int> FindElementsWithNode(int nodeId) => new HashSet<int>(elementsOfNodes[nodeId]);

		public int[] FindNodesInsideCircle(Circle2D circle, bool findBoundaryNodes = true, int startingElement = int.MinValue)
		{
			var selectedNodes = new List<int>();
			for (int n = 0; n < Nodes.Count; ++n) // O(nodesCount)
			{
				double[] coords = Nodes[n];
				CirclePointPosition relativePosition =
					circle.FindRelativePositionOfPoint(new CartesianPoint(coords[0], coords[1]));
				if ((relativePosition == CirclePointPosition.Inside) ||
					(findBoundaryNodes && (relativePosition == CirclePointPosition.On)))
				{
					selectedNodes.Add(n);
				}
			}

			return selectedNodes.ToArray();
		}

		public bool IsPointInsideBoundary(CartesianPoint point)
		{
			return boundary.IsInside(point);
		}

		private IReadOnlyList<double[]> GetNodalCoordsOfElement(int element)
		{
			(_, int[] nodes) = Elements[element];
			var nodalCoords = new List<double[]>(nodes.Length);
			foreach (int node in nodes)
			{
				nodalCoords.Add(Nodes[node]);
			}

			return nodalCoords;
		}
	}
}
