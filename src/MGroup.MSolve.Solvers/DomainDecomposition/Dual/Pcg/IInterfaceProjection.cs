﻿using System;
using System.Collections.Generic;
using MGroup.LinearAlgebra.Vectors;

namespace MGroup.MSolve.Solvers.DomainDecomposition.Dual.Pcg
{
    internal interface IInterfaceProjection
    {
        Vector CalcParticularLagrangeMultipliers(Vector rigidBodyModesWork);

        Vector CalcRigidBodyModesCoefficients(Vector flexibilityTimeslagrangeMultipliers,
            Vector boundaryDisplacements);

        void InvertCoarseProblemMatrix();

        void ProjectVector(Vector original, Vector projected, bool transposeProjector);
    }
}
