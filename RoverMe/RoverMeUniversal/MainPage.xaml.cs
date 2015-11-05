using RoverLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;



namespace RoverMe
{

    public sealed partial class MainPage : Page
    {
        private RoverController controller;

        public MainPage()
        {
            this.InitializeComponent();

            this.controller = new RoverController();
            this.cbDevices.ItemsSource = controller.GetDevicesList();
        }

        #region Command Buttons

        private async void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            await this.controller.Connect((this.cbDevices.SelectedValue as string));
            if (this.controller.IsConnected)
                btnConnect.Foreground = new SolidColorBrush(Colors.DarkGreen);
            else
                btnConnect.Foreground = new SolidColorBrush(Colors.DarkRed);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.controller.RefreshDevicesList();
            this.cbDevices.ItemsSource = controller.GetDevicesList();
        }

        #endregion

        #region Controls Buttons

        private async void UP_Click(object sender, RoutedEventArgs e)
        {
            if (controller.IsConnected)
                await this.controller.GoFoward();
        }

        private async void STOP_Click(object sender, RoutedEventArgs e)
        {
            if (controller.IsConnected)
                await this.controller.GoStop();
        }

        private async void LEFT_Click(object sender, RoutedEventArgs e)
        {
            if (controller.IsConnected)
                await this.controller.GoLeft();
        }

        private async void RIGHT_Click(object sender, RoutedEventArgs e)
        {
            if (controller.IsConnected)
                await this.controller.GoRight();
        }

        private async void DOWN_Click(object sender, RoutedEventArgs e)
        {
            if (controller.IsConnected)
                await this.controller.GoBackward();
        }

        #endregion
    }
}
