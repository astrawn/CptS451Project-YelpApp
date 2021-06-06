using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace milestone1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class Business
        {
            public string businessId { get; set; }
            public string name { get; set; }
            public string state { get; set; }
            public string city { get; set; }
            public string address { get; set; }
            public double distance { get; set; }
            public decimal stars { get; set; }
            public int tips { get; set; }
            public int checkins { get; set; }
        }

        public class Friend
        {
            public string name { get; set; }
            public int tiplikes { get; set; }
            public double averagestars { get; set; }
            public string datejoined { get; set; }
        }

        public class FriendTip
        {
            public string name { get; set; }
            public string businessname { get; set; }
            public string city { get; set; }
            public string text { get; set; }
            public string date { get; set; }
        }

            public class User
        {
            public string userid { get; set; }
            public string name { get; set; }
            public decimal stars { get; set; }
            public int fans { get; set; }
            public string yelpingSince { get; set; }
            public int cool { get; set; }
            public int funny { get; set; }
            public int useful { get; set; }
            public int tipCount { get; set; }
            public int tipLikes { get; set; }
            public decimal latitude { get; set; }
            public decimal longitude { get; set; }
        }

        string time;
        User currUser;

        // tuple list for identifying attributes in attribute list
        // format: (attributename, value, display_in_list_as)
        Tuple<string, string, string>[] tupleList =
        {
            Tuple.Create("AcceptsInsurance", "True", "Accepts Insurance"),
            Tuple.Create("africanamerican", "True", "Hairstylist Specializes In: African American"),
            Tuple.Create("AgesAllowed", "21plus", "21+"),
            Tuple.Create("Alcohol", "beer_and_wine", "Beer and Wine"),
            Tuple.Create("Alcohol", "full_bar", "Full Bar"),
            Tuple.Create("asian", "True", "Hairstylist Specializes In: Asian"),
            Tuple.Create("background_music", "True", "Music - Background"),
            Tuple.Create("BikeParking", "True", "Bike Parking"),
            Tuple.Create("BusinessAcceptsBitcoin", "True", "Accepts Bitcoin"),
            Tuple.Create("BusinessAcceptsCreditCards", "True", "Accepts Credit Cards"),
            Tuple.Create("BusinessParking", "None", "No Parking"),
            Tuple.Create("background_music", "True", "Music - Background"),
            Tuple.Create("ByAppointmentOnly", "True", "By Appointment Only"),
            Tuple.Create("BYOB", "True", "BYOB"),
            Tuple.Create("BYOBCorkage", "yes_corkage", "BYOB Corkage Fee"),
            Tuple.Create("BYOBCorkage", "yes_free", "BYOB No Corkage Fee"),
            Tuple.Create("casual", "True", "Casual"),
            Tuple.Create("Caters", "True", "Caters"),
            Tuple.Create("classy", "True", "Classy"),
            Tuple.Create("CoatCheck", "True", "Coat Check"),
            Tuple.Create("coloring", "True", "Hairstylist Specializes In: Coloring"),
            Tuple.Create("Corkage", "True", "Corkage Fee"),
            Tuple.Create("Corkage", "False", "No Corkage Fee"),
            Tuple.Create("curly", "True", "Hairstylist Specializes In: Curly Hair"),
            Tuple.Create("divey", "True", "Divey"),
            Tuple.Create("dj", "True", "Music - DJ"),
            Tuple.Create("DogsAllowed", "True", "Dogs Allowed"),
            Tuple.Create("DriveThru", "True", "Drive Thru"),
            Tuple.Create("extensions", "True", "Hairstylist Specializes In: Extensions"),
            Tuple.Create("friday", "True", "Best Nights: Friday"),
            Tuple.Create("garage", "True", "Parking Garage"),
            Tuple.Create("gluten-free", "True", "Gluten-free"),
            Tuple.Create("GoodForDancing", "True", "Good for Dancing"),
            Tuple.Create("GoodForKids", "True", "Good for Kids"),
            Tuple.Create("halal", "True", "Halal"),
            Tuple.Create("HappyHour", "True", "Happy Hour"),
            Tuple.Create("HasTV", "True", "Has TV"),
            Tuple.Create("hipster", "True", "Hipster"),
            Tuple.Create("intimate", "True", "Intimate"),
            Tuple.Create("jukebox", "True", "Music - Jukebox"),
            Tuple.Create("karaoke", "True", "Music - Karaoke"),
            Tuple.Create("kids", "True", "Hairstylist Specializes In: Kids"),
            Tuple.Create("kosher", "True", "Kosher"),
            Tuple.Create("live", "True", "Music - Live"),
            Tuple.Create("lot", "True", "Parking"),
            Tuple.Create("monday", "True", "Best Nights: Monday"),
            Tuple.Create("Music", "None", "No Music"),
            Tuple.Create("background_music", "True", "Music - Background"),
            Tuple.Create("NoiseLevel", "loud", "Loud"),
            Tuple.Create("NoiseLevel", "quiet", "Quiet"),
            Tuple.Create("NoiseLevel", "average", "Average Noise"),
            Tuple.Create("NoiseLevel", "very_loud", "Very Loud"),
            Tuple.Create("Open24Hours", "True", "Open 24 Hours"),
            Tuple.Create("OutdoorSeating", "True", "Outdoor Seating"),
            Tuple.Create("perms", "True", "Hairstylist Specializes In: Perms"),
            Tuple.Create("RestaurantsAttire", "dressy", "Dressy Attire"),
            Tuple.Create("RestaurantsAttire", "casual", "Casual Attire"),
            Tuple.Create("RestaurantsAttire", "formal", "Formal Attire"),
            Tuple.Create("RestaurantsCounterService", "True", "Restaurant Counter Service"),
            Tuple.Create("RestaurantsDelivery", "True", "Restaurant Delivery"),
            Tuple.Create("RestaurantsGoodForGroups", "True", "Restaurants Good For Groups"),
            Tuple.Create("RestaurantsReservations", "True", "Restaurant Reservations"),
            Tuple.Create("RestaurantsTableService", "True", "Restaurant Table Service"),
            Tuple.Create("RestaurantsTakeOut", "True", "Restaurant Takeout"),
            Tuple.Create("romantic", "True", "Romantic"),
            Tuple.Create("saturday", "True", "Best Nights: Saturday"),
            Tuple.Create("Smoking", "no", "No Smoking"),
            Tuple.Create("Smoking", "outdoor", "Outdoor Smoking Allowed"),
            Tuple.Create("Smoking", "yes", "Smoking Allowed"),
            Tuple.Create("soy-free", "True", "Soy-free"),
            Tuple.Create("straightperms", "True", "Hairstylist Specializes In: Straight Perms"),
            Tuple.Create("street", "True", "Street Parking"),
            Tuple.Create("sunday", "True", "Best Nights: Sunday"),
            Tuple.Create("thursday", "True", "Best Nights: Thursday"),
            Tuple.Create("touristy", "True", "Touristy"),
            Tuple.Create("trendy", "True", "Trendy"),
            Tuple.Create("tuesday", "True", "Best Nights: Tuesday"),
            Tuple.Create("upscale", "True", "Upscale"),
            Tuple.Create("valet", "True", "Valet Parking"),
            Tuple.Create("validated", "True", "Validated Parking"),
            Tuple.Create("vegan", "True", "Vegan"),
            Tuple.Create("vegetarian", "True", "Vegetarian"),
            Tuple.Create("video", "True", "Music - Video"),
            Tuple.Create("wednesday", "True", "Best Nights: Wednesday"),
            Tuple.Create("WheelchairAccessible", "True", "Wheelchair Accessible"),
            Tuple.Create("WiFi", "free", "Free WiFi"),
            Tuple.Create("WiFi", "paid", "Paid WiFi")
        };


        public MainWindow()
        {
            InitializeComponent();
            addState();
            time = DateTime.Now.ToString();
            addColumns2Grid();
            addColsTofriends();
            currUser = new User();
            initMealList();
            addColsTofriendTips();
        }

        // main window init and base functions
        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = yelp; password = psqladmin";
        }

        private void addState()
        {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {

                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT distinct state FROM business ORDER BY state";

                    try
                    {

                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            stateList.Items.Add(reader.GetString(0));
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

        private void addColumns2Grid()
        {

            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("name");
            col1.Header = "BusinessName";
            col1.Width = 125;
            businessGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("address");
            col2.Header = "Address";
            col2.Width = 200;
            businessGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("city");
            col3.Header = "City";
            col3.Width = 80;
            businessGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("state");
            col4.Header = "State";
            col4.Width = 40;
            businessGrid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Binding = new Binding("distance");
            col5.Header = "Distance (mi)";
            col5.Width = 70;
            businessGrid.Columns.Add(col5);

            DataGridTextColumn col6 = new DataGridTextColumn();
            col6.Binding = new Binding("stars");
            col6.Header = "Stars";
            col6.Width = 50;
            businessGrid.Columns.Add(col6);

            DataGridTextColumn col7 = new DataGridTextColumn();
            col7.Binding = new Binding("tips");
            col7.Header = "# Tips";
            col7.Width = 60;
            businessGrid.Columns.Add(col7);

            DataGridTextColumn col8 = new DataGridTextColumn();
            col8.Binding = new Binding("checkins");
            col8.Header = "Total Check-ins";
            col8.Width = 80;
            businessGrid.Columns.Add(col8);

            DataGridTextColumn col9 = new DataGridTextColumn();
            col9.Binding = new Binding("businessid");
            col9.Header = "";
            col9.Width = 0;
            businessGrid.Columns.Add(col9);

        }

        private void addColsTofriends()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("name");
            col1.Header = "Name";
            col1.Width = 200;
            friendsDataGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("tiplikes");
            col2.Header = "tiplikes";
            col2.Width = 100;
            friendsDataGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("averagestars");
            col3.Header = "Avg Stars";
            col3.Width = 100;
            friendsDataGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("datejoined");
            col4.Header = "Date Joined";
            col4.Width = 201;
            friendsDataGrid.Columns.Add(col4);
        }

        private void addColsTofriendTips()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("name");
            col1.Header = "Name";
            col1.Width = 100;
            friendsTipsDataGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("businessname");
            col2.Header = "Business Name";
            col2.Width = 100;
            friendsTipsDataGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("city");
            col3.Header = "City";
            col3.Width = 100;
            friendsTipsDataGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("text");
            col4.Header = "Text";
            col4.Width = 255;
            friendsTipsDataGrid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Binding = new Binding("date");
            col5.Header = "Date";
            col5.Width = 100;
            friendsTipsDataGrid.Columns.Add(col5);
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

        // business search functions
        private void addCity(NpgsqlDataReader reader)
        {
            cityList.Items.Add(reader.GetString(0));
        }

        private void stateList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cityList.Items.Clear();
            if (stateList.SelectedIndex > -1)
            {
                string sqlstr = "SELECT distinct city FROM business WHERE state = '" + stateList.SelectedItem.ToString() + "' ORDER BY city";
                executeQuery(sqlstr, addCity);

            }
        }

        private void addGridRow(NpgsqlDataReader reader)
        {
            businessGrid.Items.Add(new Business() { businessId = reader.GetString(0), name = reader.GetString(1), state = reader.GetString(4), city = reader.GetString(3), address = reader.GetString(2), distance = calculateDistance(reader.GetDecimal(6), reader.GetDecimal(7)), stars = reader.GetDecimal(10), tips = reader.GetInt32(8), checkins = reader.GetInt32(9) });
        }

        private void addZip(NpgsqlDataReader reader)
        {
            zipList.Items.Add(reader.GetString(0));
        }

        private void cityList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            zipList.Items.Clear();
            if (cityList.SelectedIndex > -1)
            {
                string sqlstr = "SELECT DISTINCT zipcode FROM business WHERE state = '" + stateList.SelectedItem.ToString() + "' AND city = '" + cityList.SelectedItem.ToString() + "' ORDER BY zipcode";
                executeQuery(sqlstr, addZip);
               
            }
        }

        private void addCategoryListItem(NpgsqlDataReader reader)
        {
            categoryList.Items.Add(reader.GetString(0));
        }

        private void addAttributeListItem(NpgsqlDataReader reader)
        {

            foreach (Tuple<string, string, string> t in tupleList)
            {
                if (t.Item1 == reader.GetString(0) && t.Item2 == reader.GetString(1))
                {
                    attributeList.Items.Add(t.Item3);
                    break;
                }
            }
        }

        private void showTipsButton_Click(object sender, RoutedEventArgs e)
        {
            if (businessGrid.SelectedIndex > -1)
            {
                Business B = businessGrid.Items[businessGrid.SelectedIndex] as Business;
                if ((B.businessId != null) && (B.businessId.ToString().CompareTo("") != 0))
                {
                    BusinessDetails businessWindow = new BusinessDetails(B.businessId.ToString(), currUser.userid);
                    businessWindow.Show();
                }
            }
        }

        private double calculateDistance(decimal bLat, decimal bLong)
        {
            // uses Haversine Formula
            double R = 3958.8; // radius of Earth in miles
            double convert = Math.PI / 180; // convert from degrees to radians
            double diffLat = (double)(bLat - currUser.latitude) * convert;
            double diffLong = (double)(bLong - currUser.longitude) * convert;
            double a = Math.Pow(Math.Sin(diffLat / 2), 2) + Math.Cos((double)currUser.latitude * convert) * Math.Cos((double)bLat * convert) * Math.Pow(Math.Sin(diffLong / 2), 2);
            double c = (2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a)));
            double d = R * c;

            return Math.Round(d, 1);
        }

        private void sortAttributes()
        {

            List<string> q = new List<string>();
            foreach (string o in attributeList.Items)
            {
                q.Add(o);
            }
            q.Sort();
            attributeList.Items.Clear();
            foreach (string o in q)
            {
                attributeList.Items.Add(o);
            }
        }

        private void zipList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categoryList.Items.Clear();
            businessGrid.Items.Clear();
            attributeList.Items.Clear();
            if (zipList.SelectedIndex > -1)
            {
                string sqlstr = "SELECT DISTINCT categoryname FROM (SELECT business.businessid, zipcode, state, city, categoryname FROM (business INNER JOIN category ON business.businessid = category.businessid) WHERE city = '" + cityList.SelectedItem.ToString() + "' AND state = '" + stateList.SelectedItem.ToString() + "' AND zipcode = '" + zipList.SelectedItem.ToString() + "') AS bc ORDER BY categoryname";
                executeQuery(sqlstr, addCategoryListItem);
                string sqlstr2 = "SELECT DISTINCT attributename, value FROM (SELECT business.businessid, zipcode, state, city, attributename, value FROM (business INNER JOIN attribute ON business.businessid = attribute.businessid) WHERE city = '" + cityList.SelectedItem.ToString() + "' AND state = '" + stateList.SelectedItem.ToString() + "' AND zipcode = '" + zipList.SelectedItem.ToString() + "' AND attributename NOT IN ('dessert', 'latenight', 'lunch', 'dinner', 'brunch', 'breakfast')) AS ba ORDER BY attributename";
                executeQuery(sqlstr2, addAttributeListItem);
                sortAttributes();
                string sqlstr4 = "SELECT * FROM business WHERE state = '" + stateList.SelectedItem.ToString() + "' AND city = '" + cityList.SelectedItem.ToString() + "' AND zipcode = '" + zipList.SelectedItem.ToString() + "' ORDER BY name";
                executeQuery(sqlstr4, addGridRow);
            }
        }

        private void categoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateBusinessGrid();
        }

        private void refreshBusinessButton_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGrid();
        }


        // Login functions
        private void nameSearchButton_Click(object sender, RoutedEventArgs e)
        {
            string sqlstr = "SELECT name, userid FROM usertable WHERE name = '" + nameSearchTextBox.Text.ToString() + "';";
            useridListBox.Items.Clear();
            executeQuery(sqlstr, addUserID);
        }

        private void addUserID(NpgsqlDataReader reader)
        {
            useridListBox.Items.Add(reader.GetString(1));
        }

        private void useridListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (useridListBox.SelectedItem != null)
            {
                string sqlstr = "SELECT * FROM usertable WHERE userid = '" + useridListBox.SelectedItem.ToString() + "';";
                clearUserInfo();
                executeQuery(sqlstr, updateUserInfo);

                sqlstr = "SELECT name, tiplikes, averagestars, datejoined FROM usertable, friends WHERE friends.userid = '" + useridListBox.SelectedItem.ToString() + "' AND friends.friendid = usertable.userid;";
                clearFriendsInfo();
                executeQuery(sqlstr, updateFriendsInfo);

                // display recent friend tips
                sqlstr = "SELECT usertable.name, business.name, city, text, tip.date FROM usertable, business, tip, (SELECT tip.userid, MAX(date) as date FROM tip, friends WHERE friends.userid = '" + useridListBox.SelectedItem.ToString() + "' AND friendid = tip.userid GROUP BY tip.userid) friendTips WHERE business.businessid = tip.businessid AND friendTips.userid = usertable.userid AND tip.date = friendTips.date AND tip.userid = friendTips.userid;";
                clearFriendTips();
                executeQuery(sqlstr, updateFriendTips);
            }
            else
            {
                clearUserInfo();
                clearFriendsInfo();
                clearFriendTips();
            }

            if(stateList.SelectedItem != null && cityList.SelectedItem != null && zipList.SelectedItem != null)
            {
                updateBusinessGrid();
            }
        }


        // userinfo update functions
        private void clearUserInfo()
        {
            nameTextBox.Clear();
            starsTextBox.Clear();
            fansTextBox.Clear();
            yelpingTextBox.Clear();
            coolTextBox.Clear();
            funnyTextBox.Clear();
            usefulTextBox.Clear();
            tipCountTextBox.Clear();
            tipLikesTextBox.Clear();
            curLatTextBox.Clear();
            curLongTextBox.Clear();
        }

        private void updateUserInfo(NpgsqlDataReader reader)
        {
            currUser.userid = reader.GetString(0);
            currUser.name = reader.GetString(1);
            currUser.stars = reader.GetDecimal(2);
            currUser.fans = reader.GetInt16(7);
            currUser.yelpingSince = reader.GetDate(6).ToString();
            currUser.cool = reader.GetInt16(3);
            currUser.funny = reader.GetInt16(4);
            currUser.useful = reader.GetInt16(5);
            currUser.tipCount = reader.GetInt16(9);
            currUser.tipLikes = reader.GetInt16(10);
            currUser.latitude = reader.GetDecimal(11);
            currUser.longitude = reader.GetDecimal(12);

            nameTextBox.Text = reader.GetString(1);
            starsTextBox.Text = reader.GetDecimal(2).ToString();
            fansTextBox.Text = reader.GetInt16(7).ToString();
            yelpingTextBox.Text = reader.GetDate(6).ToString();
            coolTextBox.Text = reader.GetInt16(3).ToString();
            funnyTextBox.Text = reader.GetInt16(4).ToString();
            usefulTextBox.Text = reader.GetInt16(5).ToString();
            tipCountTextBox.Text = reader.GetInt16(9).ToString();
            tipLikesTextBox.Text = reader.GetInt16(10).ToString();
            curLatTextBox.Text = reader.GetDecimal(11).ToString();
            curLongTextBox.Text = reader.GetDecimal(12).ToString();
        }

        // friendslist update functions
        private void clearFriendsInfo()
        {
            friendsDataGrid.Items.Clear();
        }

        private void updateFriendsInfo(NpgsqlDataReader reader)
        {
            friendsDataGrid.Items.Add(new Friend() { name = reader.GetString(0), tiplikes = reader.GetInt32(1), averagestars = reader.GetDouble(2), datejoined = reader.GetDate(3).ToString() });
        }

        // display friend tips functions
        private void clearFriendTips()
        {
            friendsTipsDataGrid.Items.Clear();
        }

        private void updateFriendTips(NpgsqlDataReader reader)
        {
            friendsTipsDataGrid.Items.Add(new FriendTip() { name = reader.GetString(0), businessname = reader.GetString(1), city = reader.GetString(2), text = reader.GetString(3), date = reader.GetString(4) });
        }

        // update location functions
        private void updateLocation(NpgsqlDataReader reader)
        {
            curLatTextBox.Text = reader.GetDecimal(0).ToString();
            curLongTextBox.Text = reader.GetDecimal(1).ToString();
            currUser.latitude = reader.GetDecimal(0);
            currUser.longitude = reader.GetDecimal(1);
        }

        private void updateLocationButton_Click(object sender, RoutedEventArgs e)
        {
            if (updateLatTextBox.Text != "" && updateLongTextBox.Text != "" && useridListBox.SelectedItem != null)
            {
                string sqlstring = "UPDATE usertable SET latitude = " + updateLatTextBox.Text + ", longitude = " + updateLongTextBox.Text + " WHERE userid = '" + useridListBox.SelectedItem.ToString() + "';";
                executeQuery(sqlstring, null);
                sqlstring = "SELECT latitude, longitude FROM usertable WHERE userid = '" + useridListBox.SelectedItem.ToString() + "';";
                executeQuery(sqlstring, updateLocation);
            }

            updateLatTextBox.Clear();
            updateLongTextBox.Clear();

            if(stateList.SelectedItem != null && cityList.SelectedItem != null && zipList.SelectedItem != null)
            {
                updateBusinessGrid();
            }
        }


        // search by attributes functions

        private void initMealList()
        {
            mealListBox.Items.Add("breakfast");
            mealListBox.Items.Add("brunch");
            mealListBox.Items.Add("lunch");
            mealListBox.Items.Add("dinner");
            mealListBox.Items.Add("dessert");
            mealListBox.Items.Add("latenight");
        }
    
        private void mealListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateBusinessGrid();
        }

        private void attributeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateBusinessGrid();
        }

        private void price1CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            updateBusinessGrid();
        }

        private void price1CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            updateBusinessGrid();
        }

        private void price2CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            updateBusinessGrid();
        }

        private void price2CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            updateBusinessGrid();
        }

        private void price3CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            updateBusinessGrid();
        }

        private void price3CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            updateBusinessGrid();
        }

        private void price4CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            updateBusinessGrid();
        }

        private void price4CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            updateBusinessGrid();
        }

        private void updateBusinessGrid()
        {
            businessGrid.Items.Clear();
            if ((zipList.SelectedIndex > -1))
            {
                string sqlstr = "SELECT DISTINCT bc.businessid, name, streetaddress, city, state, zipcode, latitude, longitude, numtips, numcheckins, stars FROM attribute INNER JOIN (SELECT DISTINCT business.businessid, name, streetaddress, city, state, zipcode, latitude, longitude, numtips, numcheckins, stars, categoryname FROM business INNER JOIN category ON business.businessid = category.businessid) AS bc ON attribute.businessid = bc.businessid WHERE state = '" + stateList.SelectedItem.ToString() + "' AND city = '" + cityList.SelectedItem.ToString() + "' AND zipcode = '" + zipList.SelectedItem.ToString() + "' ";

                List<string> cStringList = new List<string>();
                List<string> aStringList = new List<string>();


                if (categoryList.SelectedIndex > -1)
                {

                    sqlstr += "AND categoryname IN ('" + categoryList.SelectedItem.ToString().Replace("'", "''");


                    foreach (string str in categoryList.SelectedItems)
                    {
                        cStringList.Add(str);
                    }

                    for (int i = 1; i < cStringList.Count; i++)
                    {
                        sqlstr += "', '" + cStringList[i].Replace("'", "''");
                    }

                    sqlstr += "')";
                }

                if (attributeList.SelectedItems.Count > 0 || mealListBox.SelectedItems.Count > 0 || price1CheckBox.IsChecked == true || price2CheckBox.IsChecked == true || price3CheckBox.IsChecked == true || price4CheckBox.IsChecked == true)
                {
                    sqlstr += "AND (";
                }

                int count = 0;

                if (attributeList.SelectedItems.Count > 0)
                {

                    sqlstr += "(attributename = '";
                    foreach (Tuple<string, string, string> t in tupleList)
                    {
                        if (t.Item3 == attributeList.SelectedItems[0].ToString())
                        {
                            sqlstr += t.Item1 + "' AND value = '" + t.Item2 + "')";
                            break;
                        }
                    }

                    count++;

                }

                if (attributeList.SelectedItems.Count > 1)
                {

                    for (int i = 1; i < attributeList.SelectedItems.Count; i++)
                    {
                        sqlstr += " OR (attributename = '";
                        foreach (Tuple<string, string, string> t in tupleList)
                        {
                            if (t.Item3 == attributeList.SelectedItems[i].ToString())
                            {
                                sqlstr += t.Item1 + "' AND value = '" + t.Item2 + "')";
                                break;
                            }
                        }

                        count++;
                    }

                }

                foreach (string str in mealListBox.SelectedItems)
                {
                    if (attributeList.SelectedItems.Count > 0 || count != 0)
                    {
                        sqlstr += " OR ";
                    }

                    sqlstr += "(attributename = '" + str + "' AND value = 'True')";
                    count++;
                }

                int priceChecked = 0;


                if (price1CheckBox.IsChecked == true)
                {

                    if (count > 0)
                    {
                        sqlstr += " OR ";
                    }

                    sqlstr += "(attributename = 'RestaurantsPriceRange2' AND value = '1')";
                    priceChecked++;
                }

                if (price2CheckBox.IsChecked == true)
                {
                    if (priceChecked > 0 || count > 0)
                    {
                        sqlstr += " OR ";
                    }

                    sqlstr += "(attributename = 'RestaurantsPriceRange2' AND value = '2')";
                    priceChecked++;
                }

                if (price3CheckBox.IsChecked == true)
                {
                    if (priceChecked > 0 || count > 0)
                    {
                        sqlstr += " OR ";
                    }

                    sqlstr += "(attributename = 'RestaurantsPriceRange2' AND value = '3')";
                    
                }

                if (price4CheckBox.IsChecked == true)
                {
                    if (priceChecked > 0 || count > 0)
                    {
                        sqlstr += " OR ";
                    }

                    sqlstr += "(attributename = 'RestaurantsPriceRange2' AND value = '4')";
                    
                }

                if (price1CheckBox.IsChecked == true || price2CheckBox.IsChecked == true || price3CheckBox.IsChecked == true || price4CheckBox.IsChecked == true)
                {
                    count++;
                }

                if (count > 0)
                {
                    sqlstr += ")";
                }

                sqlstr += " GROUP BY bc.businessid, name, streetaddress, city, state, zipcode, latitude, longitude, numtips, numcheckins, stars";

                if (categoryList.SelectedItems.Count > 0 || count > 0)
                {
                    sqlstr += " HAVING ";
                }

                if (categoryList.SelectedItems.Count > 0) {
                    sqlstr += " Count(DISTINCT categoryname) = " + cStringList.Count;
                }

                if (count > 0) {
                    if (categoryList.SelectedItems.Count > 0)
                    {
                        sqlstr += " AND";
                    }

                    sqlstr += " Count(DISTINCT attributename) = " + count;
                } 
                
                sqlstr += " ORDER BY name";


                executeQuery(sqlstr, addGridRow);
            }
        }


    }
}
