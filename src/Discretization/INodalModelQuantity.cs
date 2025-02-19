namespace MGroup.MSolve.Discretization
{
	using MGroup.MSolve.Discretization.Dofs;
	using MGroup.MSolve.Discretization.Entities;

	public interface INodalModelQuantity : INodalModelQuantity<IDofType>
	{

	}

	public interface INodalModelQuantity<out T> where T : IDofType
	{
		T DOF { get; }

		INode Node { get; }

		double Amount { get; }
		INodalModelQuantity<T> WithAmount(double amount);

	}
}
