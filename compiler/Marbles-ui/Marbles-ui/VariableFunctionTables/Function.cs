using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbles
{
	/// <summary>
	/// Stores all relevant data for a user-defined function.
	/// </summary>
	public class Function
	{

        /// <summary>
        /// Memory of the function.
        /// </summary>
		public FunctionMemory memory = new FunctionMemory();

        /// <summary>
        /// Return type of the function.
        /// </summary>
		private SemanticCubeUtilities.DataTypes returnType;

        /// <summary>
        /// ID of the function.
        /// </summary>
		private string name;

        /// <summary>
        /// Location in global memory of the variable that represents the function.
        /// </summary>
		private int location;

        /// <summary>
        /// Quadruple number where the function's operations start.
        /// </summary>
		private int quadrupleStart;

        /// <summary>
        /// Local variables defined in the function's scope.
        /// </summary>
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
		/// Constructor for function.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="returnType"></param>
		public Function(string name, SemanticCubeUtilities.DataTypes returnType)
		{
			this.name = name;
			this.returnType = returnType;
		}

        /// <summary>
        /// Returns all assets.
        /// Called by <see cref="QuadrupleManager.ReadAssetId(string)"/>.
        /// </summary>
        /// <returns>A Dictionary of assets with their ID as the key.</returns>
        public Dictionary<string, Asset> GetAssets()
        {
            return assets;
        }

		/// <summary>
		/// Getter for the return data type of the function.
        /// Called by <see cref="QuadrupleManager"/> and <see cref="MemoryManager"/>.
		/// </summary>
		/// <returns>A <see cref="SemanticCubeUtilities.DataTypes" value./></returns>
		public SemanticCubeUtilities.DataTypes GetReturnType()
		{
			return returnType;
		}

		/// <summary>
		/// Getter for the function's name.
        /// Called by <see cref="Parser"/>, <see cref="QuadrupleManager"/>, <see cref="MemoryManager"/>, and <see cref="VirtualMachine"/>.
		/// </summary>
		/// <returns>The function's ID.</returns>
		public string GetName()
		{
			return name;
		}

		/// <summary>
		/// Getter for the function's parameters.
        /// Called by <see cref="QuadrupleManager"/> and <see cref="VirtualMachine"/>.
		/// </summary>
		/// <returns>A list of <see cref="Variable"/> objects.</returns>
		public List<Variable> GetParameters()
		{
			return parameters;
		}

		/// <summary>
		/// Getter for the local variables in the function.
        /// Called by <see cref="QuadrupleManager"/>.
		/// </summary>
		/// <returns>A dictionary of <see cref="Variable"/> objects with their ID as the key.</returns>
		public Dictionary<string, Variable> GetLocalVariables()
		{
			return this.localVariables;
		}

		/// <summary>
		/// Returns a <see cref="Variable"/> object found in the function's parameters with a given name.
        /// Called by <see cref="QuadrupleManager.VerifyVariableIDExists(string, out SemanticCubeUtilities.DataTypes, out int)"/>.
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
        /// Retrieves the global variables.
        /// Called by <see cref="QuadrupleManager"/>.
        /// </summary>
        /// <returns>A Dictionary of <see cref="Variable"/> objects with their ID as the key.</returns>
        public Dictionary<string, Variable> GetGlobalVariables()
        {
            return globalVariables;
        }

        /// <summary>
        /// Adds a new local variable to the function.
        /// Called by <see cref="QuadrupleManager.CreateFunction_LoadLocalVariable(string, Variable)"/>.
        /// </summary>
        /// <param name="variable"></param>
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
		/// Adds a variable to the global directory.
        /// Called by <see cref="MemoryManager.AddGlobalVariable(Variable)"/>.
		/// </summary>
		/// <param name="variable"></param>
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
		/// Adds an asset to the global list of assets.
        /// Called by <see cref="MemoryManager.SetAssetInMemory(Asset)"/>.
		/// </summary>
		/// <param name="asset"></param>
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
		/// Adds a new parameter to the function definition.
        /// Called by <see cref="QuadrupleManager.CreateFunction_LoadParameter(string, Variable)"/>.
		/// </summary>
		/// <param name="variable"></param>
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
		/// Sets the address of the function in global memory.
        /// Called by <see cref="MemoryManager.AddFunctionAsGlobalVariable(Function)"/>.
		/// </summary>
		/// <param name="mem"></param>
		public void SetLocation(int mem)
		{
			location = mem;
		}

		/// <summary>
		/// Gets the address of the function in global memory.
        /// Called by <see cref="FunctionDirectory"/> and <see cref="QuadrupleManager"/>.
		/// </summary>
		public int GetLocation()
		{
			return location;
		}

        /// <summary>
        /// Returns the quadruple number in which this function starts.
        /// Called by <see cref="QuadrupleManager.CallFunctionEnd"/>.
        /// </summary>
        public int GetQuadrupleStart()
        {
            return quadrupleStart;
        }

        /// <summary>
        /// Sets the quadruple number in which this function starts.
        /// Called by <see cref="Parser.CREATE_FUNCTION"/>.
        /// </summary>
        /// <param name="start"></param>
        public void SetQuadrupleStart(int start)
        {
            quadrupleStart = start;
        }

		/// <summary>
		/// Returns the size of the function based on its number of 
		/// local and temporary variables.
        /// Called by <see cref="MemoryManager"/>, <see cref="QuadrupleManager"/>, and <see cref="VirtualMachine"/>.
		/// </summary>
		/// <returns>The size of the function.</returns>
		public int GetFunctionSize()
		{
			return memory.FunctionMemorySize();
		}
		

		/// <summary>
		/// Remove all entries from local variables.
        /// Called by <see cref="QuadrupleManager.ExitFunction"/>.
		/// </summary>
		public void ReleaseLocalVariables()
		{
			localVariables.Clear();
		}

        /// <summary>
        /// Cleans memory.
        /// Called by <see cref="FunctionDirectory.Reset"/>.
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
