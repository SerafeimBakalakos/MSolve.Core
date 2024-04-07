namespace MGroup.MSolve.AnalysisWorkflow.Logging
{
	using System;

	using MGroup.MSolve.Solution.LinearSystem;

	public interface IAnalysisWorkflowLog
	{
		void StoreResults(DateTime startTime, DateTime endTime, IGlobalVector solution);
	}
}
