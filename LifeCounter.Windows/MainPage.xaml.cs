﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LifeCounter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Player1.Flip();
            Player2.Flip();

            Player1.SetBackGround(BackGroundColors.Yellow);
            Player2.SetBackGround(BackGroundColors.Green);
            Player3.SetBackGround(BackGroundColors.Purple);
            Player4.SetBackGround(BackGroundColors.Blue);

            Player1.viewModel.PlayerName = "Player 1";
            Player2.viewModel.PlayerName = "Player 2";
            Player3.viewModel.PlayerName = "Player 3";
            Player4.viewModel.PlayerName = "Player 4";

        }


        private void cmdOpen_Click(object sender, RoutedEventArgs e)
        {
            CmdBar.IsOpen = true;

        }
        

        private void BtnResetMP_Click(object sender, RoutedEventArgs e)
        {
            Player1.Reset();
            Player2.Reset();
            Player3.Reset();
            Player4.Reset();
        }

        private void BtnResetCommander_Click(object sender, RoutedEventArgs e)
        {
            Player1.Reset(Gametypes.Commander);
            Player2.Reset(Gametypes.Commander);
            Player3.Reset(Gametypes.Commander);
            Player4.Reset(Gametypes.Commander);
        }
    }
}