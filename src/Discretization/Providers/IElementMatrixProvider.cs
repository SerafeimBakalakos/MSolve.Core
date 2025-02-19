//TODO: Since this interface defines only a single method and the implementations are trivial, I think this interface should be 
//      integrated into a provider interface.
namespace MGroup.MSolve.Discretization.Providers
{
	using MGroup.LinearAlgebra.Matrices;

	public interface IElementMatrixProvider
	{
		IMatrix Matrix(IElementType element);

	}
}
