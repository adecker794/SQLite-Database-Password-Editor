using Microsoft.Win32;
using System;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace DatabasePasswordEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            error.Opacity = 0;
            sqlerror.Opacity = 0;
        }

        private void btn_browseclick(object sender, RoutedEventArgs e)
        {
            error.Background = Brushes.White;
            error.Text = "";
            error.Opacity = 0;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (Directory.Exists(@"C:\ProgramData\SDDC.HHTI"))
            {
                openFileDialog.InitialDirectory = @"C:\ProgramData\SDDC.HHTI";
            }
            if (openFileDialog.ShowDialog() == true)
            {
                browsefilename.Text = openFileDialog.FileName;
            }
        }

        private void removepasswordclick(object sender, RoutedEventArgs e)
        {
            
            if (!String.IsNullOrEmpty(browsefilename.Text))
            {
                error.Background = Brushes.White;
                error.Text = "";
                error.Opacity = 0;

                if (!String.IsNullOrEmpty(currentpassword.Text))
                {

                    try
                    {
                        SQLiteConnection cnn = new SQLiteConnection("Data Source=" + browsefilename.Text + ";Password=" + currentpassword.Text);
                        //cnn.Close();
                        cnn.Open();
                        cnn.ChangePassword("");
                        cnn.Close();
                    }
                    catch (SQLiteException sqex)
                    {
                        sqlerror.Opacity = 100;
                        sqlerror.Background = Brushes.Red;
                        string err = sqex.Message.ToString();
                        sqlerror.Text = err;
                    }
                }
                else
                {
                    error.Opacity = 100;
                    error.Background = Brushes.Red;
                    error.Text = "YOU MUST SELECT A PASSWORD!!!";
                }
            }
            else
            {
                error.Opacity = 100;
                error.Background = Brushes.Red;
                error.Text = "YOU MUST SELECT A FILE AND INPUT A PASSWORD!!!";
            }
        }

        private void setpasswordclick(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(browsefilename.Text))
            {
                error.Background = Brushes.Transparent;
                error.Text = "";
                error.Opacity = 0;

                if (!String.IsNullOrEmpty(newpassword.Text))
                {
                    try
                    {

                        SQLiteConnection cnn = new SQLiteConnection("Data Source=" + browsefilename.Text);
                        //cnn.Close();
                        cnn.Open();
                        cnn.ChangePassword(newpassword.Text);
                        cnn.Close();
                    }
                    catch (SQLiteException sqex)
                    {
                        sqlerror.Opacity = 100;
                        sqlerror.Background = Brushes.Red;
                        string err = sqex.Message.ToString();
                        sqlerror.Text = err;
                    }
                }
                else
                {
                    error.Opacity = 100;
                    error.Background = Brushes.Red;
                    error.Text = "YOU MUST SELECT A PASSWORD!!!";
                }
            }
            else
            {
                error.Opacity = 100;
                error.Background = Brushes.Red;
                error.Text = "YOU MUST SELECT A FILE AND INPUT A PASSWORD!!!";
            }
        }
    }
}
