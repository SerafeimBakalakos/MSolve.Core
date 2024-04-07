namespace MGroup.MSolve.Discretization.BoundaryConditions
{
	using MGroup.MSolve.Discretization.Dofs;

	public interface IElementBoundaryCondition<out T> : IElementModelQuantity<T> where T : IDofType
	{
		IElementBoundaryCondition<T> WithMultiplier(double amount);
	}
}
