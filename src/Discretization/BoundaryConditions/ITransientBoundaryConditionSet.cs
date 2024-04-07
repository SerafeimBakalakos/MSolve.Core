namespace MGroup.MSolve.Discretization.BoundaryConditions
{
	using MGroup.MSolve.Discretization.Dofs;

	public interface ITransientBoundaryConditionSet<out T> : IBoundaryConditionSet<T> where T : IDofType
	{
		double CurrentTime { get; set; }
	}
}
