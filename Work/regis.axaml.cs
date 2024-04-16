using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Work;

public partial class regis : Window
{
    private MySqlConnection _connection;
    private string connectionString = "server=10.10.1.24;Port=3306;database=abd6;User id=user_01;password=user01pro";
    public regis()
    {
        InitializeComponent();
    }

    private void Add_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
            string insert = "INSERT INTO stuff (name, last_name, phone,password, year, level) VALUES ('"+text2.Text+"', '"+text3.Text+"', '"+text4.Text+"', '"+text5.Text+"', '"+text6.Text+"', '"+text7.Text+"')";
            MySqlCommand command = new MySqlCommand(insert, _connection);
            command.ExecuteNonQuery();
            _connection.Close();
            text1.Text = "Succesfully data";
        }
        catch (Exception exception)
        {
            text1.Text = "Incorrect data";
        }
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        MainWindow _mainWindow = new MainWindow();
        this.Hide();
        _mainWindow.Show();
    }

    
}