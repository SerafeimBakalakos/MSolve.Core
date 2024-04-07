namespace MGroup.MSolve.Discretization.Embedding
{
	using System.Collections.Generic;

	using MGroup.MSolve.Discretization.Dofs;

	public interface IEmbeddedDOFInHostTransformationVector
	{
		IList<IDofType> GetDependentDOFTypes { get; }
		IReadOnlyList<IReadOnlyList<IDofType>> GetDOFTypesOfHost(EmbeddedNode node);
		double[][] GetTransformationVector(EmbeddedNode node);
	}
}
