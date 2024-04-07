namespace MGroup.MSolve.AnalysisWorkflow
{
	using MGroup.MSolve.AnalysisWorkflow.Logging;
	using MGroup.MSolve.DataStructures;
	using MGroup.MSolve.Solution.LinearSystem;

	public interface IAnalyzer : ICreateState
	{
		IAnalysisWorkflowLog[] Logs { get; }
		IGlobalVector CurrentAnalysisResult { get; }
		void Initialize(bool isFirstAnalysis);
		void Solve();

		GenericAnalyzerState CurrentState { get; set; }
		new GenericAnalyzerState CreateState();
		bool ICreateState.IsCurrentStateDifferent(IHaveState state) => state == null || state.Equals(CurrentState) == false;
	}
}
