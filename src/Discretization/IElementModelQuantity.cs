namespace MGroup.MSolve.Discretization
{
	using MGroup.MSolve.Discretization.Dofs;

	public interface IElementModelQuantity : IElementModelQuantity<IDofType>
	{

	}

	public interface IElementModelQuantity<out T> where T : IDofType
	{
		T DOF { get; }
		IElementType Element { get; }
		double Multiplier { get; }

		IElementModelQuantity<T> WithMultiplier(double multiplier);
		double Amount(double[] coordinates);
	}
}
