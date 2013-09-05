using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pwrs.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                btnReset.IsEnabled = false;
            }
            else
            {
                btnReset.IsEnabled = true;
            }
        }

        private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                btnReset.IsEnabled = false;
            }
            else
            {
                btnReset.IsEnabled = true;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string userDN = Account.GetUserDn(txtUserName.Text, AppSettings.LDAPServer + AppSettings.UserLDAPPath);
                Account.ResetPassword(userDN, txtPassword.Text, chkPwdChange.IsChecked.Value);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("DISP_E_UNKNOWNNAME"))
                {
                    MessageBox.Show("Cannot find the user on the server","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                else if (ex.InnerException != null)
                {
                    if (ex.InnerException.ToString().Contains("0x80072035"))
                    {
                       MessageBox.Show("The chosen password does not meet the password standards.","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            txtPassword.Clear();
        }
    }
}
