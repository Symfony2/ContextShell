using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using ShellContext.Properties;

namespace ShellContext
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("98FF0E56-86BF-4403-B8BC-4FAF8643BAD3"), ComVisible(true)]
    public class PGPFileProcessor : IShellExtInit, IContextMenu
    {
        // The name of the selected file.//**//
        private string selectedFile;

        private string menuSignEncryptText = "&Подписать и закрыть информацию";
        private string menuVerifyDecryptText = "&Раскодировать и проверить подпись";

        private IntPtr lockBmp = IntPtr.Zero;
        private IntPtr unlockBmp = IntPtr.Zero;
        
        private uint IDM_DISPLAY = 0;

        #region Constructor
        public PGPFileProcessor()
        {
            Bitmap _bitmapLockBmp = Resources.Lock;
            Bitmap _bitmapUnlockBmp = Resources.Unlock;
            _bitmapLockBmp.MakeTransparent(_bitmapLockBmp.GetPixel(0,0));
            _bitmapUnlockBmp.MakeTransparent(_bitmapUnlockBmp.GetPixel(0,0));
            this.lockBmp = _bitmapLockBmp.GetHbitmap();
            this.unlockBmp = _bitmapUnlockBmp.GetHbitmap();
        }
        ~PGPFileProcessor()
        {
            if (this.lockBmp != IntPtr.Zero)
            {
                NativeMethods.DeleteObject(this.lockBmp);
                this.lockBmp = IntPtr.Zero;
            }
            if (this.unlockBmp != IntPtr.Zero)
            {
                NativeMethods.DeleteObject(this.unlockBmp);
                this.unlockBmp = IntPtr.Zero;
            }

        }
        #endregion

        #region Registration in REGEDIT

        [ComRegisterFunction()]
        public static void Register(Type t)
        {
            try
            {
                ShellExtReg.RegisterShellExtContextMenuHandler(t.GUID, ".docx",
                    "CSShellExtContextMenuHandler.FileContextMenuExt Class");
                ShellExtReg.RegisterShellExtContextMenuHandler(t.GUID, ".doc",
                    "CSShellExtContextMenuHandler.FileContextMenuExt Class");
                ShellExtReg.RegisterShellExtContextMenuHandler(t.GUID, ".xlsx",
                    "CSShellExtContextMenuHandler.FileContextMenuExt Class");
                ShellExtReg.RegisterShellExtContextMenuHandler(t.GUID, ".xls",
                    "CSShellExtContextMenuHandler.FileContextMenuExt Class");
                ShellExtReg.RegisterShellExtContextMenuHandler(t.GUID, ".txt",
                    "CSShellExtContextMenuHandler.FileContextMenuExt Class");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the error
                throw;  // Re-throw the exception
            }
        }

        [ComUnregisterFunction()]
        public static void Unregister(Type t)
        {
            try
            {
                ShellExtReg.UnregisterShellExtContextMenuHandler(t.GUID, ".docx");
                ShellExtReg.UnregisterShellExtContextMenuHandler(t.GUID, ".doc");
                ShellExtReg.UnregisterShellExtContextMenuHandler(t.GUID, ".xlsx");
                ShellExtReg.UnregisterShellExtContextMenuHandler(t.GUID, ".xls");
                ShellExtReg.UnregisterShellExtContextMenuHandler(t.GUID, ".txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the error
                throw;  // Re-throw the exception
            }
        }
        #endregion

        #region Initialize Context Menu Handler

        public void Initialize(IntPtr pidlFolder, IntPtr pDataObj, IntPtr hKeyProgID)
        {
            if (pDataObj == IntPtr.Zero)
            {
                throw new ArgumentException();
            }

            FORMATETC fe = new FORMATETC();
            fe.cfFormat = (short)CLIPFORMAT.CF_HDROP;
            fe.ptd = IntPtr.Zero;
            fe.dwAspect = DVASPECT.DVASPECT_CONTENT;
            fe.lindex = -1;
            fe.tymed = TYMED.TYMED_HGLOBAL;
            STGMEDIUM stm = new STGMEDIUM();

            // The pDataObj pointer contains the objects being acted upon. In this 
            // example, we get an HDROP handle for enumerating the selected files 
            // and folders.
            IDataObject dataObject = (IDataObject)Marshal.GetObjectForIUnknown(pDataObj);
            dataObject.GetData(ref fe, out stm);

            try
            {
                // Get an HDROP handle.
                IntPtr hDrop = stm.unionmember;
                if (hDrop == IntPtr.Zero)
                {
                    throw new ArgumentException();
                }

                // Determine how many files are involved in this operation.
                uint nFiles = NativeMethods.DragQueryFile(hDrop, UInt32.MaxValue, null, 0);

                // This code sample displays the custom context menu item when only 
                // one file is selected. 
                if (nFiles == 1)
                {
                    // Get the path of the file.
                    StringBuilder fileName = new StringBuilder(260);
                    if (0 == NativeMethods.DragQueryFile(hDrop, 0, fileName,
                        fileName.Capacity))
                    {
                        Marshal.ThrowExceptionForHR(WinError.E_FAIL);
                    }
                    this.selectedFile = fileName.ToString();
                }
                else
                {
                    Marshal.ThrowExceptionForHR(WinError.E_FAIL);
                }

                
            }
            finally
            {
                NativeMethods.ReleaseStgMedium(ref stm);
            }
        }
        #endregion

        public int QueryContextMenu(
            IntPtr hMenu,
            uint iMenu,
            uint idCmdFirst,
            uint idCmdLast,
            uint uFlags)
        {
            // If uFlags include CMF_DEFAULTONLY then we should not do anything.
            if (((uint)CMF.CMF_DEFAULTONLY & uFlags) != 0)
            {
                return WinError.MAKE_HRESULT(WinError.SEVERITY_SUCCESS, 0, 0);
            }

            // Add a separator.
            MENUITEMINFO frontSep = new MENUITEMINFO();
            frontSep.cbSize = (uint)Marshal.SizeOf(frontSep);
            frontSep.fMask = MIIM.MIIM_TYPE;
            frontSep.fType = MFT.MFT_SEPARATOR;
            if (!NativeMethods.InsertMenuItem(hMenu, iMenu + 1, true, ref frontSep))
            {
                return Marshal.GetHRForLastWin32Error();
            }



            // Use either InsertMenu or InsertMenuItem to add menu items.
            MENUITEMINFO mii = new MENUITEMINFO();
            mii.cbSize = (uint)Marshal.SizeOf(mii);
            mii.fMask = MIIM.MIIM_BITMAP | MIIM.MIIM_STRING | MIIM.MIIM_FTYPE | MIIM.MIIM_ID | MIIM.MIIM_STATE;
            mii.wID = idCmdFirst + IDM_DISPLAY;
            mii.fType = MFT.MFT_STRING;
            mii.dwTypeData = this.menuSignEncryptText;
            mii.fState = MFS.MFS_ENABLED;
            mii.hbmpItem = this.lockBmp;
            if (!NativeMethods.InsertMenuItem(hMenu, iMenu, true, ref mii))
            {
                return Marshal.GetHRForLastWin32Error();
            }

            MENUITEMINFO sep = new MENUITEMINFO();            
            mii.dwTypeData = this.menuVerifyDecryptText;
            mii.fState = MFS.MFS_ENABLED;
            mii.hbmpItem = this.unlockBmp;
            if (!NativeMethods.InsertMenuItem(hMenu, iMenu, true, ref mii))
            {
                return Marshal.GetHRForLastWin32Error();
            }
            

            if (!NativeMethods.InsertMenuItem(hMenu, iMenu + 1, true, ref sep))
            {
                return Marshal.GetHRForLastWin32Error();
            }

            // Return an HRESULT value with the severity set to SEVERITY_SUCCESS. 
            // Set the code value to the offset of the largest command identifier 
            // that was assigned, plus one (1).
            return WinError.MAKE_HRESULT(WinError.SEVERITY_SUCCESS, 0,
                IDM_DISPLAY + 1);
        }

        

        public void InvokeCommand(IntPtr pici)
        {
            throw new NotImplementedException();
        }

        public void GetCommandString(UIntPtr idCmd, uint uFlags, IntPtr pReserved, StringBuilder pszName, uint cchMax)
        {
            throw new NotImplementedException();
        }
    }
}
