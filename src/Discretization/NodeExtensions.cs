namespace MGroup.MSolve.Discretization
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using MGroup.MSolve.Discretization.Entities;
	using MGroup.MSolve.Geometry.Coordinates;

	public static class NodeExtensions
	{
		//TODO: perhaps this should be in the geometry project.
		public static double CalculateEuclidianDistanceFrom(this INode node1, INode node2)
		{
			double dx = node1.X - node2.X;
			double dy = node1.Y - node2.Y;
			double dz = node1.Z - node2.Z;
			return Math.Sqrt(dx * dx + dy * dy + dz * dz);
		}

		public static CartesianPoint ToCartesianPoint(this INode node) => new CartesianPoint(node.X, node.Y);

		public static IReadOnlyList<CartesianPoint> ToCartesianPoints<TNode>(this IReadOnlyList<TNode> nodes) where TNode : INode
			=> nodes.Select(node => new CartesianPoint(node.X, node.Y)).ToArray();
	}
}
