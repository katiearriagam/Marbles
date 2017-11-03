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
		private SemanticCubeUtilities.DataTypes returnType;
		private string name;
		private int location;
        private int quadrupleStart;
		private Dictionary<string, Variable> parameters = new Dictionary<string, Variable>();
		private Dictionary<string, Variable> localVariables = new Dictionary<string, Variable>();

		// only used for global
		private static Dictionary<string, Variable> globalVariables = new Dictionary<string, Variable>();
		private static Dictionary<string, Asset> assets = new Dictionary<string, Asset>();

		// number, boolean, text
		private int[] counterLocal = new int[] {0,0,0};
		private int[] counterTemp = new int[] {0,0,0};

		public enum FunctionScope
		{
			local = 0,
			temporary = 1
		}

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
		/// Getter for the function name/ID
		/// </summary>
		/// <returns></returns>
		public string GetName()
		{
			return this.name;
		}

		/// <summary>
		/// Getter for the function parameters
		/// </summary>
		/// <returns></returns>
		public Dictionary<string, Variable> GetParameters()
		{
			return this.parameters;
		}

		/// <summary>
		/// Getter for the local variables in the function
		/// </summary>
		/// <returns></returns>
		public Dictionary<string, Variable> GetLocalVariables()
		{
			return this.localVariables;
		}

		public Variable GetParameter(string varId)
		{
			if (parameters.ContainsKey(varId)) { return parameters[varId]; }

			throw new ArgumentException();
		}

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
        public bool AddLocalVariable(Variable variable)
		{
			if (!localVariables.ContainsKey(variable.GetName()) 
				&& !parameters.ContainsKey(variable.GetName())
				&& !FunctionDirectory.GlobalFunction().GetLocalVariables().ContainsKey(variable.GetName()))
			{
				localVariables.Add(variable.GetName(), variable);
				counterLocal[(int)variable.GetDataType() - 1]++;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Adds a variable to the global directory
		/// </summary>
		/// <param name="func"></param>
		/// <returns></returns>
		public bool AddGlobalVariable(Variable variable)
		{
			if (!FunctionDirectory.GlobalFunction().GetLocalVariables().ContainsKey(variable.GetName()))
			{
				globalVariables.Add(variable.GetName(), variable);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Function that accounts for a new temporary variable to the function
		/// </summary>
		/// <param name="dataType"></param>
		/// <returns></returns>
		public void AddTempVariable(SemanticCubeUtilities.DataTypes dataType)
		{
			if (dataType == SemanticCubeUtilities.DataTypes.number){ counterTemp[(int)SemanticCubeUtilities.DataTypes.number - 1]++; }
			if (dataType == SemanticCubeUtilities.DataTypes.text) { counterTemp[(int)SemanticCubeUtilities.DataTypes.text - 1]++; }
			if (dataType == SemanticCubeUtilities.DataTypes.boolean) { counterTemp[(int)SemanticCubeUtilities.DataTypes.boolean - 1]++; }
			else
			{
				throw new Exception("Can't add temporary. No valid type.");
			}
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
		public bool AddParameter(Variable variable)
		{
			if (!parameters.ContainsKey(variable.GetName()))
			{
				parameters.Add(variable.GetName(), variable);
				counterLocal[(int)variable.GetDataType() - 1]++;
				return true;
			}
			return false;
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
			return counterLocal[0] + counterLocal[1] + counterLocal[2] +
				   counterTemp[0] + counterTemp[1] + counterTemp[2];
		}
		
		/// <summary>
		/// Gets the data type of a memory address in the function
		/// </summary>
		/// <param name="address"></param>
		/// <param name="initialAddress"></param>
		/// <returns></returns>
		public SemanticCubeUtilities.DataTypes GetDataTypeFromAddress(int address, int initialAddress)
		{
			// cumulative limits
			int amountIntLocal = counterLocal[0];
			int amountBooleanLocal = amountIntLocal + counterLocal[1];
			int amountStringLocal = amountBooleanLocal + counterLocal[2];
			int amountIntTemp = amountStringLocal + counterTemp[0];
			int amountBooleanTemp = amountIntTemp + counterTemp[1];
			int amountStringTemp = amountBooleanTemp + counterTemp[2];

			// normalized address
			address = -initialAddress;

			if (address >= 0 && address < amountIntLocal) { return SemanticCubeUtilities.DataTypes.number; }
			if (address >= amountIntLocal && address < amountBooleanLocal) { return SemanticCubeUtilities.DataTypes.boolean; }
			if (address >= amountBooleanLocal && address < amountStringLocal) { return SemanticCubeUtilities.DataTypes.text; }
			if (address >= amountStringLocal && address < amountIntTemp) { return SemanticCubeUtilities.DataTypes.number; }
			if (address >= amountIntTemp && address < amountBooleanTemp) { return SemanticCubeUtilities.DataTypes.boolean; }
			if (address >= amountBooleanTemp && address < amountStringTemp) { return SemanticCubeUtilities.DataTypes.text; }

			return SemanticCubeUtilities.DataTypes.invalidDataType;
		}

		/// <summary>
		/// Remove all entries from local variables
		/// </summary>
		public void ReleaseLocalVariables()
		{
			localVariables.Clear();
		}

        public void Reset()
        {
            assets.Clear();
            globalVariables.Clear();
            localVariables.Clear();
            parameters.Clear();
            
            counterLocal = new int[] { 0, 0, 0 };
            counterTemp = new int[] { 0, 0, 0 };

            assets = new Dictionary<string, Asset>();
            globalVariables = new Dictionary<string, Variable>();
            localVariables = new Dictionary<string, Variable>();
            parameters = new Dictionary<string, Variable>();
        }
	}
}
