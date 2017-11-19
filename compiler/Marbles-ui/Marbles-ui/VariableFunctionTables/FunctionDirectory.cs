using System;
using System.Collections.Generic;

namespace Marbles
{
	/// <summary>
	/// Class that stores all the functions used in a program.
	/// </summary>
    public static class FunctionDirectory
    {
        /// <summary>
        /// Dictionary that stores all functions by using the ID (function name) as the key.
        /// </summary>
        private static Dictionary<String, Function> FunctionDictionary;

        /// <summary>
        /// Function Directory constructor.
        /// </summary>
        /// <remarks>
        /// Adds by default the function "_Global", which represents the global context 
        /// of the program. This way, we can keep track of global variables.
        /// </remarks>
        static FunctionDirectory()
        {
            FunctionDictionary = new Dictionary<String, Function>
            {
                { "_Global", new Function("_Global", SemanticCubeUtilities.DataTypes.number) }
            };
        }

        /// <summary>
        /// Returns a Function object given the function's name (ID).
        /// This function is called by <see cref="QuadrupleManager"/>, <see cref="Parser"/>, <see cref="VirtualMachine"/>,
        /// and <see cref="MemoryManager"/> to retrieve a function given its ID.
        /// </summary>
        /// <param name="funcID"></param>
        /// <returns>
        /// A Function object with the given ID.
        /// </returns>
        public static Function GetFunction(String funcID)
        {
            if (!FunctionDictionary.ContainsKey(funcID))
            {
                throw new KeyNotFoundException("Use of undeclared function " + funcID);
            }
            else
            {
                return FunctionDictionary[funcID];
            }
        }

        /// <summary>
        /// Returns a Function given its memory address.
        /// This function is called by the VM when doing the ERA action.
        /// </summary>
        /// <param name="address"></param>
        /// <returns>
        /// A Function object.
        /// </returns>
        public static Function GetFunctionWithAddress(int address)
        {
            foreach (Function func in FunctionDictionary.Values)
            {
                if (func.GetLocation() == address)
                {
                    return func;
                }
            }

            throw new Exception("Function with memory address " + address + " does not exist.");
        }

        /// <summary>
        /// Inserts a new function into the Function Directory.
        /// This function is called by <see cref="QuadrupleManager"/> whenever <see cref="Parser"/> reads
        /// a new valid function.
        /// </summary>
        /// <param name="funcID"></param>
        /// <returns>
        /// If the insertion succeeds, returns true.
        /// If the function's name already exists, returns false.
        /// </returns>
        public static bool InsertFunction(Function func)
        {
            if (FunctionDictionary.ContainsKey(func.GetName()))
            {
                return false;
            }
            FunctionDictionary.Add(func.GetName(), func);
            return true;
        }

        /// <summary>
        /// Removes all entries from the Function Directory.
        /// This function is called every time before starting compilation.
        /// </summary>
        public static void Reset()
        {
            foreach (Function fn in FunctionDictionary.Values)
            {
                fn.Reset();
            }
            FunctionDictionary.Clear();
            FunctionDictionary = new Dictionary<String, Function>
            {
                { "_Global", new Function("_Global", SemanticCubeUtilities.DataTypes.number) }
            };
        }

        /// <summary>
        /// Returns whether a specific function exists in the Function Directory or not,
        /// given the function's name (ID) as a parameter.
        /// This function is called by <see cref="QuadrupleManager"/>.
        /// </summary>
        /// <param name="funcID"></param>
        /// <returns>
        /// A boolean indicating whether the function exists.
        /// </returns>
        public static bool FunctionExists(String funcID)
        {
            return FunctionDictionary.ContainsKey(funcID);
        }

        /// <summary>
        /// Returns the _Global Function from the Function Directory.
        /// This function is called by <see cref="MemoryManager"/> to manage memory of global variables and assets
        /// and by <see cref="QuadrupleManager"/> to get the current state of the global function.
        /// </summary>
        /// <remarks>
        /// If for some reason the _Global Function has been removed from the directory,
        /// a KeyNotFound exception will be thrown.
        /// </remarks>
        /// <returns>
        /// A <see cref="Function"/> object with ID: "_Global".
        /// </returns>
        public static Function GlobalFunction()
        {
            if (!FunctionDictionary.ContainsKey("_Global"))
            {
                throw new KeyNotFoundException("The _Global function is not present in the Function Directory.");
            }

            return FunctionDictionary["_Global"];
        }
    }
}
