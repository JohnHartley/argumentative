using System;
using System.Collections;
using System.Windows.Forms;


namespace Argumentative
{
	/// <summary>
	/// Interface for Undo function.  This is a far from a complete implementation.
	/// </summary>
	public interface Icommand
	{
		/// <summary>Executes undoable action</summary>
		/// <returns>true if successful</returns>
		bool execute();  // returns true if executed
		/// <summary>Undo this action</summary>
		bool undo();
		/// <summary>Returns the name of this action</summary>
		string getName();  // returns the command name
	}
	/// <summary>
	/// Implements a command pattern for undo
	/// </summary>
	public class Command
	{
		ArrayList commands;
		private Icommand lastExecuted;
		/// <summary>Constructs the command list</summary>
		public Command()
		{
			commands = new ArrayList();
			lastExecuted = null;
		}
		/// <summary>
		/// Load a Command object into the undo list
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public int loadCommand(Icommand c)
		{
			return commands.Add(c);
		}

		/// <summary>
		/// Execute a command in the undo sequence. Used for Redo
		/// </summary>
		/// <param name="slot">Which command</param>
		/// <param name="arg1">First argument to be passed</param>
		/// <param name="arg2">Second argument to be passed</param>
		public void executeCommand(int slot,object arg1,object arg2)
		{
			Icommand c = (Icommand) commands[slot];
			c.execute();
			lastExecuted = c;
		}
		/// <summary>
		/// Executes command and adds it to the undo queue.
		/// </summary>
		/// <param name="c"></param>
		public void executeCommand(Icommand c)
		{
			c.execute();
			this.lastExecuted = c;
		}

		/// <summary>
		/// Executes a command by name and adds it to the undo queue.
		/// </summary>
		/// <param name="whichCommand">Name of the command</param>
		public void executeCommand(string whichCommand)
		{
			int f;
			Icommand c;
			if(whichCommand == null)  // command without undo
			{
				lastExecuted = null;
				return;
			}
			for(f=0;f<commands.Count;f++)
			{
				c = (Icommand) commands[f];
				if(c.getName().Equals(whichCommand))
				{
					c.execute();
					lastExecuted = c;
					return;
				}
			}
		}
		/// <summary>
		/// Undo the last command.
		/// </summary>
		/// <returns></returns>
		public bool undo()
		{
			if(lastExecuted == null)
				return false;
			lastExecuted.undo();
			lastExecuted=null;
			return true;
		}
	}

	// **************************************** Commands *************************************
	
	/// <summary>
	/// Used for undo delete - should be stored in the Command object
	/// </summary>
	public class deleteCommand : Icommand
	{
		TreeNode oldNode,oldParent;
		int oldPosition;
		ArgMapInterface instance;

		/// <summary>
		/// Delete command constructor
		/// </summary>
		/// <param name="a"></param>
		public deleteCommand(ArgMapInterface a)
		{
			instance = a;
			oldNode = null;
		}
		/// <summary>
		/// Get the command's name
		/// </summary>
		/// <returns>Commands name</returns>
		public string getName()	{return "delete"; }

		/// <summary>Execute a delete command</summary>
		/// <returns></returns>
		public bool execute()
		{
			TreeView tv;
			TreeNode tn;
			// take a copy of the tree being deleted.
			tv = instance.getTreeView();
			oldNode = tv.SelectedNode;
			if(oldNode == null) return false;  // nothing selected
			oldParent = oldNode.Parent;
			if(oldParent==null) return false;
			oldPosition = oldParent.Nodes.IndexOf(oldNode);
			tn = instance.deleteNodeFromTree();
			if(tn == null) return false;
			System.Diagnostics.Debug.Assert(tn == oldNode,"Delete command failed");
			return true;
		}

		/// <summary>Undo a delete command</summary>
		/// <returns>Success</returns>
		public bool undo()
		{
			Node n,pn;
			if(oldNode==null | oldParent==null) return false;
			oldParent.Nodes.Insert(oldPosition,oldNode);
			// Reconnect the argument nodes
			n = (Node) oldNode.Tag;
			pn = (Node) oldParent.Tag;
			pn.kids.Insert(oldPosition,n);
			// Verify
			System.Diagnostics.Debug.Assert(instance.verifyTree(false)==null,"Sorry, Undo delete failed");
			return true;
		}

	}
	/// <summary>Implements a keypress command</summary>
	public class treeKeyPressCommand : Icommand
	{
		char c;
		string oldText;
		RichTextBox richTextBox1;

		/// <summary>
		/// Key press constructor
		/// </summary>
		/// <param name="c">key pressed</param>
		/// <param name="rtb">RichTextBox in which the key was pressed</param>
		public treeKeyPressCommand (char c,RichTextBox rtb)
		{
			this.c = c;
			this.richTextBox1 = rtb;
		}
		/// <summary>
		/// Execute keypress command
		/// </summary>
		/// <returns></returns>
		public bool execute()
		{
			oldText = richTextBox1.Text;
			richTextBox1.Focus();
			richTextBox1.Text = c + "";
			richTextBox1.SelectionStart = 2;
			return true;
		}

		/// <summary>
		/// Keypress undo
		/// </summary>
		/// <returns></returns>
		public bool undo()
		{
			richTextBox1.Text = oldText;
			// shift focus?
			return true;
		}

		/// <summary>
		/// Get name "treeKeyPress"
		/// </summary>
		/// <returns></returns>
		public string getName() { return "treeKeyPress"; }

	}
}
