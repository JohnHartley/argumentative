/*
 * Created by SharpDevelop.
 * User: John
 * Date: 25/10/2007
 * Time: 8:52 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using NUnit.Framework;

namespace Argumentative.tests
{
	
	/// <summary>
	/// Test Node operations
	/// </summary>
	[TestFixture]
	public class TestNode
	{
		
		/// <summary>
		/// Creating and adding element nodes.
		/// </summary>
		[Test]
		public void TestNewAndAdd()
		{
			Node n,nn,n3;
			
			n = new Node();
			Assert.IsNotNull(n);
			Assert.IsTrue(n.ArgumentNodeTypeString().Equals("unknown"),"unknown expected,"+n.ArgumentNodeTypeString()+" returned");
			n.nodeType = Node.ArgumentNodeType.premise;
			Assert.IsTrue(n.ArgumentNodeTypeString().Equals("Premise"),"Premise expected,"+n.ArgumentNodeTypeString()+" returned");
			Assert.AreEqual(0,n.countKids());
			nn = new Node("Reason One",Node.ArgumentNodeType.reason,"R1");
			n3 = new Node("Reason Two");
			n.addKid(nn);
			Assert.AreEqual(1,n.countKids());
			n.addKid(nn);
			Assert.AreEqual(1,n.countKids());
			n.addKid(n3);
			Assert.AreEqual(2,n.countKids());
		}
		
		/// <summary>
		/// Changing nodes
		/// </summary>
		[Test]
		public void TestChange()
		{
			Node n;
			n = new Node("Premise",Node.ArgumentNodeType.premise,"Q1");
			Assert.AreSame("Q1",n.getRef(),"Reference does not match");
			Assert.AreNotSame("R1",n.getRef(),"Not Q1");
			Assert.AreSame(null,n.getRnotation(),"R Notation should not be defined");
			n.setRef("R1");
			Assert.That(n.getRef(),Is.EqualTo("R1"));
			Assert.AreNotSame(n.name,"Dog","What is name");
			Assert.That(n.EditorText,Is.EqualTo("Premise"));
			Assert.IsTrue(n.nodeType == Node.ArgumentNodeType.premise);
		}
		
		/// <summary>
		/// Test adding child nodes
		/// </summary>
		[Test]
		public void TestKids()
		{
			Node n,k1,k2;
			n = new Node("Premise",Node.ArgumentNodeType.premise,"Q1");
			k1 = new Node();
			k2 = new Node();
			n.addKid(k1);
			Assert.IsTrue(n.countKids() == 0,"Empty child element added");
			k1.EditorText = "A reason";
			n.addKid(k1);
			Assert.AreEqual(n.countKids(),1);
			Argument b = new Argument();
			int d = b.getDepth(n);
			Assert.AreEqual(2,d);
		}
	}
}
