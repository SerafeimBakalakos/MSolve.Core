namespace MGroup.MSolve.Discretization.BoundaryConditions
{
	using MGroup.MSolve.Discretization.Dofs;

	public interface IDomainNeumannBoundaryCondition : IDomainNeumannBoundaryCondition<IDofType>
	{
	}

	public interface IDomainNeumannBoundaryCondition<out T> : IDomainBoundaryCondition<T> where T : IDofType
	{
	}
}
