namespace MGroup.MSolve.Discretization.BoundaryConditions
{
	using MGroup.MSolve.Discretization.Dofs;

	public interface IElementNeumannBoundaryCondition : IElementNeumannBoundaryCondition<IDofType>
	{
	}

	public interface IElementNeumannBoundaryCondition<out T> : IElementBoundaryCondition<T> where T : IDofType
	{
	}
}
