namespace MGroup.MSolve.Discretization.BoundaryConditions
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using MGroup.MSolve.Discretization.Entities;
	using MGroup.MSolve.Discretization.Dofs;

	public abstract class TransientBoundaryConditionSet<T> : ITransientBoundaryConditionSet<T> where T : IDofType
	{
		protected readonly IEnumerable<IBoundaryConditionSet<T>> boundaryConditionSets;
		protected readonly Func<double, double, double> timeFunc;

		public TransientBoundaryConditionSet(IEnumerable<IBoundaryConditionSet<T>> boundaryConditionSets, Func<double, double, double> timeFunc)
		{
			this.boundaryConditionSets = boundaryConditionSets;
			this.timeFunc = timeFunc;
		}

		public double CurrentTime { get; set; } = 0;

		//public IEnumerable<IDomainBoundaryCondition<T>> EnumerateDomainBoundaryConditions()
		//  => boundaryConditionSets.SelectMany(x => x.EnumerateDomainBoundaryConditions().Select(bc => bc.WithMultiplier(timeFunc(CurrentTime, bc.Multiplier))));

		public IEnumerable<INodalBoundaryCondition<T>> EnumerateNodalBoundaryConditions(IEnumerable<IElementType> elements)
			=> boundaryConditionSets.SelectMany(x => x.EnumerateNodalBoundaryConditions(elements).Select(bc => bc.WithAmount(timeFunc(CurrentTime, bc.Amount))));

		public abstract IEnumerable<INodalNeumannBoundaryCondition<T>> EnumerateEquivalentNodalNeumannBoundaryConditions(IEnumerable<IElementType> elements, IEnumerable<(int NodeID, IDofType DOF)> dofsToExclude);

		public abstract IBoundaryConditionSet<T> CreateBoundaryConditionSetOfSubdomain(ISubdomain subdomain);
	}
}
