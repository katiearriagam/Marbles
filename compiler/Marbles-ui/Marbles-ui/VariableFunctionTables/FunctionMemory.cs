using System;
using System.Collections.Generic;

namespace Marbles
{
	public class FunctionMemory
	{
		public Dictionary<int, object> memoryGlobal = new Dictionary<int, object>();
		public Dictionary<int, object> memoryTemporary = new Dictionary<int, object>();

		// Global Lower Limits
		const int lowestGlobalIntAddress = 0000;
		const int lowestGlobalStringAddress = 1000;
		const int lowestGlobalBoolAddress = 2000;

		// Global Upper Limits
		const int highestGlobalIntAddress = 0999;
		const int highestGlobalStringAddress = 1999;
		const int highestGlobalBoolAddress = 2999;

		// Global Current Indexes
		int currentGlobalIntAddress = 0000;
		int currentGlobalStringAddress = 1000;
		int currentGlobalBoolAddress = 2000;

		// Temporary Lower Limits
		const int lowestTempIntAddress = 3000;
		const int lowestTempStringAddress = 4000;
		const int lowestTempBoolAddress = 5000;

		// Temporary Upper Limits
		const int highestTempIntAddress = 3999;
		const int highestTempStringAddress = 4999;
		const int highestTempBoolAddress = 5999;

		// Temporary Current Indexes
		int currentTempIntAddress = 3000;
		int currentTempStringAddress = 4000;
		int currentTempBoolAddress = 5000;

		public enum FunctionMemoryScope
		{
			global = 0,
			temporary = 1
		}

		/// <summary>
		/// Gets the next available memory address for each scope and data type
		/// </summary>
		/// <param name="scope"></param>
		/// <param name="type"></param>
		public int GetNextAvailable(FunctionMemoryScope scope, SemanticCubeUtilities.DataTypes type)
		{
			switch (scope)
			{
				case FunctionMemoryScope.global:
					switch (type)
					{
						case SemanticCubeUtilities.DataTypes.number:
							if (currentGlobalIntAddress <= highestGlobalIntAddress)
								return currentGlobalIntAddress;
							break;
						case SemanticCubeUtilities.DataTypes.text:
							if (currentGlobalStringAddress <= highestGlobalStringAddress)
								return currentGlobalStringAddress;
							break;
						case SemanticCubeUtilities.DataTypes.boolean:
							if (currentGlobalBoolAddress <= highestGlobalBoolAddress)
								return currentGlobalBoolAddress;
							break;
						default:
							break;
					}
					break;

				case FunctionMemoryScope.temporary:
					switch (type)
					{
						case SemanticCubeUtilities.DataTypes.number:
							if (currentTempIntAddress <= highestTempIntAddress)
								return currentTempIntAddress;
							break;
						case SemanticCubeUtilities.DataTypes.text:
							if (currentTempStringAddress <= highestTempStringAddress)
								return currentTempStringAddress;
							break;
						case SemanticCubeUtilities.DataTypes.boolean:
							if (currentTempBoolAddress <= highestTempBoolAddress)
								return currentTempBoolAddress;
							break;
						default:
							break;
					}
					break;

				default:
					break;
			}
			return -1;
		}

		/// <summary>
		/// Loads value into a memory address
		/// </summary>
		/// <param name="memAddress"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public int SetMemory(int memAddress, object value)
		{
			// insert a global number in memory
			if (memAddress >= lowestGlobalIntAddress && memAddress <= highestGlobalIntAddress)
			{
				memoryGlobal[memAddress] = (int)value;
				currentGlobalIntAddress++;
				return memAddress;
			}

			// insert a temporary number in memory
			else if (memAddress >= lowestTempIntAddress && memAddress <= highestTempIntAddress)
			{
				memoryTemporary[memAddress] = (int)value;
				currentTempIntAddress++;
				return memAddress;
			}

			// insert a global string in memory
			else if (memAddress >= lowestGlobalStringAddress && memAddress <= highestGlobalStringAddress)
			{
				memoryGlobal[memAddress] = (string)value;
				currentGlobalStringAddress++;
				return memAddress;
			}

			// insert a temporary string in memory
			else if (memAddress >= lowestTempStringAddress && memAddress <= highestTempStringAddress)
			{
				memoryTemporary[memAddress] = (string)value;
				currentTempStringAddress++;
				return memAddress;
			}

			// insert a global boolean in memory
			else if (memAddress >= lowestGlobalBoolAddress && memAddress <= highestGlobalBoolAddress)
			{
				memoryGlobal[memAddress] = (bool)value;
				currentGlobalBoolAddress++;
				return memAddress;
			}

			// insert temporary boolean in memory
			else if (memAddress >= lowestTempBoolAddress && memAddress <= highestTempBoolAddress)
			{
				memoryTemporary[memAddress] = (bool)value;
				currentTempBoolAddress++;
				return memAddress;
			}

			throw new Exception("Out of memory");
		}

		/// <summary>
		/// Gets the value stored in a memory address
		/// </summary>
		/// <param name="memAddress"></param>
		/// <returns>Stored value</returns>
		public object GetValueFromAddress(int memAddress)
		{
			if (memoryGlobal.ContainsKey(memAddress))
			{
				return memoryGlobal[memAddress];
			}
			else if (memoryTemporary.ContainsKey(memAddress))
			{
				return memoryTemporary[memAddress];
			}
			// if memory address does not exist, throw error
			throw new Exception("Memory address not currently set");
		}

		/// <summary>
		/// Adds a variable to the global directory
		/// </summary>
		/// <param name="newGlobalVariable"></param>
		/// <returns>The variable address in the global directory</returns>
		public int AddGlobalVariable(Variable newGlobalVariable)
		{
			// retrieve the memory address where the variable will live
			int memorySpace = GetNextAvailable(FunctionMemoryScope.global, newGlobalVariable.GetDataType());

			// if memory space is insufficient, throw and exception
			if (memorySpace == -1) { throw new Exception("Out of global memory"); }

			// pass on the memory address meant for the variable
			newGlobalVariable.SetMemoryAddress(memorySpace);

			// try to add variable to global directory
			try
			{
				if (newGlobalVariable.GetDataType() == SemanticCubeUtilities.DataTypes.number)
				{
					try { memorySpace = SetMemory(memorySpace, 0); }
					catch (Exception e) { throw new Exception(e.Message); }
				}
				else if (newGlobalVariable.GetDataType() == SemanticCubeUtilities.DataTypes.text)
				{
					try { memorySpace = SetMemory(memorySpace, ""); }
					catch (Exception e) { throw new Exception(e.Message); }
				}
				else if (newGlobalVariable.GetDataType() == SemanticCubeUtilities.DataTypes.boolean)
				{
					try { memorySpace = SetMemory(memorySpace, false); }
					catch (Exception e) { throw new Exception(e.Message); }
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			return memorySpace;
		}


		public int FunctionMemorySize()
		{
			return memoryGlobal.Count + memoryTemporary.Count;
		}

		/// <summary>
		/// resets memory
		/// </summary>
		public void Reset()
		{
			memoryGlobal.Clear();
			memoryTemporary.Clear();

			// Global Current Indexes
			currentGlobalIntAddress = 0000;
			currentGlobalStringAddress = 1000;
			currentGlobalBoolAddress = 2000;

			// Temporary Current Indexes
			currentTempIntAddress = 3000;
			currentTempStringAddress = 4000;
			currentTempBoolAddress = 5000;
		}
	}
}