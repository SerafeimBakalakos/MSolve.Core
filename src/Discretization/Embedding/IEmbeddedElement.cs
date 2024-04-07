namespace MGroup.MSolve.Discretization.Embedding
{
	using System.Collections.Generic;

	using MGroup.MSolve.Discretization.Dofs;
	using MGroup.MSolve.Discretization.Entities;

	public interface IEmbeddedElement
	{
		IList<EmbeddedNode> EmbeddedNodes { get; }
		Dictionary<IDofType, int> GetInternalNodalDOFs(IElementType element, INode node);
		double[] GetLocalDOFValues(IElementType hostElement, double[] hostDOFValues);
	}
}
