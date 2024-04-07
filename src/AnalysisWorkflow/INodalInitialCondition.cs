namespace MGroup.MSolve.AnalysisWorkflow.Transient
{
	using MGroup.MSolve.Discretization;
	using MGroup.MSolve.Discretization.Dofs;

	public interface INodalInitialCondition : INodalInitialCondition<IDofType>
	{

	}

	public interface INodalInitialCondition<out T> : INodalModelQuantity<T> where T : IDofType
	{
		DifferentiationOrder DifferentiationOrder { get; }

		INodalInitialCondition<T> WithAmount(double amount);
	}
}
