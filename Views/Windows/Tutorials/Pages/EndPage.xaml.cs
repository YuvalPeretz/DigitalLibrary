using DigitalLibrary.Models.Classes.Useable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitalLibrary.Views.Windows.Tutorials.Pages
{
    /// <summary>
    /// Interaction logic for EndPage.xaml
    /// </summary>
    public partial class EndPage : Page
    {
        StaticInformationHandler SIH = StaticInformationHandler.Instance;
        public EndPage()
        {
            InitializeComponent();
        }

        private void BTN_EndTutorial_Click(object sender, RoutedEventArgs e)
        {
            SIH.OverrideLine("tutorial=false", "tutorial=true");
            Application.Current.Windows.OfType<MainTutorialFrameWindow>().First().Close();
        }
    }
}
