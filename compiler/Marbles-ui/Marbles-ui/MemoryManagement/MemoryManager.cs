using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbles.MemoryManagement
{
	static class MemoryManager
	{
		public enum MemoryScope
		{
			global = 0,
			local = 1,
			temporary = 2,
			constant = 3
		}

		public static Dictionary<int, object> memory;
		public static Dictionary<int, object> memoryLocal;
		public static Dictionary<int, object> memoryConstant;

		// Global Upper Limits
		static int highestGlobalIntAddress = 1999;
		static int highestGlobalStringAddress = 2999;
		static int highestGlobalBoolAddress = 3999;

		// Global Current Indexes
		static int currentGlobalIntAddress = 1000;
		static int currentGlobalStringAddress = 2000;
		static int currentGlobalBoolAddress = 3000;

		// Local Upper Limits
		static int highestLocalAddress = 6999;

		// Local Current Indexes
		static int currentLocalAddress = 6000;

		// Temporary Upper Limits
		static int highestTempIntAddress = 7999;
		static int highestTempStringAddress = 8999;
		static int highestTempBoolAddress = 9999;

		// Temporary Current Indexes
		static int currentTempIntAddress = 7000;
		static int currentTempStringAddress = 8000;
		static int currentTempBoolAddress = 9000;

		// Constant Upper Limits
		static int highestConstantIntAddress = 10999;
		static int highestConstantStringAddress = 11999;
		static int highestConstantBoolAddress = 12001;

		// Constant Current Indexes
		static int currentConstantIntAddress = 10000;
		static int currentConstantStringAddress = 11000;
		static int currentConstantBoolAddress = 12000;

		/// <summary>
		/// Gets the next available memory address for each scope and data type
		/// </summary>
		/// <param name="scope"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static int GetNextAvailable(MemoryScope scope, SemanticCubeUtilities.DataTypes type)
		{
			switch (scope)
			{
				case MemoryScope.global:
					switch (type)
					{
						case SemanticCubeUtilities.DataTypes.number:
							if (currentGlobalIntAddress < highestGlobalIntAddress)
								return currentGlobalIntAddress;
							break;
						case SemanticCubeUtilities.DataTypes.text:
							if (currentGlobalStringAddress < highestGlobalStringAddress)
								return currentGlobalStringAddress;
							break;
						case SemanticCubeUtilities.DataTypes.boolean:
							if (currentGlobalBoolAddress < highestGlobalBoolAddress)
								return currentGlobalBoolAddress;
							break;
						default:
							break;
					}
					break;

				case MemoryScope.temporary:
					switch (type)
					{
						case SemanticCubeUtilities.DataTypes.number:
							if (currentTempIntAddress < highestTempIntAddress)
								return currentTempIntAddress;
							break;
						case SemanticCubeUtilities.DataTypes.text:
							if (currentTempStringAddress < highestTempStringAddress)
								return currentTempStringAddress;
							break;
						case SemanticCubeUtilities.DataTypes.boolean:
							if (currentTempBoolAddress < highestTempBoolAddress)
								return currentTempBoolAddress;
							break;
						default:
							break;
					}
					break;

				case MemoryScope.constant:
					switch (type)
					{
						case SemanticCubeUtilities.DataTypes.number:
							if (currentConstantIntAddress < highestConstantIntAddress)
								return currentConstantIntAddress;
							break;
						case SemanticCubeUtilities.DataTypes.text:
							if (currentConstantStringAddress < highestConstantStringAddress)
								return currentConstantStringAddress;
							break;
						case SemanticCubeUtilities.DataTypes.boolean:
							if (currentConstantBoolAddress < highestConstantBoolAddress)
								return currentConstantBoolAddress;
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

		public static int SetMemory(int memAddress, object value)
		{
			// insert a global/temporary number in memory
			if ((memAddress >= currentGlobalIntAddress && memAddress <= highestGlobalIntAddress) ||
				(memAddress >= currentTempIntAddress && memAddress <= highestTempIntAddress))
			{
				memory[memAddress] = (int)value;
				return memAddress;
			}

			// insert a global/temporary string in memory
			else if ((memAddress >= currentGlobalStringAddress && memAddress <= highestGlobalStringAddress) ||
				(memAddress >= currentTempStringAddress && memAddress <= highestTempStringAddress))
			{
				memory[memAddress] = (string)value;
				return memAddress;
			}

			// insert a global/temporary boolean in memory
			else if ((memAddress >= currentGlobalBoolAddress && memAddress <= highestGlobalBoolAddress) ||
				(memAddress >= currentTempBoolAddress && memAddress <= highestTempBoolAddress))
			{
				memory[memAddress] = (bool)value;
				return memAddress;
			}

			// insert numeric constant
			else if (memAddress >= currentConstantIntAddress && memAddress <= highestConstantIntAddress)
			{
				if (!memoryConstant.ContainsValue(value))
				{
					memoryConstant[memAddress] = (int)value;
					return memAddress;
				}
				else
				{
					return memoryConstant.FirstOrDefault(x => x.Value == value).Key;
				}
			}

			// insert string constant
			else if (memAddress >= currentConstantStringAddress && memAddress <= highestConstantStringAddress)
			{
				if (!memoryConstant.ContainsValue(value))
				{
					memoryConstant[memAddress] = (string)value;
					return memAddress;
				}
				else
				{
					return memoryConstant.FirstOrDefault(x => x.Value == value).Key;
				}
			}

			// insert boolean constant
			else if (memAddress >= currentConstantBoolAddress && memAddress <= highestConstantBoolAddress)
			{
				if (!memoryConstant.ContainsValue(value))
				{
					memoryConstant[memAddress] = (bool)value;
					return memAddress;
				}
				else
				{
					return memoryConstant.FirstOrDefault(x => x.Value == value).Key;
				}
			}
			return -1;
		}

		/// <summary>
		/// Allocates the memory for a new function (in locals)
		/// </summary>
		/// <param name="func"></param>
		public static void AllocateLocalMemory(Function func)
		{
			if (currentLocalAddress + func.GetFunctionSize() < highestLocalAddress)
			{
				currentLocalAddress += func.GetFunctionSize();
			}
			else
			{
				throw new Exception("Local memory overflow.");
			}
		}

		/// <summary>
		/// Removes a function from local memory mapping
		/// </summary>
		/// <param name="func"></param>
		public static void DeallocateLocalMemory(Function func)
		{
			if (currentLocalAddress + func.GetFunctionSize() >= 6000)
			{
				currentLocalAddress -= func.GetFunctionSize();
			}
			else
			{
				throw new Exception("Trying to deallocate memory from non-local scope.");
			}
		}
	}
}
