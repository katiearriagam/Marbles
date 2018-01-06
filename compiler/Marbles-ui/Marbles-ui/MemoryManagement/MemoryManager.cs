using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbles
{
    /// <summary>
    /// Manages the main memory of the program.
    /// </summary>
	public static class MemoryManager
	{
		public enum MemoryScope
		{
			global		= 0,
			local		= 1,
			temporary	= 2,
			constant	= 3
		}
        
		public enum AssetAttributes
		{
			id			= 0,
			x			= 1,
			y			= 2,
			width		= 3,
			height		= 4,
			rotation	= 5, 
			number		= 6,
			label		= 7
		}

        public static Dictionary<int, object> memoryGlobalAssets = new Dictionary<int, object>();
		public static Dictionary<int, object> memoryGlobal = new Dictionary<int, object>();
		public static Dictionary<int, object> memoryLocal = new Dictionary<int, object>();
		public static Dictionary<int, object> memoryTemporary = new Dictionary<int, object>();
		public static Dictionary<int, object> memoryConstant = new Dictionary<int, object>();

		// Asset Limits
		public const int lowestAssetAddress = 0000;
		public const int highestAssetAddress = 0999;
		public static int currentAssetAddress = 0000;

		// Global Lower Limits
		public const int lowestGlobalIntAddress = 1000;
		public const int lowestGlobalStringAddress = 2000;
		public const int lowestGlobalBoolAddress = 3000;

		// Global Upper Limits
		public const int highestGlobalIntAddress = 1999;
		public const int highestGlobalStringAddress = 2999;
		public const int highestGlobalBoolAddress = 3999;

		// Global Current Indexes
		public static int currentGlobalIntAddress = 1000;
		public static int currentGlobalStringAddress = 2000;
		public static int currentGlobalBoolAddress = 3000;

		// Local Limits
		public const int lowestLocalAddress = 4000;
		public const int highestLocalAddress = 6999;

		// Local Current Index
		public static int currentLocalAddress = 4000;

		// Temporary Lower Limits
		public const int lowestTempIntAddress = 7000;
		public const int lowestTempStringAddress = 8000;
		public const int lowestTempBoolAddress = 9000;

		// Temporary Upper Limits
		public const int highestTempIntAddress = 7999;
		public const int highestTempStringAddress = 8999;
		public const int highestTempBoolAddress = 9999;

		// Temporary Current Indexes
		public static int currentTempIntAddress = 7000;
		public static int currentTempStringAddress = 8000;
		public static int currentTempBoolAddress = 9000;

		// Constant Upper Limits
		public const int lowestConstantIntAddress = 10000;
		public const int lowestConstantStringAddress = 11000;
		public const int lowestConstantBoolAddress = 12000;

		// Constant Upper Limits
		public const int highestConstantIntAddress = 10999;
		public const int highestConstantStringAddress = 11999;
		public const int highestConstantBoolAddress = 12001;

		// Constant Current Indexes
		public static int currentConstantIntAddress = 10000;
		public static int currentConstantStringAddress = 11000;
		public static int currentConstantBoolAddress = 12000;

		/// <summary>
		/// Gets the next available memory address for each scope and <see cref="SemanticCubeUtilities.DataTypes"/> value.
        /// Called by <see cref="QuadrupleManager"/> to allot a space in memory to a value.
		/// </summary>
		/// <param name="scope"></param>
		/// <param name="type"></param>
        /// <returns>A memory address.</returns>
		public static int GetNextAvailable(MemoryScope scope, SemanticCubeUtilities.DataTypes type)
		{
			switch (scope)
			{
				case MemoryScope.global:
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

				case MemoryScope.temporary:
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

				case MemoryScope.constant:
					switch (type)
					{
						case SemanticCubeUtilities.DataTypes.number:
							if (currentConstantIntAddress <= highestConstantIntAddress)
								return currentConstantIntAddress;
							break;
						case SemanticCubeUtilities.DataTypes.text:
							if (currentConstantStringAddress <= highestConstantStringAddress)
								return currentConstantStringAddress;
							break;
						case SemanticCubeUtilities.DataTypes.boolean:
							if (currentConstantBoolAddress <= highestConstantBoolAddress)
								return currentConstantBoolAddress;
							break;
						default:
							break;
					}
					break;

				case MemoryScope.local:
					if (currentLocalAddress <= highestLocalAddress)
						return currentLocalAddress;
					break;

				default:
					break;
			}
			return -1;
		}

		/// <summary>
		/// Gets the next available memory address for assets.
        /// Called by <see cref="SetAssetInMemory(Asset)"/>.
		/// </summary>
		/// <returns>The memory address available for an asset.</returns>
		public static int GetNextAssetAvailable()
		{
			if (currentAssetAddress + Enum.GetNames(typeof(AssetAttributes)).Length <= highestAssetAddress)
			{
				return currentAssetAddress;
			}

			return -1;
		}

		/// <summary>
		/// Loads an asset into memory.
        /// Called by <see cref="Parser"/> when an Asset is read.
		/// </summary>
		/// <param name="memoryAddress"></param>
		/// <param name="asset"></param>
		/// <returns>The memory address where the asset was set.</returns>
		public static int SetAssetInMemory(Asset asset)
		{
			int memoryAddress = GetNextAssetAvailable();
			if (memoryAddress == -1) { throw new Exception("Out of asset memory"); }
			asset.SetMemoryAddress(memoryAddress);

			// try to add asset to global asset directory
			try
			{
				if (currentAssetAddress + Enum.GetNames(typeof(AssetAttributes)).Length <= highestAssetAddress)
				{
					FunctionDirectory.GlobalFunction().AddAssetVariable(asset);

					// add attribute values to memory
					memoryGlobalAssets[memoryAddress + (int)AssetAttributes.id] = asset.GetID();
					memoryGlobalAssets[memoryAddress + (int)AssetAttributes.x] = asset.GetX();
					memoryGlobalAssets[memoryAddress + (int)AssetAttributes.y] = asset.GetY();
					memoryGlobalAssets[memoryAddress + (int)AssetAttributes.width] = asset.GetWidth();
					memoryGlobalAssets[memoryAddress + (int)AssetAttributes.height] = asset.GetHeight();
					memoryGlobalAssets[memoryAddress + (int)AssetAttributes.rotation] = asset.GetRotation();
					memoryGlobalAssets[memoryAddress + (int)AssetAttributes.number] = asset.GetNumber();
					memoryGlobalAssets[memoryAddress + (int)AssetAttributes.label] = asset.GetLabel();

					currentAssetAddress += Enum.GetNames(typeof(AssetAttributes)).Length;
				}

				return memoryAddress;
			}
			catch (Exception)
			{
                throw;
			}
		}

		/// <summary>
		/// Loads value into a memory address.
        /// Called by instructions that store a new value in main memory.
		/// </summary>
		/// <param name="memAddress"></param>
		/// <param name="value"></param>
		/// <returns>Memory address where the value was stored.</returns>
		public static int SetMemory(int memAddress, object value)
		{
			// insert an asset attribute in memory
			if (memAddress >= lowestAssetAddress && memAddress <= highestAssetAddress)
			{
				memoryGlobalAssets[memAddress] = value;
				return memAddress;
			}
			// insert a global number in memory
			if (memAddress >= lowestGlobalIntAddress && memAddress <= highestGlobalIntAddress)
			{
                if (!memoryGlobal.ContainsKey(memAddress))
                {
                    currentGlobalIntAddress++;
                }

                memoryGlobal[memAddress] = (int)value;
				return memAddress;
			}

			// insert a temporary number in memory
			else if (memAddress >= lowestTempIntAddress && memAddress <= highestTempIntAddress)
			{
                if (!memoryTemporary.ContainsKey(memAddress))
                {
                    currentTempIntAddress++;
                }

                memoryTemporary[memAddress] = (int)value;
                return memAddress;
			}

			// insert a global string in memory
			else if (memAddress >= lowestGlobalStringAddress && memAddress <= highestGlobalStringAddress)
			{
                if (!memoryGlobal.ContainsKey(memAddress))
                {
                    currentGlobalStringAddress++;
                }

                memoryGlobal[memAddress] = (string)value;
                return memAddress;
			}

			// insert a temporary string in memory
			else if (memAddress >= lowestTempStringAddress && memAddress <= highestTempStringAddress)
			{
                if (!memoryTemporary.ContainsKey(memAddress))
                {
                    currentTempStringAddress++;
                }

                memoryTemporary[memAddress] = (string)value;
                return memAddress;
			}

			// insert a global boolean in memory
			else if (memAddress >= lowestGlobalBoolAddress && memAddress <= highestGlobalBoolAddress)
			{
                if (!memoryGlobal.ContainsKey(memAddress))
                {
                    currentGlobalBoolAddress++;
                }

                memoryGlobal[memAddress] = (bool)value;
                return memAddress;
			}

			// insert temporary boolean in memory
			else if (memAddress >= lowestTempBoolAddress && memAddress <= highestTempBoolAddress)
			{
                if (!memoryTemporary.ContainsKey(memAddress))
                {
                    currentTempBoolAddress++;
                }

                memoryTemporary[memAddress] = (bool)value;
                return memAddress;
			}

			// insert numeric constant
			else if ((memAddress >= lowestConstantIntAddress && memAddress <= highestConstantIntAddress) || (memAddress == -1 && value.GetType() == typeof(Int32)))
			{
				if (!memoryConstant.ContainsValue(value))
				{
                    if (memAddress == -1)
                    {
                        // constant numeric memory was full, and you tried to add a new value.
                        throw new Exception("Constant memory overflow.");
                    }

                    memoryConstant[memAddress] = (int)value;
                    currentConstantIntAddress++;
					return memAddress;
				}
				else
				{
                    return memoryConstant.Where(x => x.Value.GetType() == typeof(int) && (int)(x.Value) == (int)value).First().Key;
                }
            }

			// insert string constant
			else if ((memAddress >= lowestConstantStringAddress && memAddress <= highestConstantStringAddress) || (memAddress == -1 && value.GetType() == typeof(string)))
			{
				if (!memoryConstant.ContainsValue(value))
				{
                    if (memAddress == -1)
                    {
                        // constant string memory was full, and you tried to add a new value.
                        throw new Exception("Constant memory overflow.");
                    }

                    memoryConstant[memAddress] = (string)value;
                    currentConstantStringAddress++;
					return memAddress;
				}
				else
				{
                    return memoryConstant.Where(x => x.Value.GetType() == typeof(string) && (string)(x.Value) == (string)value).First().Key;
                }
            }

			// insert boolean constant
			else if ((memAddress >= lowestConstantBoolAddress && memAddress <= highestConstantBoolAddress) || (memAddress == -1 && value.GetType() == typeof(bool)))
			{
				if (!memoryConstant.ContainsValue(value))
				{
                    if (memAddress == -1)
                    {
                        // constant bool memory was full, and you tried to add a new value.
                        // (this should never happen, since boolean only has 2 possible constants)
                        throw new Exception("Constant memory overflow.");
                    }

					memoryConstant[memAddress] = (bool)value;
                    currentConstantBoolAddress++;
					return memAddress;
				}
				else
				{
                    return memoryConstant.Where(x => x.Value.GetType() == typeof(bool) && (bool)(x.Value) == (bool)value).First().Key;
				}
			}
			
			// insert a local object in memory
			else if (memAddress >= lowestLocalAddress && memAddress <= highestLocalAddress)
			{
                if (!memoryLocal.ContainsKey(memAddress))
                {
                    currentLocalAddress++;
                }

                memoryLocal[memAddress] = value;
                return memAddress;
			}

			throw new Exception("Out of memory");
		}

		/// <summary>
		/// Retrieves the offset we try to access in local memory.
        /// Called by <see cref="VirtualMachine.MapAddressToLocalMemory(int)"/>.
		/// </summary>
		/// <param name="functionId"></param>
		/// <param name="address"></param>
		/// <returns>The memory address of an object in local memory.</returns>
		public static int FunctionMemoryToMemoryManager(string functionId, int address)
		{
			Function currentFunctionInCallStack = FunctionDirectory.GetFunction(functionId);
			return currentFunctionInCallStack.memory.GetIndexFromMemoryList(address);
		}

		/// <summary>
		/// Allocates the memory for a new function (in locals).
        /// Called by <see cref="VirtualMachine"/> when executing the <see cref="Utilities.QuadrupleAction.era"/> action.
		/// </summary>
		/// <param name="func"></param>
        /// <returns>The first address in local memory that was allocated.</returns>
		public static int AllocateLocalMemory(Function func)
		{
			if (currentLocalAddress + func.GetFunctionSize() <= highestLocalAddress)
			{
				int startingLocalAddress = currentLocalAddress;

				// get global memory
				List<int> GlobalKeyList = func.memory.GetMemoryGlobal().Keys.ToList();

				// sort keys (they are ordered by addition and type)
				GlobalKeyList.Sort();

				// load global memory
				foreach (int address in GlobalKeyList)
				{
					SetMemory(currentLocalAddress, func.memory.GetValueFromAddress(address));
				}

				// get temp memory
				List<int> TempKeyList = func.memory.GetMemoryTemporary().Keys.ToList();

				// sort keys (they are ordered by addition and type)
				TempKeyList.Sort();

				// load temp memory
				foreach (int address in TempKeyList)
				{
					SetMemory(currentLocalAddress, func.memory.GetValueFromAddress(address));
				}

				// return memory address where the function is loaded
				return startingLocalAddress;
			}
			else
			{
				throw new Exception("Out of local memory.");
			}
		}

		/// <summary>
		/// Removes a function from local memory mapping.
        /// Called by <see cref="VirtualMachine"/> when executing <see cref="Utilities.QuadrupleAction.retorno"/>
        /// and <see cref="Utilities.QuadrupleAction.endProc"/> actions.
		/// </summary>
		/// <param name="func"></param>
		public static void DeallocateLocalMemory(int funcSize)
		{
			if (currentLocalAddress - funcSize >= lowestLocalAddress)
			{
				for (int i = currentLocalAddress - 1; i >= currentLocalAddress - funcSize; i--)
				{
					memoryLocal.Remove(i);
				}
				currentLocalAddress -= funcSize;
			}
			else
			{
				throw new Exception("Trying to deallocate memory from non-local scope.");
			}
		}

		/// <summary>
		/// Gets the <see cref="SemanticCubeUtilities.DataTypes"/> of an asset attribute.
        /// Called by <see cref="QuadrupleManager.ReadAssetAttribute(AssetAttributes)"/>.
		/// </summary>
		/// <param name="attribute"></param>
		/// <returns>A <see cref="SemanticCubeUtilities.DataTypes"/> value.</returns>
        public static SemanticCubeUtilities.DataTypes AttributeToType(AssetAttributes attribute)
        {
            switch (attribute)
            {
                case AssetAttributes.x:
                    return SemanticCubeUtilities.DataTypes.number;
				case AssetAttributes.y:
                    return SemanticCubeUtilities.DataTypes.number;
				case AssetAttributes.width:
                    return SemanticCubeUtilities.DataTypes.number;
				case AssetAttributes.height:
                    return SemanticCubeUtilities.DataTypes.number;
				case AssetAttributes.rotation:
                    return SemanticCubeUtilities.DataTypes.number;
				case AssetAttributes.number:
                    return SemanticCubeUtilities.DataTypes.number;
				case AssetAttributes.label:
                    return SemanticCubeUtilities.DataTypes.text;
                default: // will never execute as we limit the user's options with a drop-down
                    return SemanticCubeUtilities.DataTypes.invalidDataType;
            }
        }
		
		/// <summary>
		/// Gets the value stored in a memory address.
        /// Called by <see cref="VirtualMachine"/> to retrieve values at a specific memory address.
		/// </summary>
		/// <param name="memAddress"></param>
		/// <returns>The stored value</returns>
		public static object GetValueFromAddress(int memAddress)
		{
            if (memoryGlobalAssets.ContainsKey(memAddress))
            {
                return memoryGlobalAssets[memAddress];
            }
			else if (memoryGlobal.ContainsKey(memAddress))
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

			// if memory address does not exist, throw exception
			throw new Exception("Memory address not currently set");
		}

        /// <summary>
        /// Returns the <see cref="Type"/> of an object stored in memory given a memory address.
        /// Called by <see cref="QuadrupleManager.PopOperator(int)"/>.
        /// </summary>
        /// <param name="memAddress"></param>
        /// <returns>A <see cref="Type"/>, or null if the memory address is invalid.</returns>
        public static Type GetTypeFromAddress(int memAddress)
        {
			// if memory belongs to asset memory
            if (memAddress >= lowestAssetAddress && memAddress <= highestAssetAddress)
            {
				if (memoryGlobalAssets.ContainsKey(memAddress))
				{
					return memoryGlobalAssets[memAddress].GetType();
				}
            }
			// if memory belongs to local memory
			if (memAddress >= lowestLocalAddress && memAddress <= highestLocalAddress)
			{
				if (memoryLocal.ContainsKey(memAddress))
				{
					return memoryLocal[memAddress].GetType();
				}
			}
			else if ((memAddress >= lowestGlobalIntAddress && memAddress <= highestGlobalIntAddress) || (memAddress >= lowestTempIntAddress && memAddress <= highestTempIntAddress) || (memAddress >= lowestConstantIntAddress && memAddress <= highestConstantIntAddress))
            {
                return typeof(int);
            }
            else if (memAddress >= lowestGlobalStringAddress && memAddress <= highestGlobalStringAddress || (memAddress >= lowestTempStringAddress && memAddress <= highestTempStringAddress) || (memAddress >= lowestConstantStringAddress && memAddress <= highestConstantStringAddress))
            {
                return typeof(string);
            }
            else if (memAddress >= lowestGlobalBoolAddress && memAddress <= highestGlobalBoolAddress || (memAddress >= lowestTempBoolAddress && memAddress <= highestTempBoolAddress) || (memAddress >= lowestConstantBoolAddress && memAddress <= highestConstantBoolAddress))
            {
                return typeof(bool);
            }

            throw new Exception("Memory address does not have a type.");
        }

        /// <summary>
        /// Returns a <see cref="MemoryScope"/> value given a memory address.
        /// </summary>
        /// <param name="memAddress"></param>
        /// <returns>A <see cref="MemoryScope"/> value.</returns>
        public static MemoryScope GetScopeFromAddress(int memAddress)
        {
            if (memAddress >= lowestAssetAddress && memAddress <= highestGlobalBoolAddress)
            {
                return MemoryScope.global;
            }
            else if (memAddress >= lowestLocalAddress && memAddress <= highestLocalAddress)
            {
                return MemoryScope.local;
            }
            else if (memAddress >= lowestTempIntAddress && memAddress <= highestTempBoolAddress)
            {
                return MemoryScope.temporary;
            }
            else if (memAddress >= lowestConstantIntAddress && memAddress <= highestConstantBoolAddress)
            {
                return MemoryScope.constant;
            }

            throw new Exception("Invalid memory address.");
        }
        
		/// <summary>
		/// Adds a variable to the global directory.
        /// Called when a new variable ID is read in global scope.
		/// </summary>
		/// <param name="newGlobalVariable"></param>
		/// <returns>The variable address in the global directory where the variable was stored.</returns>
		public static int AddGlobalVariable(Variable newGlobalVariable)
		{
			// retrieve the memory address where the variable will live
			int memorySpace = GetNextAvailable(MemoryScope.global, newGlobalVariable.GetDataType());

			// if memory space is insufficient, throw and exception
			if (memorySpace == -1){ throw new Exception("Out of global memory"); }

			// pass on the memory address meant for the variable
            newGlobalVariable.SetMemoryAddress(memorySpace);

			// try to add variable to global directory
			try
			{
				FunctionDirectory.GlobalFunction().AddGlobalVariable(newGlobalVariable);
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
					try { memorySpace = SetMemory(memorySpace,false); }
					catch (Exception e) { throw new Exception(e.Message); }
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			return memorySpace;
		}

		/// <summary>
		/// Adds a function to the global directory to save the current value.
        /// Called by <see cref="QuadrupleManager.EnterFunction(Function)"/>.
		/// </summary>
		/// <param name="func"></param>
		public static void AddFunctionAsGlobalVariable(Function func)
		{
			Variable var = new Variable(func.GetName(), func.GetReturnType());
			try
			{
				int functionLocationInMemory = AddGlobalVariable(var);
				func.SetLocation(functionLocationInMemory);
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

        /// <summary>
        /// Reset all current entries in memory to their original values (before execution).
        /// NOTE: This does not erase any entry, only resets their values to their default.
        /// Called by <see cref="CanvasView"/> after <see cref="VirtualMachine"/>'s execution has completed.
        /// </summary>
        public static void RunReset()
        {
            // Reset global memory
            var memoryGlobalCopy = new List<int>();
            foreach (KeyValuePair<int, object> kvp in memoryGlobal)
            {
                memoryGlobalCopy.Add(kvp.Key);
            }

            foreach (var key in memoryGlobalCopy)
            {
                memoryGlobal[key] = Utilities.GetDefaultValueFromType(memoryGlobal[key].GetType());
            }

            // Reset local memory
            var memoryLocalCopy = new List<int>();
            foreach (KeyValuePair<int, object> kvp in memoryLocal)
            {
                memoryLocalCopy.Add(kvp.Key);
            }

            foreach (var key in memoryLocalCopy)
            {
                memoryLocal[key] = Utilities.GetDefaultValueFromType(memoryLocal[key].GetType());
            }

            // Reset temporary memory
            var memoryTemporaryCopy = new List<int>();
            foreach (KeyValuePair<int, object> kvp in memoryTemporary)
            {
                memoryTemporaryCopy.Add(kvp.Key);
            }

            foreach (var key in memoryTemporaryCopy)
            {
                memoryTemporary[key] = Utilities.GetDefaultValueFromType(memoryTemporary[key].GetType());
            }

            // No need to reset constants as they will always hold the same value.
        }

		/// <summary>
		/// Resets memory.
        /// Called by <see cref="CodeView"/> before starting compilation.
		/// </summary>
        public static void Reset()
        {
            memoryGlobalAssets.Clear();
            memoryGlobal.Clear();
            memoryLocal.Clear();
            memoryTemporary.Clear();
            memoryConstant.Clear();

            // Asset Limits
            currentAssetAddress = lowestAssetAddress;

            // Global Current Indexes
            currentGlobalIntAddress = lowestGlobalIntAddress;
            currentGlobalStringAddress = lowestGlobalStringAddress;
            currentGlobalBoolAddress = lowestGlobalBoolAddress;

            // Local Current Index
            currentLocalAddress = lowestLocalAddress;

            // Temporary Current Indexes
            currentTempIntAddress = lowestTempIntAddress;
            currentTempStringAddress = lowestTempStringAddress;
            currentTempBoolAddress = lowestTempBoolAddress;

            // Constant Current Indexes
            currentConstantIntAddress = lowestConstantIntAddress;
            currentConstantStringAddress = lowestConstantStringAddress;
            currentConstantBoolAddress = lowestConstantBoolAddress;
        }

        /// <summary>
        /// Prints the memory addresses allocated in main memory.
        /// Called by <see cref="CodeView"/> before execution and by
        /// <see cref="CanvasView"/> after execution.
        /// </summary>
        public static void PrintMemory()
        {
            Debug.WriteLine("--- START GLOBAL MEMORY ---");

            Debug.WriteLine("\n");
            Debug.WriteLine(">--- GLOBAL ASSETS---< ");
            foreach (KeyValuePair<int, object> kvp in memoryGlobalAssets)
            {
                Debug.WriteLine(kvp.Key + "[" + SemanticCubeUtilities.GetDataTypeFromType(kvp.Value.GetType()).ToString() + "]" + " -> " + kvp.Value.ToString());
            }

            Debug.WriteLine("\n");
            Debug.WriteLine(">--- GLOBAL VARIABLES---< ");
            foreach (KeyValuePair<int, object> kvp in memoryGlobal)
            {
                Debug.WriteLine(kvp.Key + "[" + SemanticCubeUtilities.GetDataTypeFromType(kvp.Value.GetType()).ToString() + "]" + " -> " + kvp.Value.ToString());
            }

            Debug.WriteLine("\n");
            Debug.WriteLine(">--- LOCAL ---< ");
            foreach (KeyValuePair<int, object> kvp in memoryLocal)
            {
				if (kvp.Value == null)
				{
					Debug.WriteLine(kvp.Key + "[" + "NULL" + "]" + " -> " + "NULL");
				}
				else
				{
					Debug.WriteLine(kvp.Key + "[" + SemanticCubeUtilities.GetDataTypeFromType(kvp.Value.GetType()).ToString() + "]" + " -> " + kvp.Value.ToString());
				}
            }

            Debug.WriteLine("\n");
            Debug.WriteLine(">--- TEMPORARY ---< ");
            foreach (KeyValuePair<int, object> kvp in memoryTemporary)
            {
                Debug.WriteLine(kvp.Key + "[" + SemanticCubeUtilities.GetDataTypeFromType(kvp.Value.GetType()).ToString() + "]" + " -> " + kvp.Value.ToString());
            }

            Debug.WriteLine("\n");
            Debug.WriteLine(">--- CONSTANT ---< ");
            foreach (KeyValuePair<int, object> kvp in memoryConstant)
            {
                Debug.WriteLine(kvp.Key + "[" + SemanticCubeUtilities.GetDataTypeFromType(kvp.Value.GetType()).ToString() + "]" + " -> " + kvp.Value.ToString());
            }
            Debug.WriteLine("--- END GLOBAL MEMORY ---");
        }
    }
}
