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
using Microsoft.Win32;

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

            createInDB.IsChecked = usi.createInDB;
            createInDBC.IsChecked = usi.createInDBC;
            updateInDBCBox.IsChecked = usi.updateInDBC;
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 1)
            {
                if (System.IO.Path.GetExtension(files[0]).ToLower() == ".dbc" || System.IO.Path.GetExtension(files[0]).ToLower() == ".db2")
                    dbcBox.Text = files[0];
            }
        }

        #region Input field event handlers...
        private void dbcBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            usi.dbcPath = dbcBox.Text;
        }

        private void hostBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            usi.host = hostBox.Text;
        }

        private void loginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            usi.login = Utilities.ToSecureString(loginBox.Text);
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            usi.password = Utilities.ToSecureString(passwordBox.Password);
        }

        private void databaseBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            usi.database = databaseBox.Text;
        }

        private void tableBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            usi.table = tableBox.Text;
        }

        private void portBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            usi.port = (int)portBox.Value;
        }

        private void startIDBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            usi.startID = (int)startIDBox.Value;
        }

        private void endIDBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            usi.endID = (int)endIDBox.Value;
        }
        #endregion

        #region Button event handlers...
        private void dbcButt_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "DBC files (*.dbc)|*.dbc|DB2 files (*.db2)|*.db2";
            if ((bool)ofd.ShowDialog())
                dbcBox.Text = ofd.FileName;
        }

        private void testConnButt_Click(object sender, RoutedEventArgs e)
        {
            core.TestConnection();
        }

        private void saveSettButt_Click(object sender, RoutedEventArgs e)
        {
            core.SaveUserSettings();
        }

        private void helpButt_Click(object sender, RoutedEventArgs e)
        {
            core.Help();
        }

        private void checkChangesButt_Click(object sender, RoutedEventArgs e)
        {
            core.CheckChanges();
        }

        private void doStuffButt_Click(object sender, RoutedEventArgs e)
        {
            core.Sync();
        }
        #endregion

        #region Checkbox event handlers...
        private void savePassBox_Checked(object sender, RoutedEventArgs e)
        {
            usi.savePassword = (bool)savePassBox.IsChecked;
        }

        private void createInDB_Checked(object sender, RoutedEventArgs e)
        {
            usi.createInDB = (bool)createInDB.IsChecked;
        }

        private void createInDBC_Checked(object sender, RoutedEventArgs e)
        {
            usi.createInDBC = (bool)createInDBC.IsChecked;
        }

        private void updateInDBCBox_Checked(object sender, RoutedEventArgs e)
        {
            usi.updateInDBC = (bool)updateInDBCBox.IsChecked;
        }
        #endregion
    }
}
