namespace MGroup.MSolve.Discretization.BoundaryConditions
{
	using MGroup.MSolve.Discretization.Dofs;

	public interface INodalBoundaryCondition : INodalBoundaryCondition<IDofType>
	{

	}

	public interface INodalBoundaryCondition<out T> : INodalModelQuantity<T> where T : IDofType
	{
		INodalBoundaryCondition<T> WithAmount(double amount);
	}
}
