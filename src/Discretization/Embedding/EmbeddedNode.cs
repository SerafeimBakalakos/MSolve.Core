namespace MGroup.MSolve.Discretization.Embedding
{
	using System;
	using System.Collections.Generic;

	using MGroup.MSolve.Discretization.Dofs;
	using MGroup.MSolve.Discretization.Entities;

	public class EmbeddedNode
	{
		private readonly INode node;
		private readonly IElementType embeddedInElement;
		private readonly IList<double> coordinates = new List<double>();
		private readonly IList<IDofType> dependentDOFs;

		public INode Node => node;
		public IElementType EmbeddedInElement => embeddedInElement;
		public IList<IDofType> DependentDOFs => dependentDOFs;
		public IList<double> Coordinates => coordinates;

		public EmbeddedNode(INode node, IElementType hostElement, IList<IDofType> dependentDOFs)
		{
			this.node = node;
			this.embeddedInElement = hostElement;
			this.dependentDOFs = dependentDOFs;
		}

		public override bool Equals(object obj)
		{
			EmbeddedNode e = obj as EmbeddedNode;
			if (e == null) return false;

			return (e.Node == this.node && e.EmbeddedInElement == embeddedInElement);
		}

		public override string ToString()
		{
			if (node == null) return "(Null inside node)";
			string hostElementID = embeddedInElement == null ? "(N/A)" : embeddedInElement.ID.ToString();
			string coordinateString = String.Empty;
			foreach (var c in coordinates)
				coordinateString += c.ToString() + " ";

			var header = String.Format("{0} (host: {4}): ({1}, {2}, {3}) [{5}]", node.ID, node.X, node.Y, node.Z, hostElementID, coordinateString);
			string constraintsDescripton = string.Empty;
			#region removeMaria
			//UNDONE: fix text
			//foreach (var c in node.Constraints)
			//{
			//    string con = string.Empty;
			//    switch (c)
			//    {
			//        case DOFType.Pore:
			//            con = "Pore";
			//            break;
			//        case DOFType.RotX:
			//            con = "rX";
			//            break;
			//        case DOFType.RotY:
			//            con = "rY";
			//            break;
			//        case DOFType.RotZ:
			//            con = "rZ";
			//            break;
			//        case DOFType.Unknown:
			//            con = "?";
			//            break;
			//        case DOFType.X:
			//            con = "X";
			//            break;
			//        case DOFType.Y:
			//            con = "Y";
			//            break;
			//        case DOFType.Z:
			//            con = "Z";
			//            break;
			//    }
			//    constraintsDescripton += c.ToString() + ", ";
			//}
			#endregion
			constraintsDescripton = constraintsDescripton.Length > 1 ? constraintsDescripton.Substring(0, constraintsDescripton.Length - 2) : constraintsDescripton;

			return String.Format("{0} - Con ({1})", header, constraintsDescripton);
		}

	}
}
