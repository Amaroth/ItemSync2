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
using ItemSync2.Core;

namespace ItemSync2.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserSettings usi = UserSettings.Instance;
        CoreController core = CoreController.Instance;

        public MainWindow()
        {
            InitializeComponent();
        }

        internal void LoadSettings()
        {
            dbcBox.Text = usi.dbcPath;

            hostBox.Text = usi.host;
            loginBox.Text = Utilities.ToInsecureString(usi.login);
            passwordBox.Password = Utilities.ToInsecureString(usi.password);
            savePassBox.IsChecked = usi.savePassword;
            databaseBox.Text = usi.database;
            tableBox.Text = usi.table;
            portBox.Value = usi.port;

            startIDBox.Value = usi.startID;
            endIDBox.Value = usi.endID;
        }

        private void dbcBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void hostBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void loginBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void databaseBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tableBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void portBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void startIDBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void endIDBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void savePassBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Drop(object sender, DragEventArgs e)
        {

        }

        private void dbcButt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void testConnButt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void saveSettButt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void helpButt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void checkChangesButt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dbToDBCButt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dbcToDBButt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
