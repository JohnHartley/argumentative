using System;

namespace Argumentative
{
	/// <summary>
	/// Basis for a Node and used mainly in RE3 import
	/// </summary>
	public class Element
	{
		/// <summary>Only used in RE3 import</summary>
		internal string name;
		/// <summary>Token type of element</summary>
		public Tokeniser.TokenType type;
	
		/// <summary>
		/// Expect a particular token in the input stream
		/// </summary>
		/// <param name="T"></param>
		/// <param name="token"></param>
		/// <param name="msg"></param>
		public void assertToken(Tokeniser T,Tokeniser.TokenType token,String msg) 
		{
			if(T.getTokenType()== token)
				return;
			System.Diagnostics.Debug.Assert(false,T.getTokenType()+" token found,"+token+" expected : "+msg);
		}
		/// <summary>Expect a label in the input stream</summary>
		/// <param name="T"></param>
		/// <param name="labelName"></param>
		public void labelExpected(Tokeniser T,String labelName) 
		{
			T.getNextToken();
			assertToken(T,Tokeniser.TokenType.T_Label,"A label starting with '// "+
				labelName+"' expected.  Found '"+T.getToken_string()+"'.");
			System.Diagnostics.Debug.Assert(T.getToken_string().Equals(labelName),
			                                "// "+labelName+" expected. Found '"+T.getToken_string()+"'.");
		}
	}
}
