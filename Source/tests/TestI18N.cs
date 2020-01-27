/*
 * Created by SharpDevelop.
 * User: John
 * Date: 9/09/2008
 * Time: 9:13 PM
 *
 */

using System;
using NUnit.Framework;

namespace Argumentative.tests
{
	/// <summary>
	/// Test internationalisation functionality.
	/// </summary>
	[TestFixture]
	public class TestI18N
	{
		/// <summary>
		/// String retrieval
		/// </summary>
		[Test]
		public void TestStrings()
		{
			I18N.init("");
			string s = I18N.getString("testString");
			Assert.IsTrue(s.Equals("test"),"String incorrect: "+s);
			Assert.That(s, Is.EqualTo("test"));
			s = I18N.getString("dummy");
			Assert.That( s, Is.EqualTo("dummy is an unknown string resource") );
			
			Assert.AreEqual("Default",I18N.getString("dummy","Default"));
			Assert.AreEqual("test",I18N.getString("testString","Default"));
			
			Assert.That(I18N.getLanguage,Is.EqualTo("en"));
			Assert.That(I18N.getLocale(),Is.EqualTo("en-AU"));
		}
	}
}
