using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Marbles
{
	public class FunctionMemory
	{
		public Dictionary<int, object> memoryGlobal = new Dictionary<int, object>();
		public Dictionary<int, object> memoryTemporary = new Dictionary<int, object>();

        // Global Lower Limits
        const int lowestGlobalIntAddress = 100000;
        const int lowestGlobalStringAddress = 101000;
        const int lowestGlobalBoolAddress = 102000;

        // Global Upper Limits
        const int highestGlobalIntAddress = 100999;
        const int highestGlobalStringAddress = 101999;
        const int highestGlobalBoolAddress = 102999;

        // Global Current Indexes
        int currentGlobalIntAddress = 100000;
        int currentGlobalStringAddress = 101000;
        int currentGlobalBoolAddress = 102000;

        // Temporary Lower Limits
        const int lowestTempIntAddress = 103000;
        const int lowestTempStringAddress = 104000;
        const int lowestTempBoolAddress = 105000;

        // Temporary Upper Limits
        const int highestTempIntAddress = 103999;
        const int highestTempStringAddress = 104999;
        const int highestTempBoolAddress = 105999;

        // Temporary Current Indexes
        int currentTempIntAddress = 103000;
        int currentTempStringAddress = 104000;
        int currentTempBoolAddress = 105000;

        // Stores the total size of the function
        int totalSize = 0;

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
                if (!memoryGlobal.ContainsKey(memAddress)) { totalSize++; }
				memoryGlobal[memAddress] = (int)value;
				currentGlobalIntAddress++;
				return memAddress;
			}

			// insert a temporary number in memory
			else if (memAddress >= lowestTempIntAddress && memAddress <= highestTempIntAddress)
			{
                if (!memoryTemporary.ContainsKey(memAddress)) { totalSize++; }
                memoryTemporary[memAddress] = (int)value;
				currentTempIntAddress++;
				return memAddress;
			}

			// insert a global string in memory
			else if (memAddress >= lowestGlobalStringAddress && memAddress <= highestGlobalStringAddress)
			{
                if (!memoryGlobal.ContainsKey(memAddress)) { totalSize++; }
                memoryGlobal[memAddress] = (string)value;
				currentGlobalStringAddress++;
				return memAddress;
			}

			// insert a temporary string in memory
			else if (memAddress >= lowestTempStringAddress && memAddress <= highestTempStringAddress)
			{
                if (!memoryTemporary.ContainsKey(memAddress)) { totalSize++; }
                memoryTemporary[memAddress] = (string)value;
				currentTempStringAddress++;
				return memAddress;
			}

			// insert a global boolean in memory
			else if (memAddress >= lowestGlobalBoolAddress && memAddress <= highestGlobalBoolAddress)
			{
                if (!memoryGlobal.ContainsKey(memAddress)) { totalSize++; }
                memoryGlobal[memAddress] = (bool)value;
				currentGlobalBoolAddress++;
				return memAddress;
			}

			// insert temporary boolean in memory
			else if (memAddress >= lowestTempBoolAddress && memAddress <= highestTempBoolAddress)
			{
                if (!memoryTemporary.ContainsKey(memAddress)) { totalSize++; }
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
        /// Returns a data type of an object stored in memory given a memory address.
        /// </summary>
        /// <param name="memAddress"></param>
        /// <returns>A data type, or null if the memory address is invalid.</returns>
        public static Type GetTypeFromAddress(int memAddress)
        {
            if ((memAddress >= lowestGlobalIntAddress && memAddress <= highestGlobalIntAddress)
                || (memAddress >= lowestTempIntAddress && memAddress <= highestTempIntAddress))
            {
                return typeof(int);
            }
            else if (memAddress >= lowestGlobalStringAddress && memAddress <= highestGlobalStringAddress
                || (memAddress >= lowestTempStringAddress && memAddress <= highestTempStringAddress))
            {
                return typeof(string);
            }
            else if (memAddress >= lowestGlobalBoolAddress && memAddress <= highestGlobalBoolAddress
                || (memAddress >= lowestTempBoolAddress && memAddress <= highestTempBoolAddress))
            {
                return typeof(bool);
            }

            return null;
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
                totalSize++;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			return memorySpace;
		}

		public Dictionary<int, object> GetMemoryGlobal()
		{
			return memoryGlobal;
		}

		public Dictionary<int, object> GetMemoryTemporary()
		{
			return memoryTemporary;
		}

		public int FunctionMemorySize()
		{
			return totalSize;
		}

		public int GetIndexFromMemoryList(int address)
		{
			List<int> memoryList = MemoryKeysToList();
			int counter = -1; 
			foreach (int key in memoryList)
			{
				counter++;
				if (key == address)
				{
					break;
				}
			}
			return counter;
		}

		public List<int> MemoryKeysToList()
		{
			List<int> keys = memoryGlobal.Keys.ToList();
			keys.Concat(memoryGlobal.Keys.ToList());
			keys.Sort();
			return keys;
		}
		/// <summary>
		/// resets memory
		/// </summary>
		public void Reset()
		{
			memoryGlobal.Clear();
			memoryTemporary.Clear();
  
			// Global Current Indexes
			currentGlobalIntAddress = lowestGlobalIntAddress;
			currentGlobalStringAddress = lowestGlobalStringAddress;
			currentGlobalBoolAddress = lowestGlobalBoolAddress;

			// Temporary Current Indexes
			currentTempIntAddress = lowestTempIntAddress;
			currentTempStringAddress = lowestTempStringAddress;
			currentTempBoolAddress = lowestTempBoolAddress;
		}

        public void PrintMemory(string id)
        {
            Debug.WriteLine("--- START LOCAL MEMORY [" + id + "] --- ");
            Debug.WriteLine("\t>--- GLOBAL ---< ");
            foreach (KeyValuePair<int, object> kvp in memoryGlobal)
            {
                Debug.WriteLine("\t" + kvp.Key + "[" + SemanticCubeUtilities.GetDataTypeFromType(kvp.Value.GetType()).ToString() + "]" + " -> " + kvp.Value.ToString());
            }

            Debug.WriteLine("\n\t>--- TEMPORARY ---< ");
            foreach (KeyValuePair<int, object> kvp in memoryTemporary)
            {
                Debug.WriteLine("\t" + kvp.Key + "[" + SemanticCubeUtilities.GetDataTypeFromType(kvp.Value.GetType()).ToString() + "]" + " -> " + kvp.Value.ToString());
            }
            Debug.WriteLine("--- END LOCAL MEMORY [" + id + "] --- \n");
        }
    }
}