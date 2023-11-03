using Microsoft.Extensions.DependencyInjection;
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

namespace HealthCareAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Stack<UserControl> _viewHistory = new Stack<UserControl>();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void NavigateToView(UserControl view)
        {
            _viewHistory.Push(view);
            MainContentControl.Content = view;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewHistory.Count > 1) // Ensure there's a view to go back to
            {
                _viewHistory.Pop(); // Pop the current view
                UserControl previousView = _viewHistory.Peek(); // Get the previous view
                MainContentControl.Content = previousView; // Set it as the content
            }
        }
    }
}
