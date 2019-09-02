using System;

namespace Argumentative
{
	/// <summary>Tokenises a RE3 file</summary>
	public class Tokeniser
	{
		private string filename;
		System.IO.StreamReader file;
		string line;
		/// <summary>
		/// Starts the tokeniser
		/// </summary>
		/// <param name="fileName">File to tokenise</param>
		public Tokeniser(string fileName)
		{
			filename = fileName;
			if(System.IO.File.Exists(fileName))
				file = new System.IO.StreamReader(fileName);
		}
		/// <summary>Closes down the tokeniser</summary>
		public void close() 
		{
			file.Close();
		}

		/// <summary>Token types recognised.</summary>
		public enum TokenType 
		{
			/// <summary>End of File</summary>
			T_Eof	= -1,
			/// <summary>Undefined token</summary>
			T_Null	= 0,
			/// <summary>Label token</summary>
			T_Label,
			/// <summary>Node definition</summary>
			T_Node,
			/// <summary>A collection of nodes</summary>
			T_Nodes,
			/// <summary>Edge token represts a line</summary>
			T_Edge,
			/// <summary>Collection of edges</summary>update here
			T_Edges,
			/// <summary>Number token</summary>
			T_Number,
			/// <summary>String token</summary>
			T_String, // in double quotes
		}

		// Tokeniser variables
		private TokenType currentToken = TokenType.T_Null;
		private string tokenStringValue;
		private double tokenNumberValue;  // not used externally
		// private int c;  // Current character
		private int currentLine = 1;

		/// <summary>
		/// Get the next token from the RE3 file
		/// </summary>
		/// <returns>The token type</returns>
		public TokenType getNextToken()
		{
			char cc;
		
			if(file == null)
			{
				currentToken = TokenType.T_Eof;
				return TokenType.T_Eof;
			}

			line = file.ReadLine();
			currentLine ++;

			if(line == null)
			{
				currentToken = TokenType.T_Eof;
				return TokenType.T_Eof;
			}

			tokenStringValue = line;
			if(line.Equals(""))
			{
				this.currentToken = TokenType.T_String;
				return currentToken;
			}

			if(line.StartsWith("//"))  // could be a comment
			{ 
				currentToken = TokenType.T_Label;
				tokenStringValue = line.Substring(3);
				if(tokenStringValue.Equals("Node"))
					currentToken = TokenType.T_Node;
				if(tokenStringValue.Equals("Nodes"))
					currentToken = TokenType.T_Nodes;
				if(tokenStringValue.Equals("Edge"))
					currentToken = TokenType.T_Edge;
				if(tokenStringValue.Equals("Edges"))
					currentToken = TokenType.T_Edges;
				return currentToken;
			}

			cc = line[0];
			if(Char.IsDigit( (char) cc) | cc=='.'  | cc=='-' )  
			{
				tokenStringValue = line;
				
				try  // is throwing an exception when converting "-"
				{
					tokenNumberValue = Double.Parse(line.Trim());
				} 
				catch (Exception e)
				{
					tokenStringValue = "" + e;	// append error
				}
				currentToken = TokenType.T_Number;
				return currentToken;
			}
			// unknown - just a string
			currentToken = TokenType.T_String;
			return currentToken;
		}

		/// <summary>Get current token type</summary>
		/// <returns>Current token type</returns>
		public TokenType getTokenType() {return currentToken; }
		/// <summary>String value of current token.</summary>
		/// <returns>String value.</returns>
		public string getToken_string() {return tokenStringValue; }
		/// <summary>Current token if a number</summary>
		/// <returns>Converted number.</returns>
		public long getLongValue() {return (long) tokenNumberValue; }
	}
}
