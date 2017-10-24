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
		private Dictionary<string, Variable> parameters;
		private Dictionary<string, Variable> localVariables;

		/// <summary>
		/// Constructor for function
		/// </summary>
		/// <param name="name"></param>
		/// <param name="returnType"></param>
		public Function(string name, SemanticCubeUtilities.DataTypes returnType)
		{
			this.name = name;
			this.returnType = returnType;
			this.parameters = new Dictionary<string, Variable>();
			this.localVariables = new Dictionary<string, Variable>();
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

		/// <summary>
		/// Function that adds a new local variable to the function
		/// </summary>
		/// <param name="variable"></param>
		/// <returns></returns>
		public bool AddLocalVariable(Variable variable)
		{
			if (!localVariables.ContainsKey(""))
			{
				localVariables.Add("", variable);
				return true;
			}
			return false;
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
	}
}
