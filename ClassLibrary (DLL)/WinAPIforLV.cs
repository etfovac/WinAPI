using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace WinAPIforLV
{
    public class WinAPIforLV
    {
        #region Window
        /// <summary>
        /// Selected Win API Function Calls
        /// </summary>


        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int which);
        
        [DllImport("user32.dll")]
        private static extern void
            SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter,
                         int X, int Y, int width, int height, uint flags);

        private const int SM_CXSCREEN = 0;
        // The width of the screen of the primary display monitor, in pixels. This is the same value obtained by calling GetDeviceCaps as follows: GetDeviceCaps( hdcPrimaryMonitor, HORZRES).
        private const int SM_CYSCREEN = 1;
        // The height of the screen of the primary display monitor, in pixels. This is the same value obtained by calling GetDeviceCaps as follows: GetDeviceCaps( hdcPrimaryMonitor, VERTRES).
        private const int SM_CXFULLSCREEN = 16;
            // The width of the client area for a full-screen window on the primary display monitor, in pixels. 
            // To get the coordinates of the portion of the screen that is not obscured by the system taskbar or by application desktop toolbars, 
            // call the SystemParametersInfo function with the SPI_GETWORKAREA value.
        private const int SM_CYFULLSCREEN = 17;
            // The height of the client area for a full-screen window on the primary display monitor, in pixels. 
            // To get the coordinates of the portion of the screen not obscured by the system taskbar or by application desktop toolbars, 
            // call the SystemParametersInfo function with the SPI_GETWORKAREA value.
        private const int SM_CXVIRTUALSCREEN = 78;
        // The width of the virtual screen, in pixels. The virtual screen is the bounding rectangle of all display monitors. 
        // The SM_XVIRTUALSCREEN metric is the coordinates for the left side of the virtual screen.
        private const int SM_CYVIRTUALSCREEN = 79;
        // The height of the virtual screen, in pixels.The virtual screen is the bounding rectangle of all display monitors.
        // The SM_YVIRTUALSCREEN metric is the coordinates for the top of the virtual screen.
        private const int SM_CMONITORS = 80; // The number of display monitors on a desktop.

        // https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-showwindow
        private const int SW_HIDE = 0;
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2; // Activates the window and displays it as a minimized window. 
        // Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. 
        // An application should specify this flag when displaying the window for the first time.
        private const int SW_SHOWMAXIMIZED = 3;
        private const int SW_SHOW = 5;
        // Activates the window and displays it in its current size and position.
        private const int SW_MINIMIZE = 6;
        private const int SW_SHOWMINNOACTIVE = 7;
        // Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window is not activated.
        private const int SW_SHOWNA = 8; 
            // Displays the window in its current size and position.This value is similar to SW_SHOW, except that the window is not activated.
        private const int SW_RESTORE = 9;
        // Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. 
        // An application should specify this flag when restoring a minimized window.


        private static IntPtr HWND_TOP = IntPtr.Zero;
        private const int SWP_SHOWWINDOW = 64; // 0x0040

        /// <summary>
        /// Rectangle
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Dimensions
        {
            public int X;
            public int Y;
        }
        public Dimensions MainScreenDimensions()
        {
            Dimensions MainScr = new Dimensions();
            MainScr.X = GetSystemMetrics(SM_CXSCREEN);
            MainScr.Y = GetSystemMetrics(SM_CYSCREEN);
            return MainScr;
        }

        public int  NumOfMonitors
        {
            get { return GetSystemMetrics(SM_CMONITORS); }
        }

        public Dimensions VirtualScreenDimensions()
        {
            Dimensions VScr = new Dimensions();
            VScr.X = GetSystemMetrics(SM_CXVIRTUALSCREEN);
            VScr.Y = GetSystemMetrics(SM_CYVIRTUALSCREEN);
            return VScr;
        }

        public void SetWindowToFullScreenMain(string WindowName)
        {
            SetWindowPos(FindWindow(null, WindowName), HWND_TOP, 0, 0, GetSystemMetrics(SM_CXSCREEN), GetSystemMetrics(SM_CYSCREEN), SWP_SHOWWINDOW);
        }

        public void SetWinFullScreenByIntPtr(UInt32 hwnd)
        {
            SetWindowPos((IntPtr)hwnd, HWND_TOP, 0, 0, GetSystemMetrics(SM_CXSCREEN), GetSystemMetrics(SM_CYSCREEN), SWP_SHOWWINDOW);
        }

        /// <summary>
        ///     The MoveWindow function changes the position and dimensions of the specified window. For a top-level window, the
        ///     position and dimensions are relative to the upper-left corner of the screen. For a child window, they are relative
        ///     to the upper-left corner of the parent window's client area.
        ///     <para>
        ///     Go to https://msdn.microsoft.com/en-us/library/windows/desktop/ms633534%28v=vs.85%29.aspx for more
        ///     information
        ///     </para>
        /// </summary>
        /// <param name="hWnd">C++ ( hWnd [in]. Type: HWND )<br /> Handle to the window.</param>
        /// <param name="X">C++ ( X [in]. Type: int )<br />Specifies the new position of the left side of the window.</param>
        /// <param name="Y">C++ ( Y [in]. Type: int )<br /> Specifies the new position of the top of the window.</param>
        /// <param name="nWidth">C++ ( nWidth [in]. Type: int )<br />Specifies the new width of the window.</param>
        /// <param name="nHeight">C++ ( nHeight [in]. Type: int )<br />Specifies the new height of the window.</param>
        /// <param name="bRepaint">
        ///     C++ ( bRepaint [in]. Type: bool )<br />Specifies whether the window is to be repainted. If this
        ///     parameter is TRUE, the window receives a message. If the parameter is FALSE, no repainting of any kind occurs. This
        ///     applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the
        ///     parent window uncovered as a result of moving a child window.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.<br /> If the function fails, the return value is zero.
        ///     <br />To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public bool MoveWindowByName(string WindowName, int X, int Y, int Width, int Height)
        {
            return MoveWindow(FindWindow(null, WindowName), X, Y, Width, Height, true);
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        public RECT GetWindowRectangle(string WindowName)
        {
            RECT pRect;
            GetWindowRect(FindWindow(null, WindowName), out pRect);

            return pRect;
        }
        public Size GetWindowSize(string WindowName)
        {
            RECT pRect;
            Size cSize = new Size();
            // get coordinates relative to window
            GetWindowRect(FindWindow(null, WindowName), out pRect);

            cSize.Width = pRect.Right - pRect.Left;
            cSize.Height = pRect.Bottom - pRect.Top;

            return cSize;
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string strClassName, string strWindowName);
        public IntPtr FindWindow(string WindowName)
        {
            return FindWindow(null, WindowName);
        }

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public bool RestoreWindow(string WindowName)
        {
            return ShowWindow(FindWindow(null, WindowName), SW_RESTORE);
        }
        public bool ShowWindow(string WindowName)
        {
            return ShowWindow(FindWindow(null, WindowName), SW_SHOW);
        }
        public bool MaximizeWindow(string WindowName)
        {
            return ShowWindow(FindWindow(null, WindowName), SW_SHOWMAXIMIZED);
        }
        public bool MinimizeWindow(string WindowName)
        {
            return ShowWindow(FindWindow(null, WindowName), SW_MINIMIZE);
        }

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hWnd);
        public int SetWindowToForeground(string WindowName)
        {
            return SetForegroundWindow(FindWindow(null, WindowName));
        }
        #endregion

        #region TaskBar
        [DllImport("shell32.dll")]
        private static extern UInt32 SHAppBarMessage(UInt32 dwMessage, ref APPBARDATA pData);

        public enum AppBarMessages
        {
            New = 0x00,
            Remove = 0x01,
            QueryPos = 0x02,
            SetPos = 0x03,
            GetState = 0x04,
            GetTaskBarPos = 0x05,
            Activate = 0x06,
            GetAutoHideBar = 0x07,
            SetAutoHideBar = 0x08,
            WindowPosChanged = 0x09,
            SetState = 0x0a
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public UInt32 cbSize;
            public IntPtr hWnd;
            public UInt32 uCallbackMessage;
            public UInt32 uEdge;
            public Rectangle rc;
            public Int32 lParam;
        }

        public enum AppBarStates
        {
            AutoHide = 0x01,
            AlwaysOnTop = 0x02
        }

        /// <summary>
        /// Set the Taskbar State option
        /// </summary>
        /// <param name="option">AppBarState to activate</param>
        public void SetTaskbarState(AppBarStates option)
        {
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.hWnd = FindWindow("System_TrayWnd", null);
            msgData.lParam = (Int32)(option);
            SHAppBarMessage((UInt32)AppBarMessages.SetState, ref msgData);
        }

        /// <summary>
        /// Gets the current Taskbar state
        /// </summary>
        /// <returns>current Taskbar state</returns>
        public AppBarStates GetTaskbarState()
        {
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.hWnd = FindWindow("System_TrayWnd", null);
            return (AppBarStates)SHAppBarMessage((UInt32)AppBarMessages.GetState, ref msgData);
        }
        #endregion

        #region NEW

        // AllScreens is an array of all screens attached to
        // your system. If you have two monitors, 0 represents
        // the primary screen and 1 represents the secondary
        // and so on.   https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.screen.allscreens?redirectedfrom=MSDN&view=netframework-4.7.2#System_Windows_Forms_Screen_AllScreens
        public Screen[] AllScreens = System.Windows.Forms.Screen.AllScreens;


        #endregion

    }
}
