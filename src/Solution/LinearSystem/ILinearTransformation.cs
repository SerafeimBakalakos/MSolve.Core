//TODO: This belongs in LinearAlgebra project
namespace MGroup.MSolve.Solution.LinearSystem
{
	public interface ILinearTransformation
	{
		void MultiplyVector(IGlobalVector input, IGlobalVector output);
	}
}
