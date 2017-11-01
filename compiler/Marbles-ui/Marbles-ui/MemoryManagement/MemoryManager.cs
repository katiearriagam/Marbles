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

        public static Dictionary<int, object> memoryGlobalAssets = new Dictionary<int, object>();
		public static Dictionary<int, object> memoryGlobal = new Dictionary<int, object>();
		public static Dictionary<int, object> memoryLocal = new Dictionary<int, object>();
		public static Dictionary<int, object> memoryTemporary = new Dictionary<int, object>();
		public static Dictionary<int, object> memoryConstant = new Dictionary<int, object>();

		// Asset Limits
		const int lowestAssetAddress = 0000;
		const int highestAssetAddress = 0999;
		static int currentAssetAddress = 0000;

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
		const int lowestTempIntAddress = 7000;
		const int lowestTempStringAddress = 8000;
		const int lowestTempBoolAddress = 9000;

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

		/// <summary>
		/// Gets the next available memory address for assets
		/// </summary>
		/// <returns></returns>
		public static int GetNextAssetAvailable()
		{
			if (currentAssetAddress + 7 <= highestAssetAddress)
			{
				return currentAssetAddress;
			}
			return -1;
		}

		/// <summary>
		/// Load an asset into memory
		/// </summary>
		/// <param name="memoryAddress"></param>
		/// <param name="asset"></param>
		/// <returns></returns>
		public static int SetAssetInMemory(int memoryAddress, Asset asset)
		{
			if (currentAssetAddress + 7 <= highestAssetAddress)
			{
				memoryGlobalAssets[memoryAddress + (int)AssetAttributes.x] = asset.GetX();
				memoryGlobalAssets[memoryAddress + (int)AssetAttributes.y] = asset.GetY();
				memoryGlobalAssets[memoryAddress + (int)AssetAttributes.width] = asset.GetWidth();
				memoryGlobalAssets[memoryAddress + (int)AssetAttributes.height] = asset.GetHeight();
				memoryGlobalAssets[memoryAddress + (int)AssetAttributes.rotation] = asset.GetRotation();
				memoryGlobalAssets[memoryAddress + (int)AssetAttributes.number] = asset.GetNumber();
				memoryGlobalAssets[memoryAddress + (int)AssetAttributes.label] = asset.GetLabel();

                asset.SetMemoryAddress(memoryAddress);

				currentAssetAddress += 7;
				return memoryAddress;
			}

			return -1;
		}

		public static int SetMemory(int memAddress, object value)
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

			// insert numeric constant
			else if (memAddress >= lowestConstantIntAddress && memAddress <= highestConstantIntAddress)
			{
				if (!memoryConstant.ContainsValue(value))
				{
					memoryConstant[memAddress] = (int)value;
                    currentConstantIntAddress++;
					return memAddress;
				}
				else
				{
                    return memoryConstant.Where(x => (int)(x.Value) == (int)value).First().Key;
                }
            }

			// insert string constant
			else if (memAddress >= lowestConstantStringAddress && memAddress <= highestConstantStringAddress)
			{
				if (!memoryConstant.ContainsValue(value))
				{
					memoryConstant[memAddress] = (string)value;
                    currentConstantStringAddress++;
					return memAddress;
				}
				else
				{
                    return memoryConstant.Where(x => (string)(x.Value) == (string)value).First().Key;
                }
            }

			// insert boolean constant
			else if (memAddress >= lowestConstantBoolAddress && memAddress <= highestConstantBoolAddress)
			{
				if (!memoryConstant.ContainsValue(value))
				{
					memoryConstant[memAddress] = (bool)value;
                    currentConstantBoolAddress++;
					return memAddress;
				}
				else
				{
                    return memoryConstant.Where(x => (bool)(x.Value) == (bool)value).First().Key;
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
		
		// TODO: Document this.
		public static object GetValueFromAddress(int memAddress)
		{
			if (memoryGlobal.ContainsKey(memAddress))
			{
				return memoryGlobal[memAddress];
			}
			else if (memoryLocal.ContainsKey(memAddress))
			{
				return memoryLocal[memAddress];
			}
			else if (memoryTemporary.ContainsKey(memAddress))
			{
				return memoryTemporary[memAddress];
			}
			else if (memoryConstant.ContainsKey(memAddress))
			{
				return memoryConstant[memAddress];
			}

			throw new Exception("Memory address not currently set");
		}

		public static void AddGlobalVariable(Variable newGlobalVariable)
		{
			if (!FunctionDirectory.GlobalFunction().AddGlobalVariable(newGlobalVariable))
			{
				throw new ArgumentException("Name " + newGlobalVariable.GetName() + " is duplicated in global values.");
			}

			int memorySpace = GetNextAvailable(MemoryScope.global, newGlobalVariable.GetDataType());

			if (memorySpace == -1)
			{
				throw new InvalidOperationException("Out of global memory");
			}

            newGlobalVariable.SetMemoryAddress(memorySpace);

			if (newGlobalVariable.GetDataType() == SemanticCubeUtilities.DataTypes.number)
			{
				SetMemory(memorySpace, 0);
			}
			else if (newGlobalVariable.GetDataType() == SemanticCubeUtilities.DataTypes.text)
			{
				SetMemory(memorySpace, "");
			}
			else if (newGlobalVariable.GetDataType() == SemanticCubeUtilities.DataTypes.boolean)
			{
				SetMemory(memorySpace, false);
			}
		}

		public static void AddFunctionAsGlobalVariable(Function func)
		{
			Variable var = new Variable(func.GetName(), func.GetReturnType());
			try
			{
				AddGlobalVariable(var);
			}
			catch (ArgumentException e)
			{
				throw new ArgumentException(e.Message);
			}
			catch (InvalidOperationException e)
			{
				throw new InvalidOperationException(e.Message);
			}
		}

        public static void Reset()
        {
            memoryGlobalAssets.Clear();
            memoryGlobal.Clear();
            memoryLocal.Clear();
            memoryTemporary.Clear();
            memoryConstant.Clear();

            // Asset Limits
            currentAssetAddress = 0000;

            // Global Current Indexes
            currentGlobalIntAddress = 1000;
            currentGlobalStringAddress = 2000;
            currentGlobalBoolAddress = 3000;

            // Local Current Index
            currentLocalAddress = 6000;

            // Temporary Current Indexes
            currentTempIntAddress = 7000;
            currentTempStringAddress = 8000;
            currentTempBoolAddress = 9000;

            // Constant Current Indexes
            currentConstantIntAddress = 10000;
            currentConstantStringAddress = 11000;
            currentConstantBoolAddress = 12000;
        }
	}
}
