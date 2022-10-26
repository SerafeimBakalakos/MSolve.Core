using System.Collections.Generic;

using MGroup.MSolve.AnalysisWorkflow.Logging;
using MGroup.MSolve.DataStructures;
using MGroup.MSolve.Solution.LinearSystem;

namespace MGroup.MSolve.AnalysisWorkflow
{
	public interface IAnalyzer : ICreateState
	{
		IAnalysisWorkflowLog[] Logs { get; }
		IGlobalVector CurrentAnalysisResult { get; }
		void BuildMatrices(); //This makes sense for parent analyzers only.
		void Initialize(bool isFirstAnalysis);
		void Solve();

		GenericAnalyzerState CurrentState { get; set; }
		new GenericAnalyzerState CreateState();
		bool ICreateState.IsCurrentStateDifferent(IHaveState state) => state == null || state.Equals(CurrentState) == false;
	}
}
