using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Npgsql;

namespace yelpApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TipWindow : Window
    {
        string businessid;
        string userId;

        public TipWindow(string businessID, string userId)
        {
            InitializeComponent();
            this.businessid = businessID;
            this.userId = userId;
        }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = yelp; password = psqladmin";
        }

        private void executeQuery(string sqlstr)
        {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {

                    cmd.Connection = connection;
                    cmd.CommandText = sqlstr;

                    try
                    {
                        var reader = cmd.ExecuteReader();
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (tipText.Text.Length > 0)
            {
                string sqlstr = "INSERT INTO Tip VALUES ('" + businessid + "', '" + userId + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " + 0 + ", '" + tipText.Text + "');";
                executeQuery(sqlstr);
                this.Close();
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
