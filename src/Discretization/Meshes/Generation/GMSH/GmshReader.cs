namespace MGroup.MSolve.Core.Discretization.Meshes.Generation.GMSH
{
	using System;
	using System.IO;

	using MGroup.MSolve.Discretization.Meshes.Unstructured;

	/// <summary>
	/// Creates meshes by reading GMSH output files (.msh). Unrecognized GMSH cell types will be ignored along with any 1D cells
	/// in the .msh file, therefore some care is needed.
	/// </summary>
	public class GmshReader : IDisposable
	{
		private readonly int dimension;
		private readonly StreamReader reader;

		/// <summary>
		/// Opens the .msh file but doesn't read it.
		/// </summary>
		/// <param name="mshFilePath">
		/// The absolute path of the .msh file where GMSH has written the mesh data. The .msh file will not be modified.
		/// </param>
		public GmshReader(int dimension, string mshFilePath)
		{
			reader = new StreamReader(mshFilePath);
			this.dimension = dimension;
		}

		~GmshReader()
		{
			if (reader != null)
			{
				reader.Dispose();
			}
		}

		/// <summary>
		/// Reads the whole .msh file and converts it to MSolve mesh data.
		/// </summary>
		public UnstructuredMesh CreateMesh()
		{
			var mesh = new UnstructuredMesh();
			ReadNodes(mesh); // Vertices must be listed before cells
			ReadElements(mesh);
			return mesh;
		}


		public void Dispose()
		{
			if (reader != null)
			{
				reader.Dispose();
			}
		}

		private void ReadNodes(UnstructuredMesh mesh)
		{
			string line;

			// Find node segment
			while (true)
			{
				line = reader.ReadLine();
				if (line[0] == '$')
				{
					if (line.Equals("$Nodes")) break; // Next line will be the nodes count.
				}
			}

			// Read the vertices
			var numVertices = int.Parse(reader.ReadLine()); // Read it to move forward.
			while (true)
			{
				line = reader.ReadLine();
				if (line[0] == '$')
				{
					break; // This line is "$EndNodes". Next line will be the next segment.
				}
				else
				{
					// Line = nodeID x y z
					var words = line.Split(new char[] { ' ' });
					var id = int.Parse(words[0]) - 1; // MSolve uses 0-based indexing
					double[] coords;
					if (dimension == 2)
					{
						coords = new double[] { double.Parse(words[1]), double.Parse(words[2]) };
					}
					else
					{
						coords = new double[] { double.Parse(words[1]), double.Parse(words[2]), double.Parse(words[3]) };
					}

					mesh.Nodes.Add(coords);
				}
			}
		}

		/// <summary>
		/// It must be called after vertices are read.
		/// </summary>
		/// <param name="vertices"></param>
		private void ReadElements(UnstructuredMesh mesh)
		{
			string line;

			// Find cell segment
			while (true)
			{
				line = reader.ReadLine();
				if (line[0] == '$')
				{
					if (line.Equals("$Elements")) break; // Next line will be the number of cells.
				}
			}

			// Read the elements
			var numFauxCells = int.Parse(reader.ReadLine()); // Not all of them are actual 2D cells. Read it to move forward.
			var cellFactory = new GmshCellFactory(mesh.Nodes);
			while (true)
			{
				line = reader.ReadLine();
				if (line[0] == '$')
				{
					break; // This line is "$EndElements". Next line will be the next segment.
				}
				else
				{
					// Line = cellID cellCode tagsCount <tags>(0 or more) vertexIds(2 or more)
					var words = line.Split(new char[] { ' ' });
					var code = int.Parse(words[1]);
					var numTags = int.Parse(words[2]);

					var firstVertexPos = 3 + numTags;
					var numVertices = words.Length - firstVertexPos;
					var gmshVertexIDs = new int[numVertices];
					for (var i = 0; i < numVertices; ++i)
					{
						gmshVertexIDs[i] = int.Parse(words[firstVertexPos + i]) - 1; // MSolve uses 0-based indexing
					}

					var isValidCell = cellFactory.TryGetMSolveCellType(code, out var cellType);
					if (isValidCell)
					{
						var msolveNodeIDs = cellFactory.ChangeOrderOfElementNodes(cellType, gmshVertexIDs);
						mesh.Elements.Add((cellType, msolveNodeIDs));
					}
				}
			}
		}
	}
}
