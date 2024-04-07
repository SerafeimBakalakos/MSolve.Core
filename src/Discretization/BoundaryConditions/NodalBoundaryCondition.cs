namespace MGroup.MSolve.Discretization.BoundaryConditions
{
	using MGroup.MSolve.Discretization.Dofs;
	using MGroup.MSolve.Discretization.Entities;

	public class NodalBoundaryCondition : INodalBoundaryCondition<IDofType>
	{
		public IDofType DOF { get; }

		public INode Node { get; }

		public double Amount { get; }

		public NodalBoundaryCondition(INode node, IDofType dof, double amount)
		{
			this.Node = node;
			this.DOF = dof;
			this.Amount = amount;
		}

		public INodalBoundaryCondition<IDofType> WithAmount(double amount) => new NodalBoundaryCondition(Node, DOF, amount);
		INodalModelQuantity<IDofType> INodalModelQuantity<IDofType>.WithAmount(double amount) => new NodalBoundaryCondition(Node, DOF, amount);
	}
}
