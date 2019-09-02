/*
 * Created by SharpDevelop.
 * User: John
 * Date: 28/07/2008
 * Time: 8:08 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Reflection;
using System.Resources;


namespace Argumentative
{
	/// <summary>
	/// Internationalisation Resources
	/// </summary>
	public class I18N
	{
		private static I18N instance = null;
		private ResourceManager resources;
		
		/// <summary>
		/// Internationalisation constructor
		/// </summary>
		public I18N()
		{
			Assembly assembly;
			string locale,resFileName;
			assembly = Assembly.GetExecutingAssembly();
			locale = getLocale();
			resFileName = "Argumentative.Resource-"+locale;
			resources = new ResourceManager(resFileName,assembly);
			System.Diagnostics.Debug.Assert(resources != null);
			
			// resources = new ResourceManager("Argumentative.Resource", assembly);
		}
		
		/// <summary>
		/// Internationalisation singleton creation.
		/// </summary>
		/// <param name="localeName">Locale name e.g. "en-GB". Empty string will lookup the current locale</param>
		public static void init(string localeName)
		{
			string locale;
			if(String.IsNullOrEmpty(localeName))
				locale = getLocale();
			if(instance == null)
				instance = new I18N();
		}
		
		/// <summary>Get two letter ISO language string</summary>
		public static string getLanguage
		{
			get { return System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName; }
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static string getLocale()
		{
			return System.Threading.Thread.CurrentThread.CurrentCulture.Name;
		}
		/// <summary>
		/// Get string
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static string getString(string name)
		{
			if(instance == null)
				instance = new I18N();
			
			string s = instance.resources.GetString(name);
			if(s==null)
				s = String.Format("{0} is an unknown string resource",name);
			return s;
		}
		
		/// <summary>
		/// Get international string
		/// </summary>
		/// <remarks>An interim function to transition to internationalisation</remarks>
		/// <param name="name">Resource name</param>
		/// <param name="defaultString">Default string if the resource name not found</param>
		/// <returns></returns>
		public static string getString(string name,string defaultString)
		{
			if(instance == null)
				instance = new I18N();
			
			string s = instance.resources.GetString(name);
			if(s==null)
				return defaultString;
			return s;
		}
	}
}
