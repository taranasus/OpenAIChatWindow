using IWshRuntimeLibrary;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Animation;

namespace OpenAI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;

            double monitorHeight = SystemParameters.PrimaryScreenHeight;

            Rect desktop = SystemParameters.WorkArea;

            // Get the height of the start bar
            var startBarHeight = SystemParameters.PrimaryScreenHeight - desktop.Height;

            // Calculate the height of the desktop minus the start bar
            var desktopHeightMinusStartBar = desktop.Height - startBarHeight;

            // Set the height and position of the window
            this.Height = desktopHeightMinusStartBar;
            this.Top = startBarHeight / 2;
            this.Left = -185;

            // Code written by AI with my help
            UpdateStartupButtonText();

            // Code written by me
            //ClassicSetButtonTextBasedOnStartupWithWindwos();
        }

        private void remove_add_startup(object sender, RoutedEventArgs e)
        {
            // Code written by AI with my help
            RemoveAddStartupButtonLogic();

            // Code written by me
            //ClassicRemoveAddStartupButtonLogic();
        }

        #region OpenAI generated code with my help

        private void RemoveAddStartupButtonLogic()
        {
            if (CheckStartupFolderForLnkFile("OpenAI.lnk"))
            {
                DeleteLnkFileFromStartupFolder("OpenAI.lnk");
            }
            else
            {
                CreateLnkFileInStartupFolder("OpenAI.lnk");
            }

            UpdateStartupButtonText();
        }       

        public void CreateLnkFileInStartupFolder(string lnkFileName)
        {
            // Get the path to the current WPF Windows app's executable file
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(".dll", ".exe");

            // Get the path to the Windows Startup folder
            string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            // Combine the startup path with the name of the .lnk file to get the full path
            string lnkFilePath = System.IO.Path.Combine(startupPath, lnkFileName);

            // Create a new .lnk file in the Startup folder that points to the current WPF Windows app's executable file
            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(lnkFilePath);
            shortcut.TargetPath = appPath;
            shortcut.Save();
        }

        public void DeleteLnkFileFromStartupFolder(string lnkFileName)
        {
            // Get the path to the Windows Startup folder
            string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            // Combine the startup path with the name of the .lnk file to get the full path
            string lnkFilePath = System.IO.Path.Combine(startupPath, lnkFileName);

            // Check if the .lnk file exists in the Startup folder
            if (System.IO.File.Exists(lnkFilePath))
            {
                // Delete the .lnk file from the Startup folder
                System.IO.File.Delete(lnkFilePath);
            }
        }


        private void UpdateStartupButtonText()
        {
            // Check if the .lnk file exists in the Startup folder
            bool lnkFileExists = CheckStartupFolderForLnkFile("OpenAI.lnk");           
            if (lnkFileExists)
            {
                button1.Content = "Remove from Windows Startup";
            }
            else
            {
                button1.Content = "Add to Windows Startup";
            }
        }


        public bool CheckStartupFolderForLnkFile(string lnkFileName)
        {
            // Get the path to the Windows Startup folder
            string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            // Combine the startup path with the name of the .lnk file to get the full path
            string lnkFilePath = System.IO.Path.Combine(startupPath, lnkFileName);

            // Check if the .lnk file exists in the Startup folder
            return System.IO.File.Exists(lnkFilePath);
        }

        #endregion
        #region Code written by me that does the same thing as the AI one
        private void ClassicRemoveAddStartupButtonLogic()
        {
            if (ClassicIsStartupWithWindowsEnabled())
                ClassicDeleteShortcut();
            else
                ClassicCreateShortcut();

            ClassicSetButtonTextBasedOnStartupWithWindwos();
        }

        public void ClassicDeleteShortcut()
        {
            if(System.IO.File.Exists(ClassicGetFullPathToShortcut()))
                System.IO.File.Delete(ClassicGetFullPathToShortcut());
        }

        public void ClassicCreateShortcut()
        {
            WshShell wsh = new WshShell();
            IWshRuntimeLibrary.IWshShortcut shortcut = wsh.CreateShortcut(ClassicGetFullPathToShortcut()) as IWshRuntimeLibrary.IWshShortcut;            

            var theExe = System.Reflection.Assembly.GetEntryAssembly().Location.Replace(".dll",".exe");
            shortcut.TargetPath = theExe;
            shortcut.Description = "OpenAI Chat";
            shortcut.WorkingDirectory = System.IO.Path.GetDirectoryName(theExe);            
            shortcut.Save();
        }


        public void ClassicSetButtonTextBasedOnStartupWithWindwos()
        {
            if (ClassicIsStartupWithWindowsEnabled())
                button1.Content = "Remove from Windows Startup";
            else
                button1.Content = "Add to Windows Startup";
        }

        private string ClassicGetFullPathToShortcut()
        {
            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var OpenAIShortcutPath = System.IO.Path.Combine(appDataFolder, "Microsoft\\Windows\\Start Menu\\Programs\\Startup\\OpenAI.lnk");
            return OpenAIShortcutPath;
        }

        public bool ClassicIsStartupWithWindowsEnabled()
        {           

            if (System.IO.File.Exists(ClassicGetFullPathToShortcut()))
            {
                return true;
            }

            return false;
        }

        #endregion
        #region rest of the software logic

        Visibility visible = OpenAI.Visibility.Chat;

        private void show_hide_click(object sender, RoutedEventArgs e)
        {
            if (visible == OpenAI.Visibility.Chat)
                visible = OpenAI.Visibility.Hidden;
            else
                visible = OpenAI.Visibility.Chat;
            ProcessVisibility();
        } 

        private void ProcessVisibility()
        {
            DoubleAnimation leftAnimation = new DoubleAnimation();
            leftAnimation.From = this.Left;

            if (visible == OpenAI.Visibility.Chat)
            {
                leftAnimation.To = -185;
            }
            else if (visible == OpenAI.Visibility.Hidden)
            {
                leftAnimation.To = -this.Width + 20;
            }
            else
            {
                leftAnimation.To = 30;
            }

            leftAnimation.Duration = TimeSpan.FromSeconds(0.3f);
            this.BeginAnimation(Window.LeftProperty, leftAnimation);
        }

        private void show_hide_menu(object sender, RoutedEventArgs e)
        {
            if (visible == OpenAI.Visibility.Menu)
                visible = OpenAI.Visibility.Chat;
            else
                visible = OpenAI.Visibility.Menu;
            ProcessVisibility();
        }      

        private void show_remaining_credits(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://beta.openai.com/account/usage") { UseShellExecute = true });
        }
        #endregion
    }

    public enum Visibility
    {
        Hidden,
        Chat,
        Menu
    }
}
