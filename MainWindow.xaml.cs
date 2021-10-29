using DigitalLibrary.Models.Classes.Common;
using DigitalLibrary.Models.Classes.Useable;
using DigitalLibrary.Views.Windows.Mains;
using DigitalLibrary.Views.Windows.Mains.Settings;
using DigitalLibrary.Views.Windows.Mains.Students;
using DigitalLibrary.Views.Windows.Tutorials;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace DigitalLibrary
{
    public partial class MainWindow : Window
    {
        StaticInformationHandler SIH = StaticInformationHandler.Instance;
        Window BW, SW, MW, STW, TW;
        TextFileHandler TFH = TextFileHandler.Instance;

        public MainWindow()
        {
            SIH.InitializeMainTemp();
            Paths.InitializeSettings();
            FrameworkInstall();
            InitializeComponent();
            MW = (MainWindow)Window.GetWindow(this);
            TFH.InitializeFilesWithoutWriting();
            TutorialStart();
        }

        private void FrameworkInstall()
        {
            if (!SIH.CheckLineExistens("framworkinstalled=true"))
            {
                if (MessageBox.Show("לצורך הפעלת התוכנה נדרשת התקנה של Framework 4.7.2, האם להמשיך בתהקנה?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    try
                    {
                        var pInstallFramwork = Process.Start(path.Substring(0, path.IndexOf("bin")) + @"Resources\Installers\NDP472-KB4054531-Web.exe");
                        pInstallFramwork.WaitForExit();
                        SIH.OverrideLine("framworkinstalled=true", "framworkinstalled=false");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("ERROR:\n ההתקנה נעצרה.");
                        Close();
                    }
                }
                else
                    Close();
            }
        }

        /*private static void Get45PlusFromRegistry()
         {
             const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

             using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
             {
                 if (ndpKey != null && ndpKey.GetValue("Release") != null)
                 {
                     //Do nothing if .Net 4.5 or higher is found.
                     MessageBox.Show("You Have Installed:  .NET Framework Version: " + CheckFor45PlusVersion((int)ndpKey.GetValue("Release")));
                     //if (MessageBox.Show("Continue With The Installaiton?", "Question", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                     //{
                     //    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                     //    Process.Start(path.Substring(0, path.IndexOf("bin")) + @"Resources\Installers\NDP472-KB4054531-Web.exe");
                     //}
                 }
                 else
                 {
                     // Do something if .Net 4.5 or higher is found
                     MessageBox.Show("This program Requires .NET Framework Version 4.5 or later. Click OK to access Microsoft official website and download .NET 4.5 framework.");
                     if (MessageBox.Show("Continue With The Installaiton?", "Question", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                     {
                         Process.Start(@"http://go.microsoft.com/fwlink/?LinkId=863262");
                     }
                 }
             }
         }

         //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
         //Process.Start(path.Substring(0, path.IndexOf("bin")) + @"Resources\Installers\NDP472-KB4054531-Web.exe");
         // Checking the version using >= will enable forward compatibility.
         private static string CheckFor45PlusVersion(int releaseKey)
         {
             if (releaseKey >= 461808)
                 return "4.7.2 or later";
             if (releaseKey >= 461308)
                 return "4.7.1";
             if (releaseKey >= 460798)
                 return "4.7";
             if (releaseKey >= 394802)
                 return "4.6.2";
             if (releaseKey >= 394254)
                 return "4.6.1";
             if (releaseKey >= 393295)
                 return "4.6";
             if (releaseKey >= 379893)
                 return "4.5.2";
             if (releaseKey >= 378675)
                 return "4.5.1";
             if (releaseKey >= 378389)
                 return "4.5";
             // This code should never execute. A non-null release key should mean
             // that 4.5 or later is installed.
             return "No 4.5 or later version detected";
         }*/

        private void TutorialStart()
        {
            if (SIH.CheckLineExistens("tutorial=true"))
            {
                if (!Application.Current.Windows.OfType<MainTutorialFrameWindow>().Any())
                {
                    TW = new MainTutorialFrameWindow
                    {
                        Tag = "mdi_child"
                    };
                    TW.ShowDialog();
                }
            }

        }
        private void BTN_Books_Click(object sender, RoutedEventArgs e)
        {
            BW = new BooksWindow();
            BW.Show();
            MW.Visibility = Visibility.Collapsed;
        }
        private void BTN_Students_Click(object sender, RoutedEventArgs e)
        {
            SW = new StudentsWindow();
            SW.Show();
            MW.Visibility = Visibility.Collapsed;
        }
        private void BTN_Settings_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<SettingsWindow>().Any())
            {
                STW = new SettingsWindow
                {
                    Tag = "mdi_child"
                };
                STW.ShowDialog();
            }
        }
        private void BTN_Exit_Click(object sender, RoutedEventArgs e)
        {
            SIH.TriggerBackup();
            Application.Current.Shutdown();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
