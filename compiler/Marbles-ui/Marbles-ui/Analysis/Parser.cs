/*----------------------------------------------------------------------
Compiler Generator Coco/R,
Copyright (c) 1990, 2004 Hanspeter Moessenboeck, University of Linz
extended by M. Loeberbauer & A. Woess, Univ. of Linz
with improvements by Pat Terry, Rhodes University

This program is free software; you can redistribute it and/or modify it 
under the terms of the GNU General Public License as published by the 
Free Software Foundation; either version 2, or (at your option) any 
later version.

This program is distributed in the hope that it will be useful, but 
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License 
for more details.

You should have received a copy of the GNU General Public License along 
with this program; if not, write to the Free Software Foundation, Inc., 
59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.

As an exception, it is allowed to write an extension of Coco/R that is
used as a plugin in non-free software.

If not otherwise stated, any source code generated by Coco/R (other than 
Coco/R itself) does not fall under the GNU General Public License.
-----------------------------------------------------------------------*/

using Marbles;
using Marbles.Analysis;
using Marbles.MemoryManagement;
using System;



public class Parser {
	public const int _EOF = 0;
	public const int _id = 1;
	public const int _true = 2;
	public const int _false = 3;
	public const int _const_i = 4;
	public const int _const_s = 5;
	public const int maxT = 53;

	const bool T = true;
	const bool x = false;
	const int minErrDist = 2;
	
	public Scanner scanner;
	public Errors  errors;

	public Token t;    // last recognized token
	public Token la;   // lookahead token
	int errDist = minErrDist;

	// helper variables
	public int AssetIndex = 0;


	public Parser(Scanner scanner) {
		this.scanner = scanner;
		errors = new Errors();
	}

	void SynErr (int n) {
		if (errDist >= minErrDist) errors.SynErr(la.line, la.col, n);
		errDist = 0;
	}

	public void SemErr (string msg) {
		if (errDist >= minErrDist) errors.SemErr(t.line, t.col, msg);
		errDist = 0;
	}
	
	void Get () {
		for (;;) {
			t = la;
			la = scanner.Scan();
			if (la.kind <= maxT) { ++errDist; break; }

			la = t;
		}
	}
	
	void Expect (int n) {
		if (la.kind==n) Get(); else { SynErr(n); }
	}
	
	bool StartOf (int s) {
		return set[s, la.kind];
	}
	
	void ExpectWeak (int n, int follow) {
		if (la.kind == n) Get();
		else {
			SynErr(n);
			while (!StartOf(follow)) Get();
		}
	}


	bool WeakSeparator(int n, int syFol, int repFol) {
		int kind = la.kind;
		if (kind == n) {Get(); return true;}
		else if (StartOf(repFol)) {return false;}
		else {
			SynErr(n);
			while (!(set[syFol, kind] || set[repFol, kind] || set[0, kind])) {
				Get();
				kind = la.kind;
			}
			return StartOf(syFol);
		}
	}

	
	void Marbles() {
		PROGRAM();
	}

	void PROGRAM() {
		while (la.kind == 18) {
			int index = CREATE_ASSET();
			int memoryAddress = MemoryManager.GetNextAssetAvailable();
			if (memoryAddress != -1)
			{
				MemoryManager.SetAssetInMemory(memoryAddress, (Asset)Utilities.finalAssetsInCanvas[index]);
			}
		}
		AssetIndex = 0;
		while (la.kind == 16) {
			Variable newGlobalVariable = CREATE_VAR();
			try
			{
				MemoryManager.AddGlobalVariable(newGlobalVariable);
			}
			catch (ArgumentException e)
			{
				SemErr(e.Message);
			}
			catch (InvalidOperationException e)
			{
				SemErr(e.Message);
			}
		}
		while (la.kind == 9) {
			CREATE_FUNCTION();
		}
		Expect(6);
		Expect(7);
		while (StartOf(1)) {
			INSTRUCTION();
		}
		Expect(8);
	}

	/// <summary>
	/// Action called when a creat asset block is found
	/// </summary>
	/// <returns> Returns the index of the asset being 
	/// created in Utilities.finalAssetsInCanvas </returns>
	int CREATE_ASSET() {
		Expect(18);
		Expect(1);
		Expect(17);
		return AssetIndex++;
	}

	Variable CREATE_VAR() {
		Expect(16);
		SemanticCubeUtilities.DataTypes varType = TYPE_VAR();
		Expect(1);
		string varName = t.val;
		Variable newVar = new Variable(varName, varType);
		Expect(17);
		return newVar;
	}

	void CREATE_FUNCTION() {
		Expect(9);
		SemanticCubeUtilities.DataTypes functionType = TYPE_FUNC();
		Expect(1);
		string functionName = t.val;
		Function newFunction = new Function(functionName, functionType);

		QuadrupleManager.EnterFunction(functionName);

		Expect(10);
		if (la.kind == 13 || la.kind == 14 || la.kind == 15) {
			SemanticCubeUtilities.DataTypes parameterType = TYPE_VAR();
			Expect(1);
			string parameterName = t.val;
			newFunction.AddParameter(new Variable(parameterName, parameterType));
			while (la.kind == 11) {
				Get();
				parameterType = TYPE_VAR();
				Expect(1);
				parameterName = t.val;
				newFunction.AddParameter(new Variable(parameterName, parameterType));
			}
		}
		Expect(12);
		Expect(7);
		while (la.kind == 16) {
			// add variables to the local variables directory
			var localVariable = CREATE_VAR();
			newFunction.AddLocalVariable(localVariable);
		}
		while (StartOf(1)) {
			INSTRUCTION();
		}
		Expect(8);

		// if function name already exists, throw a semantic error.
		if (FunctionDirectory.FunctionExists(newFunction))
		{
			SemErr("Function named " + newFunction.GetName() + " already exists.");
		}
		else
		{
			try
			{
				MemoryManager.AddFunctionAsGlobalVariable(newFunction);
			}
			catch (ArgumentException e)
			{
				SemErr(e.Message);
			}
			catch (InvalidOperationException e)
			{
				SemErr(e.Message);
			}
		}

		//TODO: Add actual memory address
		QuadrupleManager.ExitFunction(0);
	}

	void INSTRUCTION() {
		if (StartOf(2)) {
			if (la.kind == 20) {
				STOP();
			} else if (la.kind == 21) {
				DO();
			} else if (la.kind == 32) {
				ASSIGNMENT();
			} else {
				RETURN();
			}
			Expect(17);
		} else if (la.kind == 28 || la.kind == 30 || la.kind == 31) {
			if (la.kind == 28) {
				FOR();
			} else if (la.kind == 30) {
				WHILE();
			} else {
				IFF();
			}
		} else SynErr(54);
	}

	SemanticCubeUtilities.DataTypes TYPE_FUNC() {
		if (la.kind == 13) {
			Get();
			return SemanticCubeUtilities.DataTypes.text;
		} else if (la.kind == 14) {
			Get();
			return SemanticCubeUtilities.DataTypes.number;
		} else if (la.kind == 15) {
			Get();
			return SemanticCubeUtilities.DataTypes.boolean;
		} else SynErr(55);
		return SemanticCubeUtilities.DataTypes.invalidDataType;
	}

	SemanticCubeUtilities.DataTypes TYPE_VAR() {
		if (la.kind == 13)
		{
			Get();
			return SemanticCubeUtilities.DataTypes.text;
		}
		else if (la.kind == 14)
		{
			Get();
			return SemanticCubeUtilities.DataTypes.number;
		}
		else if (la.kind == 15)
		{
			Get();
			return SemanticCubeUtilities.DataTypes.boolean;
		} else SynErr(56);
		return SemanticCubeUtilities.DataTypes.invalidDataType;
	}

	void CALL_TO_FUNCTION() {
		Expect(19);
		Expect(1);
		Expect(10);
		if (StartOf(3)) {
			EXP();
			while (la.kind == 11) {
				Get();
				EXP();
			}
		}
		Expect(12);
	}

	void EXP() {
		TERM();
		while (la.kind == 36 || la.kind == 37) {
			if (la.kind == 36) {
				Get();
			} else {
				Get();
			}
			TERM();
		}
	}

	void STOP() {
		Expect(20);
	}

	void DO() {
		Expect(21);
		Expect(1);
		Expect(22);
		ACTION();
	}

	void ASSIGNMENT() {
		Expect(32);
		Expect(1);
		if (la.kind == 22) {
			Get();
			ATTRIBUTE();
		}
		// quadruple_store.lastIdName = t.val;
		// initassign()
		Expect(33);
		SUPER_EXP();
		// assign() pop de lo que se mete en la pila
	}

	void RETURN() {
		Expect(52);
		SUPER_EXP();
	}

	void FOR() {
		Expect(28);
		Expect(10);
		EXP();
		Expect(12);
		Expect(29);
		Expect(7);
		while (StartOf(1)) {
			INSTRUCTION();
		}
		Expect(8);
	}

	void WHILE() {
		Expect(30);
		Expect(10);
		SUPER_EXP();
		Expect(12);
		Expect(7);
		while (StartOf(1)) {
			INSTRUCTION();
		}
		Expect(8);
	}

	void IFF() {
		Expect(31);
		Expect(10);
		SUPER_EXP();
		Expect(12);
		Expect(7);
		while (StartOf(1)) {
			INSTRUCTION();
		}
		Expect(8);
	}

	void ACTION() {
		if (StartOf(4)) {
			if (la.kind == 23) {
				Get();
				Expect(10);
			} else if (la.kind == 24) {
				Get();
				Expect(10);
			} else if (la.kind == 25) {
				Get();
				Expect(10);
			} else {
				Get();
				Expect(10);
				EXP();
				Expect(11);
			}
			EXP();
			Expect(12);
		} else if (la.kind == 27) {
			Get();
			Expect(10);
			Expect(12);
		} else SynErr(57);
	}

	void SUPER_EXP() {
		EXP_L();
		while (la.kind == 34) {
			Get();
			EXP_L();
		}
	}

	void ATTRIBUTE() {
		switch (la.kind) {
		case 46: {
			Get();
			break;
		}
		case 47: {
			Get();
			break;
		}
		case 48: {
			Get();
			break;
		}
		case 49: {
			Get();
			break;
		}
		case 50: {
			Get();
			break;
		}
		case 51: {
			Get();
			break;
		}
		default: SynErr(58); break;
		}
	}

	void EXP_L() {
		EXP_R();
		while (la.kind == 35) {
			Get();
			EXP_R();
		}
	}

	void EXP_R() {
		EXP();
		if (StartOf(5)) {
			OP();
			EXP();
		}
	}

	void OP() {
		switch (la.kind) {
		case 40: {
			Get();
			break;
		}
		case 41: {
			Get();
			break;
		}
		case 42: {
			Get();
			break;
		}
		case 43: {
			Get();
			break;
		}
		case 44: {
			Get();
			break;
		}
		case 45: {
			Get();
			break;
		}
		default: SynErr(59); break;
		}
	}

	void TERM() {
		FACTOR();
		while (la.kind == 38 || la.kind == 39) {
			if (la.kind == 38) {
				Get();
			} else {
				Get();
			}
			FACTOR();
		}
	}

	void FACTOR() {
		if (la.kind == 10) {
			Get();
			EXP_R();
			Expect(12);
		} else if (StartOf(6)) {
			if (la.kind == 37) {
				Get();
			}
			if (la.kind == 4) {
				Get();
			} else if (la.kind == 1) {
				Get();
				if (la.kind == 22) {
					Get();
					ATTRIBUTE();
				}
			} else if (la.kind == 19) {
				CALL_TO_FUNCTION();
			} else if (la.kind == 2 || la.kind == 3) {
				BOOL();
			} else if (la.kind == 5) {
				Get();
			} else SynErr(60);
		} else SynErr(61);
	}

	void BOOL() {
		if (la.kind == 2) {
			Get();
		} else if (la.kind == 3) {
			Get();
		} else SynErr(62);
	}



	public void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		Marbles();
		Expect(0);

	}
	
	static readonly bool[,] set = {
		{T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,x,x, T,x,T,T, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x},
		{x,T,T,T, T,T,x,x, x,x,T,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, T,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,T,T, T,T,x,x, x,x,x,x, x,x,x},
		{x,T,T,T, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x}

	};
} // end Parser


public class Errors {
	public int count = 0;                                    // number of errors detected
	// public System.IO.TextWriter errorStream = Console.Out;   // error messages go to this stream
	public string errMsgFormat = "-- line {0} col {1}: {2}"; // 0=line, 1=column, 2=text

	public virtual void SynErr (int line, int col, int n) {
		string s;
		switch (n) {
			case 0: s = "EOF expected"; break;
			case 1: s = "id expected"; break;
			case 2: s = "true expected"; break;
			case 3: s = "false expected"; break;
			case 4: s = "const_i expected"; break;
			case 5: s = "const_s expected"; break;
			case 6: s = "\"instructions\" expected"; break;
			case 7: s = "\"{\" expected"; break;
			case 8: s = "\"}\" expected"; break;
			case 9: s = "\"function\" expected"; break;
			case 10: s = "\"(\" expected"; break;
			case 11: s = "\",\" expected"; break;
			case 12: s = "\")\" expected"; break;
			case 13: s = "\"text\" expected"; break;
			case 14: s = "\"number\" expected"; break;
			case 15: s = "\"bool\" expected"; break;
			case 16: s = "\"var\" expected"; break;
			case 17: s = "\";\" expected"; break;
			case 18: s = "\"asset\" expected"; break;
			case 19: s = "\"call\" expected"; break;
			case 20: s = "\"stop\" expected"; break;
			case 21: s = "\"do\" expected"; break;
			case 22: s = "\".\" expected"; break;
			case 23: s = "\"move_x\" expected"; break;
			case 24: s = "\"move_y\" expected"; break;
			case 25: s = "\"rotate\" expected"; break;
			case 26: s = "\"set_position\" expected"; break;
			case 27: s = "\"spin\" expected"; break;
			case 28: s = "\"for\" expected"; break;
			case 29: s = "\"loops\" expected"; break;
			case 30: s = "\"while\" expected"; break;
			case 31: s = "\"if\" expected"; break;
			case 32: s = "\"set\" expected"; break;
			case 33: s = "\"=\" expected"; break;
			case 34: s = "\"or\" expected"; break;
			case 35: s = "\"and\" expected"; break;
			case 36: s = "\"+\" expected"; break;
			case 37: s = "\"-\" expected"; break;
			case 38: s = "\"*\" expected"; break;
			case 39: s = "\"/\" expected"; break;
			case 40: s = "\">\" expected"; break;
			case 41: s = "\"<\" expected"; break;
			case 42: s = "\"==\" expected"; break;
			case 43: s = "\"<=\" expected"; break;
			case 44: s = "\">=\" expected"; break;
			case 45: s = "\"!=\" expected"; break;
			case 46: s = "\"value\" expected"; break;
			case 47: s = "\"label\" expected"; break;
			case 48: s = "\"position_x\" expected"; break;
			case 49: s = "\"position_y\" expected"; break;
			case 50: s = "\"width\" expected"; break;
			case 51: s = "\"height\" expected"; break;
			case 52: s = "\"return\" expected"; break;
			case 53: s = "??? expected"; break;
			case 54: s = "invalid INSTRUCTION"; break;
			case 55: s = "invalid TYPE_FUNC"; break;
			case 56: s = "invalid TYPE_VAR"; break;
			case 57: s = "invalid ACTION"; break;
			case 58: s = "invalid ATTRIBUTE"; break;
			case 59: s = "invalid OP"; break;
			case 60: s = "invalid FACTOR"; break;
			case 61: s = "invalid FACTOR"; break;
			case 62: s = "invalid BOOL"; break;

			default: s = "error " + n; break;
		}
		ErrorPrinter.AddError(line, s);
		count++;
	}

	public virtual void SemErr (int line, int col, string s) {
		ErrorPrinter.AddError(line, s);
		count++;
	}
	
	public virtual void SemErr (string s) {
		ErrorPrinter.AddError(s);
		count++;
	}
	
	public virtual void Warning (int line, int col, string s) {
		ErrorPrinter.AddWarning(line, s);
	}
	
	public virtual void Warning(string s) {
		ErrorPrinter.AddWarning(s);
	}
} // Errors


public class FatalError: Exception {
	public FatalError(string m): base(m) {}
}
