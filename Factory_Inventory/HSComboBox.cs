using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HatchStyleComboBox
{
	/// <summary>
	/// Summary description for HSComboBox.
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.ComboBox))]
	public class HSComboBox : ComboBox
	{
		public HSComboBox() : base()
		{
			//
			// TODO: Add constructor logic here
			//
			this.DrawMode = DrawMode.OwnerDrawVariable;
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.InitializeDropDown();
		}

		~HSComboBox()
		{
			base.Dispose();
			this.Dispose(true);
		}

		protected void InitializeDropDown()
		{
			foreach (string styleName in Enum.GetNames(typeof(HatchStyle)))
			{
				this.Items.Add(styleName);
			}
		}

		protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
		{
			// The following method should generally be called before drawing.
			// It is actually superfluous here, since the subsequent drawing
			// will completely cover the area of interest.
			e.DrawBackground();
			//The system provides the context
			//into which the owner custom-draws the required graphics.
			//The context into which to draw is e.graphics.
			//The index of the item to be painted is e.Index.
			//The painting should be done into the area described by e.Bounds.

			if (e.Index > 0)
			{
				Graphics g = e.Graphics;
				Rectangle r = e.Bounds;

				Rectangle rd = r;
				rd.Width = rd.Left + 25;
				Rectangle rt = r;
				r.X = rd.Right;
				string displayText = this.Items[e.Index].ToString();

				HatchStyle hs = (HatchStyle)Enum.Parse(typeof(HatchStyle), displayText, true);
				// TODO add user selected foreground and background colors here
				HatchBrush b = new HatchBrush(hs, Color.Black, e.BackColor);
				g.DrawRectangle(new Pen(Color.Black, 2), rd.X + 3, rd.Y + 2, rd.Width - 4, rd.Height - 4);
				g.FillRectangle(b, new Rectangle(rd.X + 3, rd.Y + 2, rd.Width - 4, rd.Height - 4));

				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Near;

				//If the current item has focus.
				if ((e.State & DrawItemState.Focus) == 0)
				{
					e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), r);
					e.Graphics.DrawString(displayText, this.Font, new SolidBrush(SystemColors.WindowText), r, sf);
				}
				else
				{
					e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), r);
					e.Graphics.DrawString(displayText, this.Font, new SolidBrush(SystemColors.HighlightText), r, sf);
				}
			}

			//Draws a focus rectangle on the specified graphics surface and within the specified bounds.
			e.DrawFocusRectangle();
		}

		protected override void OnMeasureItem(System.Windows.Forms.MeasureItemEventArgs e)
		{
			//Work out what the text will be
			string displayText = this.Items[e.Index].ToString();

			//Get width & height of string
			SizeF stringSize = e.Graphics.MeasureString(displayText, this.Font);

			//Account for top margin
			stringSize.Height += 5;

			// set hight to text height
			e.ItemHeight = (int)stringSize.Height;

			// set width to text width
			e.ItemWidth = (int)stringSize.Width;
		}
	}
}
