/*
 * Created by SharpDevelop.
 * User: John Hartley
 * Date: 14/10/2007
 * Time: 9:32 PM
 * 
 */

using System;
using NUnit.Framework;

namespace Argumentative.tests
{
	/// <summary>
	/// Preference testing
	/// </summary>
	[TestFixture]
	public class TestPrefs
	{
		private Prefs p;
		
		/// <summary>Set and retrieve a string preference.</summary>
		[Test]
		public void TestString()
		{
			p = new Prefs();
			p.setPref("pref1","general","A value");
			Assert.AreSame("A value",p.getPref("pref1"));
			Assert.IsTrue(p.prefExists("pref1"));
			Assert.IsFalse(p.prefExists("dog"));
			Assert.IsFalse(p.prefExists("Pref1"));
			
			p.setPref("1","standard","one");
			Assert.AreSame("one",p.getPref("1"));
			Assert.IsTrue(p.prefExists("1"));
			Assert.IsFalse(p.prefExists("one"));
			
			p.setPref("","standard","empty");
			Assert.AreSame("empty",p.getPref(""));
		}
		
		/// <summary>Set and retrieve a int number preference.</summary>
		[Test]
		public void TestInt()
		{
			p = new Prefs();
			int i=123;
			int maxneg;
			maxneg = -(int.MaxValue-1000);
			System.Drawing.Color cRed  = System.Drawing.Color.Red;
			p.setPref("prefInt","general",i.ToString());
			Assert.AreEqual(123,p.getInt("prefInt",999));
			p.setPref("prefInt","general","#0000FF");
			Assert.AreEqual(255L,p.getInt("prefInt",999));
			p.setPref("prefNegativeInt","general","-456");
			Assert.AreEqual("-456",p.getPref("prefNegativeInt"));
			Assert.AreEqual(-456,p.getInt("prefNegativeInt",999));
			p.setPref("prefNegativeInt","general",maxneg.ToString());
			Assert.AreEqual(maxneg,p.getInt("prefNegativeInt",999));
		}
		
		/// <summary>Set and retrieve a boolean preference.</summary>
		[Test]
		public void TestBool()
		{
			p = new Prefs();
			bool b=true;
			p.setPref("prefbool","general",b.ToString());
			Assert.AreEqual(true,p.getBool("prefbool"));
			Assert.AreEqual(false,p.getBool("Prefbool"));
		}
		
		/// <summary>Set and retrieve a colour preference.</summary>
		[Test]
		public void TestColour()
		{
			System.Drawing.Color c,c2;
			string s;
			
			c = System.Drawing.Color.Chartreuse;
			c2 = System.Drawing.Color.CornflowerBlue;
			p = new Prefs();
			p.setColour("theColour","general",c);
			s = p.getPref("theColour");
			Assert.That(s,Is.EqualTo("#7FFF00"));
			try {
			c2 = p.getColour("theColour",System.Drawing.Color.Red);
			}
			catch (Exception ex)
			{
				Assert.IsTrue(false,ex.Message);
			}
			Assert.That("#"+c2.ToArgb().ToString("X").Substring(2),Is.EqualTo("#7FFF00"));
		}
		
		/// <summary>The original set of tests for preferences.</summary>
		[Test]
		public void TestOriginal()
		{
			Prefs p;
			string s;
			int i=3212,i2;
			float f=32.3F,f2;
			p = new Prefs();
			p.setPref("One","general", "1");
			p.setPref("Two","general", "2");
			p.setPref("Int","general",i.ToString());
			p.setPref("Float","general",f.ToString());
			Assert.IsTrue(p.prefExists("One"));
			Assert.IsTrue(p.prefExists("Two"));

			Assert.IsTrue(!p.prefExists("one"));
			s = p.getPref("One");
			Assert.IsTrue(s.Equals("1"));
			s = p.getPref("Two");
			Assert.IsTrue(s.Equals("2"));
			s = p.getPref("one");
			Assert.IsTrue(s.Equals(""));
			i2 = (int) p.getInt("Int",0,0);
			Assert.IsTrue(i == i2);
			f2 = p.getFloat("Float");
			Assert.IsTrue(f.Equals(f2));
			System.Drawing.Font font = new System.Drawing.Font("Arial",16);
			p.setFont("TheFont","general",font);
			System.Drawing.Font font2;
			font2 = p.getFont("TheFont",new System.Drawing.Font("Arial",10));
			Assert.IsTrue(font2.Name.Equals("Arial"));
			System.Drawing.Color c = System.Drawing.Color.Chartreuse;
			p.save("","");
			p = new Prefs();
			p.load("");
			Assert.IsTrue(p.prefExists("One"));
			Assert.IsTrue(p.prefExists("Two"));

			Assert.IsTrue(!p.prefExists("one"));
			s = p.getPref("One");
			Assert.IsTrue(s.Equals("1"));
			s = p.getPref("Two");
			Assert.IsTrue(s.Equals("2"));
			s = p.getPref("one");
			Assert.IsTrue(s.Equals(""));
			
		}
	}
}
