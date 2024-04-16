using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Work;

public partial class bottom : Window
{
    private MySqlConnection _connection;
    private string connectionString = "server=10.10.1.24;Port=3306;database=abd6;User id=user_01;password=user01pro";
    private List<userslist> _list;
    private List<filter> _filters;
    public bottom()
    {
        InitializeComponent();
        string sql = "Select * FROM users";
        ShowTable(sql);
        Filter();
    }

    public class userslist
    {
        public int id { get; set; }
        public string name { get; set; }
        public string last_name { get; set; }
        public int phone { get; set; }
        public string password { get; set; }
        public DateTime year { get; set; }
        public string level { get; set; }
        public int discount { get; set; }
    }

    public class filter
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    private void ShowTable(string sql)
    {
        _connection = new MySqlConnection(connectionString);
        _list = new List<userslist>();
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read() && reader.HasRows)
        {
            var current = new userslist()
            {
                id = reader.GetInt32("id"),
                name = reader.GetString("name"),
                last_name = reader.GetString("last_name"),
                phone = reader.GetInt32("phone"),
                password = reader.GetString("password"),
                year = reader.GetDateTime("year"),
                level = reader.GetString("level"),
                discount = reader.GetInt32("discount")
            };
            _list.Add(current);
        }

        Grid.ItemsSource = _list;
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        MainWindow _mainWindow = new MainWindow();
        this.Hide();
        _mainWindow.Show();
    }

    private void Search_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        string sql = "SELECT * FROM users WHERE name LIKE '"+search.Text+"' OR last_name LIKE '"+search.Text+"'";
        ShowTable(sql);
    }

    private void Filter()
    {
        _connection = new MySqlConnection(connectionString);
        _connection.Open();
        string sql = "SELECT id, name FROM users";
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new filter()
            {
                id = reader.GetInt32("id"),
                name = reader.GetString("name")
            };
            _filters.Add(current);
        }
        var combobox = this.Find<ComboBox>("Box");
        combobox.ItemsSource = _filters;
        _connection.Close();
    }

    private void Box_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var combobox = (ComboBox)sender;
        var current = combobox.SelectedItem as filter;
        var filter = _list.Where(x => x.name == current.name).ToList();
        Grid.ItemsSource = filter; 
    }

   
    private void Delete_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
            string update = "DELETE FROM users WHERE id = '"+text1.Text+"'";
            MySqlCommand command = new MySqlCommand(update, _connection);
            command.ExecuteNonQuery();
            _connection.Close();
            text1.Text = "Succesfully data";
        }
        catch (Exception exception)
        {
            text1.Text = "Incorrect data";
        }
    }

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        _connection = new MySqlConnection(connectionString);
        _list = new List<userslist>();
        _connection.Open();
        string sql = "SELECT * FROM users";
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read() && reader.HasRows)
        {
            var current = new userslist()
            {
                id = reader.GetInt32("id"),
                name = reader.GetString("name"),
                last_name = reader.GetString("last_name"),
                phone = reader.GetInt32("phone"),
                password = reader.GetString("password"),
                year = reader.GetDateTime("year"),
                level = reader.GetString("level"),
                discount = reader.GetInt32("discount")
            };
            _list.Add(current);
        }

        Grid.ItemsSource = _list;
    }

    private void A_Z_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = "SELECT * FROM users ORDER BY last_name asc";
        ShowTable(sql); 
    }
}