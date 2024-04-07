namespace MGroup.MSolve.AnalysisWorkflow
{
	using MGroup.MSolve.Solution.LinearSystem;

	public interface INonLinearParentAnalyzer : IParentAnalyzer
	{
		IGlobalVector GetOtherRhsComponents(IGlobalVector currentSolution);
	}
}
