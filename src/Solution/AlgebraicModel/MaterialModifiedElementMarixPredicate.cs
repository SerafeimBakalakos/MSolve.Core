namespace MGroup.MSolve.Solution.AlgebraicModel
{
	using MGroup.MSolve.Discretization;

	public class MaterialModifiedElementMarixPredicate : IElementMatrixPredicate
	{
		// GOAT: This was the original implementation
		//public bool MustBuildMatrixForElement(IElementType element) => element.ConstitutiveLawModified;
		//public void ProcessElementAfterBuildingMatrix(IElementType element) => element.ResetConstitutiveLawModified();

		public bool MustBuildMatrixForElement(IElementType element) => true;
		public void ProcessElementAfterBuildingMatrix(IElementType element) { } // Do nothing
		public void ProcessElementAfterNotBuildingMatrix(IElementType element) { } // Do nothing
	}
}
