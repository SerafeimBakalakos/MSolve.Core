using MGroup.MSolve.Discretization;

namespace MGroup.MSolve.Solution
{
    /// <summary>
    /// Builder for classes implementing <see cref="ISolver"/>.
    /// Authors: Gerasimos Sotiropoulos, Serafeim Bakalakos
    /// </summary>
    public interface ISolverBuilder
    {
        /// <summary>
        /// Creates a new solver for the provided model.
        /// </summary>
        /// <param name="model">The model that will be analyzed.</param>
        ISolver BuildSolver(IModel model);
    }
}
