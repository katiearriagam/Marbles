using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariableAndFunctionTables
{
    public static class FunctionDirectory
    {
=======
using SemanticCube;

namespace VariableAndFunctionTables
{
	/// <summary>
	/// Class that stores all the functions used in the program.
	/// </summary>
    public static class FunctionDirectory
    {
		/// <summary>
		/// Dictionary that stores all functions by using the ID (function name)
		/// as the key.
		/// </summary>
>>>>>>> 29a75c3c3e6de1b6f61facc5d3b24fd7cc6284aa
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
<<<<<<< HEAD
                { "_Global", new Function() }
=======
                { "_Global", new Function("_Global", Utilities.DataTypes.empty) }
>>>>>>> 29a75c3c3e6de1b6f61facc5d3b24fd7cc6284aa
            };
        }

        /// <summary>
        /// Returns a Function object given the function's name (ID).
        /// </summary>
        /// <param name="funcID"></param>
        public static Function GetFunction(String funcID)
        {
            if (!FunctionDictionary.ContainsKey(funcID))
            {
                throw new KeyNotFoundException("The function named " + funcID + " was not found in the directory.");
            }
            else
            {
                return FunctionDictionary[funcID];
            }
        }

        /// <summary>
<<<<<<< HEAD
        /// Inserts a new function into the Function Directory.
        /// </summary>
        /// <param name="funcID"></param>
        /// <returns>
        /// If the insertion succeeds, returns true.
        /// If the function's name already exists, returns false.
        /// </returns>
        public static bool InsertFunction(String funcID)
=======
		/// Inserts a new Function into de Function Directory.
		/// </summary>
		/// <param name="funcID"></param>
		/// <param name="func"></param>
		/// <returns>
		/// If the insertion succeeds, return true.
		/// If the insertion fails, return false.
		/// </returns>
        public static bool InsertFunction(String funcID, Function func)
>>>>>>> 29a75c3c3e6de1b6f61facc5d3b24fd7cc6284aa
        {
            if (FunctionDictionary.ContainsKey(funcID))
            {
                return false;
            }

<<<<<<< HEAD
            FunctionDictionary.Add(funcID, new Function());
=======
            FunctionDictionary.Add(funcID, func);
>>>>>>> 29a75c3c3e6de1b6f61facc5d3b24fd7cc6284aa

            return true;
        }

<<<<<<< HEAD
        /// <summary>
        /// Returns whether the Function Directory is empty or not.
        /// </summary>
        public static bool IsEmpty()
=======
		/// <summary>
		/// Returns whether the Function Directory is empty or not.
		/// </summary>
		public static bool IsEmpty()
>>>>>>> 29a75c3c3e6de1b6f61facc5d3b24fd7cc6284aa
        {
            return FunctionDictionary.Count == 0;
        }

        /// <summary>
        /// Returns the amount of Functions registered in the Function Directory.
        /// </summary>
        public static int Size()
        {
            return FunctionDictionary.Count;
        }

        /// <summary>
        /// Removes all entries from the Function Directory.
        /// </summary>
        public static void Clear()
        {
            FunctionDictionary.Clear();
        }

        /// <summary>
        ///  Removes a specific Function from the Function Directory, given the function's name.
        /// </summary>
        /// <param name="funcID"></param>
        /// <returns>
        /// True if the function was removed successfully.
        /// False if it did not extist or it could not be removed.
        /// </returns>
        public static bool DeleteFunction(String funcID)
        {
            if (!FunctionDictionary.ContainsKey(funcID))
            {
                return false;
            }

            return FunctionDictionary.Remove(funcID);
        }

        /// <summary>
        /// Returns whether a specific function exists in the Function Directory or not,
        /// given the function's name (ID) as a parameter.
        /// </summary>
        /// <param name="funcID"></param>
        public static bool FunctionExists(String funcID)
        {
            return FunctionDictionary.ContainsKey(funcID);
        }

        /// <summary>
        /// Returns whethere a specific function exists in the Function Directory or not, 
        /// given the Function as a parameter.
        /// </summary>
        /// <param name="func"></param>
        public static bool FunctionExists(Function func)
        {
            return FunctionDictionary.ContainsValue(func);
        }

<<<<<<< HEAD
        /// <summary>
        /// Returns the _Global Function from the Function Directory.
        /// </summary>
        /// <remarks>
        /// If for some reason the _Global Function has been removed from the directory,
        /// a KeyNotFound exception will be thrown.
        /// </remarks>
        public static Function GlobalFunction()
=======
		/// <summary>
		/// Returns the _Global Function from the Function Directory.
		/// </summary>
		/// <remarks>
		/// If for some reason the _Global Function has been removed from the directory,
		/// a KeyNotFoundException will be thrown.
		/// </remarks>
		public static Function GlobalFunction()
>>>>>>> 29a75c3c3e6de1b6f61facc5d3b24fd7cc6284aa
        {
            if (!FunctionDictionary.ContainsKey("_Global"))
            {
                throw new KeyNotFoundException("The _Global function is not present in the Function Directory.");
            }

            return FunctionDictionary["_Global"];
        }

<<<<<<< HEAD
=======

>>>>>>> 29a75c3c3e6de1b6f61facc5d3b24fd7cc6284aa
    }
}
