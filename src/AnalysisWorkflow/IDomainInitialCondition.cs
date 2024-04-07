namespace MGroup.MSolve.AnalysisWorkflow.Transient
{
	using MGroup.MSolve.Discretization;
	using MGroup.MSolve.Discretization.Dofs;

	public interface IDomainInitialCondition<out T> : IDomainModelQuantity<T> where T : IDofType
	{
		DifferentiationOrder DifferentiationOrder { get; }

		IDomainInitialCondition<T> WithMultiplier(double multiplier);
	}
}
