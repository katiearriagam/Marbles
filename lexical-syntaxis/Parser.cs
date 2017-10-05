
using System;

namespace Marbles {



public class Parser {
	public const int _EOF = 0;
	public const int _id = 1;
	public const int _true = 2;
	public const int _false = 3;
	public const int _const_i = 4;
	public const int _const_s = 5;
	public const int maxT = 72;

	const bool _T = true;
	const bool _x = false;
	const int minErrDist = 2;
	
	public Scanner scanner;
	public Errors  errors;

	public Token t;    // last recognized token
	public Token la;   // lookahead token
	int errDist = minErrDist;



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
		while (la.kind == 18 || la.kind == 19 || la.kind == 33) {
			if (la.kind == 18) {
				CREATE_VAR();
			} else if (la.kind == 19) {
				CREATE_ASSET();
			} else {
				ASSIGNMENT();
				Expect(6);
			}
		}
		while (la.kind == 11) {
			CREATE_FUNCTION();
		}
		Expect(7);
		Expect(8);
		Expect(9);
		while (StartOf(1)) {
			INSTRUCTION();
		}
		Expect(10);
	}

	void CREATE_VAR() {
		Expect(18);
		TYPE_VAR();
		Expect(1);
		Expect(6);
	}

	void CREATE_ASSET() {
		Expect(19);
		Expect(1);
		Expect(6);
	}

	void ASSIGNMENT() {
		Expect(33);
		Expect(1);
		if (la.kind == 23) {
			Get();
			ATTRIBUTE();
		}
		Expect(34);
		if (StartOf(2)) {
			SUPER_EXP();
		} else if (StartOf(3)) {
			COLOR();
		} else SynErr(73);
	}

	void CREATE_FUNCTION() {
		Expect(11);
		TYPE_FUNC();
		Expect(1);
		Expect(12);
		if (la.kind == 15 || la.kind == 16 || la.kind == 17) {
			TYPE_VAR();
			Expect(1);
			while (la.kind == 13) {
				Get();
				TYPE_VAR();
				Expect(1);
			}
		}
		Expect(14);
		Expect(9);
		while (la.kind == 18) {
			CREATE_VAR();
		}
		while (StartOf(1)) {
			INSTRUCTION();
		}
		Expect(10);
	}

	void INSTRUCTION() {
		if (StartOf(4)) {
			if (la.kind == 21) {
				STOP();
			} else if (la.kind == 22) {
				DO();
			} else if (la.kind == 33) {
				ASSIGNMENT();
			} else if (la.kind == 20) {
				CALL_TO_FUNCTION();
			} else {
				RETURN();
			}
			Expect(6);
		} else if (la.kind == 29 || la.kind == 31 || la.kind == 32) {
			if (la.kind == 29) {
				FOR();
			} else if (la.kind == 31) {
				WHILE();
			} else {
				IFF();
			}
		} else SynErr(74);
	}

	void TYPE_FUNC() {
		if (la.kind == 15) {
			Get();
		} else if (la.kind == 16) {
			Get();
		} else if (la.kind == 17) {
			Get();
		} else if (la.kind == 7) {
			Get();
		} else SynErr(75);
	}

	void TYPE_VAR() {
		if (la.kind == 15) {
			Get();
		} else if (la.kind == 16) {
			Get();
		} else if (la.kind == 17) {
			Get();
		} else SynErr(76);
	}

	void CALL_TO_FUNCTION() {
		Expect(20);
		Expect(1);
		Expect(12);
		if (StartOf(2)) {
			EXP();
			while (la.kind == 13) {
				Get();
				EXP();
			}
		}
		Expect(14);
	}

	void EXP() {
		TERM();
		while (la.kind == 37 || la.kind == 38) {
			if (la.kind == 37) {
				Get();
			} else {
				Get();
			}
			TERM();
		}
	}

	void STOP() {
		Expect(21);
	}

	void DO() {
		Expect(22);
		Expect(1);
		Expect(23);
		ACTION();
	}

	void RETURN() {
		Expect(71);
		if (StartOf(2)) {
			SUPER_EXP();
		}
	}

	void FOR() {
		Expect(29);
		Expect(12);
		CREATE_VAR();
		ASSIGNMENT();
		Expect(6);
		Expect(1);
		Expect(30);
		EXP();
		Expect(6);
		ASSIGNMENT();
		Expect(14);
		Expect(9);
		while (StartOf(1)) {
			INSTRUCTION();
		}
		Expect(10);
	}

	void WHILE() {
		Expect(31);
		Expect(12);
		SUPER_EXP();
		Expect(14);
		Expect(9);
		while (StartOf(1)) {
			INSTRUCTION();
		}
		Expect(10);
	}

	void IFF() {
		Expect(32);
		Expect(12);
		SUPER_EXP();
		Expect(14);
		Expect(9);
		while (StartOf(1)) {
			INSTRUCTION();
		}
		Expect(10);
	}

	void ACTION() {
		if (StartOf(5)) {
			if (la.kind == 24) {
				Get();
				Expect(12);
			} else if (la.kind == 25) {
				Get();
				Expect(12);
			} else if (la.kind == 26) {
				Get();
				Expect(12);
			} else {
				Get();
				Expect(12);
				EXP();
				Expect(13);
			}
			EXP();
			Expect(14);
		} else if (la.kind == 28) {
			Get();
		} else SynErr(77);
	}

	void SUPER_EXP() {
		EXP_L();
		while (la.kind == 35) {
			Get();
			EXP_L();
		}
	}

	void ATTRIBUTE() {
		switch (la.kind) {
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
		case 15: {
			Get();
			break;
		}
		case 51: {
			Get();
			break;
		}
		case 52: {
			Get();
			break;
		}
		case 53: {
			Get();
			break;
		}
		case 54: {
			Get();
			break;
		}
		case 55: {
			Get();
			break;
		}
		case 56: {
			Get();
			break;
		}
		case 57: {
			Get();
			break;
		}
		case 58: {
			Get();
			break;
		}
		default: SynErr(78); break;
		}
	}

	void COLOR() {
		switch (la.kind) {
		case 59: {
			Get();
			break;
		}
		case 60: {
			Get();
			break;
		}
		case 61: {
			Get();
			break;
		}
		case 62: {
			Get();
			break;
		}
		case 63: {
			Get();
			break;
		}
		case 64: {
			Get();
			break;
		}
		case 65: {
			Get();
			break;
		}
		case 66: {
			Get();
			break;
		}
		case 67: {
			Get();
			break;
		}
		case 68: {
			Get();
			break;
		}
		case 69: {
			Get();
			break;
		}
		case 70: {
			Get();
			break;
		}
		default: SynErr(79); break;
		}
	}

	void EXP_L() {
		EXP_R();
		while (la.kind == 36) {
			Get();
			EXP_R();
		}
	}

	void EXP_R() {
		EXP();
		if (StartOf(6)) {
			OP();
			EXP();
		}
	}

	void OP() {
		switch (la.kind) {
		case 42: {
			Get();
			break;
		}
		case 30: {
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
		case 46: {
			Get();
			break;
		}
		default: SynErr(80); break;
		}
	}

	void TERM() {
		FACTOR();
		while (la.kind == 39 || la.kind == 40) {
			if (la.kind == 39) {
				Get();
			} else {
				Get();
			}
			FACTOR();
		}
	}

	void FACTOR() {
		if (la.kind == 12) {
			Get();
			EXP_R();
			Expect(14);
		} else if (StartOf(7)) {
			if (la.kind == 38 || la.kind == 41) {
				if (la.kind == 38) {
					Get();
				} else {
					Get();
				}
			}
			if (la.kind == 4) {
				Get();
			} else if (la.kind == 1) {
				Get();
				if (la.kind == 23) {
					Get();
					ATTRIBUTE();
				}
			} else if (la.kind == 20) {
				CALL_TO_FUNCTION();
			} else if (la.kind == 2 || la.kind == 3) {
				BOOL();
			} else if (la.kind == 5) {
				Get();
			} else SynErr(81);
		} else SynErr(82);
	}

	void BOOL() {
		if (la.kind == 2) {
			Get();
		} else if (la.kind == 3) {
			Get();
		} else SynErr(83);
	}



	public void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		Marbles();
		Expect(0);

	}
	
	static readonly bool[,] set = {
		{_T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_x, _x,_x,_x,_x, _x,_T,_x,_T, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x},
		{_x,_T,_T,_T, _T,_T,_x,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_x, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_x, _x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x},
		{_x,_T,_T,_T, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_x, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x}

	};
} // end Parser


public class Errors {
	public int count = 0;                                    // number of errors detected
	public System.IO.TextWriter errorStream = Console.Out;   // error messages go to this stream
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
			case 6: s = "\";\" expected"; break;
			case 7: s = "\"empty\" expected"; break;
			case 8: s = "\"instructions\" expected"; break;
			case 9: s = "\"{\" expected"; break;
			case 10: s = "\"}\" expected"; break;
			case 11: s = "\"function\" expected"; break;
			case 12: s = "\"(\" expected"; break;
			case 13: s = "\",\" expected"; break;
			case 14: s = "\")\" expected"; break;
			case 15: s = "\"text\" expected"; break;
			case 16: s = "\"number\" expected"; break;
			case 17: s = "\"bool\" expected"; break;
			case 18: s = "\"var\" expected"; break;
			case 19: s = "\"asset\" expected"; break;
			case 20: s = "\"call\" expected"; break;
			case 21: s = "\"stop\" expected"; break;
			case 22: s = "\"do\" expected"; break;
			case 23: s = "\".\" expected"; break;
			case 24: s = "\"move_x\" expected"; break;
			case 25: s = "\"move_y\" expected"; break;
			case 26: s = "\"turn\" expected"; break;
			case 27: s = "\"set_position\" expected"; break;
			case 28: s = "\"spin\" expected"; break;
			case 29: s = "\"for\" expected"; break;
			case 30: s = "\"<\" expected"; break;
			case 31: s = "\"while\" expected"; break;
			case 32: s = "\"if\" expected"; break;
			case 33: s = "\"set\" expected"; break;
			case 34: s = "\"=\" expected"; break;
			case 35: s = "\"or\" expected"; break;
			case 36: s = "\"and\" expected"; break;
			case 37: s = "\"+\" expected"; break;
			case 38: s = "\"-\" expected"; break;
			case 39: s = "\"*\" expected"; break;
			case 40: s = "\"/\" expected"; break;
			case 41: s = "\"not\" expected"; break;
			case 42: s = "\">\" expected"; break;
			case 43: s = "\"==\" expected"; break;
			case 44: s = "\"<=\" expected"; break;
			case 45: s = "\">=\" expected"; break;
			case 46: s = "\"!=\" expected"; break;
			case 47: s = "\"color\" expected"; break;
			case 48: s = "\"transparency\" expected"; break;
			case 49: s = "\"size\" expected"; break;
			case 50: s = "\"value\" expected"; break;
			case 51: s = "\"length\" expected"; break;
			case 52: s = "\"type\" expected"; break;
			case 53: s = "\"position_x\" expected"; break;
			case 54: s = "\"position_y\" expected"; break;
			case 55: s = "\"rotation\" expected"; break;
			case 56: s = "\"font_size\" expected"; break;
			case 57: s = "\"width\" expected"; break;
			case 58: s = "\"height\" expected"; break;
			case 59: s = "\"red\" expected"; break;
			case 60: s = "\"orange\" expected"; break;
			case 61: s = "\"yellow\" expected"; break;
			case 62: s = "\"green\" expected"; break;
			case 63: s = "\"blue\" expected"; break;
			case 64: s = "\"violet\" expected"; break;
			case 65: s = "\"purple\" expected"; break;
			case 66: s = "\"pink\" expected"; break;
			case 67: s = "\"brown\" expected"; break;
			case 68: s = "\"white\" expected"; break;
			case 69: s = "\"gray\" expected"; break;
			case 70: s = "\"black\" expected"; break;
			case 71: s = "\"return\" expected"; break;
			case 72: s = "??? expected"; break;
			case 73: s = "invalid ASSIGNMENT"; break;
			case 74: s = "invalid INSTRUCTION"; break;
			case 75: s = "invalid TYPE_FUNC"; break;
			case 76: s = "invalid TYPE_VAR"; break;
			case 77: s = "invalid ACTION"; break;
			case 78: s = "invalid ATTRIBUTE"; break;
			case 79: s = "invalid COLOR"; break;
			case 80: s = "invalid OP"; break;
			case 81: s = "invalid FACTOR"; break;
			case 82: s = "invalid FACTOR"; break;
			case 83: s = "invalid BOOL"; break;

			default: s = "error " + n; break;
		}
		errorStream.WriteLine(errMsgFormat, line, col, s);
		count++;
	}

	public virtual void SemErr (int line, int col, string s) {
		errorStream.WriteLine(errMsgFormat, line, col, s);
		count++;
	}
	
	public virtual void SemErr (string s) {
		errorStream.WriteLine(s);
		count++;
	}
	
	public virtual void Warning (int line, int col, string s) {
		errorStream.WriteLine(errMsgFormat, line, col, s);
	}
	
	public virtual void Warning(string s) {
		errorStream.WriteLine(s);
	}
} // Errors


public class FatalError: Exception {
	public FatalError(string m): base(m) {}
}
}