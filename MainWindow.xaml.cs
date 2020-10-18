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
        MainFilesHandler MFH = MainFilesHandler.Instance;

        public MainWindow()
        {
            // If any problem comes up at the startiung of the app, the application will delete all information from the static file and try again
            // If after two attempts the applicaiton will still won't open, the application will open an email to send to the developer

            try
            {
                SIH.InitializeFiles();
                Paths.InitializeSettings();
                FrameworkInstall();
                InitializeComponent();
                MW = (MainWindow)Window.GetWindow(this);
                MFH.InitializeFilesWithoutWriting();
                TutorialStart();
            }
            catch (Exception)
            {
                try
                {
                    if (MessageBox.Show("אוי, משהו קרה והתוכנה נכשלה, האם לנסות לתקן?", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        File.WriteAllText(Paths.tempDLFile, String.Empty);
                        SIH.InitializeFiles();
                        Paths.InitializeSettings();
                        FrameworkInstall();
                        InitializeComponent();
                        MW = (MainWindow)Window.GetWindow(this);
                        MFH.InitializeFilesWithoutWriting();
                        TutorialStart();
                    }
                }
                catch (Exception)
                {
                    if (MessageBox.Show("הייתה בעיה שוב, האם תרצו ליצור קשר עם המנהל של התוכנה?", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Process.Start(@"https://mail.google.com/mail/?view=cm&fs=1&to=yuvalperetzj@gmail.com
                        &su=ספרייה דיגיטלית - עזרה
                        &body=יש לרשום את המייל כאן
                        &bcc=נא לרשום כאן את כתובת המייל");
                    }
                }
            }
        }

        private void FrameworkInstall()
        {
            // Triggers the installation of the Framework needed for this application

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

        private void TutorialStart()
        {
            // Triggers the start of the tutorial

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
            // When exiting the application, the backup will be triggerd

            Task BUTask = Task.Run(()=> SIH.TriggerBackup());
            var awaiter = BUTask.GetAwaiter();
            awaiter.OnCompleted(()=> 
            { 
                Application.Current.Shutdown();
            });
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
