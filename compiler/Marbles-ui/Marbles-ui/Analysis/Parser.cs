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
        QuadrupleManager.AddQuadruple(new Quadruple(Utilities.QuadrupleAction.Goto, -1, -1, -1));
		PROGRAM();
        QuadrupleManager.AddQuadruple(new Quadruple(Utilities.QuadrupleAction.end, -1, -1, -1));
    }

	void PROGRAM() {
		while (la.kind == 18) { // "asset"
			int index = CREATE_ASSET();
			try { MemoryManager.SetAssetInMemory((Asset)Utilities.finalAssetsInCanvas[index]); }
			catch (Exception e) { SemErr(e.Message); }
		}
		AssetIndex = 0;
		while (la.kind == 16) { // "var"
			// receive new variable to be created
			Variable newGlobalVariable = CREATE_VAR();

			// try to add it to globals
			try { MemoryManager.AddGlobalVariable(newGlobalVariable);}
			catch (Exception e) { SemErr(e.Message); }
		}
		while (la.kind == 9) { // "function"
			CREATE_FUNCTION();
		}
		Expect(6); // "instructions"
		Expect(7); // '{'
        QuadrupleManager.UpdateBeginQuadruple();
		while (StartOf(1)) {
			INSTRUCTION();
		}
		Expect(8); // '}'
	}

	/// <summary>
	/// Action called when a creat asset block is found
	/// </summary>
	/// <returns> The index of the asset being 
	/// created in Utilities.finalAssetsInCanvas </returns>
	int CREATE_ASSET() {
		Expect(18); // "asset"
		Expect(1); // id
		Expect(17); // ';'
		return AssetIndex++;
	}

	Variable CREATE_VAR() {
		Expect(16); // "var"
		SemanticCubeUtilities.DataTypes varType = TYPE_VAR();
		Expect(1); // id
		string varName = t.val;

		Variable newVar = new Variable(varName, varType);
		Expect(17); // ';'
		return newVar;
	}

	void CREATE_FUNCTION() {
		Expect(9); // "function"
		SemanticCubeUtilities.DataTypes functionType = TYPE_FUNC();
		Expect(1); // id
		string functionName = t.val;
		Function newFunction = new Function(functionName, functionType);
		try { QuadrupleManager.EnterFunction(newFunction); }
		catch (Exception e) { SemErr(e.Message); }

		Expect(10); // '('

		if (la.kind == 13 || la.kind == 14 || la.kind == 15) { // "text" || "number" || "bool"
			SemanticCubeUtilities.DataTypes parameterType = TYPE_VAR();
			Expect(1); // id
			string parameterName = t.val;
			try { QuadrupleManager.CreateFunction_LoadParameter(functionName, new Variable(parameterName, parameterType)); }
			catch (Exception e) { SemErr(e.Message); }
			while (la.kind == 11) { // ','
				Get();
				parameterType = TYPE_VAR();
				Expect(1); // id
				parameterName = t.val;
				try { QuadrupleManager.CreateFunction_LoadParameter(functionName, new Variable(parameterName, parameterType)); }
				catch (Exception e) { SemErr(e.Message); }
			}
		}
		Expect(12); // ')'
		Expect(7); // '{'
        while (la.kind == 16) { // "var"
			// add variables to the local variables directory
			var localVariable = CREATE_VAR();
			try { QuadrupleManager.CreateFunction_LoadLocalVariable(functionName, localVariable); }
			catch (Exception e) { SemErr(e.Message); }
		}

		try { FunctionDirectory.GetFunction(newFunction.GetName()).SetQuadrupleStart(QuadrupleManager.GetCounter()); }
		catch (Exception e) { SemErr(e.Message); }

		while (StartOf(1)) {
			INSTRUCTION();
		}

		Expect(8); // '}'
		try
		{
			QuadrupleManager.ExitFunction(); // releases local variable and generates quadruple 'endProc'
		}
		catch (Exception e) { SemErr(e.Message); }
	}

	void INSTRUCTION() {
		if (StartOf(2)) {
			if (la.kind == 20) { // "stop"
				STOP();
			} else if (la.kind == 21) { // "do"
				DO();
			} else if (la.kind == 32) { // "set"
				ASSIGNMENT();
			} else {
				RETURN();
			}
			Expect(17);
		} else if (la.kind == 28 || la.kind == 30 || la.kind == 31) { // "for" || "while" || "if"
			if (la.kind == 28) { // "for"
				FOR();
			} else if (la.kind == 30) { // "while"
				WHILE();
			} else {
				IFF();
			}
		} else SynErr(54);
	}

	SemanticCubeUtilities.DataTypes TYPE_FUNC() {
		if (la.kind == 13) { // "text"
			Get();
			return SemanticCubeUtilities.DataTypes.text;
		} else if (la.kind == 14) { // "number"
			Get();
			return SemanticCubeUtilities.DataTypes.number;
		} else if (la.kind == 15) { // "bool"
			Get();
			return SemanticCubeUtilities.DataTypes.boolean;
		} else SynErr(55);
		return SemanticCubeUtilities.DataTypes.invalidDataType;
	}

	SemanticCubeUtilities.DataTypes TYPE_VAR() {
		if (la.kind == 13) // "text"
		{
			Get();
			return SemanticCubeUtilities.DataTypes.text;
		}
		else if (la.kind == 14) // "number"
		{
			Get();
			return SemanticCubeUtilities.DataTypes.number;
		}
		else if (la.kind == 15) // "boolean"
		{
			Get();
			return SemanticCubeUtilities.DataTypes.boolean;
		}
        else
        {
            SynErr(56);
        }
            
		return SemanticCubeUtilities.DataTypes.invalidDataType;
	}

	void CALL_TO_FUNCTION() {
        Expect(19); // "call"
		Expect(1); // id
		string functionId = t.val;
        try { QuadrupleManager.CallFunctionBeforeParameters(functionId); }
        catch (Exception e) { SemErr(e.Message); }
        Expect(10); // '('
        try { QuadrupleManager.CallFunctionOpeningParenthesis(); }
        catch (Exception e) { SemErr(e.Message); }
        if (StartOf(3)) {
			SUPER_EXP(); // result of parameter
            try {
                QuadrupleManager.CallFunctionParameter(); 
                while (la.kind == 11) { // ','
				    Get();
                    SUPER_EXP(); // result of parameter
                    QuadrupleManager.CallFunctionParameter();
                }
            }
            catch (Exception e) { SemErr(e.Message); }
        }
		Expect(12); // ')'
        try { QuadrupleManager.CallFunctionClosingParenthesis(); }
        catch (Exception e) { SemErr(e.Message); }
        try { QuadrupleManager.CallFunctionEnd(); }
        catch (Exception e) { SemErr(e.Message); }
    }
	
	void EXP() {
		TERM();
		try { QuadrupleManager.PopOperator(SemanticCubeUtilities.OperatorToPriority(SemanticCubeUtilities.Operators.plus)); }
		catch (Exception e) { SemErr(e.Message); }
		while (la.kind == 36 || la.kind == 37) { // '+' or '-'
			if (la.kind == 36) { // '+'
				Get();
                QuadrupleManager.PushOperator(SemanticCubeUtilities.Operators.plus);
			} else { // '-'
				Get();
                QuadrupleManager.PushOperator(SemanticCubeUtilities.Operators.minus);
			}
			TERM();
			try { QuadrupleManager.PopOperator(SemanticCubeUtilities.OperatorToPriority(SemanticCubeUtilities.Operators.plus)); }
			catch (Exception e) { SemErr(e.Message); }
		}
	}

	void STOP() {
		Expect(20); // "stop"
		QuadrupleManager.ReadStop();
	}

	void DO() {
		Expect(21); // "do"
		Expect(1); // id
		try { QuadrupleManager.ReadAssetId(t.val); }
		catch (Exception e) { SemErr(e.Message); }
		Expect(22); // '.'
		ACTION();
	}

    // DONE
	void ASSIGNMENT() {
		Expect(32); // set
		Expect(1); // id
        string id = t.val;
        if (la.kind == 22) // '.'
        {
            try
			{
				QuadrupleManager.ReadAssetId(id);
				Get();
				ATTRIBUTE(); // we don't need to verify the attribute as the UI forces the user to select a valid one
			}
            catch (Exception e) { SemErr(e.Message); }
        }
        else
        {
			try { QuadrupleManager.ReadIDVariable(id); }
			catch (Exception e) { SemErr(e.Message); }
		}

        Expect(33); // '='
		SUPER_EXP();

        try { QuadrupleManager.AssignEnd(); }
        catch (Exception e) { SemErr(e.Message); }
    }

	void RETURN() {
		Expect(52); // "return"
		SUPER_EXP();
		try { QuadrupleManager.ReturnEnd(); }
		// TODO: fatal error
		catch (Exception e) { SemErr(e.Message); }
	}

	void FOR() {
		Expect(28); // "for"
		Expect(10); // '('
		SUPER_EXP();
		Expect(12); // ')'
		try { QuadrupleManager.ForAfterCondition(); }
		catch (Exception e) { SemErr(e.Message); }
		Expect(29); // "loops"
		Expect(7); // '{'
		while (StartOf(1)) {
			INSTRUCTION();
		}
		Expect(8); // '}'
        QuadrupleManager.ForEnd();
	}

	void WHILE() {
		Expect(30); // "while"
        QuadrupleManager.WhileBeforeCondition();
        Expect(10); // '('
		SUPER_EXP();
		Expect(12); // ')'
        QuadrupleManager.WhileAfterCondition();
		Expect(7); // '{'
		while (StartOf(1)) {
			INSTRUCTION();
		}
		Expect(8); // '}'
        QuadrupleManager.WhileEnd();
	}

	void IFF() {
		Expect(31); // "if"
		Expect(10); // '('
		SUPER_EXP();
		Expect(12); // ')'
        try { QuadrupleManager.IfAfterCondition(); }
        catch (Exception e) { SemErr(e.Message); }
		Expect(7); // '{'
		while (StartOf(1)) {
			INSTRUCTION();
		}
		Expect(8); // '}'
        try { QuadrupleManager.IfEnd(); }
        catch (Exception e) { SemErr(e.Message); }
    }

	void ACTION() {
        Utilities.AssetAction action;
		if (StartOf(4)) {
			if (la.kind == 23) { // "move_x"
                action = Utilities.AssetAction.move_x;
				Get();
				Expect(10); // '('
			} else if (la.kind == 24) { // "move_y"
                action = Utilities.AssetAction.move_y;
				Get();
				Expect(10); // '('
			} else if (la.kind == 25) { // "rotate"
                action = Utilities.AssetAction.rotate;
				Get();
				Expect(10); // '('
			} else { // "set_position"
                action = Utilities.AssetAction.set_position;
				Get();
				Expect(10); // '('
				SUPER_EXP();
				Expect(11); // ','
			}
			SUPER_EXP();
			Expect(12); // ')'
			try { QuadrupleManager.DoBlock_ReadAssetAction(action); }
			catch (Exception e) { SemErr(e.Message); }
		} else if (la.kind == 27) { // "spin"
            action = Utilities.AssetAction.spin;
			Get();
			Expect(10); // (
			Expect(12); // )
			try { QuadrupleManager.DoBlock_ReadAssetAction(action); }
			catch (Exception e) { SemErr(e.Message); }
		} else SynErr(57);
	}

	void SUPER_EXP() {
		EXP_L();
		while (la.kind == 34) { // or
			Get();
            QuadrupleManager.PushOperator(SemanticCubeUtilities.Operators.or);
            EXP_L();
			try { QuadrupleManager.PopOperator(SemanticCubeUtilities.OperatorToPriority(SemanticCubeUtilities.Operators.or)); }
			catch (Exception e) { SemErr(e.Message); }
		}
	}

	void ATTRIBUTE() {
		switch (la.kind) {
		case 46: { // "value"
			Get();
			QuadrupleManager.ReadAssetAttribute(MemoryManager.AssetAttributes.number);
			break;
		}
		case 47: { // "label"
			Get();
			QuadrupleManager.ReadAssetAttribute(MemoryManager.AssetAttributes.label);
			break;
		}
		case 48: { // "position_x"
			Get();
			QuadrupleManager.ReadAssetAttribute(MemoryManager.AssetAttributes.x);
			break;
		}
		case 49: { // "position_y"
			Get();
			QuadrupleManager.ReadAssetAttribute(MemoryManager.AssetAttributes.y);
			break;
		}
		case 50: { // "width"
			Get();
			QuadrupleManager.ReadAssetAttribute(MemoryManager.AssetAttributes.width);
			break;
		}
		case 51: { // "height"
			Get();
			QuadrupleManager.ReadAssetAttribute(MemoryManager.AssetAttributes.height);
			break;
		}
		default: SynErr(58); break;
		}
	}

	void EXP_L() {
		EXP_R();
		while (la.kind == 35) { // "and"
			Get();
            QuadrupleManager.PushOperator(SemanticCubeUtilities.Operators.and);
			EXP_R();
			try { QuadrupleManager.PopOperator(SemanticCubeUtilities.OperatorToPriority(SemanticCubeUtilities.Operators.and)); }
			catch (Exception e) { SemErr(e.Message); }
		}
	}

    // DONE
	void EXP_R() {
		EXP();
		if (StartOf(5)) {
			OP();
            SemanticCubeUtilities.Operators op = SemanticCubeUtilities.GetOperatorFromString(t.val);
            QuadrupleManager.PushOperator(op);
			EXP();
			try { QuadrupleManager.PopOperator(SemanticCubeUtilities.OperatorToPriority(op)); }
			catch (Exception e){ SemErr(e.Message); }
		}
	}

	void OP() {
		switch (la.kind) {
		case 40: { // >
			Get();
			break;
		}
		case 41: { // <
			Get();
			break;
		}
		case 42: { // ==
			Get();
			break;
		}
		case 43: { // <=
			Get();
			break;
		}
		case 44: { // >=
			Get();
			break;
		}
		case 45: { // !=
			Get();
			break;
		}
		default: SynErr(59); break;
		}
	}

    // DONE
	void TERM() {
		FACTOR();
		try { QuadrupleManager.PopOperator(SemanticCubeUtilities.OperatorToPriority(SemanticCubeUtilities.Operators.multiply)); }
		catch (Exception e) { SemErr(e.Message); }
		while (la.kind == 38 || la.kind == 39) { // '*' or '/'
			if (la.kind == 38) { // '*'
				Get();
                QuadrupleManager.PushOperator(SemanticCubeUtilities.Operators.multiply);
			} else { // '/'
				Get();
                QuadrupleManager.PushOperator(SemanticCubeUtilities.Operators.divide);
            }
			FACTOR();
			try { QuadrupleManager.PopOperator(SemanticCubeUtilities.OperatorToPriority(SemanticCubeUtilities.Operators.multiply)); }
			catch (Exception e) { SemErr(e.Message); }
		}
	}

    // Done
	void FACTOR() {
		if (la.kind == 10) { // '('
            // push a fake bottom
            QuadrupleManager.PushFakeBottom();
			Get();
			EXP_R();
			Expect(12); // ')'
            // exit fake bottom
            QuadrupleManager.PopFakeBottom();
		} else if (StartOf(6)) {
			if (la.kind == 37) { // negative sign
				Get();
				QuadrupleManager.PushOperator(SemanticCubeUtilities.Operators.negative);
			}
			if (la.kind == 4) { // number constant
				Get();
				try { QuadrupleManager.ReadConstantNumber(Int32.Parse(t.val)); }
				catch (Exception e) { SemErr(e.Message); }

            } else if (la.kind == 1) { // id
				Get();
                string id = t.val;
                if (la.kind == 22) // '.' character
                {
					try
					{
						QuadrupleManager.ReadAssetId(id);
						Get();
						ATTRIBUTE(); // we don't need to verify the attribute as the UI forces the user to select a valid one
					}
					catch (Exception e) { SemErr(e.Message); }
				}
                else
                {
					try { QuadrupleManager.ReadIDVariable(id); }
					catch (Exception e) { SemErr(e.Message); }
                }
			} else if (la.kind == 19) { // "call"
				CALL_TO_FUNCTION();
			} else if (la.kind == 2 || la.kind == 3) { // true or false
				BOOL();
			} else if (la.kind == 5) { // string constant
				Get();
				try { QuadrupleManager.ReadConstantText(t.val); }
				catch (Exception e) { SemErr(e.Message); }
			} else SynErr(60); // invalid FACTOR
		} else SynErr(61); // invalid FACTOR
	}

    // DONE
	void BOOL() {
		if (la.kind == 2) { // true
			Get();
			try { QuadrupleManager.ReadConstantBool(true); }
			catch (Exception e) { SemErr(e.Message); }
		} else if (la.kind == 3) { // false
			Get();
			try { QuadrupleManager.ReadConstantBool(false); }
			catch (Exception e) { SemErr(e.Message); }
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
			case 1: s = "ID expected"; break;
			case 2: s = "TRUE expected"; break;
			case 3: s = "FALSE expected"; break;
			case 4: s = "Numeric constant expected"; break;
			case 5: s = "Text constant expected"; break;
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
			case 57: s = "invalid ASSET BEHAVIOR"; break;
			case 58: s = "invalid ASSET PROPERTY"; break;
			case 59: s = "invalid OPERATOR"; break;
			case 60: s = "invalid FACTOR"; break;
			case 61: s = "invalid FACTOR"; break;
			case 62: s = "invalid BOOL"; break;

			default: s = "error " + n; break;
		}
		ErrorPrinter.AddError(line, s);
		count++;
		throw new Exception(s);
	}

	public virtual void SemErr (int line, int col, string s) {
		ErrorPrinter.AddError(line, s);
		count++;
        throw new Exception(s);
	}
	
	public virtual void SemErr (string s) {
		ErrorPrinter.AddError(s);
		count++;
        throw new Exception(s);
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
