using System;
using System.Collections;

namespace Argumentative
{
	/// <summary>
	/// An edge object connects elements in a RE3 file
	/// </summary>
	public class Edge : Element
	{
		/// <summary>Reference of previous node</summary>
		public string fromNodeName;
		/// <summary>Reference of previous node</summary>
		public string toNodeName;
		private long hFromPortNumber;  
	
		/// <summary>Edge constructor</summary>
		public Edge()  : base()
		{
			type = Tokeniser.TokenType.T_Edge;
		}
	
		/// <summary>
		/// Passes an edge into the edge object
		/// </summary>
		/// <param name="T">Tokeniser to use</param>
		/// <returns></returns>
		public bool parseElement(Tokeniser T) 
		{
		
			assertToken(T,Tokeniser.TokenType.T_Edge,"Edge expected");
			labelExpected(T,"name");
			T.getNextToken();
			name = T.getToken_string();
			labelExpected(T,"fromNodeName");
			T.getNextToken();
			assertToken(T,Tokeniser.TokenType.T_String,"String expected");
			fromNodeName = T.getToken_string();
		
			labelExpected(T,"toNodeName");
			T.getNextToken();
			assertToken(T,Tokeniser.TokenType.T_String,"String expected");
			toNodeName = T.getToken_string();
		
			T.getNextToken();
			if(T.getToken_string().Equals("hFromPortNumber"))
			{
				T.getNextToken();
				assertToken(T,Tokeniser.TokenType.T_Number,"Number expected");
				hFromPortNumber = T.getLongValue();
				T.getNextToken();
			}
		
			return true;
		}
	}
}
