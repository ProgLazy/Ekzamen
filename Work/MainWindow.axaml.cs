using System;
using System.Runtime.InteropServices.JavaScript;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;

namespace Work;

public partial class MainWindow : Window
{
    private MySqlConnection _connection;
    private string connectionString = "server=10.10.1.24;Port=3306;database=abd6;User id=user_01;password=user01pro";
    public MainWindow()
    {
        InitializeComponent();
    }


    private void Enter_OnClick(object? sender, RoutedEventArgs e)
    {
            try
            {
                string sql1 = "SELECT name, password, role_id FROM users WHERE login = '" + login.Text + "' AND password = '" +
                              password.Text + "' AND role_id = '1'";
                _connection = new MySqlConnection(connectionString);
                _connection.Open();
                string sql2 = "SELECT name, password, role_id FROM users WHERE login = '" + login.Text + "' AND password = '" +
                              password.Text + "' AND role_id = '2'";
                MySqlCommand sqlCommand = new MySqlCommand(sql2, _connection);
                MySqlCommand mySqlCommand = new MySqlCommand(sql1, _connection);
                if (mySqlCommand.ExecuteScalar() != null)
                {
            
                    bottom _bottom = new bottom();
                    this.Hide();
                    _bottom.Show();
                }
                if (sqlCommand.ExecuteScalar() != null)
                {
                    bottom _bottom = new bottom();
                    this.Hide();
                    
                    _bottom.Show();
                    login.Text = "Sucessfully";
                }
            }
            catch (Exception exception)
            {
                login.Text = "Incorrect login or password)";
            }
        
    }

    private void Registration_OnClick(object? sender, RoutedEventArgs e)
    {
        regis _regis = new regis();
        this.Hide();
        _regis.Show(); 
    }
}