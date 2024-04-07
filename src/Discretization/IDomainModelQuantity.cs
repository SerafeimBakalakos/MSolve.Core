namespace MGroup.MSolve.Discretization
{
	using MGroup.MSolve.Discretization.Dofs;

	public interface IDomainModelQuantity<out T> where T : IDofType
	{
		T DOF { get; }
		double Multiplier { get; }

		IDomainModelQuantity<T> WithMultiplier(double multiplier);
		double Amount(double[] coordinates);
	}
}
