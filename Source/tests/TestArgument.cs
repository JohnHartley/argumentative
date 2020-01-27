/*
 * Created by SharpDevelop.
 * User: John
 * Date: 26/10/2007
 * Time: 5:57 PM
 */

using System;
using NUnit.Framework;

namespace Argumentative.tests
{
	/// <summary>Test the Argument object</summary>
	[TestFixture]
	public class TestArgument
	{
		/// <summary>Creating a default argument</summary>
		[Test]
		public void TestArgNew()
		{
			Options.OptionsData.init(null);
			Argument a,b;
			a = new Argument();
			b = new Argument();
			a.setupSample();
			b.setupSample();
			Assert.IsTrue(a.Equals(b),"Arguments should be equal");
			Assert.AreEqual(System.Environment.UserName,a.Author,"Author should not be "+a.Author);
			Assert.AreEqual(a.getDepth(),3);
		}
		
		/// <summary>Test statistics against the default argument</summary>
		[Test]
		public void TestStats()
		{
			Options.OptionsData.init(null);
			Argument a;
			a = new Argument();
			a.setupSample();
			int d = a.getDepth(null);
			Assert.AreEqual(3,d);
			int wordcount = a.wordCount(null);
			Assert.AreEqual(20,wordcount);
			int nodeCount = a.nodeCount(null);
			Assert.AreEqual(5,nodeCount);
		}
		
		/// <summary></summary>
		[Test]
		public void TestArgumentOperations()
		{
			Argument a;
			Node head,n;
			
			Options.OptionsData.init(null);
			a = new Argument();
			a.setupSample();
			
			head = a.findHead();
			Assert.IsNotNull(head);
			a.referenceTree();
			n = a.findByReference("1.1");
			Assert.IsNotNull(n);
			Assert.That(n.EditorText,Is.EqualTo("A helper for the reason"));
		}
		
		/// <summary>
		/// Export and import an argument using XML in a string.
		/// </summary>
		[Test]
		public void TestArgumentXML()
		{
			Argument a,b;
			System.Text.StringBuilder sb;
			string s;
			
			a = new Argument();
			a.setupSample();
			Node head = a.findHead();
			sb = a.writeArgumentXMLstring(a.findHead(),false);
			Assert.IsNotNull(sb);
			s = sb.ToString();
			Assert.Greater(s.Length,99);
			int i = s.IndexOf("<modified>");
			Assert.IsTrue(i > 0,"<modified> tag not found");
			
			i = s.IndexOf("</modified>");
			Assert.IsTrue(i > 0,"</modified> tag not found");
			
			b = new Argument();
			b = b.loadXmlArgString(sb.ToString());
			int d = b.getDepth(b.findHead());
			Assert.AreEqual(3,d);
		}
	}
}
