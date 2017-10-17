using System;
using System.Collections.Generic;
using System.IO;
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
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ReflectionUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CbAll_Unchecked(new object(), new RoutedEventArgs());
        }

        private void BtClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtDirChoice_Click(object sender, RoutedEventArgs e)
        {
            LbDll.Items.Clear();
            var folderDialog = new FolderBrowserDialog();
            folderDialog.ShowDialog();
            var path = folderDialog.SelectedPath;
            TbDirectoryChoosen.Text = path;
            var files = Directory.GetFiles(path);
            string pattern = "^.*.(dll)$";
            Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
            files.Where(f => reg.IsMatch(f)).ToList().ForEach(d => LbDll.Items.Add(d));
        }

        private void LbDll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LbTypes.Items.Clear();
            Assembly assembly = Assembly.LoadFrom(LbDll.SelectedItem.ToString());
            assembly.GetTypes().ToList().Select(t => t.Name).ToList().ForEach(t => LbTypes.Items.Add(t));
        }

        private void LbTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LbProperties.Items.Clear();
            LbFields.Items.Clear();
            LbMethods.Items.Clear();
            Assembly assembly = Assembly.LoadFrom(LbDll.SelectedItem.ToString());
            var selectedType = LbTypes.SelectedItem.ToString();
            var type = assembly.DefinedTypes.First(t => t.Name == selectedType);
            type.GetProperties().ToList().Select(p => p.Name).ToList().ForEach(p => LbProperties.Items.Add(p));
            type.GetFields().ToList().Select(f => f.Name).ToList().ForEach(f => LbFields.Items.Add(f));
            type.GetMethods().ToList().Select(m => m.Name).ToList().ForEach(m => LbMethods.Items.Add(m));
        }

        private void CbAll_Checked(object sender, RoutedEventArgs e)
        {
            this.Height = 850;
            CbProperties.IsEnabled = false;
            CbFields.IsEnabled = false;
            CbMethods.IsEnabled = false;
            CbProperties.IsChecked = true;
            CbFields.IsChecked = true;
            CbMethods.IsChecked = true;
            LbMethodsLable.Visibility = Visibility.Visible;
            LbMethods.Visibility = Visibility.Visible;
            LbFieldsLable.Visibility = Visibility.Visible;
            LbFields.Visibility = Visibility.Visible;
            LbPropertiesLable.Visibility = Visibility.Visible;
            LbProperties.Visibility = Visibility.Visible;
        }

        private void CbAll_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Height = 475;
            CbProperties.IsEnabled = true;
            CbFields.IsEnabled = true;
            CbMethods.IsEnabled = true;
            CbProperties.IsChecked = false;
            CbFields.IsChecked = false;
            CbMethods.IsChecked = false;
            LbMethodsLable.Visibility = Visibility.Hidden;
            LbMethods.Visibility = Visibility.Hidden;
            LbFieldsLable.Visibility = Visibility.Hidden;
            LbFields.Visibility = Visibility.Hidden;
            LbPropertiesLable.Visibility = Visibility.Hidden;
            LbProperties.Visibility = Visibility.Hidden;
        }

        private void CbProperties_Checked(object sender, RoutedEventArgs e)
        {
            ResizeWindowAnyIsChecked();
            LbPropertiesLable.Visibility = Visibility.Visible;
            LbProperties.Visibility = Visibility.Visible;
        }

        private void CbProperties_Unchecked(object sender, RoutedEventArgs e)
        {
            ResizeWindowAllAreUnChecked();
            LbPropertiesLable.Visibility = Visibility.Hidden;
            LbProperties.Visibility = Visibility.Hidden;
            if (CbAll.IsChecked == true)
                CbAll.IsChecked = false;
        }

        private void CbFields_Checked(object sender, RoutedEventArgs e)
        {
            ResizeWindowAnyIsChecked();
            LbFieldsLable.Visibility = Visibility.Visible;
            LbFields.Visibility = Visibility.Visible;
        }
    

        private void CbFields_Unchecked(object sender, RoutedEventArgs e)
        {
            ResizeWindowAllAreUnChecked();
            LbFieldsLable.Visibility = Visibility.Hidden;
            LbFields.Visibility = Visibility.Hidden;
            if (CbAll.IsChecked == true)
            {
                CbAll.IsChecked = false;
            }
        }

        private void CbMethods_Checked(object sender, RoutedEventArgs e)
        {
            ResizeWindowAnyIsChecked();
            LbMethodsLable.Visibility = Visibility.Visible;
            LbMethods.Visibility = Visibility.Visible;
        }

        private void CbMethods_Unchecked(object sender, RoutedEventArgs e)
        {
            ResizeWindowAllAreUnChecked();
            LbMethodsLable.Visibility = Visibility.Hidden;
            LbMethods.Visibility = Visibility.Hidden;
            if (CbAll.IsChecked == true)
                CbAll.IsChecked = false;
        }

        private void ResizeWindowAnyIsChecked()
        {
            if(CbProperties.IsChecked == true || CbFields.IsChecked == true || CbMethods.IsChecked == true)
            {
                this.Height = 850;
                BtCloseAllInvisible.Visibility = Visibility.Hidden;
            }
        }

        private void ResizeWindowAllAreUnChecked()
        {
            if (CbProperties.IsChecked == false && CbFields.IsChecked == false && CbMethods.IsChecked == false)
            {
                this.Height = 475;
                BtCloseAllInvisible.Visibility = Visibility.Visible;
            }
        }
    }
}
