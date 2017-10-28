﻿using System;

namespace Marbles
{
	/// <summary>
	/// This class defines what a variable is in Marbles.
	/// A variable has a data type associated with it and an id (name).
	/// </summary>
	public class Variable
	{
		/// <summary>
		/// The variable's associated data type.
		/// </summary>
		private SemanticCubeUtilities.DataTypes dataType;

		/// <summary>
		/// The variable's identifier.
		/// </summary>
		private String name;

		/// <summary>
		/// The variable's memory address
		/// </summary>
		private int memoryAddress;

		/// <summary>
		/// Variable constructor. A variable must have a name and a data type.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="dataType"></param>
		public Variable(String name, SemanticCubeUtilities.DataTypes dataType)
		{
			this.name = name;
			this.dataType = dataType;
		}

		/// <summary>
		/// Variable's data type getter method.
		/// </summary>
		public SemanticCubeUtilities.DataTypes GetDataType()
		{
			return dataType;
		}

		/// <summary>
		/// Variable's identifier (name) getter method.
		/// </summary>
		public String GetName()
		{
			return name;
		}

		/// <summary>
		/// Variable's identifier (name) setter method.
		/// </summary>
		public void SetName(string variableName)
		{
			name = variableName;
		}

		/// <summary>
		/// Sets or modifies the memory address value
		/// </summary>
		/// <param name="mem"></param>
		public void SetMemoryAddress(int mem)
		{
			memoryAddress = mem;
		}

		/// <summary>
		/// Retrieves the memory address
		/// </summary>
		public int GetMemoryAddress()
		{
			return memoryAddress;
		}
    }
}
