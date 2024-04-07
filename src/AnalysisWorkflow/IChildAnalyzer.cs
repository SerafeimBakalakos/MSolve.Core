namespace MGroup.MSolve.AnalysisWorkflow
{
	using MGroup.LinearAlgebra.Iterative;
	using MGroup.MSolve.Solution.LinearSystem;

	public interface IChildAnalyzer : IAnalyzer
	{
		IterativeStatistics AnalysisStatistics { get; }
		IParentAnalyzer ParentAnalyzer { get; set; }

		public IGlobalVector CurrentAnalysisLinearSystemRhs { get; }
	}
}
