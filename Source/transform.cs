using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Text;

namespace Argumentative
{
	/// <summary>
	/// Transforms an argument using XSLT
	/// </summary>
	public class Transform
	{
		private string outfilepath = "";
		
		/// <summary>
		/// Transform argument through XSL
		/// </summary>
		/// <param name="filename">Source AXL file name</param>
		/// <param name="stylesheet">XSLT Stylesheet file</param>
		/// <param name="outfilename">Output File Name</param>
		/// <param name="arg">Current argument</param>
		/// <returns></returns>
		public bool doTransform(string filename,string stylesheet,string outfilename,Argument arg)
		{
			StringBuilder sb;
			XPathDocument xpathdocument;
			
			// Get output path
			outfilepath = System.IO.Path.GetDirectoryName(outfilename);
			if(outfilepath.IndexOf('\\') >= 0)
				outfilepath += '\\';
			
			// Send text to outfilename, a text file
			StreamWriter sw = new StreamWriter(outfilename);
			// 
			//XslTransform xslt = new XslTransform();
			XslCompiledTransform xslt = new XslCompiledTransform();

			if(filename == null | filename.Equals(""))  // empty string means read from current XML
			{
				// load current argument into the string
				sb = arg.writeArgumentXMLstring(arg.findHead(),true);
				xpathdocument = new XPathDocument(new XmlTextReader(new StringReader(sb.ToString())));
			}
			else
			{
				xpathdocument = new XPathDocument(filename);
			}

			XmlTextReader xtr = new XmlTextReader(stylesheet);
			xslt.Load(xtr,null,null);
			
			XmlTextWriter writer = new XmlTextWriter(sw);
			writer.Formatting=Formatting.Indented;
			
			// Create an XsltArgumentList.
			XsltArgumentList xslArg = new XsltArgumentList();
			
			// Add an object to create image TODO Possible error - Current argument can only have a transformed image
			ImageXSLextension obj = new ImageXSLextension(arg,this.outfilepath);
			xslArg.AddExtensionObject("urn:image", obj);


			xslt.Transform(xpathdocument,xslArg,writer);
			// xslt.Transform(xpathdocument, writer);
			
			xtr.Close();
			sw.Close();
			return true;
		}
	}

	/// <summary>
	/// Converts all or part of an image to RTF.  Used as an XSL extension
	/// </summary>
	public class ImageXSLextension{

		private Argument arg;
		private string filePath;
		
		/// <summary>
		/// Constructor for ImageXSLextension
		/// </summary>
		/// <param name="arg">The current argument map</param>
		/// <param name="filePath">The path the XSL output is being rendered to</param>
		public ImageXSLextension(Argument arg,string filePath)
		{
			this.arg = arg;
			this.filePath = filePath;
		}
		
		/// <summary>
		/// Converts the argument element referred to a picture in RTF format. 
		/// The sub elements are also rendered in the image.
		/// </summary>
		/// <param name="reference">Element reference e.g "1.1"</param>
		/// <returns></returns>
		public string ImageToRTF(string reference)
		{
			Node n;
			n = arg.findByReference(arg.findHead(),reference);
			
			if(n == null)
				return "ImageToRTF ("+reference+") not found";
			DrawTree draw;
			System.Drawing.Bitmap b;
			draw = new DrawTree(0,0,1F); // no offset or zoom
			b = draw.drawTree(n);
			RTF rtf = new RTF();
			return rtf.ImageRTF(b);
		}
		
		/// <summary>
		/// Converts an element to a PNG file.
		/// </summary>
		/// <param name="reference"></param>
		/// <param name="fileName">File name for PNG file</param>
		/// <returns></returns>
		public string ImageToPNG(string reference,string fileName)
		{
			
			Node n;
			n = arg.findByReference(arg.findHead(),reference);
			
			if(n == null)
				return String.Format("ImageToPNG ({0}) not found",reference);
			DrawTree draw;
			System.Drawing.Bitmap b;
			draw = new DrawTree(0,0,1F); // no offset or zoom
			b = draw.drawTree(n);
			b.Save(filePath+fileName,System.Drawing.Imaging.ImageFormat.Png);
			return fileName;
		}
	}

}
