namespace MGroup.MSolve.AnalysisWorkflow.Providers
{
	using MGroup.MSolve.Discretization.BoundaryConditions;
	using MGroup.MSolve.Discretization.Dofs;

	using System.Collections.Generic;

	public interface IAnalyzerProvider
	{
		void Reset();
		IEnumerable<INodalNeumannBoundaryCondition<IDofType>> EnumerateEquivalentNeumannBoundaryConditions(int subdomainID);
	}
}
