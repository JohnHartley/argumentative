using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;  // for MessageBox
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Argumentative
{
	/// <summary>
	/// The drawing engine for the Graphical View and Argument Map graphics
	/// </summary>
	public class DrawTree
	{
		/// <summary>The margin around an Argument graphic.</summary>
		public static readonly float margin = 10.0F;
		private System.Drawing.Graphics graphics;
		private Font drawFont,drawFontSmall;
		private SolidBrush drawBrush;
		private float offsetX,offsetY;	// for scrolling
		private ArrayList heights;		// maximum row heights
		private ArrayList widths;		// maximum row widths

		/// <summary>Current zoom factor</summary>
		public float zoom = 1F;
		
		private float maxWidth,maxHeight;	// how wide and high is the drawing
		private float legendHeight;			// height of the R notation Legend if rendered
		private float widthWithoutLegend;	// width before legend width is calculated.
		private float heightWithoutLegend;	// height before legend width is calculated. Unused
		private int maxLevels;				// maximum depth of the argument
		/// <summary>Justification of the boxes in the argument</summary>
		public enum justificationType {
			/// <summary>Boxes aligned left</summary>
			jLeft,
			/// <summary>Boxes centred </summary>
			jCentre,
			/// <summary>Boxes aligned right</summary>
			jRight
		}
		private justificationType justification;
		/// <summary>How boxes are joined</summary>
		public enum joinType {
			/// <summary>Straight line</summary>
			direct,
			/// <summary>joins at right angles _|</summary>
			dogleg,
			/// <summary>Bezier curve joins</summary>
			curve
		}
		private joinType join;

		/// <summary>Arrow types on joining lines</summary>
		public enum arrowType {
			/// <summary>No arrows on joining lines</summary>
			none,
			/// <summary>Arrow at the start (originating element)</summary>
			start,
			/// <summary>Arrow pointing to destination element.</summary>
			end,
			/// <summary>Both ends have an arrow.</summary>
			both
		}
		private arrowType arrows;
		/// <summary>Visual, non colour dependent element type indicator</summary>
		public enum markerType {
			/// <summary>No element marker</summary>
			none,
			/// <summary>Element indicated by a line just before the connection</summary>
			lines,
			/// <summary>The letter starting the element used i.e. P for Premise, R for reason, O for Objection and, H for Helper</summary>
			letters
		}
		private markerType marker;
		private bool helpersAsCoPremises;
		private bool expanded = false;  // TODO display according to TreeView [+] & [-] expansions
		private int levelLimit;  // TODO limits the number of levels down rendered.
		
		/// <summary>Which way around is the argument drawn</summary>
		public enum treeOrientationType  {
			/// <summary>Main premise at the top and the rest of the argument cascading down</summary>
			top_down,
			/// <summary>The argument cascades into the main premise</summary>
			bottom_up,
			/// <summary>Main premise at the left and the rest of the argument to the right</summary>
			left_to_right,
			/// <summary>Main premise at the right and the rest of the argument to the left</summary>
			right_to_left };
		private treeOrientationType treeOrientation;

		/// <summary>Controls if boxes or R notation is used</summary>
		public enum notationType {
			/// <summary>Show elements in boxes</summary>
			boxes,
			/// <summary>Show R1,R2.. for reasons,O1,O2... for objections</summary>
			Rnotation};
		private notationType useRnotation = notationType.boxes;

		private bool dropShadow = true,boxShading = true;
		private bool showLegend = true;

		private Options.OptionsData opts = Options.OptionsData.Instance;
		private float defaultGapHeight = 60F;
		private float defaultGapWidth = 30F;
		
		
		/// <summary>
		/// New drawing object for the argument map
		/// </summary>
		/// <param name="offsetX">Offset from the left</param>
		/// <param name="offsetY">Offset from the top</param>
		/// <param name="zoom">Magnification</param>
		public DrawTree(float offsetX,float offsetY, float zoom)
		{
			// Create font and brush.
			drawFont		= new Font("Arial", 16);
			drawFontSmall	= new Font("Arial", 8);
			drawBrush		= new SolidBrush(Color.Black);

			justification = justificationType.jCentre;
			join = joinType.dogleg;
			arrows = arrowType.none;
			marker = markerType.none;
			helpersAsCoPremises = false;

			this.offsetX = offsetX; this.offsetY = offsetY;
			this.zoom = zoom;
			System.Diagnostics.Debug.Assert(zoom > 0F,"Zoom cannot be zero or less");
		}

		
		/// <summary>
		/// Draws lines between boxes
		/// </summary>
		/// <param name="a">First node (From)</param>
		/// <param name="b">Second Node (To)</param>
		/// <param name="whichKid">The number of the child node being connected to.</param>
		/// <param name="kidCount">Number of kids being connected to from the first node.</param>
		private void joinNodes(Node a,Node b,int whichKid,int kidCount)
		{
			PointF[] points = null;
			float dx,dy,x1,y1,x2,y2;
			Node temp;
			Options.ElementOptions eo;
			
			eo = Options.OptionsData.whichOption(b);  // the second node determines the line style
			if((treeOrientation == DrawTree.treeOrientationType.bottom_up) | (treeOrientation == DrawTree.treeOrientationType.top_down))
			{
				if(a.y > b.y)  // swap
				{ temp = b; b = a; a = temp; }
				
			}
			else
			{
				if(a.x > b.x)  // swap
				{ temp = b; b = a; a = temp;}
				
			}

			// Create pen.
			Pen pen = new Pen(Color.Black, 1);
			
			// calc offset
			if(treeOrientation == DrawTree.treeOrientationType.top_down)
				temp = a;
			else
				temp = b;
			
			// bool distributeX = false;

			float gap;
			float xdif=0;
			
			if(opts.Distributed)
			{
				gap = temp.width / (float) (kidCount + 1);
				
				if(treeOrientation == DrawTree.treeOrientationType.top_down)
					xdif = gap * (float) (whichKid+1) - temp.width/2;
				else
					xdif = -(gap * (float) (whichKid+1) - temp.width/2);
			}
			
			if((treeOrientation == DrawTree.treeOrientationType.bottom_up) | (treeOrientation == DrawTree.treeOrientationType.top_down))
			{
				dx = (b.x+b.width/2) - (a.x+a.width/2);	// x difference
				dy = (b.y - a.y-a.height)/2F;				// y difference
				x1 = a.x+a.width/2F + offsetX;					// right side of top box
				y1 = a.y+a.height+this.offsetY;				// bottom of the box
				x2 = b.x+(b.width/2.0F)+this.offsetX;		// middle of the bottom box
				y2 = b.y+this.offsetY;						// top of bottom box
				
				if(opts.Distributed)
				{
					if(treeOrientation == DrawTree.treeOrientationType.top_down)
					{ x1 += xdif; dx -= xdif;}
					else
					{
						x2 -= xdif;
						dx = x2 - x1;
					}
				}
			}
			else
			{
				x1 = a.x+a.width+offsetX;					// halfway in x-axis for the top box
				y1 = a.y+a.height/2+this.offsetY;			// bottom of the box
				x2 = b.x+this.offsetX;						// left of right box
				y2 = b.y+b.height/2F+this.offsetY;			// top of bottom box
				dx = (x2 - x1) / 2;	// x difference
				dy = y2 - y1;				// y difference
			}

			// Create array of points that define lines to draw.
			if((treeOrientation == DrawTree.treeOrientationType.bottom_up) | (treeOrientation == DrawTree.treeOrientationType.top_down))
			{
				if(join == joinType.dogleg || join == joinType.curve)
				{
					if(dx == 0F)  // If there is no horizontal movement, just draw one line
					{
						PointF[] p1 = { new PointF( x1, y1 ), new PointF( x2, y2 ) };
						points = p1;
					}
					else
					{
						PointF[] p1 = {	new PointF( x1,y1 ),
							new PointF( x1,  y1 + dy),
							new PointF( x1 + dx,  y1 + dy),
							new PointF( x2,y2)
						};
						points = p1;
					}
				}
				else if(join == joinType.direct)// joinType.direct - i.e. a straight line
				{
					PointF[] p1 = { new PointF( x1, y1 ), new PointF( x2, y2 ) };
					points = p1;
				}
				else if(join == joinType.curve)	{} // do nothing
				else System.Diagnostics.Debug.Assert(false,"Unknown join type");
			}
			else  // left to right
			{
				if(join == joinType.dogleg || join == joinType.curve)
				{
					if(dy == 0F)  // If there is no vertical movement, just draw one line
					{
						PointF[] p1 = { new PointF( x1, y1 ), new PointF( x2, y2 ) };
						points = p1;
					}
					else
					{
						PointF[] p1 = {	new PointF( x1,y1 ),
							new PointF( x1+dx,  y1),
							new PointF( x1 + dx,  y1 + dy),
							new PointF( x2,y2)
						};
						points = p1;
					}
				}
				else if(join == joinType.direct)// joinType.direct - i.e. a straight line
				{
					PointF[] p1 = { new PointF( x1, y1 ), new PointF( x2, y2 ) };
					points = p1;
				}
				else if(join == joinType.curve)
				{
					PointF[] p1 = {	new PointF( x1,y1 ),
						new PointF( x1,  y1 + dy),
						new PointF( x1 + dx,  y1 + dy),
						new PointF( x2,y2)
					};
					points = p1;
				}
				else System.Diagnostics.Debug.Assert(false,"Unknown join type");
			}

			pen.DashStyle = eo.lineStyle;

			// add arrow if required
			if(arrows != arrowType.none)
			{
				AdjustableArrowCap myArrow = new AdjustableArrowCap(4, 6, true);
				if(arrows == arrowType.start | arrows == arrowType.both)
					pen.CustomStartCap = myArrow;
				if(arrows == arrowType.end | arrows == arrowType.both)
					pen.CustomEndCap = myArrow;
			}

			//Draw lines to screen.
			
			if(join == joinType.curve)
				graphics.DrawBezier(pen, x1, y1, x1, y1 + dy, x1 + dx, y1 + dy,	x2,y2);
			else
				graphics.DrawLines(pen, points);
			if(marker != markerType.none)
				drawElementMarker(b,join,x2,y2,pen,new SolidBrush(Color.BlueViolet));
		}

		/// <summary>
		/// Draws the optional Reason or Objection tag in the appropriate spot
		/// </summary>
		/// <param name="n">Node being designated</param>
		/// <param name="join"></param>
		/// <param name="x">x-axis location</param>
		/// <param name="y">y-axis location</param>
		/// <param name="pen">pen to use</param>
		/// <param name="brush">Brush to use</param>
		private void drawElementMarker(Node n,joinType join,float x,float y,Pen pen,Brush brush)
		{
			// x,y is the base mid point of the indicator
			// if(this.showElement == showElementType.roundBox)
			{
				string text;
				text = n.ArgumentNodeTypeString();
				text = text.Substring(0,1);
				
				GraphicsPath path = new GraphicsPath();
				if(this.marker == markerType.lines)
				{
					// path.AddLine(x-5,y-5,x+5,y+5);
					if(this.treeOrientation == treeOrientationType.top_down)
					{
						if(n.nodeType == Node.ArgumentNodeType.reason)
							path.AddLine(x-10F,y-5,x,y-5);  // single bar left
						if(n.nodeType == Node.ArgumentNodeType.objection)
							path.AddLine(x,y-5,x+10F,y-5); // bar right
						if(n.nodeType == Node.ArgumentNodeType.helper)
							path.AddLine(x-10F,y-5,x+10F,y-5); // bar across
					}
					// TODO Add markers for other orientations
//					else if(this.treeOrientation == treeOrientationType.bottom_up)
//					{
//						if(n.nodeType == Node.ArgumentNodeType.reason)
//							path.AddLine(x-10F,y+5+n.height,x,y+5+n.height);  // single bar left
//						if(n.nodeType == Node.ArgumentNodeType.objection)
//							path.AddLine(x,y+5+n.height,x+10F,y+5+n.height); // bar right
//						if(n.nodeType == Node.ArgumentNodeType.helper)
//							path.AddLine(x-10F,y+5+n.height,x+10F,y+5+n.height); // bar across
//					}
				}
				
				
				if(this.marker == markerType.letters)
				{
					FontFamily family = new FontFamily("Arial");
					int fontStyle = (int)FontStyle.Regular;
					int emSize = 24;
					Point origin = new Point((int)x - emSize/2, (int) y - emSize - 5);
					StringFormat format = StringFormat.GenericDefault;
					if(this.treeOrientation == treeOrientationType.top_down)
					{
						path.AddString(text,family,fontStyle,emSize,origin,format);
					}
				}
				graphics.DrawPath(Pens.Black,path);
				// drawFancyRectangle(x,y,15,10,pen,true);
			}
		}

		// private int Rcount=0,Ocount=0,Hcount=0;
		private string getNodeText(Node n)  // Get notation or text
		{
			if(n==null)	// reset counters
			{
				System.Diagnostics.Debug.Assert(false,"No longer used for reset");
				return null;
			}

			if(useRnotation == notationType.Rnotation)
				return n.getRnotation();
			return n.EditorText;
		}

		private SizeF calcWidthHeight(Node n, Font f)
		{
			SizeF stringSize,layoutSize;
			Font sf;
			Options.ElementOptions eo;

			eo = Options.OptionsData.whichOption(n);
			stringSize = new SizeF(0,0);
			if(f==null)  // font not provided
				f = eo.font;
			// scale font
			sf = new Font(f.FontFamily,f.Size * zoom);
			try
			{
				//if(! force & n.height != 0F) // do we need to calc?
				// 	return new SizeF(n.width, n.height);

				string drawString = getNodeText(n);

				if(useRnotation == notationType.Rnotation)  // R notation uses minimum room
					layoutSize = new SizeF(0F,0F);
				else if(n.nodeType == Node.ArgumentNodeType.premise)
				{
					if(eo.boxWidth == 0)
						layoutSize = new SizeF(0F,0F);
					else
						layoutSize = new SizeF((float) eo.boxWidth, 10000.0F);
				}
				else
					layoutSize = new SizeF((float) eo.boxWidth, 10000.0F);  // too high on purpose

				// Measure string.
				stringSize = new SizeF();
				stringSize = graphics.MeasureString(drawString, sf, layoutSize);

				if(useRnotation == notationType.Rnotation) return stringSize;
				
				float height = 25.0F;  // minimum height
				if(stringSize.Height < height)
				{
					stringSize.Height = height;
				}

				if(stringSize.Width < (float) eo.boxWidth)
					stringSize.Width = (float) eo.boxWidth;

				n.width		= stringSize.Width;
				n.height	= stringSize.Height;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace);
			}
			return stringSize;
		}

		/// <summary>
		/// Recalculate the Node with standard screen resolution.
		/// </summary>
		/// <param name="n">Node to be recalculated.</param>
		public void recalc(Node n)
		{
			heights = new ArrayList();
			widths = new ArrayList();
			
			recalc(n,90F,90F,false); // Calc widths and heights only
			recalc(n,90F,90F,true); // calc x and y positions
		}
		private void recalc(Node n,float resX,float resY,bool setXY)
		{
			int f;
			float x,y;
			
			x = margin;
			y = margin;
			
			if(setXY) calcStartXY(n,ref x,ref y);
			
			float mw=0,mh=0;

			if(this.graphics == null)
			{
				Bitmap b = new Bitmap(10,10); // exists just for font calculations
				// b.SetResolution(resX,resY);
				graphics = Graphics.FromImage(b);
			}

			if(useRnotation == notationType.Rnotation)
				Node.calcRnotation(n);
			recalcNode(n,x,y,0,setXY);
			
			if(!setXY)
			{
				for(f=0;f<heights.Count;f++)
					if(f>0)
					mh += (float) heights[f]+this.defaultGapHeight;
				else
					mh = (float) heights[f];  // just the height of the premise
				maxHeight = mh;

				for(f=0;f<widths.Count;f++)
					if(f>0)
					mw += (float) widths[f]+this.defaultGapWidth;
				else
					mw = (float) widths[f];
				maxWidth = mw;
				if(treeOrientation == DrawTree.treeOrientationType.bottom_up)
				{
					// n.fullHeight += n.height + this.defaultGapHeight;
					maxWidth = n.fullWidth  + 2 * margin;
					maxHeight  = mh  + 2 * margin;
				}
				else if(treeOrientation == DrawTree.treeOrientationType.top_down)
				{
					maxWidth = n.fullWidth + 2 * margin;
					maxHeight  = mh + 2 * margin;
				}
				else if(treeOrientation == DrawTree.treeOrientationType.left_to_right)
				{
					maxWidth = mw + 2 * margin;
					maxHeight  = n.fullHeight + 2 * margin;
				}
				else if(treeOrientation == DrawTree.treeOrientationType.right_to_left)
				{
					maxWidth = mw + 2 * margin;
					maxHeight  = n.fullHeight + 2 * margin;
				}
				

				// add Legend if exists
				if(this.useRnotation == DrawTree.notationType.Rnotation && showLegend)
				{
					legendHeight = maxHeight;  // drawLegend() increments maxHeight
					drawLegend(n,false);  // calculate legend width and height
					legendHeight = maxHeight - legendHeight;
				}
				else legendHeight = 0F;
			}
		}

		private void recalcNode(Node n,float x,float y,int level,bool setXY)
		{
			// recalculate the with of all nodes and subtrees
			Node k; // for calculating kids
			float fullwidth=0,fullheight=0;
			SizeF s = new SizeF(0,0);
			int f;  // loop

			if(!setXY)
			{
				maxLevels = Math.Max(maxLevels,level);  // calculate the maximum depth of the argument.

				s = calcWidthHeight(n,null);  // null means calculate the font from the node type
				n.width			= s.Width;
				n.height		= s.Height;
				n.fullWidth		= s.Width;
				n.fullHeight	= s.Height;
			}
			if(this.helpersAsCoPremises)
			{
				
			}
			
			if(setXY)
				calcNodePosition(n,x,y);
			else
			{
				if(heights.Count == level) // level 0=Premise
					heights.Add(s.Height);  // add a new height
				else
					heights[level] = Math.Max(s.Height,(float) heights[level]);
				
				if(widths.Count == level)
					widths.Add(s.Width);
				else
					widths[level] = Math.Max(s.Width,(float) widths[level]);
				
				fullwidth = s.Width;
				fullheight = s.Height;
			}
			if(n.countKids()==0)
				return;

			if(n.countKids()!= 0)
			{
				if(setXY) moveToChildLevel(n,ref x,ref y,level);  // move to next row (or column)
				
				// calculate width including kids
				
				for(f=0;f<n.kids.Count;f++)
				{
					k = (Node) n.kids[f];
					recalcNode(k,x,y,level+1,setXY);
					if(setXY) calcNextChildPosition(k,ref x,ref y);
					if(f==0)
					{
						fullwidth = k.fullWidth;
						fullheight = k.fullHeight;
					}
					else
					{
						fullwidth = fullwidth + defaultGapWidth + k.fullWidth;
						fullheight = fullheight + defaultGapHeight + k.fullHeight;
					}
				}
				n.fullWidth = Math.Max(fullwidth,n.width);  // new full with for node / tree
				n.fullHeight = Math.Max(fullheight,n.height);
			}
		}

		private void calcStartXY(Node n,ref float x,ref float y)
		{
			// Inset from the upper left (0,0) origin.
			x = margin;
			y = margin;
			if(treeOrientation == DrawTree.treeOrientationType.bottom_up)
				y = maxHeight - margin;

			// The drawing origin may be changed by the width of the legend
			if(treeOrientation == DrawTree.treeOrientationType.right_to_left)
			{
				if(showLegend && this.useRnotation == DrawTree.notationType.Rnotation)
					x = widthWithoutLegend - n.width - margin;
				else
					x = maxWidth - n.width - margin;
			}
		}
		
		private void calcNodePosition(Node n,float x,float y)
		{
			n.x = x;
			n.y = y;
			if(treeOrientation == treeOrientationType.bottom_up)
				n.y -= (float)  n.height + legendHeight;
			
			if((treeOrientation == treeOrientationType.bottom_up) | (treeOrientation == treeOrientationType.top_down))
			{
				// Justification refers to the location of the box in its containing area not the text within it
				if(this.justification == justificationType.jCentre)
					n.x += n.fullWidth / 2F - n.width/2F;
				else if(this.justification==justificationType.jRight)
					n.x = n.x + n.fullWidth - n.width;
			}
			else if(treeOrientation == treeOrientationType.left_to_right)
			{
				n.y += n.fullHeight / 2F - n.height / 2F;
			}
			else if(treeOrientation == treeOrientationType.right_to_left)
			{
				n.y += n.fullHeight / 2F - n.height / 2F;
			}
		}
		
		private void moveToChildLevel(Node n,ref float x,ref float y,int lvl)
		{
			try {
				if(treeOrientation == treeOrientationType.top_down)
					y += (float) heights[lvl] + defaultGapHeight; // y offset to next row down + gap
				else if(treeOrientation == DrawTree.treeOrientationType.bottom_up)
					y -= (float) heights[lvl] + defaultGapHeight; // next row up
				else if(treeOrientation == DrawTree.treeOrientationType.left_to_right)
					x += (float) widths[lvl] + defaultGapWidth;  // column to the right
				else if(treeOrientation == DrawTree.treeOrientationType.right_to_left)
					x -= (float) widths[lvl+1] + defaultGapWidth;  // column to the left (+1)
			}
			catch (Exception ex)
			{
				MessageBox.Show("drawTree error: "+lvl.ToString()+"."+ex.StackTrace);
			}
		}
		
		private void calcNextChildPosition(Node nn,ref float x,ref float y)
		{
			// the nodes at the same level
			if(treeOrientation == DrawTree.treeOrientationType.top_down)
				x+= nn.fullWidth + defaultGapWidth;  // move to the right
			else if(treeOrientation == DrawTree.treeOrientationType.left_to_right)
				y+= nn.fullHeight + defaultGapHeight;  // move down
			else if(treeOrientation == DrawTree.treeOrientationType.bottom_up)
				x+= nn.fullWidth + defaultGapWidth;  // move to the right
			else if(treeOrientation == DrawTree.treeOrientationType.right_to_left)
				y += nn.fullHeight +this.defaultGapHeight;
		}
		
		private bool isTextFormatted(string s)
		{
			// Bold is two * e.g. *bold*  this is used in Rationale
			// <strong>bold</strong> is the internal representation
			int i;
			i = s.IndexOf('*');
			if(i != -1)
			{
				if(s.IndexOf('*',i+1) != -1) // is there a terminating asterix?
					return true;
			}
			i = s.IndexOf("\\n");
			if(i != -1)
				return true;
			
			return false;
		}
		
		private void drawFormattedText(string s,Font f,SolidBrush brush,RectangleF drawRect)
		{
			string s1,s2,s3;
			int i,j,len,r;
			CharacterRange[] ranges = new CharacterRange[5];
			StringFormat stringFormat1 = new StringFormat();
			r=0;
			len = s.Length;
			i = s.IndexOf('*');
			while(i != -1){
				j = s.IndexOf('*',i+1);
				CharacterRange range = new CharacterRange(i,j-i-1);
				ranges[++r] = range;
				s1 = s.Substring(0,i);  // text before bold
				s2 = s.Substring(i+1,j-i-1); // just the bold text
				s3 = s.Substring(j+1);  // text after the bold
				s = s1+s2+s3;
				i = s.IndexOf('*');
			}
			stringFormat1.SetMeasurableCharacterRanges(ranges);

			Region[] charRegion = graphics.MeasureCharacterRanges(s,
			                                                      f, drawRect, stringFormat1);

			graphics.DrawString(s, f, brush, drawRect);
			int k;
			for(k=0;k<charRegion.Length;k++)
				graphics.FillRegion(new SolidBrush(Color.FromArgb(50, Color.Fuchsia)),charRegion[k]);
		}
		
		private void drawNode(float x,float y,Node n,int lvl)
		{
			// x and y are the upper left coordinate of the containing rectangle x,y x+n.fullWidth, y+n.height
			Font font;
			int i;
			Node nn;
			SolidBrush drawFontBrush;
			Options.ElementOptions eo;

			if(n==null) return;

			eo = Options.OptionsData.whichOption(n);
			font = eo.font;
			font = new Font(font.FontFamily,font.Size * zoom);
			string drawString = getNodeText(n);

			
			// define the rectangle
			RectangleF drawRect = new RectangleF( n.x + offsetX, n.y+offsetY, n.width, n.height);

			if(useRnotation != notationType.Rnotation) // R notation does not have the rectangle
			{
				if(dropShadow |boxShading) // fancy box
					drawFancyRectangle( n.x + offsetX, n.y+offsetY, n.width, n.height,Options.OptionsData.whichOption(n).BoxPen(),false);
				else
					graphics.DrawRectangle(Options.OptionsData.whichOption(n).BoxPen(), n.x + offsetX, n.y + offsetY, n.width, n.height);
			}
			// Draw string to screen.
			drawFontBrush = new SolidBrush(eo.fontColour);
			
			if(this.isTextFormatted(drawString))
				drawFormattedText(drawString, font, drawFontBrush, drawRect);
			else
			{
				// graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.DrawString(drawString, font, drawFontBrush, drawRect);
			}
			
			// draw co-premises
			if(opts.HelpersAsCoPremises) // TODO Does argument have co-premises at all?
			{
				
			}
			
			// draw kids
			if(n.countKids()!= 0)
			{
				// move to the child level

				for(i=0;i < n.kids.Count;i++)
				{
					nn = (Node) n.kids[i];
					if(nn.IsVisible)
					{
						joinNodes(n,nn,i,n.countKids());
						drawNode(x,y,nn,lvl+1);
					}
				}
			}
		}

		private void drawFancyRectangle(float x1,float y1,float width,float height,Pen boxpen,bool surpressShadow)
		{
			// based on code by
			// found on https://secure.codeproject.com/cs/media/FuzzyDropShadows.asp

			const float fudge = 7F;
			float _ShadowDistance = -7f;

			x1 += fudge;
			
			graphics.SmoothingMode = SmoothingMode.AntiAlias; // clean lines - set the smoothingmode to Anti-Alias
			Rectangle _Rectangle = new Rectangle((int) x1,(int) y1,(int) width,(int) height);
			// float _Radius = (int)(_Rectangle.Height * .2);
			float _Radius = 5;  // set non proportional

			// create an x and y variable so that we can reduce the length of our code lines
			float X = _Rectangle.Left;
			float Y = _Rectangle.Top + fudge;// fudge factor

			// make sure that we have a valid radius, too small and we have a problem
			if(_Radius < 1)
				_Radius = 1;

			try
			{
				// Create a graphicspath object with the using operator so the framework
				// can clean up the resources for us
				using(GraphicsPath _Path = new GraphicsPath())
				{
					// build the rounded rectangle starting at the top line and going around
					// until the line meets itself again
					_Path.AddLine(X + _Radius, Y, X + _Rectangle.Width - (_Radius * 2), Y);
					_Path.AddArc(X + _Rectangle.Width - (_Radius * 2), Y, _Radius * 2, _Radius * 2, 270, 90);
					_Path.AddLine(X + _Rectangle.Width, Y + _Radius, X + _Rectangle.Width, Y + _Rectangle.Height - (_Radius * 2));
					_Path.AddArc(X + _Rectangle.Width - (_Radius * 2), Y + _Rectangle.Height - (_Radius * 2), _Radius * 2, _Radius * 2,0,90);
					_Path.AddLine(X + _Rectangle.Width - (_Radius * 2), Y + _Rectangle.Height, X + _Radius, Y + _Rectangle.Height);
					_Path.AddArc(X, Y + _Rectangle.Height - (_Radius * 2), _Radius * 2, _Radius * 2, 90, 90);
					_Path.AddLine(X, Y + _Rectangle.Height - (_Radius * 2), X, Y + _Radius);
					_Path.AddArc(X, Y, _Radius * 2, _Radius * 2, 180, 90);

					// this is where we create the shadow effect, so we will use a
					// pathgradientbursh
					using(PathGradientBrush _Brush = new PathGradientBrush(_Path))
					{
						// set the wrapmode so that the colours will layer themselves
						// from the outer edge in
						_Brush.WrapMode = WrapMode.Clamp;

						// Create a color blend to manage our colors and positions and
						// since we need 3 colors set the default length to 3
						ColorBlend _ColorBlend = new ColorBlend(3);

						// here is the important part of the shadow making process, remember
						// the clamp mode on the colorblend object layers the colors from
						// the outside to the center so we want our transparent color first
						// followed by the actual shadow color. Set the shadow color to a
						// slightly transparent DimGray, I find that it works best.
						_ColorBlend.Colors = new Color[]{Color.Transparent,
							Color.FromArgb(180, Color.DimGray),
							Color.FromArgb(180, Color.DimGray)};

						// our color blend will control the distance of each color layer
						// we want to set our transparent color to 0 indicating that the
						// transparent color should be the outer most color drawn, then
						// our Dimgray color at about 10% of the distance from the edge
						_ColorBlend.Positions = new float[]{0f, .1f, 1f};

						// assign the color blend to the pathgradientbrush
						_Brush.InterpolationColors = _ColorBlend;

						// fill the shadow with our pathgradientbrush
						if(this.dropShadow & ! surpressShadow)
							graphics.FillPath(_Brush, _Path);
					}

					// since the shadow was drawm first we need to move the actual path
					// up and back a little so that we can show the shadow underneath
					// the object. To accomplish this we will create a Matrix Object
					Matrix _Matrix = new Matrix();

					// tell the matrix to move the path up and back the designated distance
					_Matrix.Translate(_ShadowDistance, _ShadowDistance);

					// assign the matrix to the graphics path of the rounded rectangle
					_Path.Transform(_Matrix);

					// get or calculate gradient colours
					Color sc = boxpen.Color;
					// Color sc = Color.Tomato;  // end colour was Color.MistyRose

					// fill the graphics path first
					using(LinearGradientBrush _Brush = new LinearGradientBrush(
						new Rectangle((int) X,(int) (Y-fudge),(int) width,(int) (height+fudge)),
						sc, Color.WhiteSmoke, LinearGradientMode.Vertical))
					{
						graphics.FillPath(_Brush, _Path);
					}

					// Draw the Graphicspath last so that we have cleaner borders
					// using(Pen _Pen = new Pen(Color.DimGray, 1f))
					{
						graphics.DrawPath(boxpen, _Path);
					}
					
				}
				graphics.SmoothingMode = SmoothingMode.None;  // Does not affect the result adversly
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(GetType().Name + ".DrawRndRect() Error: " + ex.Message);
			}
		}
		
		private float legendY = 0;
		private void drawLegend(Node n,bool display)
		{
			int i;

			if(this.useRnotation != DrawTree.notationType.Rnotation & display)
				return;
			if(!showLegend) return;

			if(widthWithoutLegend == 0)
				widthWithoutLegend= maxWidth;
			
			if(heightWithoutLegend == 0)
				heightWithoutLegend= maxHeight;
			
			Font f = Options.OptionsData.whichOption(n).font;
			f = new Font(f.FontFamily,f.Size * zoom);
			string drawString = getNodeText(n)+": "+n.EditorText;
			// Measure string.
			SizeF stringSize = new SizeF();
			stringSize = graphics.MeasureString(drawString, f);
			if(display)
			{
				PointF p = new PointF(10F+offsetX, legendY + offsetY);
				graphics.DrawString(drawString, f, drawBrush,p);
			}
			legendY += stringSize.Height;
			maxHeight += stringSize.Height;
			// if the legend is now wider than the drawing width
			maxWidth = Math.Max(maxWidth,stringSize.Width);
			// draw kids
			if(n.kids != null)
				for(i=0;i < n.kids.Count;i++)
				drawLegend((Node) n.kids[i],display);
		}
		/// <summary>
		/// Draws the tree from the specified node
		/// </summary>
		/// <param name="g">The Drawing GDI object</param>
		/// <param name="n">Root Node</param>
		public void drawTree(Graphics g,Node n)
		{
			graphics = g;
			
			Options.OptionsData d = Options.OptionsData.Instance;
			// set drawing options
			justification	= d.Justification;
			join			= d.Join;
			treeOrientation	= d.TreeOrientation;
			useRnotation	= d.Notation;
			dropShadow		= d.DropShadow;
			boxShading		= d.BoxShading;
			showLegend		= d.ShowLegend;
			arrows			= d.Arrow;
			marker			= d.Marker;
			helpersAsCoPremises = d.HelpersAsCoPremises;
			// Width and height calculation should only be a cross check
			maxWidth=0;
			maxHeight=0;
			g.PageScale = zoom;
			
			// if(needsRecalc)
			recalc(n);

			// calcStartXY(n,ref x,ref y);
			
			// for debug
			// g.DrawRectangle(new Pen(Color.Bisque),0,0,maxWidth,maxHeight);
			// g.DrawString("maxHeight & maxWidth:"+maxWidth+" : "+maxHeight,new Font("Courier",8F),new SolidBrush(Color.Black),offsetX,offsetY+maxHeight+margin);

			drawNode(n.x,n.y,n,0);

			legendY = maxHeight - legendHeight;
			drawLegend(n,true);
		}

		/// <summary>
		/// Paints the graphical view on a Windows form
		/// </summary>
		/// <param name="e">Standard PaintEventArgs</param>
		/// <param name="a">Full context of the current argument</param>
		public void drawTree(System.Windows.Forms.PaintEventArgs e,ArgMapInterface a)
		{
			Options.OptionsData d = Options.OptionsData.Instance;

			if(! d.ShowGraphicalView)
			{
				e.Graphics.Clear(Color.White);
				return;
			}
			try
			{
				e.Graphics.PageScale = 1.0F;
				drawTree(e.Graphics,a.CurrentArg.findHead());
			}
			catch (Exception ex)
			{
				MessageBox.Show("drawTree error: "+ex.StackTrace);
			}
		}
		/// <summary>
		/// Save argument as a bitmap to a file
		/// </summary>
		/// <param name="fn">File name</param>
		/// <param name="n">Starting node</param>
		/// <param name="format">Format to save with</param>
		public void drawTree(string fn,Node n,ImageFormat format)
		{
			Bitmap b;

			b = drawTree(n);
			b.Save(fn,format);
		}
		
		/// <summary>
		/// save as bmp file
		/// </summary>
		/// <param name="n"></param>
		/// <returns>Bitmap of current Node and children</returns>
		public Bitmap drawTree(Node n)
		{
			Bitmap b;
			Graphics g;

			Options.OptionsData d = Options.OptionsData.Instance;
			// set drawing options
			justification	= d.Justification;
			join			= d.Join;
			treeOrientation	= d.TreeOrientation;
			useRnotation	= d.Notation;
			dropShadow		= d.DropShadow;
			boxShading		= d.BoxShading;
			showLegend		= d.ShowLegend;
			arrows			= d.Arrow;
			marker			= d.Marker;
			helpersAsCoPremises = d.HelpersAsCoPremises;
			// Width and height calculation should only be a cross check
			maxWidth=0;
			maxHeight=0;

			recalc(n);

			b = new Bitmap((int) maxWidth + 20 ,(int) maxHeight + 20); // 20 pixel border
			g = Graphics.FromImage(b);  // gets the graphics drawing object
			g.Clear(Color.White);  // clears the background
			drawTree(g,n);

			return b;
		}
		
		
		/// <summary>Return maximum height of the argument (last drawn)</summary>
		public float MaxHeight {
			get { return maxHeight; }
		}
		
		/// <summary>Return maximum width of the argument (last drawn)</summary>
		public float MaxWidth {
			get { return maxWidth; }
		}
	}
}
