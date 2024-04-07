namespace MGroup.MSolve.Discretization.Providers
{
	public interface IElementVectorProvider
	{
		double[] CalcVector(IElementType element);
	}
}
