using DigitalLibrary.Models.Classes.Useable;
using DigitalLibrary.Views.Windows.Tutorials.Pages;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DigitalLibrary.Views.Windows.Tutorials
{
    public partial class MainTutorialFrameWindow : Window
    {
        List<Page> Pages = new List<Page>();
        int currentPageIndex = 0;
        BitmapImage // Initialize the arrows images
            btn_back_img_off = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Back_Off_Icon.png")),
            btn_back_img_on = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Back_On_Icon.png")),
            btn_next_img_off = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Next_Off_Icon.png")),
            btn_next_img_on = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Next_On_Icon.png"));
        public MainTutorialFrameWindow()
        {
            InitializeComponent();
            InitializePages();
            MainFrame.Content = Pages[currentPageIndex];
            UpdateArrows();
        }

        private void InitializePages()
        {
            // Initialize the Pages List to contain all of thew pages

            Pages.Add(new WelcomePage());
            Pages.Add(new MainWindowPage());
            Pages.Add(new BooksWindowPage());
            Pages.Add(new StudentsWindowPage());
            Pages.Add(new ImportantToKnowPage());
            Pages.Add(new InfoSavingPage());
            Pages.Add(new EndPage());
        }

        private void UpdateArrows()
        {
            // Every click, the arrows will be updated according to the situation

            if (currentPageIndex != 0 & currentPageIndex != Pages.Count() - 1)
            {
                BTN_Next.IsEnabled = true;
                BTN_Back.IsEnabled = true;
                BTN_Next_Image.Source = btn_next_img_on;
                BTN_Back_Image.Source = btn_back_img_on;
            }
            else if (currentPageIndex == 0)
            {
                BTN_Next.IsEnabled = true;
                BTN_Back.IsEnabled = false;
                BTN_Next_Image.Source = btn_next_img_on;
                BTN_Back_Image.Source = btn_back_img_off;
            }
            else if (currentPageIndex == Pages.Count() - 1)
            {
                BTN_Next.IsEnabled = false;
                BTN_Back.IsEnabled = true;
                BTN_Next_Image.Source = btn_next_img_off;
                BTN_Back_Image.Source = btn_back_img_on;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void BTN_Next_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex++;
            MainFrame.Content = Pages[currentPageIndex];
            UpdateArrows();
            SV.ScrollToTop();
        }

        private void BTN_Back_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex--;
            MainFrame.Content = Pages[currentPageIndex];
            UpdateArrows();
            SV.ScrollToTop();
        }
    }
}
