using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace Finx.App.UserControls
{
    public partial class xFileView : UserControl
    {
        public event EventHandler ItemActivated;

        public xFileView()
        {
            InitializeComponent();

            this.listView_Files.View = View.Details;
            this.listView_Files.ItemActivate += ListView_Files_ItemActivate;
        }

        private void ListView_Files_ItemActivate(object sender, EventArgs e)
        {
            if (ItemActivated != null)
                ItemActivated(sender, e);
        }

        //http://stackoverflow.com/questions/37791149/c-sharp-show-file-and-folder-icons-in-listview
        public void ShowFiles(DirectoryInfo dirInfo,string filter="*.*")
        {
            this.listView_Files.Items.Clear();

            // Obtain a handle to the system image list.
            NativeMethods.SHFILEINFO shfi = new NativeMethods.SHFILEINFO();
            IntPtr hSysImgList = NativeMethods.SHGetFileInfo("",
                                                             0,
                                                             ref shfi,
                                                             (uint)Marshal.SizeOf(shfi),
                                                             NativeMethods.SHGFI_SYSICONINDEX
                                                              | NativeMethods.SHGFI_SMALLICON);
           // Debug.Assert(hSysImgList != IntPtr.Zero);  // cross our fingers and hope to succeed!

            // Set the ListView control to use that image list.
            IntPtr hOldImgList = NativeMethods.SendMessage(this.listView_Files.Handle,
                                                           NativeMethods.LVM_SETIMAGELIST,
                                                           NativeMethods.LVSIL_SMALL,
                                                           hSysImgList);

            // If the ListView control already had an image list, delete the old one.
            if (hOldImgList != IntPtr.Zero)
            {
                NativeMethods.ImageList_Destroy(hOldImgList);
            }

            // Set up the ListView control's basic properties.
            // Put it in "Details" mode, create a column so that "Details" mode will work,
            // and set its theme so it will look like the one used by Explorer.

            this.listView_Files.Columns.Clear();
            this.listView_Files.Font= new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular, GraphicsUnit.Pixel);
            this.listView_Files.Columns.Add("Name", 400, HorizontalAlignment.Left);
            this.listView_Files.Columns.Add("Ext", 75, HorizontalAlignment.Left);
            this.listView_Files.Columns.Add("Size", 100, HorizontalAlignment.Right);
            this.listView_Files.Columns.Add("Modified", 200, HorizontalAlignment.Left);

            NativeMethods.SetWindowTheme(this.listView_Files.Handle, "Explorer", null);

            string _filter = filter == "" ? "*.*" : filter;
            if (!_filter.StartsWith("*"))
                _filter = "*" + _filter;

            if (!_filter.EndsWith("*"))
                _filter = _filter + "*";

            string[] s = Directory.GetFileSystemEntries(dirInfo.FullName, _filter);
            foreach (string file in s)
            {
                IntPtr himl = NativeMethods.SHGetFileInfo(file,
                                                0,
                                                ref shfi,
                                                (uint)Marshal.SizeOf(shfi),
                                                NativeMethods.SHGFI_DISPLAYNAME
                                                  | NativeMethods.SHGFI_SYSICONINDEX
                                                  | NativeMethods.SHGFI_SMALLICON);
               // Debug.Assert(himl == hSysImgList); // should be the same imagelist as the one we set
                ListViewItem lvi = listView_Files.Items.Add(shfi.szDisplayName, shfi.iIcon);
                lvi.Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular, GraphicsUnit.Pixel);
                FileInfo fi = new FileInfo(file);
                lvi.SubItems.Add(fi.Extension);
                try
                {
                    var MB = fi.Length / 1024;
                    lvi.SubItems.Add(MB.ToString("N0") + " MB");
                }
                catch (Exception) { lvi.SubItems.Add(""); };

                lvi.SubItems.Add(fi.LastWriteTime.ToString());
                lvi.Tag = fi;

            }

        }

        public View View { set { this.listView_Files.View = value; } }
    }

    internal static class NativeMethods
    {
        public const uint LVM_FIRST = 0x1000;
        public const uint LVM_GETIMAGELIST = (LVM_FIRST + 2);
        public const uint LVM_SETIMAGELIST = (LVM_FIRST + 3);

        public const uint LVSIL_NORMAL = 0;
        public const uint LVSIL_SMALL = 1;
        public const uint LVSIL_STATE = 2;
        public const uint LVSIL_GROUPHEADER = 3;

        [DllImport("user32")]
        public static extern IntPtr SendMessage(IntPtr hWnd,
                                                uint msg,
                                                uint wParam,
                                                IntPtr lParam);

        [DllImport("comctl32")]
        public static extern bool ImageList_Destroy(IntPtr hImageList);

        public const uint SHGFI_DISPLAYNAME = 0x200;
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0;
        public const uint SHGFI_SMALLICON = 0x1;
        public const uint SHGFI_SYSICONINDEX = 0x4000;

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260 /* MAX_PATH */)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        [DllImport("shell32")]
        public static extern IntPtr SHGetFileInfo(string pszPath,
                                                  uint dwFileAttributes,
                                                  ref SHFILEINFO psfi,
                                                  uint cbSizeFileInfo,
                                                  uint uFlags);

        [DllImport("uxtheme", CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd,
                                                string pszSubAppName,
                                                string pszSubIdList);
    }
}
