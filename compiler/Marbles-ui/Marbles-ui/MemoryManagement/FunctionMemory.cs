using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Marbles
{
    /// <summary>
    /// Manages the memory of Functions in the program.
    /// </summary>
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
        /// Gets the next available memory address for each scope and <see cref="SemanticCubeUtilities.DataTypes"/> value.
        /// Called by <see cref="QuadrupleManager"/> to allot a space in a function's memory to a value.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="type"></param>
        /// <returns>A memory address.</returns>
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
        /// Loads value into a function's memory address.
        /// Called by instructions that store a new value in a function's memory.
        /// </summary>
        /// <param name="memAddress"></param>
        /// <param name="value"></param>
        /// <returns>Address where the value was stored.</returns>
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
        /// Gets the value stored in a memory address.
        /// Called by <see cref="MemoryManager.GetValueFromAddress(int)"/> to retrieve values at a specific memory address.
        /// </summary>
        /// <param name="memAddress"></param>
        /// <returns>The stored value</returns>
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
        /// Given a memory address, returns the <see cref="Type"/> of the object stored in that address.
        /// </summary>
        /// <param name="memAddress"></param>
        /// <returns>A <see cref="Type"/>, or null if the memory address is invalid.</returns>
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
        /// Gets <see cref="memoryGlobal"/>.
        /// </summary>
        /// <returns>A dictionary representing the functions' global memory.</returns>
		public Dictionary<int, object> GetMemoryGlobal()
		{
			return memoryGlobal;
		}

        /// <summary>
        /// Gets <see cref="memoryTemporary"/>.
        /// </summary>
        /// <returns>A dictionary representing the functions' temporary memory.</returns>
		public Dictionary<int, object> GetMemoryTemporary()
		{
			return memoryTemporary;
		}

        /// <summary>
        /// Gets <see cref="totalSize"/>.
        /// </summary>
        /// <returns>The size in memory of the function.</returns>
		public int FunctionMemorySize()
		{
			return totalSize;
		}

        /// <summary>
        /// Given a memory address, returns the index it is found at.
        /// Called by <see cref="MemoryManager.FunctionMemoryToMemoryManager"/>.
        /// </summary>
        /// <param name="address"></param>
        /// <returns>A zero-based index value.</returns>
		public int GetIndexFromMemoryList(int address)
		{
			List<int> memoryList = MemoryKeysToList();
			int counter = -1;
            bool found = false;
			foreach (int key in memoryList)
			{
				counter++;
				if (key == address)
				{
                    found = true;
					break;
				}
			}

			return found ? counter : -1;
		}

        /// <summary>
        /// Combines both global and temporary memory addresses into a single, sorted <see cref="List{T}"/>.
        /// Called by <see cref="FunctionMemory.GetIndexFromMemoryList(int)"/>.
        /// </summary>
        /// <returns>A sorted <see cref="List{T}"/>.</returns>
		public List<int> MemoryKeysToList()
		{
			List<int> keys = memoryGlobal.Keys.ToList();
			keys = keys.Concat(memoryTemporary.Keys.ToList()).ToList();
			keys.Sort();
			return keys;
		}

		/// <summary>
		/// Resets the function's memory.
        /// Called by <see cref="Function.Reset"/>.
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

        /// <summary>
        /// Prints the function's memory.
        /// Called by <see cref="QuadrupleManager.ExitFunction"/>.
        /// </summary>
        /// <param name="id"></param>
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