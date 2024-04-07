namespace MGroup.MSolve.AnalysisWorkflow.Providers
{
	using MGroup.MSolve.Solution.LinearSystem;

	public interface INonTransientAnalysisProvider : IAnalyzerProvider
	{
		/// <summary>
		/// Creates the submatrix that corresponds to the free freedom degrees of a subdomain. If A is the matrix corresponding
		/// to all dofs, f denotes free dofs and c denotes constrained dofs then A = [ Aff Acf^T; Acf Acc ]. This method
		/// builds and creates only Aff.
		/// </summary>
		IGlobalMatrix GetMatrix();
		IGlobalVector GetRhs();
	}
}
