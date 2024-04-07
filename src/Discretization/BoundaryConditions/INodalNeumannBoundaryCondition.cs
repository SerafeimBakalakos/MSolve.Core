namespace MGroup.MSolve.Discretization.BoundaryConditions
{
	using MGroup.MSolve.Discretization.Dofs;

	public interface INodalNeumannBoundaryCondition : INodalNeumannBoundaryCondition<IDofType>
	{
	}

	public interface INodalNeumannBoundaryCondition<out T> : INodalBoundaryCondition<T> where T : IDofType
	{
	}
}
