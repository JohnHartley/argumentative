/*
 * Created by SharpDevelop.
 * User: John
 * Date: 15/12/2008
 * Time: 6:58 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using NUnit.Framework;
using System.IO;

namespace Argumentative.tests
{
	/// <summary>
	/// Tests the Transform engine
	/// </summary>
	[TestFixture]
	public class TestTransform
	{
		// Path to sample files
		static string p1 = "..\\..\\Examples\\";
		static string p2 = "Examples\\";
		static string testFileName = "text.xslt";
		
		/// <summary>
		/// Does a basic transform run without error?
		/// </summary>
		[Test]
		public void TestBasicTransform()
		{
			Argument a;
			string s;
			bool success;
			Transform t;
			int c;
			string path;
			
            // TODO Update path
			Assert.IsTrue(File.Exists("..\\..\\Examples\\text.xslt"));
			
			path = "";
			if(File.Exists(p1+testFileName))
				path = p1;
			else if(File.Exists(p2+testFileName))
				path = p2;
			else
				Assert.Fail(testFileName+" not found on the following paths: "+
				            p1+testFileName+" and "+p2+testFileName+" from relative path "+Directory.GetCurrentDirectory());
			
			a = new Argument();
			a.setupSample();
			Node head = a.findHead();
			// TODO Transform argument
			// TODO Load new argument
			// TODO make string version of the file
			t = new Transform();
			success = t.doTransform("",path+testFileName,"test.txt",a);
			Assert.IsTrue(success);
			
			System.IO.StreamReader tr = new System.IO.StreamReader("test.txt");
			c=0;
			while(! tr.EndOfStream)
			{
				s = tr.ReadLine();
				c++; // count line
			}
			tr.Close();
			Assert.AreEqual(5,c);
		}
	}
}
