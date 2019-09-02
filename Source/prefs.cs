using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Text;
using System.Data;
using System.Diagnostics;

namespace Argumentative
{
	/// <summary> Implements a preference file and storage </summary>
	public class Prefs
	{
		private ArrayList a;  // primary storage
		private string fileName;
		
		class prefRec
		{
			public string name;
			public string section;
			public string value;
		}

		/// <summary>Preference constructor</summary>
		public Prefs()
		{
			a = new ArrayList();
		}

		private int getIndex(string name)
		{
			int f;
			prefRec p;
			for (f = 0; f < a.Count; f++)
			{
				p = (prefRec)a[f];
				if (p.name.Equals(name))
					return f;
			}
			return -1;
		}

		/// <summary>
		/// Does a preference name exist?
		/// </summary>
		/// <param name="name">Preference name to search for. Case sensitive</param>
		/// <returns>true if there is a name match.</returns>
		public bool prefExists(string name)
		{
			int i;
			i = getIndex(name);
			if (i == -1) // new entry
				return false;
			return true;
		}

		/// <summary>
		/// Define a preference
		/// </summary>
		/// <param name="name">Preference name</param>
		/// <param name="defaultValue"></param>
		/// <param name="section"></param>
		public void defPref(string name,string defaultValue,string section)
		{
			System.Diagnostics.Debug.Assert(! prefExists(name),name+" already has been defined as a preference");
			prefRec p = new prefRec();
			p.section = section;
			p.name = name;
			p.value = defaultValue;
			a.Add(p);
		}

		/// <summary>Set a general preference. Usually using the .ToString() function</summary>
		/// <param name="name"></param>
		/// <param name="section"></param>
		/// <param name="value"></param>
		public void setPref(string name, string section, string value)
		{
			int i;
			prefRec p;
			System.Diagnostics.Debug.Assert(section != null && section.Length > 0,"section not specified for "+name);
			i = getIndex(name);
			if(i == -1)  // add new preference
			{
				p = new prefRec();
				p.section = section;
				p.name = name;
				p.value = value;
				a.Add(p);
			}
			else // set the preference
			{
				p = (prefRec) a[i];
				p.value = value;
			}
		}
		
		/// <summary>Returns the value of a string preference</summary>
		/// <param name="name">Preference name</param>
		/// <returns>The value of the preference. "" if the Preference name not found</returns>
		public string getPref(string name)
		{
			int i;
			i = getIndex(name);
			if (i == -1) // new entry
				return "";
			prefRec p;
			p = (prefRec)a[i];
			return p.value;
		}

		/// <summary>
		/// Get integer preference with minimum
		/// </summary>
		/// <param name="name">Preference name</param>
		/// <param name="defaultInt"></param>
		/// <param name="min"></param>
		/// <returns></returns>
		public long getInt(string name,int defaultInt,long min)
		{
			long i;
			i = getInt(name,defaultInt);
			if(i<min)
				i=min;
			return i;
		}
		/// <summary>
		/// Get integer preference
		/// </summary>
		/// <param name="name">Preference name</param>
		/// <param name="defaultInt"></param>
		/// <returns></returns>
		public long getInt(string name,long defaultInt)
		{
			string s;
			long i;
			if(! this.prefExists(name))
			{
				// setPref(name,section,defaultInt.ToString());
				return defaultInt;
			}
			s = getPref(name);
			if(s=="") return 0;
			try
			{
				if(s.Substring(0,1).Equals("#"))
					i = Int64.Parse(s.Substring(1),System.Globalization.NumberStyles.AllowHexSpecifier);
				else if((s.Length==8) && s.Substring(0,2).Equals("FF")) // colour with transparency
					i = Int64.Parse(s,System.Globalization.NumberStyles.AllowHexSpecifier);
				else  // anything else is an int
					i = Int64.Parse(s);
			}
			catch { i=defaultInt; }  // any funny business - return the default
			return i;
		}

		/// <summary>Retrieve the floating point value of a preference.</summary>
		/// <param name="name"></param>
		/// <returns>A converted floating point (float) value. 0F if pref name not found.</returns>
		public float getFloat(string name)
		{
			string s;
			float f;
			s = getPref(name);
			if(s.Equals("")) return 0F;
			f = System.Single.Parse(s);
			return f;
		}

		/// <summary>
		/// Get boolean preference
		/// </summary>
		/// <param name="name">Preference name</param>
		/// <returns>true if preference is found and has the valur "True", otherwise false.</returns>
		public bool getBool(string name)
		{
			string s;
			s = getPref(name);
			if(s.Equals("")) return false;
			if(s.Equals("True")) return true;
			if(s.Equals("False")) return false;
			return false;
		}

		/// <summary>
		/// Retrieves an Enumeration written to prefs using .ToString()
		/// </summary>
		/// <param name="name">Unique preference name</param>
		/// <param name="en">The enumerator type e.g. typeof(DrawTree.joinType)</param>
		/// <param name="defaultValue">Value returned if name not found</param>
		/// <returns></returns>
		public Enum getEnum(string name,Type en,Enum defaultValue)
		{
			string s;
			Enum result;

			s = getPref(name);
			if(s=="") return defaultValue;
			result = (Enum) Enum.Parse(en,s,true);
			return result;
		}

		/// <summary>
		/// Sets a colour preference. The RGB value is written in hex.
		/// </summary>
		/// <param name="name">Preference name.</param>
		/// <param name="section"></param>
		/// <param name="c"></param>
		public void setColour(string name,string section,System.Drawing.Color c)
		{
			int rgb;
			string hex;
			rgb = c.ToArgb();
			hex = rgb.ToString("X");
			setPref(name,section,"#"+hex.Substring(2)); // chops off the leading FF (transparency)
		}

		/// <summary>
		/// Get a colour preference.
		/// </summary>
		/// <param name="name">Preference name.</param>
		/// <param name="defaultColour">Colour returned if preference name not found.</param>
		/// <returns>Preference colour</returns>
		public System.Drawing.Color getColour(string name,System.Drawing.Color defaultColour)
		{
			int rgb = 0;
			System.Drawing.Color c;
			if(! this.prefExists(name))
			{
				// setColour(name,section,defaultColour);
				return defaultColour;
			}
			try
			{
				rgb = (int) getInt(name,System.Drawing.Color.Black.ToArgb());
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show
					(String.Format("Error converting colour value for {0} from preference file. {1}",name,ex.Message));
			}
			
			c = System.Drawing.Color.FromArgb(rgb);
			c = System.Drawing.Color.FromArgb(255,c.R,c.G,c.B);
			return c;
		}

		/// <summary>
		/// Set a font as a preference
		/// </summary>
		/// <param name="name">Preference name.</param>
		/// <param name="section">Preference section.</param>
		/// <param name="theFont">The font to set.</param>
		public void setFont(string name,string section, System.Drawing.Font theFont)
		{
			string n,s;
			n = theFont.Name;
			s = theFont.Size.ToString();
			n = n+";"+s;
			this.setPref(name,section,n);
		}

		/// <summary>
		/// Get a Font preference
		/// </summary>
		/// <param name="name">Preference name.</param>
		/// <param name="defaultFont">Font returned if preference name not found.</param>
		/// <returns></returns>
		public System.Drawing.Font getFont(string name,System.Drawing.Font defaultFont)
		{
			string s;
			int i;
			float fv;
			string delimStr = ";";
			char [] delimiter = delimStr.ToCharArray();
			string [] split = null;


			System.Drawing.Font f;
			if(! this.prefExists(name))
			{
				// setFont(name,defaultFont);
				return defaultFont;
			}
			s=getPref(name);
			// stored as name;size
			i = s.IndexOf(";");
			System.Diagnostics.Debug.Assert(i>0,
			                                "Preference "+name+" is not in the correct format for a font ("+s+")");
			split = s.Split(delimiter);
			System.Diagnostics.Debug.Assert(split.GetLength(0)==2);
			fv = System.Single.Parse(split[1]);
			f = new System.Drawing.Font(split[0],fv);
			return f;
		}

		private void writePrefRec(StreamWriter sw,prefRec p)
		{
			sw.WriteLine("<pref>");
			sw.Write("<name>");
			sw.Write(p.name);
			sw.WriteLine("</name>");
			sw.Write("<value>");
			sw.Write(p.value);
			sw.WriteLine("</value>");
			sw.WriteLine("</pref>");
		}

		private int writePrefSection(StreamWriter sw,string section)
		{
			int f,count=0;
			prefRec p;
			
			for (f = 0; f < a.Count; f++)
			{
				p = (prefRec) a[f];
				if(p.section.ToLower().Equals(section.ToLower()) || section.Equals(""))
				{
					writePrefRec(sw,p);
					count++;
				}
			}
			return count;
		}
		/// <summary>Saves all or selected preferences.</summary>
		/// <param name="fileName">Preference File name.  Empty saves to prefs.xml in the application's directory.</param>
		/// <param name="section">Empty string saves all sections, otherwise general or graphical</param>
		public void save(string fileName,string section)
		{
			StreamWriter sw;
			int f;

			if (fileName.Equals(""))
				fileName = System.Environment.CurrentDirectory + "\\prefs.xml";
			this.fileName = fileName;

			Debug.Assert(section.Equals("") || section.Equals("general") || section.Equals("graphical"),
			             "Unknown section type "+section);

			try
			{
				sw = new StreamWriter(fileName);
				sw.WriteLine("<!-- XML preferences file -->");
				sw.WriteLine("<preferences>");

				if(section.Equals("general") || section.Equals(""))
				{
					sw.WriteLine("<general>");
					f = writePrefSection(sw,"general");
					sw.WriteLine("</general>");
				}

				if(section.Equals("graphical") || section.Equals(""))
				{
					sw.WriteLine("<graphical>");
					writePrefSection(sw,"graphical");
					sw.WriteLine("</graphical>");
				}

				// System.Diagnostics.Debug.Assert(f == a.Count,"Some prefs not written");
				sw.WriteLine("</preferences>");
				sw.Close();
			}
			catch (Exception e)
			{
				System.Windows.Forms.MessageBox.Show(String.Format("File error on preference save: {0}.",e.Message));
			}
		}

		private void loadPrefs(string section,XmlNode xn)
		{
			int f;
			XmlNode xc,xvalue,xname;
			prefRec p;
			
			for (f = 0; f<xn.ChildNodes.Count; f++)
			{
				xc = (XmlNode) xn.ChildNodes[f];
				xname = xc.ChildNodes[0];
				p = new prefRec();
				p.name = xname.InnerText;
				xvalue = xc.ChildNodes[1];
				p.value = xvalue.InnerText;
				p.section = section;
				a.Add(p);
			}
		}
		
		/// <summary>
		/// Load preference file
		/// </summary>
		/// <param name="filename">File name to load</param>
		/// <returns>True if successful</returns>
		public bool load(string filename)
		{
			XmlTextReader reader;

			XmlNode xn;

			if (filename.Equals(""))
				filename = System.Environment.CurrentDirectory + "\\prefs.xml";
			
			fileName = filename;

			if( ! File.Exists(filename) )
				return false;

			try
			{
				reader = new XmlTextReader(filename);

				XmlDocument doc = new XmlDocument();
				doc.Load(reader);		// load into DOM
				a = new ArrayList();  // initalise main storage array
				
				xn = (XmlNode) doc.DocumentElement;
				System.Diagnostics.Debug.Assert(xn.Name.Equals("preferences"),"<preferences> expected");
				string section = xn.ChildNodes[0].Name;
				if(section.Equals("general") || section.Equals("graphical"))
				{
					// read the subsections
					xn = xn.ChildNodes[0];  // read the first child node
					while(xn != null && !xn.Name.Equals("/preferences"))
					{
						if(xn.Name.Equals("general"))
						{
							this.loadPrefs("general",xn);
							xn = xn.NextSibling;
						}
						else if(xn.Name.Equals("graphical"))
						{
							this.loadPrefs("graphical",xn);
							xn = xn.NextSibling;
						}
					}
				}
				else  // no subsections
					this.loadPrefs("",xn);
			}
			catch (Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message);
				return false;
			}
			reader.Close();
			return true;
		}

	}
	
}
