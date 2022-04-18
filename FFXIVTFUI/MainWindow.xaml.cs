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
using Engine.ViewModel;
using Microsoft.Win32;

namespace FFXIVTFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewSession _viewSession;
        public MainWindow()
        {
            InitializeComponent();

            _viewSession = new ViewSession();

            DataContext = _viewSession;
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("I've been clicked");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            //openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            
            if (openFileDialog.ShowDialog() == true)
            {
                // NEEDS REFACTORING AND MOVING STUFF AROUND
                //_viewSession.CurrentTextLog.RawText = File.ReadAllText(openFileDialog.FileName);
                _viewSession.CurrentTextLog.FilePath = openFileDialog.FileName;
                _viewSession.CurrentTextLog.RawText = File.ReadAllText(_viewSession.CurrentTextLog.FilePath);
                _viewSession.cleanedText = _viewSession.CleanRawText(_viewSession.CurrentTextLog.RawText);

            }

            //problem: this is not displaying/updating the binding
            _viewSession.DisplayBattleLogs();

            //this works
            //MessageBox.Show(openFileDialog.FileName);

            //this works, as in, DisplayBattleLogs() works
            //MessageBox.Show(_viewSession.CurrentTextLog.FilteredText);


        }
    }
}
