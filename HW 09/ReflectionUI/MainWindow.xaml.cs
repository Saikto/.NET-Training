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
            var type = assembly.DefinedTypes.Single(t => t.Name == selectedType);
            type.GetProperties().ToList().Select(p => p.Name).ToList().ForEach(p => LbProperties.Items.Add(p));
            type.GetFields().ToList().Select(f => f.Name).ToList().ForEach(f => LbFields.Items.Add(f));
            type.GetMethods().ToList().Select(m => m.Name).ToList().ForEach(m => LbMethods.Items.Add(m));
        }
    }
}
