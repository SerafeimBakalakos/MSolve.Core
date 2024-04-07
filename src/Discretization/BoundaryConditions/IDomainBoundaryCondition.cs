namespace MGroup.MSolve.Discretization.BoundaryConditions
{
	using MGroup.MSolve.Discretization.Dofs;

	public interface IDomainBoundaryCondition<out T> : IDomainModelQuantity<T> where T : IDofType
	{
		IDomainBoundaryCondition<T> WithMultiplier(double amount);
	}
}
