//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using System.Runtime.InteropServices;

//namespace Finx.UserControls
//{
//    [ClassInterface(ClassInterfaceType.AutoDispatch), DefaultBindingProperty("Rtf"),
//    Description("DescriptionRichTextBox"), ComVisible(true),
//    Docking(DockingBehavior.Ask),
//    Designer("System.Windows.Forms.Design.RichTextBoxDesigner,System.Design, Version = 4.0.0.0, Culture = neutral,PublicKeyToken = b03f5f7f11d50a3a")]
//        public partial class CustomRichTextBox : RichTextBox
//        {
//            private const int WM_HSCROLL = 0x114;
//            private const int WM_VSCROLL = 0x115;
//            private const int WM_MOUSEWHEEL = 0x20A;
//            private const int WM_PAINT = 0x00F;
//            private const int EM_GETSCROLLPOS = 0x4DD;
//            public int lineOffset = 0;

//            [DllImport("user32.dll")]
//            public static extern int SendMessage(
//                   IntPtr hWnd,
//                   int Msg,
//                   IntPtr wParam,
//                   ref Point lParam
//                   );

//            protected override void WndProc(ref Message m)
//            {
//                base.WndProc(ref m);

//                if (m.Msg == WM_PAINT)
//                {
//                    using (Graphics g = base.CreateGraphics())
//                    {
//                        Point p = new Point();

//                        //get the position of the scrollbar to calculate the offset
//                        SendMessage(this.Handle, EM_GETSCROLLPOS, IntPtr.Zero, ref p);

//                        //draw the pink line on the side
//                        g.DrawLine(new Pen(Brushes.LightPink, 1), 10, 0,10, this.Height);

//                        //determine how tall the text will be per line
//                        int h = TextRenderer.MeasureText("Testj", this.Font).Height;

//                        //calculate where the lines need to start
//                        lineOffset = h - (p.Y % h);

//                        //draw lines until there is no more box
//                        for (int x = lineOffset; x < Height; x += h)
//                        {
//                            g.DrawLine(new Pen(Brushes.LightSkyBlue, 1), 0, x, Width, x);
//                        }

//                        //force the panel under us to draw itself.
//                        Parent.Invalidate();
//                    }
//                }

//            }

//            public CustomRichTextBox()
//            {
//                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
//            }
//        [Bindable(true), RefreshProperties(RefreshProperties.All),SettingsBindable(true), DefaultValue(""), Category("Appearance")]
//        new public string Rtf
//        {
//            get
//            {
//                return base.Rtf;
//            }
//            set
//            {
//                base.Rtf = value;
//            }
//        }

//    }

//}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Finx.App.UserControls
{
    [ClassInterface(ClassInterfaceType.AutoDispatch), DefaultBindingProperty("Rtf"),
    Description("DescriptionRichTextBox"), ComVisible(true),
    Docking(DockingBehavior.Ask),
    Designer("System.Windows.Forms.Design.RichTextBoxDesigner,System.Design, Version = 4.0.0.0, Culture = neutral,PublicKeyToken = b03f5f7f11d50a3a")]

    public partial class ExtendedRichTextBox : RichTextBox
    {
        #region From the Platform SDK.
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public Int32 Left;
            public Int32 Top;
            public Int32 Right;
            public Int32 Bottom;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct CHARRANGE
        {
            public Int32 cpMin; //First character of range (0 for start of doc)
            public Int32 cpMax; //Last character of range (-1 for end of doc)
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct FORMATRANGE
        {
            public IntPtr hdc; //Actual DC to draw on
            public IntPtr hdcTarget;//Target DC for determining text formatting
            public RECT rc; //Region of the DC to draw to (in twips)
            public RECT rcPage; //Region of the whole DC (page size) (in twips)
            public CHARRANGE chrg; //Range of text to draw (see earlier declaration)
        }
        [StructLayout(LayoutKind.Sequential)]
        private class PARAFORMAT2
        {
            public int cbSize;
            public int dwMask;
            public short wNumbering;
            public short wReserved;
            public int dxStartIndent;
            public int dxRightIndent;
            public int dxOffset;
            public short wAlignment;
            public short cTabCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x20)]
            public int[] rgxTabs;

            public int dySpaceBefore; // Vertical spacing before para
            public int dySpaceAfter; // Vertical spacing after para
            public int dyLineSpacing; // Line spacing depending on Rule
            public short sStyle; // Style handle
            public byte bLineSpacingRule; // Rule for line spacing (see tom.doc)
            public byte bOutlineLevel; // Outline Level
            public short wShadingWeight; // Shading in hundredths of a per cent
            public short wShadingStyle; // Byte 0: style, nib 2: cfpat, 3: cbpat
            public short wNumberingStart; // Starting value for numbering
            public short wNumberingStyle; // Alignment, Roman/Arabic, (), ), ., etc.
            public short wNumberingTab; // Space bet 1st indent and 1st-line text
            public short wBorderSpace; // Border-text spaces (nbl/bdr in pts)
            public short wBorderWidth; // Pen widths (nbl/bdr in half twips)
            public short wBorders; // Border styles (nibble/border)

            public PARAFORMAT2()
            {
                this.cbSize = Marshal.SizeOf(typeof(PARAFORMAT2));
            }
        }

        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;
        private const int WM_MOUSEWHEEL = 0x20A;
        // private const int WM_PAINT = 0x00F;
        private const int EM_GETSCROLLPOS = 0x4DD;
        public int lineOffset = 0;

        [DllImport("user32.dll")]
        public static extern int SendMessage(
              IntPtr hWnd,
              int Msg,
              IntPtr wParam,
              ref Point lParam
              );


        private const int WM_USER = 0x0400;
        private const int EM_FORMATRANGE = WM_USER + 57;
        private const int WM_PAINT = 0xF;
        private const int WM_PRINT = 0x317;
        private const int PRF_CLIENT = 0x4; // Draw the window's client area
        private const int PRF_CHILDREN = 0x10; // Draw all visible child
        private const int PRF_OWNED = 0x20; // Draw all owned windows

        // PARAFORMAT mask values
        private const uint PFM_STARTINDENT = 0x00000001;
        private const uint PFM_RIGHTINDENT = 0x00000002;
        private const uint PFM_OFFSET = 0x00000004;
        private const uint PFM_ALIGNMENT = 0x00000008;
        private const uint PFM_TABSTOPS = 0x00000010;
        private const uint PFM_NUMBERING = 0x00000020;
        private const uint PFM_OFFSETINDENT = 0x80000000;

        // PARAFORMAT 2.0 masks and effects
        private const uint PFM_SPACEBEFORE = 0x00000040;
        private const uint PFM_SPACEAFTER = 0x00000080;
        private const int PFM_LINESPACING = 0x00000100;
        private const uint PFM_STYLE = 0x00000400;
        private const uint PFM_BORDER = 0x00000800; // (*)
        private const uint PFM_SHADING = 0x00001000; // (*)
        private const uint PFM_NUMBERINGSTYLE = 0x00002000; // RE 3.0
        private const uint PFM_NUMBERINGTAB = 0x00004000; // RE 3.0
        private const uint PFM_NUMBERINGSTART = 0x00008000; // RE 3.0

        private const uint PFM_RTLPARA = 0x00010000;
        private const uint PFM_KEEP = 0x00020000; // (*)
        private const uint PFM_KEEPNEXT = 0x00040000; // (*)
        private const uint PFM_PAGEBREAKBEFORE = 0x00080000; // (*)
        private const uint PFM_NOLINENUMBER = 0x00100000; // (*)
        private const uint PFM_NOWIDOWCONTROL = 0x00200000; // (*)
        private const uint PFM_DONOTHYPHEN = 0x00400000; // (*)
        private const uint PFM_SIDEBYSIDE = 0x00800000; // (*)
        private const uint PFM_TABLE = 0x40000000; // RE 3.0
        private const uint PFM_TEXTWRAPPINGBREAK = 0x20000000; // RE 3.0
        private const uint PFM_TABLEROWDELIMITER = 0x10000000; // RE 4.0

        // The following three properties are read only
        private const uint PFM_COLLAPSED = 0x01000000; // RE 3.0
        private const uint PFM_OUTLINELEVEL = 0x02000000; // RE 3.0
        private const uint PFM_BOX = 0x04000000; // RE 3.0
        private const uint PFM_RESERVED2 = 0x08000000; // RE 4.0

        private const int EM_SETEVENTMASK = 1073;
        private const int EM_GETPARAFORMAT = 1085;
        private const int EM_SETPARAFORMAT = 1095;
        private const int EM_SETTYPOGRAPHYOPTIONS = 1226;
        private const int WM_SETREDRAW = 11;
        private const int TO_ADVANCEDTYPOGRAPHY = 1;
        private const int SCF_SELECTION = 1;

        [DllImport("USER32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, Int32 msg, Int32 wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In, Out, MarshalAs(UnmanagedType.LPStruct)] PARAFORMAT2 lParam);
        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern int SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

        public enum RichTextBulletType
        {
            None = 0,
            Normal = 1,
            Number = 2,
            LowerCaseLetter = 3,
            UpperCaseLetter = 4,
            LowerCaseRoman = 5,
            UpperCaseRoman = 6
        }

        public enum RichTextBulletStyle
        {
            RightParenthesis = 0x000,
            DoubleParenthesis = 0x100,
            Period = 0x200,
            Plain = 0x300,
            NoNumber = 0x400
        }

        public enum TextAlign
        {
            Left = 1,
            Right = 2,
            Center = 3,
            Justify = 4,
        }
        #endregion


        [Bindable(true), RefreshProperties(RefreshProperties.All), SettingsBindable(true), DefaultValue(""), Category("Appearance")]
        new public string Rtf
        {
            get
            {
                return base.Rtf;
            }
            set
            {
                base.Rtf = value;
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // CREATE A BACKGROUND WITH LINES AND A LEFT MARGIN
            //if (m.Msg == WM_PAINT)
            //{
            //    using (Graphics g = base.CreateGraphics())
            //    {
            //        Point p = new Point();

            //        //get the position of the scrollbar to calculate the offset
            //        SendMessage(this.Handle, EM_GETSCROLLPOS, IntPtr.Zero, ref p);

            //        //draw the pink line on the side
            //        g.DrawLine(new Pen(Brushes.LightPink, 1), 10, 0, 10, this.Height);

            //        //determine how tall the text will be per line
            //        int h = TextRenderer.MeasureText("Testj", this.Font).Height;

            //        //calculate where the lines need to start
            //        lineOffset = h - (p.Y % h);

            //        //draw lines until there is no more box
            //        for (int x = lineOffset; x < Height; x += h)
            //        {
            //            g.DrawLine(new Pen(Brushes.LightSkyBlue, 1), 0, x, Width, x);
            //        }

            //        //force the panel under us to draw itself.
            //        Parent.Invalidate();
            //    }
            //}

        }
        //private Helpers _helpers;

        // constructor
        public ExtendedRichTextBox()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            // _helpers = new Helpers();
        }

        protected override void OnSelectionChanged(EventArgs e)
        {
            // we add a new properties SelectionNumber to detect if the current line are numbered or not (it work like SelectionBullet)
            PARAFORMAT2 paraformat1 = new PARAFORMAT2();
            SendMessage(new HandleRef(this, this.Handle), EM_GETPARAFORMAT, 0, paraformat1);
            _selectionNumber = ((RichTextBulletType)paraformat1.wNumbering == RichTextBulletType.Number) ? true : false;
            base.OnSelectionChanged(e);
        }

        #region Printing

        //public Int32 Print(int charFrom, int charTo, PrintPageEventArgs e, bool measureOnly)
        //{
        //    //Calculate the area to render and print
        //    RECT rectToPrint;
        //    rectToPrint.Top = _helpers.hundredthInchToTwips(e.MarginBounds.Top);
        //    rectToPrint.Bottom = _helpers.hundredthInchToTwips(e.MarginBounds.Bottom);
        //    rectToPrint.Left = _helpers.hundredthInchToTwips(e.MarginBounds.Left);
        //    rectToPrint.Right = _helpers.hundredthInchToTwips(e.MarginBounds.Right);

        //    //Calculate the size of the page
        //    RECT rectPage;
        //    rectPage.Top = _helpers.hundredthInchToTwips(e.PageBounds.Top);
        //    rectPage.Bottom = _helpers.hundredthInchToTwips(e.PageBounds.Bottom);
        //    rectPage.Left = _helpers.hundredthInchToTwips(e.PageBounds.Left);
        //    rectPage.Right = _helpers.hundredthInchToTwips(e.PageBounds.Right);

        //    IntPtr hdc = e.Graphics.GetHdc();

        //    FORMATRANGE fr;
        //    fr.chrg.cpMax = charTo; //Indicate character from to character to 
        //    fr.chrg.cpMin = charFrom;
        //    fr.hdc = hdc; //Use the same DC for measuring and rendering
        //    fr.hdcTarget = hdc; //Point at printer hDC
        //    fr.rc = rectToPrint; //Indicate the area on page to print
        //    fr.rcPage = rectPage; //Indicate size of page

        //    // Non-Zero wParam means render, Zero means measure
        //    Int32 wparam = (measureOnly ? 0 : 1);

        //    //Get the pointer to the FORMATRANGE structure in memory
        //    IntPtr lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fr));
        //    Marshal.StructureToPtr(fr, lparam, false);

        //    //Send the rendered data for printing 
        //    IntPtr res = IntPtr.Zero;
        //    res = SendMessage(Handle, EM_FORMATRANGE, wparam, lparam);

        //    //Free the block of memory allocated
        //    Marshal.FreeCoTaskMem(lparam);

        //    //Release the device context handle obtained by a previous call
        //    e.Graphics.ReleaseHdc(hdc);

        //    //Return last + 1 character printer
        //    return res.ToInt32();
        //}
        //public void PrintDone()
        //{
        //    IntPtr lParam = new IntPtr(0);
        //    SendMessage(Handle, EM_FORMATRANGE, 0, lParam);
        //}
        #endregion

        #region BULLETING

        private RichTextBulletType _BulletType = RichTextBulletType.Normal;
        private RichTextBulletStyle _BulletStyle = RichTextBulletStyle.Period;
        private short _BulletNumberStart = 1;
        private bool _selectionNumber = false;

        public bool SelectionNumber
        {
            get { return this._selectionNumber; }
            set { this._selectionNumber = value; }
        }
        public RichTextBulletType BulletType
        {
            get { return _BulletType; }
            set
            {
                _BulletType = value;
                NumberedBullet(true);
            }
        }
        public RichTextBulletStyle BulletStyle
        {
            get { return _BulletStyle; }
            set
            {
                _BulletStyle = value;
                NumberedBullet(true);
            }
        }
        public void NumberedBullet(bool TurnOn)
        {
            PARAFORMAT2 paraformat1 = new PARAFORMAT2();
            paraformat1.dwMask = (int)(PFM_NUMBERING | PFM_OFFSET | PFM_NUMBERINGSTART | PFM_NUMBERINGSTYLE | PFM_NUMBERINGTAB);
            if (!TurnOn)
            {
                paraformat1.wNumbering = 0;
                paraformat1.dxOffset = 0;
            }
            else
            {
                paraformat1.wNumbering = (short)_BulletType;
                paraformat1.dxOffset = this.BulletIndent;
                paraformat1.wNumberingStyle = (short)_BulletStyle;
                paraformat1.wNumberingStart = _BulletNumberStart;
                paraformat1.wNumberingTab = 500;
            }
            SendMessage(new HandleRef(this, this.Handle), EM_SETPARAFORMAT, 0, paraformat1);
        }
        #endregion

        #region Alignment
        public new TextAlign SelectionAlignment
        {
            get
            {
                PARAFORMAT2 fmt = new PARAFORMAT2();
                //fmt.cbSize = Marshal.SizeOf(fmt);

                // Get the alignment.
                //SendMessage(new HandleRef(this, Handle), EM_GETPARAFORMAT, SCF_SELECTION, ref fmt);
                SendMessage(new HandleRef(this, Handle), EM_GETPARAFORMAT, SCF_SELECTION, fmt);

                // Default to Left align.
                if ((fmt.dwMask & PFM_ALIGNMENT) == 0) return TextAlign.Left;

                return (TextAlign)fmt.wAlignment;
            }

            set
            {
                PARAFORMAT2 fmt = new PARAFORMAT2();
                //fmt.cbSize = Marshal.SizeOf(fmt);
                fmt.dwMask = (int)PFM_ALIGNMENT;
                fmt.wAlignment = (short)value;

                // Set the alignment.
                //SendMessage(new HandleRef(this, Handle), EM_SETPARAFORMAT, SCF_SELECTION, ref fmt);
                SendMessage(new HandleRef(this, Handle), EM_SETPARAFORMAT, SCF_SELECTION, fmt);
            }
        }
        #endregion

        #region Line Spacing
        public int LineSpacing
        {// up RTF V2.0
            get
            {
                PARAFORMAT2 fmt = new PARAFORMAT2();

                // Get
                SendMessage(new HandleRef(this, Handle), EM_GETPARAFORMAT, SCF_SELECTION, fmt);

                return fmt.dyLineSpacing;
            }
            set
            {
                PARAFORMAT2 fmt = new PARAFORMAT2();

                fmt.dwMask = PFM_LINESPACING;
                fmt.dyLineSpacing = value;

                // Set
                SendMessage(new HandleRef(this, Handle), EM_SETPARAFORMAT, SCF_SELECTION, fmt);
            }
        }
        #endregion
    }
}