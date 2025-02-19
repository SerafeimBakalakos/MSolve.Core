//TODO: Node and Element in FEM, XFEM, etc should implement corresponding interfaces (point, cell, etc). Then these interfaces
//      should be used here instead of dedicated VTK point and cell classes. This is mostly case right now.  
//      However the nodes must also be ordered and their IDs must start from 0.
//TODO: Porperly implement Dispose pattern
namespace MGroup.MSolve.Discretization.Meshes.Output.VTK
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;

	/// <summary>
	/// Writes meshes, scalars, vectors and tensor fields to .vtk output files. Then these files can be opened in Paraview.
	/// </summary>
	public class VtkFileWriter : IDisposable
	{
		public const string vtkReaderVersion = "4.1";
		private readonly StreamWriter writer;
		private readonly int dimension;
		private bool writeFieldsNext;

		public VtkFileWriter(string filePath, int dimension)
		{
			this.writer = new StreamWriter(filePath);
			writer.Write("# vtk DataFile Version ");
			writer.WriteLine(vtkReaderVersion);
			writer.WriteLine(filePath);
			writer.Write("ASCII\n\n");
			writeFieldsNext = false;

			Debug.Assert((dimension == 2) || (dimension == 3));
			this.dimension = dimension;
		}

		public void Dispose()
		{
			if (writer != null) writer.Dispose();
		}

		public void WriteMesh(IVtkMesh mesh) => WriteMesh(mesh.VtkPoints, mesh.VtkCells);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="points">They must be sorted on their IDs, which start from 0 and are contiguous.</param>
		/// <param name="cells"></param>
		public void WriteMesh(IReadOnlyList<VtkPoint> points, IReadOnlyList<VtkCell> cells)
		{
			if (writeFieldsNext) throw new InvalidOperationException("A mesh has already been written.");

			// Vertices 
			writer.WriteLine("DATASET UNSTRUCTURED_GRID");
			writer.WriteLine($"POINTS {points.Count} double");
			if (dimension == 2)
			{
				foreach (var point in points) writer.WriteLine($"{point.X} {point.Y} 0.0");
			}
			else
			{
				foreach (var point in points) writer.WriteLine($"{point.X} {point.Y} {point.Z}");
			}

			// Cell connectivity
			int cellDataCount = 0;
			foreach (var cell in cells) cellDataCount += 1 + cell.Vertices.Count;
			writer.WriteLine($"\nCELLS {cells.Count} {cellDataCount}");
			foreach (var cell in cells)
			{
				writer.Write(cell.Vertices.Count);
				foreach (var point in cell.Vertices)
				{
					writer.Write(' ');
					writer.Write(point.ID);
				}

				writer.WriteLine();
			}

			// Element types
			writer.WriteLine("\nCELL_TYPES " + cells.Count);
			foreach (var cell in cells) writer.WriteLine(cell.Code);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="pointValues">They must be in the exact same order as the nodes.</param>
		public void WriteScalarField(string fieldName, double[] pointValues)
		{
			WriteFieldsHeader(pointValues.Length);
			writer.WriteLine($"SCALARS {fieldName} double 1");
			writer.WriteLine("LOOKUP_TABLE default");
			for (int i = 0; i < pointValues.Length; ++i) writer.WriteLine(pointValues[i]);
			writer.WriteLine();
		}

		/// <summary>
		/// Tensor components are written as independent scalar fields, as I haven't found any advantage in using tensor datasets 
		/// in Paraview. By exporting each one as a different scalar field, better naming can be enforces, instead of 0, 1, etc. 
		/// indexing for each tensor component.
		/// </summary>
		/// <param name="fieldName">Each component will be prefixed by it. E.g. fieldName = "S": S_11, S_22, S_12.</param>
		/// <param name="pointValues">Each row correspond to a different node. They must be in the exact same order as the nodes.
		///     Columns 0, 1 and 2 are the tensor entries T11, T22, T12 respectively.</param>
		public void WriteTensor2DField(string fieldName, IReadOnlyList<double[]> pointTensors)
		{
			int numPoints = pointTensors.Count;
			WriteFieldsHeader(numPoints);

			// Component 11
			writer.WriteLine($"SCALARS {fieldName}_11 double 1");
			writer.WriteLine("LOOKUP_TABLE default");
			for (int i = 0; i < numPoints; ++i) writer.WriteLine(pointTensors[i][0]);
			writer.WriteLine();

			// Component 22
			writer.WriteLine($"SCALARS {fieldName}_22 double 1");
			writer.WriteLine("LOOKUP_TABLE default");
			for (int i = 0; i < numPoints; ++i) writer.WriteLine(pointTensors[i][1]);
			writer.WriteLine();

			// Component 12
			writer.WriteLine($"SCALARS {fieldName}_12 double 1");
			writer.WriteLine("LOOKUP_TABLE default");
			for (int i = 0; i < numPoints; ++i) writer.WriteLine(pointTensors[i][2]);
			writer.WriteLine();
		}

		/// <summary>
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="pointVectors">They must be in the exact same order as the nodes.</param>
		public void WriteVectorField(string fieldName, IReadOnlyList<double[]> pointVectors)
		{
			WriteFieldsHeader(pointVectors.Count);
			writer.WriteLine($"VECTORS {fieldName} double");
			if (dimension == 2)
			{
				for (int i = 0; i < pointVectors.Count; ++i)
				{
					writer.WriteLine($"{pointVectors[i][0]} {pointVectors[i][1]} 0.0");
				}
			}
			else
			{
				for (int i = 0; i < pointVectors.Count; ++i)
				{
					writer.WriteLine($"{pointVectors[i][0]} {pointVectors[i][1]} {pointVectors[i][2]}");
				}
			}

			writer.WriteLine();
		}

		private void WriteFieldsHeader(int numPoints)
		{
			if (!writeFieldsNext) // Fields header
			{
				writer.Write("\n\n");
				writer.WriteLine("POINT_DATA " + numPoints);
				writeFieldsNext = true;
			}
		}
	}
}
