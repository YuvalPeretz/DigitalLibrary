using DigitalLibrary.Models.Classes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DigitalLibrary.Views.Windows.Mains.Students
{
    /// <summary>
    /// Interaction logic for StudentsAdd.xaml
    /// </summary>
    public partial class StudentsAdd : Window
    {
        Student s = Student.Instance;
        public StudentsAdd()
        {
            InitializeComponent();
            
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void BTN_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BTN_AddStudent_Click(object sender, RoutedEventArgs e)
        {
            var preItem = new object();
            Label DataLabel;
            if (TB_PhoneNumber.Text.Count() >= 10)
            {
                MainSP.Children.OfType<StackPanel>().ToList().ForEach(sp =>
                {
                    foreach (var item in sp.Children)
                    {
                        if (item.GetType() != typeof(Label))
                        {
                            TextBox t = (TextBox)item;
                            DataLabel = (Label)preItem;
                            if (string.IsNullOrEmpty(t.Text))
                                DataLabel.Foreground = new SolidColorBrush(Colors.Red);
                            else
                                DataLabel.Foreground = new SolidColorBrush(Colors.Black);
                        }
                        else
                            preItem = (Label)item;
                    }
                });
                if (
                    L_StudentName.Foreground.ToString().Equals(Brushes.Black.ToString()) &
                    L_PhoneNumber.Foreground.ToString().Equals(Brushes.Black.ToString()))
                {
                    Student student = new Student(TB_StudentName.Text, TB_PhoneNumber.Text);
                    s.AddStudent(student);
                    TB_StudentName.Text = String.Empty;
                    TB_PhoneNumber.Text = String.Empty;
                }
                else
                    MessageBox.Show("יש למלא את כל השדות המסומנים באדום");
            }
            else
            {
                if (MessageBox.Show("מספר הטלפון לא תקין, האם בכל זאת להמשיך?", "אזהרה", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    MainSP.Children.OfType<StackPanel>().ToList().ForEach(sp =>
                    {
                        foreach (var item in sp.Children)
                        {
                            if (item.GetType() != typeof(Label))
                            {
                                TextBox t = (TextBox)item;
                                DataLabel = (Label)preItem;
                                if (string.IsNullOrEmpty(t.Text))
                                    DataLabel.Foreground = new SolidColorBrush(Colors.Red);
                                else
                                    DataLabel.Foreground = new SolidColorBrush(Colors.Black);
                            }
                            else
                                preItem = (Label)item;
                        }
                    });
                    if (
                        L_StudentName.Foreground.ToString().Equals(Brushes.Black.ToString()) &
                        L_PhoneNumber.Foreground.ToString().Equals(Brushes.Black.ToString()))
                    {
                        Student student = new Student(TB_StudentName.Text, TB_PhoneNumber.Text);
                        s.AddStudent(student);
                        TB_StudentName.Text = String.Empty;
                        TB_PhoneNumber.Text = String.Empty;
                    }
                    else
                        MessageBox.Show("יש למלא את כל השדות המסומנים באדום");
                }
            }
        }
        private void TB_PhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
