using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using mshtml;
using my.domain.lib.core.Extensions;

namespace Finx.App.UserControls
{
    public partial class xHtmlEditor : UserControl
    {
      
        private mshtml.IHTMLDocument2 doc;
        private mshtml.IHTMLDocument2 docCopy;

        private bool edits = true;
        private ContextMenuStrip custCtxMenu;

        public event EventHandler BodyChanged;
        private bool _changed;

        ToolStripMenuItem checkedFont = null;
        ToolStripMenuItem checkedSize = null;

        public bool Changed
        {
            get { return _changed; }
            set
            {
                _changed = value;
                if (this.BodyChanged != null)
                    this.BodyChanged(this, new EventArgs());
            }
        }

        public xHtmlEditor()
        {
            InitializeComponent();

            _changed = false;
            doc = (mshtml.IHTMLDocument2)this.htmlRenderer.Document.DomDocument;
            doc.designMode = "On";
            while (doc.body == null)
            {
                Application.DoEvents();
            }

            //A separate document to create a copy of the htmRenderer
            WebBrowser wb2 = new WebBrowser();
            wb2.Url = new System.Uri("about:blank", System.UriKind.Absolute);
            docCopy = (mshtml.IHTMLDocument2)wb2.Document.DomDocument;
            docCopy.designMode = "On";
            while (docCopy.body == null)
            {
                Application.DoEvents();
            }


            custCtxMenu = new ContextMenuStrip();

            ToolStripMenuItem itemFont = custCtxMenu.Items.Add("&Font") as ToolStripMenuItem;
            itemFont.DropDownItems.Add(new ToolStripMenuItem() { Text = "Verdana", Font = new Font("Verdana", 9) });
            itemFont.DropDownItems.Add(new ToolStripMenuItem() { Text = "Tahoma", Font = new Font("Tahoma", 9) });
            itemFont.DropDownItems.Add(new ToolStripMenuItem() { Text = "Century Gothic", Font = new Font("Century Gothic", 9) });
            itemFont.DropDownItems.Add(new ToolStripMenuItem() { Text = "Calibri", Font = new Font("Calibri", 9) });
            itemFont.DropDownItems.Add(new ToolStripMenuItem() { Text = "Lucida Calligraphy", Font = new Font("Lucida Calligraphy", 9) });
            itemFont.DropDownItems.Add(new ToolStripMenuItem() { Text = "Segoe Script", Font = new Font("Segoe Script", 9) });
            itemFont.DropDownItemClicked += ItemFont_DropDownItemClicked;

            checkedFont = itemFont.DropDownItems[0] as ToolStripMenuItem;

            custCtxMenu.Items.Add(itemFont);

            ToolStripMenuItem itemSize = custCtxMenu.Items.Add("&Size") as ToolStripMenuItem;
            itemSize.DropDownItems.Add(new ToolStripMenuItem() { Text = "8 pt", Font = new Font(checkedFont.Text, 8), Tag=1 });
            itemSize.DropDownItems.Add(new ToolStripMenuItem() { Text = "9 pt", Font = new Font(checkedFont.Text, 9), Tag = 2 });
            itemSize.DropDownItems.Add(new ToolStripMenuItem() { Text = "10 pt", Font = new Font(checkedFont.Text, 10), Tag = 3 });
            itemSize.DropDownItems.Add(new ToolStripMenuItem() { Text = "11 pt", Font = new Font(checkedFont.Text, 11), Tag = 4 });
            itemSize.DropDownItems.Add(new ToolStripMenuItem() { Text = "12 pt", Font = new Font(checkedFont.Text, 12), Tag = 5 });
            itemSize.DropDownItems.Add(new ToolStripMenuItem() { Text = "13 pt", Font = new Font(checkedFont.Text, 13), Tag = 6 });
            itemSize.DropDownItems.Add(new ToolStripMenuItem() { Text = "14 pt", Font = new Font(checkedFont.Text, 14), Tag = 7 });
            itemSize.DropDownItems.Add(new ToolStripMenuItem() { Text = "15 pt", Font = new Font(checkedFont.Text, 15), Tag = 8 });
            itemSize.DropDownItems.Add(new ToolStripMenuItem() { Text = "16 pt", Font = new Font(checkedFont.Text, 16), Tag = 9 });
            itemSize.DropDownItems.Add(new ToolStripMenuItem() { Text = "17 pt", Font = new Font(checkedFont.Text, 17), Tag = 10 });
            itemSize.DropDownItems.Add(new ToolStripMenuItem() { Text = "18 pt", Font = new Font(checkedFont.Text, 18), Tag = 11 });
            itemSize.DropDownItemClicked += ItemSize_DropDownItemClicked;

            custCtxMenu.Items.Add("&Copy");
            custCtxMenu.Items.Add("&Cut");
            custCtxMenu.Items.Add("&Paste");
            custCtxMenu.Items.Add("&Insert");
            custCtxMenu.ItemClicked += new ToolStripItemClickedEventHandler(custCtxMenu_ItemClicked);

            this.htmlRenderer.Document.Body.KeyDown += new System.Windows.Forms.HtmlElementEventHandler(Body_KeyDown);
            this.htmlRenderer.Document.ContextMenuShowing += new HtmlElementEventHandler(Document_ContextMenuShowing);
        }

        public void SetPlaceholders(Type type)
        {
            ToolStripMenuItem itemPlaceholder = custCtxMenu.Items.Add("&Placeholder") as ToolStripMenuItem;

            foreach (var property in type.GetProperties().OrderBy(x=>x.Name))
            {
                itemPlaceholder.DropDownItems.Add(new ToolStripMenuItem() { Text = property.Name });
            }

            itemPlaceholder.DropDownItemClicked += ItemPlaceholder_DropDownItemClicked;

        }
        private void ItemFont_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            doc.execCommand("FontName", false, item.Text);

            if (checkedFont != null)
                checkedFont.Checked = false;

            ToolStripMenuItem menuitem = item as ToolStripMenuItem;
            if (menuitem != null)
            {
                menuitem.Checked = true;
                checkedFont = menuitem;
            }

        }

        private void ItemSize_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            doc.execCommand("FontSize", false, item.Tag);

            if (checkedSize != null)
                checkedSize.Checked = false;

            ToolStripMenuItem menuitem = item as ToolStripMenuItem;
            if (menuitem != null)
            {
                menuitem.Checked = true;
                checkedSize = menuitem;
            }

        }

        private void ItemPlaceholder_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                ToolStripItem item = e.ClickedItem;

                string pasteHtml = string.Format("<span class='placeholder' value='{0}'>#{0}#</span> ", item.Text);

                IHTMLTxtRange range = (IHTMLTxtRange)doc.selection.createRange();
                range.pasteHTML(pasteHtml);
                range.collapse(false);
                range.select();
            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        /// <summary>
        /// Returns the inner html generated
        /// </summary>
        /// <returns>String html</returns>
        public String getHTML()
        {
            try
            {
                _changed = false;
                return doc.body.innerHTML;
            }
            catch (Exception)
            {
            }
            return string.Empty;
        }

        /// <summary>
        /// Returns the inner html generated
        /// </summary>
        /// <returns>String html</returns>
        public String getHTML(object obj)
        {
            _changed = false;

            //Add the DOM to the copy of the htmlRenderer
            docCopy.body.innerHTML = doc.body.innerHTML;

            // get all placeholders in docCopy
            List<mshtml.IHTMLSpanElement> allSpan = docCopy.all.OfType<mshtml.IHTMLSpanElement>().ToList();
            foreach (HTMLSpanElement span in allSpan)
                if (span.className == "placeholder")
                {
                    try
                    {
                        string value = span.getAttribute("value");
                        if (!string.IsNullOrEmpty(value))
                            span.outerHTML = string.Format("<span>{0}</span>", obj.GetPropertyValue(value).ToString());
                    }
                    catch (Exception) { };

                }

            //Images


            return docCopy.body.innerHTML;
        }

        public string InnerHtml
        {
            get { return getHTML(); }
            set { setHTML(value); }
        }
        /// <summary>
        /// Sets the Inner HTML of the documents (used to load docs into the editor)
        /// </summary>
        /// <param name="html"></param>
        public void setHTML(String html)
        {
            try
            {
                _changed = false;
                doc.body.innerHTML = html;
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Returns the plain text without any formatting
        /// </summary>
        /// <returns>String plainText</returns>
        public String getPlainText()
        {
            _changed = false;
            return doc.body.innerText;
        }

        /// <summary>
        /// Returns the plain text without any formatting
        /// </summary>
        /// <returns>String plainText</returns>
        public String getPlainText<T>(T placeholders)
        {
            _changed = false;
            return doc.body.innerText.ReplacePlaceholders<T>(placeholders);

            
        }

        //Sets the editor to either allow or dissalow edit.
        public void allowEdit(bool edit)
        {
            edits = edit;
            if (edit)
                doc.designMode = "On";
            else
                doc.designMode = "Off";
        }

        private void htmlwysiwyg_Load(object sender, EventArgs e)
        {
            
        }

        private void Body_KeyDown(object sender, System.Windows.Forms.HtmlElementEventArgs e)
        {
            // Notify class that change has been made
            if (!_changed)
            {
                this.Changed = true;
            }
        }

        void Document_ContextMenuShowing(object sender, HtmlElementEventArgs e)
        {
            custCtxMenu.Show(Cursor.Position);
            e.ReturnValue = false;
        }

        void custCtxMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                ToolStripItem item = e.ClickedItem;
                switch (item.Text)
                {
                    case "&Paste":
                        tsPaste.PerformClick();
                        break;
                    case "&Copy":
                        tsCopy.PerformClick();
                        break;
                    case "&Cut":
                        tsCut.PerformClick();
                        break;
                    case "&Insert":

                        custCtxMenu.Hide();

                        OpenFileDialog openFileDialog1 = new OpenFileDialog();

                        openFileDialog1.InitialDirectory = @"C:\";
                        openFileDialog1.Title = "Browse Image Files";

                        openFileDialog1.CheckFileExists = true;
                        openFileDialog1.CheckPathExists = true;

                        openFileDialog1.DefaultExt = "png";
                        openFileDialog1.Filter = "Image files (*.png)|*.png;*.bmp";
                        openFileDialog1.FilterIndex = 2;
                        openFileDialog1.RestoreDirectory = true;

                        openFileDialog1.ReadOnlyChecked = true;
                        openFileDialog1.ShowReadOnly = true;

                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {

                            Image bmp = Image.FromFile(openFileDialog1.FileName);

                            string base64string = ToBase64String(bmp, ImageFormat.Png);
                            string pasteImgHtml = "<img style='width:100%' src='data:image/png;base64," + base64string + "' />";

                            //this.doc.write(pasteImgHtml);

                            IHTMLSelectionObject currentSelection = doc.selection;
                            if (currentSelection != null)
                            {
                                IHTMLTxtRange range = currentSelection.createRange() as IHTMLTxtRange;

                                if (range != null)
                                {
                                    range.pasteHTML(pasteImgHtml);
                                    range.collapse(false);
                                    range.select();
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }

              
            }
            catch (Exception x)
            {

            }
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("BackColor", false, "red");
        }

        /// <summary>
        ///     Show the back color button
        /// </summary>
        [Description("Show the back color button or not"),
        Category("Toolbar")]
        public bool ShowBackColorButton
        {
            get { return tsBackColor.Visible; }
            set { tsBackColor.Visible = value; UpdateToolbarSeperators(); }
        }

        private void UpdateToolbarSeperators()
        {
            if (newTS.Visible == true || printToolStripButton.Visible == true)
                tsSeparator1.Visible = true;
            else
                tsSeparator1.Visible = false;

            if (tsCut.Visible == true || tsCopy.Visible == true || tsPaste.Visible == true)
                toolStripSeparator1.Visible = true;
            else
                toolStripSeparator1.Visible = false;

            if (tsBold.Visible == true || tsUnderline.Visible == true || tsItalics.Visible == true)
                toolStripSeparator2.Visible = true;
            else
                toolStripSeparator2.Visible = false;
            if (tsCenter.Visible == true || tsJustify.Visible == true || tsLeft.Visible == true || tsRight.Visible == true)
                toolStripSeparator3.Visible = true;
            else
                toolStripSeparator3.Visible = false;
            if (tsIndent.Visible == true || tsOutdent.Visible == true)
                toolStripSeparator4.Visible = true;
            else
                toolStripSeparator4.Visible = false;
            if (tsBullets.Visible == true || tsNumeric.Visible == true)
                toolStripSeparator5.Visible = true;
            else
                toolStripSeparator5.Visible = false;

            if (tsBackColor.Visible == true || tsTextColor.Visible == true)
                toolStripSeparator6.Visible = true;
            else
                toolStripSeparator6.Visible = false;

            if (tsLink.Visible == true || tsRemoveLink.Visible == true)
                toolStripSeparator7.Visible = true;
            else
                toolStripSeparator7.Visible = false;

        }


        private void newTS_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.body.innerText = "";
        }
        /// <summary>
        ///     Show the new button
        /// </summary>
        [Description("Show the new button or not"),
        Category("Toolbar")]
        public bool ShowNewButton
        {
            get { return newTS.Visible; }
            set { newTS.Visible = value; UpdateToolbarSeperators(); }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("Print", true, null);
        }

        /// <summary>
        ///     Show the print button
        /// </summary>
        [Description("Show the new button or not"),
        Category("Toolbar")]
        public bool ShowPrintButton
        {
            get { return printToolStripButton.Visible; }
            set { printToolStripButton.Visible = value; UpdateToolbarSeperators(); }
        }


        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            if (edits)
                try
                {
                    doc.execCommand("Cut", false, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Couldn't Cut\n\r" + ex.Message, "Erro Executing Cut Command", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

        }

        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the cut button or not"),
        Category("Toolbar")]
        public bool ShowCutButton
        {
            get { return tsCut.Visible; }
            set { tsCut.Visible = value; UpdateToolbarSeperators(); }
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("Copy", false, null);
        }

        /// <summary>
        ///     Show the copy button
        /// </summary>
        [Description("Show the copy button or not"),
        Category("Toolbar")]
        public bool ShowCopyButton
        {
            get { return tsCopy.Visible; }
            set { tsCopy.Visible = value; UpdateToolbarSeperators(); }
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            if (edits)
            {
                if (Clipboard.ContainsImage())
                {
                    Image bmp = Clipboard.GetImage();

                    string base64string = ToBase64String(bmp, ImageFormat.Png);
                    string pasteImgHtml = "<img src='data:image/png;base64," + base64string + "' />";

                    //this.doc.write(pasteImgHtml);

                    IHTMLSelectionObject currentSelection = doc.selection;
                    if (currentSelection != null)
                    {
                        IHTMLTxtRange range = currentSelection.createRange() as IHTMLTxtRange;

                        if (range != null)
                        {
                            range.pasteHTML(pasteImgHtml);
                            range.collapse(false);
                            range.select();
                        }
                    }
                 
                }
                else
                {
                    doc.execCommand("Paste", false, null);
                }
            }
        }

        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the paste button or not"),
        Category("Toolbar")]
        public bool ShowPasteButton
        {
            get { return tsPaste.Visible; }
            set { tsPaste.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsBold_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("Bold", false, null);
        }

        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the bold button or not"),
        Category("Toolbar")]
        public bool ShowBoldButton
        {
            get { return tsBold.Visible; }
            set { tsBold.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsUnderline_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("Underline", false, null);
        }

        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the underline button or not"),
        Category("Toolbar")]
        public bool ShowUnderlineButton
        {
            get { return tsUnderline.Visible; }
            set { tsUnderline.Visible = value; UpdateToolbarSeperators(); }
        }



        private void tsItalics_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the italics button or not"),
        Category("Toolbar")]
        public bool ShowItalicButton
        {
            get { return tsItalics.Visible; }
            set { tsItalics.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsLeft_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the Align Left button or not"),
        Category("Toolbar")]
        public bool ShowAlignLeftButton
        {
            get { return tsLeft.Visible; }
            set { tsLeft.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsCenter_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the Align Center button or not"),
        Category("Toolbar")]
        public bool ShowAlignCenterButton
        {
            get { return tsCenter.Visible; }
            set { tsCenter.Visible = value; UpdateToolbarSeperators(); }
        }
        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the Justify button or not"),
        Category("Toolbar")]
        public bool ShowJustifyButton
        {
            get { return tsJustify.Visible; }
            set { tsJustify.Visible = value; UpdateToolbarSeperators(); }
        }


        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the Align Right button or not"),
        Category("Toolbar")]
        public bool ShowAlignRightButton
        {
            get { return tsRight.Visible; }
            set { tsRight.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsIndent_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the Indent button or not"),
        Category("Toolbar")]
        public bool ShowIndentButton
        {
            get { return tsIndent.Visible; }
            set { tsIndent.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsOutdent_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the Outdent button or not"),
        Category("Toolbar")]
        public bool ShowOutdentButton
        {
            get { return tsOutdent.Visible; }
            set { tsOutdent.Visible = value; UpdateToolbarSeperators(); }
        }



        private void tsBullets_Click(object sender, EventArgs e)
        {

        }

        [Description("Show the Bullet button or not"),
        Category("Toolbar")]
        public bool ShowBulletButton
        {
            get { return tsBullets.Visible; }
            set { tsBullets.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsNumeric_Click(object sender, EventArgs e)
        {

        }
        [Description("Show the OutdentOrdered List button or not"),
        Category("Toolbar")]
        public bool ShowOrderedListButton
        {
            get { return tsNumeric.Visible; }
            set { tsNumeric.Visible = value; UpdateToolbarSeperators(); }
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("BackColor", false, "blue");
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("BackColor", false, "black");
        }

        private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("BackColor", false, "yellow");
        }

        private void orangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("BackColor", false, "orange");
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("BackColor", false, "green");
        }

        private void brownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("BackColor", false, "brown");
        }

        private void redToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("ForeColor", false, "red");
        }

        private void blueToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("ForeColor", false, "blue");
        }

        private void blackToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("ForeColor", false, "black");
        }

        private void yellowToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("ForeColor", false, "yellow");
        }

        private void orangeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("ForeColor", false, "orange");
        }

        private void greenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("ForeColor", false, "green");
        }

        private void brownToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("ForeColor", false, "brown");
        }

        private void tsLink_Click(object sender, EventArgs e)
        {

        }

        [Description("Show the Link button"),
       Category("Toolbar")]
        public bool ShowLinkButton
        {
            get { return tsLink.Visible; }
            set { tsLink.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsRemoveLink_Click(object sender, EventArgs e)
        {

        }

        [Description("Show the Unlink button"),
       Category("Toolbar")]
        public bool ShowUnlinkButton
        {
            get { return tsRemoveLink.Visible; }
            set { tsRemoveLink.Visible = value; UpdateToolbarSeperators(); }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {

        }

        private void verdanaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ariaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void timesNewRomanToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void currierToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void comicSansToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bookAntiquaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        [Description("Show the Text Background color button"),
        Category("Toolbar")]
        public bool ShowTxtBGButton
        {
            get { return tsTextColor.Visible; }
            set { tsTextColor.Visible = value; UpdateToolbarSeperators(); }
        }

        [Description("Show the Text color button"),
       Category("Toolbar")]
        public bool ShowTxtColorButton
        {

            get { return tsTextColor.Visible; }
            set { tsTextColor.Visible = value; UpdateToolbarSeperators(); }
        }
        [Description("Show the Font Size button"),
       Category("Toolbar")]
        public bool ShowFontSizeButton
        {
            get { return tsFontSize.Visible; }
            set { tsFontSize.Visible = value; UpdateToolbarSeperators(); }
        }



        [Description("Show the Font Family button"),
       Category("Toolbar")]
        public bool ShowFontFamilyButton
        {
            get { return tsFontFamily.Visible; }
            set { tsFontFamily.Visible = value; UpdateToolbarSeperators(); }
        }
        /// <summary>
        /// Allows you to add custome fonts to the control
        /// </summary>
        /// <param name="fontName"> Name of the font to add</param>
        public void addFont(String fontName)
        {
            ToolStripMenuItem tsMi = new ToolStripMenuItem();
            tsMi.Font = new System.Drawing.Font(fontName, 9F);
            tsMi.Name = fontName + "ToolStripMenuItem";
            tsMi.Size = new System.Drawing.Size(167, 22);
            tsMi.Text = fontName;
            tsMi.Click += new System.EventHandler(addFont_click);
            tsFontFamily.DropDownItems.Add(tsMi);
        }

        private void addFont_click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("FontName", false, ((ToolStripMenuItem)sender).Text);
        }

        private void tsTextColor_Click(object sender, EventArgs e)
        {

        }

        private void tsBackColor_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Facilitates the conversion of a Image type to a String type
        /// </summary>
        /// <param name="bmp">Image object to convert</param>
        /// <param name="imageFormat">Image format type to convert the image to in base 64</param>
        public string ToBase64String(Image bmp, ImageFormat imageFormat)
        {
            string base64String = string.Empty;

            MemoryStream memoryStream = new MemoryStream();
            bmp.Save(memoryStream, imageFormat);

            memoryStream.Position = 0;
            byte[] byteBuffer = memoryStream.ToArray();

            memoryStream.Close();

            base64String = Convert.ToBase64String(byteBuffer);
            byteBuffer = null;
            memoryStream.Dispose();
            memoryStream = null;

            return base64String;
        }
    }
}
