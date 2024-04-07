namespace MGroup.MSolve.Discretization.BoundaryConditions
{
	using MGroup.MSolve.Discretization.Dofs;

	public interface IElementDirichletBoundaryCondition : IElementDirichletBoundaryCondition<IDofType>
	{
	}

	public interface IElementDirichletBoundaryCondition<out T> : IElementBoundaryCondition<T> where T : IDofType
	{
	}
}
