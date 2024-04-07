namespace MGroup.MSolve.Discretization.BoundaryConditions
{
	using MGroup.MSolve.Discretization.Dofs;

	public interface INodalDirichletBoundaryCondition : INodalDirichletBoundaryCondition<IDofType>
	{
	}

	public interface INodalDirichletBoundaryCondition<out T> : INodalBoundaryCondition<T> where T : IDofType
	{
	}
}
