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
using yelpApp;
using System.Windows.Controls.DataVisualization;
using System.Windows.Controls.DataVisualization.Charting;

namespace milestone1
{
    /// <summary>
    /// Interaction logic for BusinessDetails.xaml
    /// </summary>
    public partial class BusinessDetails : Window
    {
        private string businessId;
        private string userId;
        Dictionary<int, int> totalCheckins = new Dictionary<int, int>();

        public BusinessDetails(string businessId, string userId)
        {
            InitializeComponent();
            this.businessId = String.Copy(businessId);
            if (userId != null)
            {
                this.userId = String.Copy(userId);
            }
            for (int i = 1; i < 13; i++) // empty month dictionary
            {
                this.totalCheckins.Add(i, 0);
            }
            addColumns2Grid();
            addColumns2Grid2();
            setTipGrid();
            setTipGrid2();
            loadDetails();
            LoadColumnChartData();
        }

        public class BusinessTip
        {
            public string userid { get; set; }
            public string name { get; set; }
            public string tip { get; set; }
            public int likes { get; set; }
            public string date { get; set; }
        }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = yelp; password = psqladmin";
        }

        private void executeQuery(string sqlstr, Action<NpgsqlDataReader> act)
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
                        while (reader.Read())
                        {
                            act(reader);
                        }

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

        // execute query 3
        private void executeQuery(string sqlstr, int i)
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
                        while (reader.Read())
                        {
                            int result = reader.GetInt32(0);
                            this.totalCheckins[i] = result;
                            //this.totalCheckins[i] = result;
                        }
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

        // Set business details
        private void setDetails(NpgsqlDataReader reader)
        {
            bname.Text = reader.GetString(0);
            sname.Text = reader.GetString(1);
            cname.Text = reader.GetString(2);
        }

        // Load business details
        private void loadDetails()
        {
            string sqlstr = "SELECT name, state, city FROM business WHERE businessid = '" + this.businessId + "';";
            executeQuery(sqlstr, setDetails);
            loadNumbers();
        }

        // Set number of businesses in same state
        void setNumInState(NpgsqlDataReader reader)
        {
            inState.Text = reader.GetInt16(0).ToString();
        }

        // Set number of businesses in same city
        void setNumInCity(NpgsqlDataReader reader)
        {
            inCity.Text = reader.GetInt16(0).ToString();
        }

        // Load business statistics
        private void loadNumbers()
        {
            string sqlstr1 = "SELECT count(*) from business WHERE state = (SELECT state FROM business WHERE businessid = '" + this.businessId + "');";
            executeQuery(sqlstr1, setNumInState);
            string sqlstr2 = "SELECT count(*) from business WHERE city = (SELECT city FROM business WHERE businessid = '" + this.businessId + "');";
            executeQuery(sqlstr2, setNumInCity);
        }

        // Add columns to tip grid
        private void addColumns2Grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("tip");
            col1.Header = "Tip";
            col1.Width = 400;
            tipGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("name");
            col2.Header = "Name";
            col2.Width = 100;
            tipGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("likes");
            col3.Header = "Likes";
            col3.Width = 100;
            tipGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("userid");
            col4.Header = "User ID";
            col4.Width = 100;
            tipGrid.Columns.Add(col4);
        }

        // Add columns to tip grid2
        private void addColumns2Grid2()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("tip");
            col1.Header = "Tip";
            col1.Width = 400;
            tipGrid2.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("name");
            col2.Header = "Name";
            col2.Width = 100;
            tipGrid2.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("likes");
            col3.Header = "Likes";
            col3.Width = 100;
            tipGrid2.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("userid");
            col4.Header = "User ID";
            col4.Width = 100;
            tipGrid2.Columns.Add(col4);
        }

        // Add row to tip grid
        private void addGridRow(NpgsqlDataReader reader)
        {
            tipGrid.Items.Add(new BusinessTip() { userid = reader.GetString(0), name = reader.GetString(1), tip = reader.GetString(2), likes = reader.GetInt32(3), date = reader.GetString(4) });
        }

        // Add row to tip2 grid
        private void addGridRow2(NpgsqlDataReader reader)
        {
            tipGrid2.Items.Add(new BusinessTip() { userid = reader.GetString(0), name = reader.GetString(1), tip = reader.GetString(2), likes = reader.GetInt32(3), date = reader.GetString(4) });
        }

        // Set up the tip grid with latest tip data
        private void setTipGrid()
        {
            tipGrid.Items.Clear();
            string sqlstr = "SELECT tu.userid, tu.name, text, likes, date FROM (business INNER JOIN (SELECT businessid, text, usertable.userid, likes, name, date FROM tip INNER JOIN usertable ON tip.userid = usertable.userid) AS tu ON business.businessid = tu.businessid) WHERE business.businessid = '" + this.businessId + "';";
            executeQuery(sqlstr, addGridRow);
            //(tipGrid.ItemsSource as DataView).Sort = "Likes";
        }

        // Set up the tip grid with latest tip data
        private void setTipGrid2()
        {
            tipGrid2.Items.Clear();
            string sqlstr = "SELECT tu2.userid, tu2.name, text, likes, date " +
            "FROM (SELECT tu.userid, tu.name, text, likes, date FROM ( business " +
            "INNER JOIN(SELECT businessid, text, usertable.userid, likes, name, date FROM tip " +
            "INNER JOIN usertable ON tip.userid = usertable.userid) AS tu " +
            "ON business.businessid = tu.businessid) " +
            "WHERE business.businessid = '"+ this.businessId+"') as tu2, " +
            "(SELECT usertable.userid, friends.friendid FROM usertable " +
            "INNER JOIN friends ON usertable.userid = friends.userid " +
            "WHERE usertable.userid = '"+this.userId+"') as sub " +
            "WHERE tu2.userid = sub.friendid";
            executeQuery(sqlstr, addGridRow2);
            //(tipGrid.ItemsSource as DataView).Sort = "Likes";
        }

        // Add Tip Button
        private void addTip_Click(object sender, RoutedEventArgs e)
        {
            if (userId != null)
            {
                TipWindow tipWindow = new TipWindow(businessId, userId);
                tipWindow.Show();
            }
        }

        // Refresh Button
        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            setTipGrid();
            setTipGrid2();
            LoadColumnChartData();
        }
        // Checkin Button
        private void checkinButton_Click(object sender, RoutedEventArgs e)
        {
            // Date-time of checkin
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string year = date.Substring(0, 4);
            string month = date.Substring(5, 2);
            string day = date.Substring(8, 2);
            string time = date.Substring(11, 8);
            // SQL INSERT statement for checkin
            string sqlstr = "INSERT INTO checkin(businessID, year, month, day, time) VALUES('" + this.businessId + "', '" + year + "', '" + month + "', '" + day + "', '" + time + "')";

            // Execute checkin query
            try
            {
                executeQuery(sqlstr, null);
                MessageBox.Show(date + ": " + "Successfully checked into " + bname.Text + "!" + "\nBusiness ID: " + this.businessId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        // Like Tip Button
        private void likeTip_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Tip data
                BusinessTip businessTip = (BusinessTip)((Button)e.Source).DataContext;
                string userid = businessTip.userid;
                string date = businessTip.date;
                // unused
                string name = businessTip.name;
                int likes = businessTip.likes;
                string tip = businessTip.tip;
                //MessageBox.Show(userid + name + likes + tip + date);
                // SQL UPDATE statement for tip like
                string sqlstr = "UPDATE tip SET likes = likes + 1 WHERE userID = '" + userid + "' AND businessID = '" + this.businessId + "' AND date = '" + date + "'";
                executeQuery(sqlstr, null);
                MessageBox.Show("You liked " + name + "'s tip!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        // Load the bar chart data
        private void LoadColumnChartData()
        {
            for (int i = 1; i < 13; i++)
            {
                string month = i.ToString();
                if (month.Length == 1)
                {
                    month = "0" + month;
                }
                string sqlstr = "SELECT COUNT FROM( SELECT month, COUNT(*) FROM( SELECT business.businessid, checkin.month FROM business INNER JOIN checkin ON business.businessid = checkin.businessid WHERE business.businessid ='" + this.businessId + "' ) AS sub GROUP BY month ORDER BY month ) AS sub1 WHERE month = '" + month + "'";
                executeQuery(sqlstr, i);
            }
            ((ColumnSeries)mcChart.Series[0]).ItemsSource =
                new KeyValuePair<string, int>[]{
                new KeyValuePair<string,int>("Jan.", this.totalCheckins[1]),
                new KeyValuePair<string,int>("Feb.", this.totalCheckins[2]),
                new KeyValuePair<string,int>("Mar.", this.totalCheckins[3]),
                new KeyValuePair<string,int>("Apr.", this.totalCheckins[4]),
                new KeyValuePair<string,int>("May", this.totalCheckins[5]),
                new KeyValuePair<string,int>("Jun.", this.totalCheckins[6]),
                new KeyValuePair<string,int>("Jul.", this.totalCheckins[7]),
                new KeyValuePair<string,int>("Aug.", this.totalCheckins[8]),
                new KeyValuePair<string,int>("Sep.", this.totalCheckins[9]),
                new KeyValuePair<string,int>("Oct.", this.totalCheckins[10]),
                new KeyValuePair<string,int>("Nov.", this.totalCheckins[11]),
                new KeyValuePair<string,int>("Dec.", this.totalCheckins[12]),
         };


        }
    }
   
}
