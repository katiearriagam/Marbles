using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbles
{
	/// <summary>
	/// Class that stores all the relevant data for a function.
	/// </summary>
	public class Function
	{
		public FunctionMemory memory = new FunctionMemory();
		private SemanticCubeUtilities.DataTypes returnType;
		private string name;

        /// <summary>
        /// Location in global memory of the variable that represents the function.
        /// </summary>
		private int location;

		private int quadrupleStart;

		private Dictionary<string, Variable> localVariables = new Dictionary<string, Variable>();
		
		/// <summary>
		/// Dictionary of parameters with the parameter name as the ky and a tuple containing
		/// the index of the parameter and a Variable object with the parameter's information.
		/// </summary>
		private List<Variable> parameters = new List<Variable>();

		// only used for global
		private static Dictionary<string, Variable> globalVariables = new Dictionary<string, Variable>();
		private static Dictionary<string, Asset> assets = new Dictionary<string, Asset>();

		/// <summary>
		/// Constructor for function
		/// </summary>
		/// <param name="name"></param>
		/// <param name="returnType"></param>
		public Function(string name, SemanticCubeUtilities.DataTypes returnType)
		{
			this.name = name;
			this.returnType = returnType;
		}

        /// <summary>
        /// Returns the dictionary of assets.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Asset> GetAssets()
        {
            return assets;
        }

		/// <summary>
		/// Getter for the return data type of the function
		/// </summary>
		/// <returns></returns>
		public SemanticCubeUtilities.DataTypes GetReturnType()
		{
			return this.returnType;
		}

		/// <summary>
		/// Getter for the function's name
		/// </summary>
		/// <returns></returns>
		public string GetName()
		{
			return this.name;
		}

		/// <summary>
		/// Getter for the function's parameters
		/// </summary>
		/// <returns></returns>
		public List<Variable> GetParameters()
		{
			return parameters;
		}

		/// <summary>
		/// Getter for the local variables in the function
		/// </summary>
		/// <returns></returns>
		public Dictionary<string, Variable> GetLocalVariables()
		{
			return this.localVariables;
		}

		/// <summary>
		/// Returns a Variable object found in the function's parameters with a given name.
		/// </summary>
		/// <param name="varId"></param>
		/// <returns></returns>
		public Variable GetParameter(string varId)
		{
			foreach (Variable param in parameters)
			{
				if (param.GetName() == varId)
				{
					return param;
				}
			}

			throw new ArgumentException();
		}

		/// <summary>
		/// Returns a Variable object found in the function's variables table with a given name.
		/// </summary>
		/// <param name="varId"></param>
		/// <returns></returns>
		public Variable GetLocalVariable(string varId)
		{
			if (localVariables.ContainsKey(varId)) { return localVariables[varId]; }

			throw new ArgumentException();
		}

        /// <summary>
        /// Retrieves the static global variables
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Variable> GetGlobalVariables()
        {
            return globalVariables;
        }

        /// <summary>
        /// Function that adds a new local variable to the function
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public void AddLocalVariable(Variable variable)
		{
			if (parameters.Any((p) => p.GetName() == variable.GetName()))
			{
				throw new Exception("A variable named " + variable.GetName() + "already exists in the parameters.");
			}
			else if (localVariables.ContainsKey(variable.GetName()))
			{
				throw new Exception("A variable named " + variable.GetName() + "already exists in function's variables.");
			}
			else if (globalVariables.ContainsKey(variable.GetName()))
			{
				throw new Exception("A variable named " + variable.GetName() + "already exists in global variables.");
			}
			else if (assets.ContainsKey(variable.GetName()))
			{
				throw new Exception("A variable named " + variable.GetName() + "already exists in asset variables.");
			}
			else
			{
				localVariables.Add(variable.GetName(), variable);
			}
		}

		/// <summary>
		/// Adds a variable to the global directory
		/// </summary>
		/// <param name="func"></param>
		/// <returns></returns>
		public void AddGlobalVariable(Variable variable)
		{
			// throw exception if variable name already exists in globals
			if (globalVariables.ContainsKey(variable.GetName()))
			{
				throw new Exception(variable.GetName() + " already exists in global variables.");
			}
			// throw exception if variable name already exists in assets
			else if (assets.ContainsKey(variable.GetName()))
			{
				throw new Exception(variable.GetName() + " already exists in asset variables.");
			}
			globalVariables.Add(variable.GetName(), variable);
		}

		/// <summary>
		/// Adds a variable to the asset directory
		/// </summary>
		/// <param name="asset"></param>
		/// <returns></returns>
		public void AddAssetVariable(Asset asset)
		{
			// throw exception if variable name already exists in globals
			if (globalVariables.ContainsKey(asset.GetID()))
			{
				throw new Exception("A variable named " + asset.GetID() + "already exists in global variables.");
			}
			// throw exception if variable name already exists in assets
			else if (assets.ContainsKey(asset.GetID()))
			{
				throw new Exception("A variable named " + asset.GetID() + "already exists in asset variables.");
			}
			assets.Add(asset.GetID(), asset);
		}

		/// <summary>
		/// Function that returns the number of variables in the function
		/// </summary>
		/// <returns></returns>
		public int CountLocalVariables()
		{
			return this.localVariables.Count;
		}

		/// <summary>
		/// Function that adds a new parameter to the function
		/// </summary>
		/// <param name="variable"></param>
		/// <returns></returns>
		public void AddParameter(Variable variable)
		{
			if (parameters.Any((p) => p.GetName() == variable.GetName()))
			{
				throw new Exception("A variable named " + variable.GetName() + "already exists in already exists in the parameters.");
			}
			else if (localVariables.ContainsKey(variable.GetName()))
			{
				throw new Exception("A variable named " + variable.GetName() + "already exists in function's variables.");
			}
			else if (globalVariables.ContainsKey(variable.GetName()))
			{
				throw new Exception("A variable named " + variable.GetName() + "already exists in global variables.");
			}
			else if (assets.ContainsKey(variable.GetName()))
			{
				throw new Exception("A variable named " + variable.GetName() + "already exists in asset variables.");
			}
			else
			{
				parameters.Add(variable);
			}
		}

		/// <summary>
		/// Function that returns the number of parameters in the function
		/// </summary>
		/// <returns></returns>
		public int CountParameters()
		{
			return this.parameters.Count;
		}

		/// <summary>
		/// Sets or modifies the location in quadruples
		/// </summary>
		/// <param name="mem"></param>
		public void SetLocation(int mem)
		{
			location = mem;
		}

		/// <summary>
		/// Retrieves the location in quadruples
		/// </summary>
		public int GetLocation()
		{
			return location;
		}

        /// <summary>
        /// Returns the quadruple number in which this function starts.
        /// </summary>
        public int GetQuadrupleStart()
        {
            return quadrupleStart;
        }

        /// <summary>
        /// Sets the quadruple number in which this function starts.
        /// </summary>
        /// <param name="start"></param>
        public void SetQuadrupleStart(int start)
        {
            quadrupleStart = start;
        }

		/// <summary>
		/// Returns the size of the function based on its number of 
		/// local and temporary variables.
		/// </summary>
		/// <returns></returns>
		public int GetFunctionSize()
		{
			return memory.FunctionMemorySize();
		}
		

		/// <summary>
		/// Remove all entries from local variables
		/// </summary>
		public void ReleaseLocalVariables()
		{
			localVariables.Clear();
		}

        /// <summary>
        /// Remove all entries from the memory simulation
        /// </summary>
        public void ReleaseMemory()
        {
            memory.Reset();
        }

        /// <summary>
        /// Clean memory
        /// </summary>
        public void Reset()
        {
            assets.Clear();
            globalVariables.Clear();
            localVariables.Clear();
            parameters.Clear();

			memory.Reset();

            assets = new Dictionary<string, Asset>();
            globalVariables = new Dictionary<string, Variable>();
            localVariables = new Dictionary<string, Variable>();
            parameters = new List<Variable>();
        }
	}
}
