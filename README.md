# WinAPI
 Selected Win API Function Calls for Window, Taskbar, etc manipulation, conveniently organized and wrapped in a C# DLL  
 
A simple wrap - ```FindWindow```:
```cs
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string strClassName, string strWindowName);
        public IntPtr FindWindow(string WindowName)
        {
            return FindWindow(null, WindowName);
        }
```
A more demanding wrap - ```GetWindowSize```:
```cs
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
```
Derived functions that use ```private const``` values: 
```cs       
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
```
        
### References  
<a href="https://docs.microsoft.com/en-gb/windows/win32/api/winuser/nf-winuser-movewindow">WinUser MoveWindow function</a>  
<a href="https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-showwindow">WinUser ShowWindow function</a>  
<a href="https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.screen.allscreens?redirectedfrom=MSDN&view=netframework-4.7.2#System_Windows_Forms_Screen_AllScreens">System Windows Forms > Screen > AllScreens </a>   

