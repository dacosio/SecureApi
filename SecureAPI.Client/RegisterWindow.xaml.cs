using MahApps.Metro.Controls.Dialogs;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using SecureAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SecureAPI.Client
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();

            var model = new RegisterViewModelDto
            {
                Email = txtEmail.Text,
                Password = txtPassword.Password,
                ConfirmPassword = txtConfirmPassword.Password
            };

            var jsonData = JsonConvert.SerializeObject(model);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:44319/api/auth/register", content);

            var responseBody = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<UserManagerResponseDto>(responseBody);

            if (responseObject.IsSuccess)
            {
                var dialog = new MessageDialog("Your account has been created successfully");
                await dialog.ShowAsync();
            }
            else
            {
                var dialog = new MessageDialog(responseObject.Errors.FirstOrDefault());
                await dialog.ShowAsync();
            }
           
        }
    }
}
