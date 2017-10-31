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

        public enum AssetAttributes
        {
            x = 0,
            y = 1,
            width = 2,
            height = 3,
            rotation = 4,
            number = 5,
            label = 6
        }

        public static Dictionary<int, object> memory;
		public static Dictionary<int, object> memoryLocal;
		public static Dictionary<int, object> memoryConstant;

		// Global Lower Limits
		const int lowestGlobalIntAddress = 1000;
		const int lowestGlobalStringAddress = 2000;
		const int lowestGlobalBoolAddress = 3000;

		// Global Upper Limits
		const int highestGlobalIntAddress = 1999;
		const int highestGlobalStringAddress = 2999;
		const int highestGlobalBoolAddress = 3999;

		// Global Current Indexes
		static int currentGlobalIntAddress = 1000;
		static int currentGlobalStringAddress = 2000;
		static int currentGlobalBoolAddress = 3000;

		// Local Limits
		const int lowestLocalAddress = 4000;
		const int highestLocalAddress = 6999;

		// Local Current Index
		static int currentLocalAddress = 6000;

		// Temporary Lower Limits
		const int lowestTempIntAddress = 7999;
		const int lowestTempStringAddress = 8999;
		const int lowestTempBoolAddress = 9999;

		// Temporary Upper Limits
		const int highestTempIntAddress = 7999;
		const int highestTempStringAddress = 8999;
		const int highestTempBoolAddress = 9999;

		// Temporary Current Indexes
		static int currentTempIntAddress = 7000;
		static int currentTempStringAddress = 8000;
		static int currentTempBoolAddress = 9000;

		// Constant Upper Limits
		const int lowestConstantIntAddress = 10000;
		const int lowestConstantStringAddress = 11000;
		const int lowestConstantBoolAddress = 12000;

		// Constant Upper Limits
		const int highestConstantIntAddress = 10999;
		const int highestConstantStringAddress = 11999;
		const int highestConstantBoolAddress = 12001;

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
			if ((memAddress >= lowestGlobalIntAddress && memAddress <= highestGlobalIntAddress) ||
				(memAddress >= lowestTempIntAddress && memAddress <= highestTempIntAddress))
			{
				memory[memAddress] = (int)value;
				return memAddress;
			}

			// insert a global/temporary string in memory
			else if ((memAddress >= lowestGlobalStringAddress && memAddress <= highestGlobalStringAddress) ||
				(memAddress >= lowestTempStringAddress && memAddress <= highestTempStringAddress))
			{
				memory[memAddress] = (string)value;
				return memAddress;
			}

			// insert a global/temporary boolean in memory
			else if ((memAddress >= lowestGlobalBoolAddress && memAddress <= highestGlobalBoolAddress) ||
				(memAddress >= lowestTempBoolAddress && memAddress <= highestTempBoolAddress))
			{
				memory[memAddress] = (bool)value;
				return memAddress;
			}

			// insert numeric constant
			else if (memAddress >= lowestConstantIntAddress && memAddress <= highestConstantIntAddress)
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
			else if (memAddress >= lowestConstantStringAddress && memAddress <= highestConstantStringAddress)
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
			else if (memAddress >= lowestConstantBoolAddress && memAddress <= highestConstantBoolAddress)
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

        public static int AddConstantInt(int constant)
        {
            int constMem = GetNextAvailable(MemoryScope.global, SemanticCubeUtilities.DataTypes.number);
            return SetMemory(constMem, constant);
        }

        public static int AddConstantString(string constant)
        {
            int constMem = GetNextAvailable(MemoryScope.global, SemanticCubeUtilities.DataTypes.text);
            return SetMemory(constMem, constant);
        }

        public static int AddConstantBool(bool constant)
        {
            int constMem = GetNextAvailable(MemoryScope.global, SemanticCubeUtilities.DataTypes.boolean);
            return SetMemory(constMem, constant);
        }

        public static int AttributeToOffset(string attr)
        {
            switch (attr)
            {
                case "x":
                    return (int)AssetAttributes.x;
                case "y":
                    return (int)AssetAttributes.y;
                case "width":
                    return (int)AssetAttributes.width;
                case "height":
                    return (int)AssetAttributes.height;
                case "rotation":
                    return (int)AssetAttributes.rotation;
                case "number":
                    return (int)AssetAttributes.number;
                case "label":
                    return (int)AssetAttributes.label;
                default: // will never execute as we limit the user with a drop-down
                    return -1;
            }
        }

        public static SemanticCubeUtilities.DataTypes AttributeToType(string attr)
        {
            switch (attr)
            {
                case "x":
                    return SemanticCubeUtilities.DataTypes.number;
                case "y":
                    return SemanticCubeUtilities.DataTypes.number;
                case "width":
                    return SemanticCubeUtilities.DataTypes.number;
                case "height":
                    return SemanticCubeUtilities.DataTypes.number;
                case "rotation":
                    return SemanticCubeUtilities.DataTypes.number;
                case "number":
                    return SemanticCubeUtilities.DataTypes.number;
                case "label":
                    return SemanticCubeUtilities.DataTypes.text;
                default: // will never execute as we limit the user with a drop-down
                    return SemanticCubeUtilities.DataTypes.invalidDataType;
            }
        }
    }
}
