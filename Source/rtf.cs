/*
 * Created by SharpDevelop.
 * User: John
 * Date: 8/01/2008
 * Time: 9:11 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Argumentative
{
	/// <summary>
	/// A toolbox for the creation of Rich Text Format files.
	/// Code based on Khendys Gordon's article in CodeProject.com, "Insert Plain Text and Images into RichTextBox at Runtime"
	/// found at http://www.codeproject.com/KB/edit/csexrichtextbox.aspx
	/// </summary>
	public class RTF
	{
		private System.Drawing.Font Font;
		
		/// <summary>
		/// Rich Text Format (RTF) object. Default font is Arial 12
		/// </summary>
		public RTF()
		{
			this.Font = new Font("Arial",12);  // inherited from richTextBox in original code
		}
		
	/// <summary>
	/// Enum for possible RTF colors. See http://en.wikipedia.org/wiki/Web_colors#HTML_color_names
	/// </summary>
	public enum RtfColor {
		/// <summary>Black</summary>
		Black, 
		/// <summary>Dark red #800000</summary>
		Maroon, 
		/// <summary>Green #008000</summary>
		Green, 
		/// <summary>Dark green #808000</summary>
		Olive, 
		/// <summary>Dark blue #000080</summary>
		Navy, 
		/// <summary>Purple #800080</summary>
		Purple, 
		/// <summary>Blue-green #008080</summary>
		Teal, 
		/// <summary>Grey #808080</summary>
		Gray, 
		/// <summary>Silver #C0C0C0</summary>
		Silver,
		/// <summary>Red #FF0000</summary>
		Red, 
		/// <summary>Lime #00FF00</summary>
		Lime, 
		/// <summary>Yellow #FFFF00</summary>
		Yellow, 
		/// <summary>Blue #0000FF</summary>
		Blue, 
		/// <summary>Fuchia, Light purple #FF00FF</summary>
		Fuchsia, 
		/// <summary>Aqua #00FFFF</summary>
		Aqua, 
		/// <summary>White #FFFFFF</summary>
		White
	}
		
		/* RTF HEADER
		 * ----------
		 * 
		 * \rtf[N]		- For text to be considered to be RTF, it must be enclosed in this tag.
		 *				  rtf1 is used because the RichTextBox conforms to RTF Specification
		 *				  version 1.
		 * \ansi		- The character set.
		 * \ansicpg[N]	- Specifies that unicode characters might be embedded. ansicpg1252
		 *				  is the default used by Windows.
		 * \deff[N]		- The default font. \deff0 means the default font is the first font
		 *				  found.
		 * \deflang[N]	- The default language. \deflang1033 specifies US English.
		 * */
		private const string RTF_HEADER = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033";

		/* RTF DOCUMENT AREA
		 * -----------------
		 * 
		 * \viewkind[N]	- The type of view or zoom level.  \viewkind4 specifies normal view.
		 * \uc[N]		- The number of bytes corresponding to a Unicode character.
		 * \pard		- Resets to default paragraph properties
		 * \cf[N]		- Foreground color.  \cf1 refers to the color at index 1 in
		 *				  the color table
		 * \f[N]		- Font number. \f0 refers to the font at index 0 in the font
		 *				  table.
		 * \fs[N]		- Font size in half-points.
		 * */
		private const string RTF_DOCUMENT_PRE = @"\viewkind4\uc1\pard\cf1\f0\fs20";
		private const string RTF_DOCUMENT_POST = @"\cf0\fs17}";
		private string RTF_IMAGE_POST = @"}";		
		
		#region Constants

		// Not used in this application.  Descriptions can be found with documentation
		// of Windows GDI function SetMapMode
		private const int MM_TEXT = 1;
		private const int MM_LOMETRIC = 2;
		private const int MM_HIMETRIC = 3;
		private const int MM_LOENGLISH = 4;
		private const int MM_HIENGLISH = 5;
		private const int MM_TWIPS = 6;

		// Ensures that the metafile maintains a 1:1 aspect ratio
		private const int MM_ISOTROPIC = 7;

		// Allows the x-coordinates and y-coordinates of the metafile to be adjusted
		// independently
		private const int MM_ANISOTROPIC = 8;

		// Represents an unknown font family
		private const string FF_UNKNOWN = "UNKNOWN";

		// The number of hundredths of millimetres (0.01 mm) in an inch
		// For more information, see GetImagePrefix() method.
		private const int HMM_PER_INCH = 2540;

		// The number of twips in an inch
		// For more information, see GetImagePrefix() method.
		private const int TWIPS_PER_INCH = 1440;

		#endregion
		
		#region My Privates

		/// <summary>The default text color</summary>
		private RtfColor textColor;

		/// <summary>The default text background color</summary>
		private RtfColor highlightColor;

		/// <summary>Dictionary that maps color enums to RTF color codes</summary>
		private HybridDictionary rtfColor;

		/// <summary>Dictionary that mapas Framework font families to RTF font families</summary>
		private HybridDictionary rtfFontFamily;

		// The horizontal resolution at which the control is being displayed
		private float xDpi;

		// The vertical resolution at which the control is being displayed
		private float yDpi;

		#endregion
		
		// Specifies the flags/options for the unmanaged call to the GDI+ method
		// Metafile.EmfToWmfBits().
		private enum EmfToWmfBitsFlags {

			// Use the default conversion
			EmfToWmfBitsFlagsDefault = 0x00000000,

			// Embedded the source of the EMF metafiel within the resulting WMF
			// metafile
			EmfToWmfBitsFlagsEmbedEmf = 0x00000001,

			// Place a 22-byte header in the resulting WMF file.  The header is
			// required for the metafile to be considered placeable.
			EmfToWmfBitsFlagsIncludePlaceable = 0x00000002,

			// Don't simulate clipping by using the XOR operator.
			EmfToWmfBitsFlagsNoXORClip = 0x00000004
		};
		
		#region Insert Image
		
		/// <summary>
		/// Inserts an image into the RichTextBox.  The image is wrapped in a Windows
		/// Format Metafile, because although Microsoft discourages the use of a WMF,
		/// the RichTextBox (and even MS Word), wraps an image in a WMF before inserting
		/// the image into a document.  The WMF is attached in HEX format (a string of
		/// HEX numbers).
		/// 
		/// The RTF Specification v1.6 says that you should be able to insert bitmaps,
		/// .jpegs, .gifs, .pngs, and Enhanced Metafiles (.emf) directly into an RTF
		/// document without the WMF wrapper. This works fine with MS Word,
		/// however, when you don't wrap images in a WMF, WordPad and
		/// RichTextBoxes simply ignore them.  Both use the riched20.dll or msfted.dll.
		/// </summary>
		/// <remarks>
		/// NOTE: The image is inserted wherever the caret is at the time of the call,
		/// and if any text is selected, that text is replaced.
		/// </remarks>
		/// <param name="_image"></param>
		public string InsertImage(Image _image) {  // JH originally void

			StringBuilder _rtf = new StringBuilder();

			// Append the RTF header
			_rtf.Append(RTF_HEADER);

			// Create the font table using the RichTextBox's current font and append
			// it to the RTF string
			_rtf.Append(GetFontTable(this.Font));

			// Create the image control string and append it to the RTF string
			_rtf.Append(GetImagePrefix(_image));

			// Create the Windows Metafile and append its bytes in HEX format
			_rtf.Append(GetRtfImage(_image));

			// Close the RTF image control string
			_rtf.Append(RTF_IMAGE_POST);

			// this.SelectedRtf = _rtf.ToString();  // jh the original routine inserted the RTF into a richTextBox
			return _rtf.ToString();
		}
		
		/// <summary>
		/// converts an image to RTF
		/// </summary>
		/// <remarks> Does not have the header and footer found InsertImage</remarks>
		/// <param name="_image"></param>
		/// <returns></returns>
		public string ImageRTF(Image _image) {
			
			StringBuilder _rtf = new StringBuilder();

			// Create the image control string and append it to the RTF string
			_rtf.Append(GetImagePrefix(_image));

			// Create the Windows Metafile and append its bytes in HEX format
			_rtf.Append(GetRtfImage(_image));

			// Close the RTF image control string
			_rtf.Append(RTF_IMAGE_POST);

			// this.SelectedRtf = _rtf.ToString();  // jh the original routine inserted the RTF into a richTextBox
			return _rtf.ToString();
		}

		/// <summary>
		/// Creates the RTF control string that describes the image being inserted.
		/// This description (in this case) specifies that the image is an
		/// MM_ANISOTROPIC metafile, meaning that both X and Y axes can be scaled
		/// independently.  The control string also gives the images current dimensions,
		/// and its target dimensions, so if you want to control the size of the
		/// image being inserted, this would be the place to do it. The prefix should
		/// have the form ...
		/// 
		/// {\pict\wmetafile8\picw[A]\pich[B]\picwgoal[C]\pichgoal[D]
		/// 
		/// where ...
		/// 
		/// A	= current width of the metafile in hundredths of millimetres (0.01mm)
		///		= Image Width in Inches * Number of (0.01mm) per inch
		///		= (Image Width in Pixels / Graphics Context's Horizontal Resolution) * 2540
		///		= (Image Width in Pixels / Graphics.DpiX) * 2540
		/// 
		/// B	= current height of the metafile in hundredths of millimetres (0.01mm)
		///		= Image Height in Inches * Number of (0.01mm) per inch
		///		= (Image Height in Pixels / Graphics Context's Vertical Resolution) * 2540
		///		= (Image Height in Pixels / Graphics.DpiX) * 2540
		/// 
		/// C	= target width of the metafile in twips
		///		= Image Width in Inches * Number of twips per inch
		///		= (Image Width in Pixels / Graphics Context's Horizontal Resolution) * 1440
		///		= (Image Width in Pixels / Graphics.DpiX) * 1440
		/// 
		/// D	= target height of the metafile in twips
		///		= Image Height in Inches * Number of twips per inch
		///		= (Image Height in Pixels / Graphics Context's Horizontal Resolution) * 1440
		///		= (Image Height in Pixels / Graphics.DpiX) * 1440
		///
		/// </summary>
		/// <remarks>
		/// The Graphics Context's resolution is simply the current resolution at which
		/// windows is being displayed.  Normally it's 96 dpi, but instead of assuming
		/// I just added the code.
		/// 
		/// According to Ken Howe at pbdr.com, "Twips are screen-independent units
		/// used to ensure that the placement and proportion of screen elements in
		/// your screen application are the same on all display systems."
		/// 
		/// Units Used
		/// ----------
		/// 1 Twip = 1/20 Point
		/// 1 Point = 1/72 Inch
		/// 1 Twip = 1/1440 Inch
		/// 
		/// 1 Inch = 2.54 cm
		/// 1 Inch = 25.4 mm
		/// 1 Inch = 2540 (0.01)mm
		/// </remarks>
		/// <param name="_image"></param>
		/// <returns></returns>
		private string GetImagePrefix(Image _image) {

			StringBuilder _rtf = new StringBuilder();

			xDpi = _image.HorizontalResolution;  // jh
			yDpi = _image.VerticalResolution;
			// Calculate the current width of the image in (0.01)mm
			int picw = (int)Math.Round((_image.Width / xDpi) * HMM_PER_INCH);

			// Calculate the current height of the image in (0.01)mm
			int pich = (int)Math.Round((_image.Height / yDpi) * HMM_PER_INCH);

			// Calculate the target width of the image in twips
			int picwgoal = (int)Math.Round((_image.Width / xDpi) * TWIPS_PER_INCH);

			// Calculate the target height of the image in twips
			int pichgoal = (int)Math.Round((_image.Height / yDpi) * TWIPS_PER_INCH);

			// Append values to RTF string
			_rtf.Append(@"{\pict\wmetafile8");
			_rtf.Append(@"\picw");
			_rtf.Append(picw);
			_rtf.Append(@"\pich");
			_rtf.Append(pich);
			_rtf.Append(@"\picwgoal");
			_rtf.Append(picwgoal);
			_rtf.Append(@"\pichgoal");
			_rtf.Append(pichgoal);
			_rtf.Append(" ");

			return _rtf.ToString();
		}

		/// <summary>
		/// Use the EmfToWmfBits function in the GDI+ specification to convert a
		/// Enhanced Metafile to a Windows Metafile
		/// </summary>
		/// <param name="_hEmf">
		/// A handle to the Enhanced Metafile to be converted
		/// </param>
		/// <param name="_bufferSize">
		/// The size of the buffer used to store the Windows Metafile bits returned
		/// </param>
		/// <param name="_buffer">
		/// An array of bytes used to hold the Windows Metafile bits returned
		/// </param>
		/// <param name="_mappingMode">
		/// The mapping mode of the image.  This control uses MM_ANISOTROPIC.
		/// </param>
		/// <param name="_flags">
		/// Flags used to specify the format of the Windows Metafile returned
		/// </param>
		[DllImportAttribute("gdiplus.dll")]
		private static extern uint GdipEmfToWmfBits (IntPtr _hEmf, uint _bufferSize,
		                                             byte[] _buffer, int _mappingMode, EmfToWmfBitsFlags _flags);


		/// <summary>
		/// Wraps the image in an Enhanced Metafile by drawing the image onto the
		/// graphics context, then converts the Enhanced Metafile to a Windows
		/// Metafile, and finally appends the bits of the Windows Metafile in HEX
		/// to a string and returns the string.
		/// </summary>
		/// <param name="_image"></param>
		/// <returns>
		/// A string containing the bits of a Windows Metafile in HEX
		/// </returns>
		private string GetRtfImage(Image _image) {

			StringBuilder _rtf = null;

			// Used to store the enhanced metafile
			MemoryStream _stream = null;

			// Used to create the metafile and draw the image
			Graphics _graphics = null;

			// The enhanced metafile
			Metafile _metaFile = null;

			// Handle to the device context used to create the metafile
			IntPtr _hdc;

			_graphics = Graphics.FromImage(_image);
			
			try {
				_rtf = new StringBuilder();
				_stream = new MemoryStream();

				// Get a graphics context from the RichTextBox
				using(_graphics = Graphics.FromImage(_image)) { // was created from the inherited richTextBox

					// Get the device context from the graphics context
					_hdc = _graphics.GetHdc();

					// Create a new Enhanced Metafile from the device context
					_metaFile = new Metafile(_stream, _hdc);

					// Release the device context
					_graphics.ReleaseHdc(_hdc);
				}

				// Get a graphics context from the Enhanced Metafile
				using(_graphics = Graphics.FromImage(_metaFile)) {

					// Draw the image on the Enhanced Metafile
					_graphics.DrawImage(_image, new Rectangle(0, 0, _image.Width, _image.Height));

				}

				// Get the handle of the Enhanced Metafile
				IntPtr _hEmf = _metaFile.GetHenhmetafile();

				// A call to EmfToWmfBits with a null buffer return the size of the
				// buffer need to store the WMF bits.  Use this to get the buffer
				// size.
				uint _bufferSize = GdipEmfToWmfBits(_hEmf, 0, null, MM_ANISOTROPIC,
				                                    EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);

				// Create an array to hold the bits
				byte[] _buffer = new byte[_bufferSize];

				// A call to EmfToWmfBits with a valid buffer copies the bits into the
				// buffer an returns the number of bits in the WMF.
				uint _convertedSize = GdipEmfToWmfBits(_hEmf, _bufferSize, _buffer, MM_ANISOTROPIC,
				                                       EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);

				// Append the bits to the RTF string
				for(int i = 0; i < _buffer.Length; ++i) {
					_rtf.Append(String.Format("{0:X2}", _buffer[i]));
				}

				return _rtf.ToString();
			}
			finally {
				if(_graphics != null)
					_graphics.Dispose();
				if(_metaFile != null)
					_metaFile.Dispose();
				if(_stream != null)
					_stream.Close();
			}
		}
		
		#endregion
		
		#region RTF Helpers

		/// <summary>
		/// Creates a font table from a font object.  When an Insert or Append 
		/// operation is performed a font is either specified or the default font
		/// is used.  In any case, on any Insert or Append, only one font is used,
		/// thus the font table will always contain a single font.  The font table
		/// should have the form ...
		/// 
		/// {\fonttbl{\f0\[FAMILY]\fcharset0 [FONT_NAME];}
		/// </summary>
		/// <param name="_font"></param>
		/// <returns></returns>
		private string GetFontTable(Font _font) {

			StringBuilder _fontTable = new StringBuilder();

			// Append table control string
			_fontTable.Append(@"{\fonttbl{\f0");
			_fontTable.Append(@"\");
			
			// If the font's family corresponds to an RTF family, append the
			// RTF family name, else, append the RTF for unknown font family.
			if (rtfFontFamily.Contains(_font.FontFamily.Name))
				_fontTable.Append(rtfFontFamily[_font.FontFamily.Name]);
			else
				_fontTable.Append(rtfFontFamily[FF_UNKNOWN]);

			// \fcharset specifies the character set of a font in the font table.
			// 0 is for ANSI.
			_fontTable.Append(@"\fcharset0 ");

			// Append the name of the font
			_fontTable.Append(_font.Name);

			// Close control string
			_fontTable.Append(@";}}");

			return _fontTable.ToString();
		}

		/// <summary>
		/// Creates a font table from the RtfColor structure.  When an Insert or Append
		/// operation is performed, _textColor and _backColor are either specified
		/// or the default is used.  In any case, on any Insert or Append, only three
		/// colours are used.  The default color of the RichTextBox (signified by a
		/// semicolon (;) without a definition), is always the first colour (index 0) in
		/// the colour table.  The second colour is always the text colour, and the third
		/// is always the highlight colour (colour behind the text).  The color table
		/// should have the form ...
		/// 
		/// {\colortbl ;[TEXT_COLOR];[HIGHLIGHT_COLOR];}
		/// 
		/// </summary>
		/// <param name="_textColor"></param>
		/// <param name="_backColor"></param>
		/// <returns></returns>
		private string GetColorTable(RtfColor _textColor, RtfColor _backColor) {

			StringBuilder _colorTable = new StringBuilder();

			// Append color table control string and default font (;)
			_colorTable.Append(@"{\colortbl ;");

			// Append the text color
			_colorTable.Append(rtfColor[_textColor]);
			_colorTable.Append(@";");

			// Append the highlight color
			_colorTable.Append(rtfColor[_backColor]);
			_colorTable.Append(@";}\n");
					
			return _colorTable.ToString();
		}
		#endregion
	}
}
