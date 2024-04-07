namespace MGroup.MSolve.Discretization.BoundaryConditions
{
	using MGroup.MSolve.Discretization.Dofs;

	public interface IDomainDirichletBoundaryCondition : IDomainDirichletBoundaryCondition<IDofType>
	{
	}

	public interface IDomainDirichletBoundaryCondition<out T> : IDomainBoundaryCondition<T> where T : IDofType
	{
	}
}
